using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoDam.Data;
using GoDam.Models;
using GoDam.ViewModel;
using GoDam.ViewModels;

namespace GoDam.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sales.Include(s => s.Cust);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.Cust)
                .FirstOrDefaultAsync(m => m.SalesID == id);

            List<SalesDetails> salesDetails = await _context.SalesDetails.Where(x => x.SalesID == id).ToListAsync();
            if (sales == null)
            {
                return NotFound();
            }
            SalesViewModel svm = new SalesViewModel();
            svm.Sales = sales;
            svm.ListSalesDetails = salesDetails;
            return View(svm);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("SalesID,BillNumber,SalesDate,CustomerID")] Sales sales,
            List<SalesDetails> ListSalesDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();

                foreach (SalesDetails element in ListSalesDetails)
                {
                    element.SalesID = sales.SalesID;
                    //element.Price = element.Quantity * element.LineTotal;

                    //add list of purchase details
                    _context.Add(element);
                    await _context.SaveChangesAsync();

                    int itemCount = _context.ProductStock.Where(x => x.ProductID == element.ProductID).Select(u => u.Quantity).First();
                    int qty = itemCount - element.Quantity;
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "UPDATE ProductStock SET Quantity=" + qty + " WHERE ProductID = " + element.ProductID;
                        _context.Database.OpenConnection();
                        using (var result = command.ExecuteReader())
                        {
                            //
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name", sales.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductName", ListSalesDetails[0].ProductID);
            SalesViewModel svm = new SalesViewModel();
            svm.Sales = sales;
            svm.ListSalesDetails = ListSalesDetails;
            return View(svm);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Address", sales.CustomerID);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesID,BillNumber,SalesDate,CustomerID")] Sales sales)
        {
            if (id != sales.SalesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.SalesID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Address", sales.CustomerID);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.Cust)
                .FirstOrDefaultAsync(m => m.SalesID == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.SalesID == id);
        }

        // GET: Items/Details/5
        public JsonResult ItemDetails(int id)
        {
            /* if (id == null)
             {
                 return Json(new { item = "" });
             }*/
            List<ItemDetailsViewModel> lstData = new List<ItemDetailsViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {

                command.CommandText = @"select i.ProductID,i.ProductName,c.CategoryName,its.Quantity from Product i
                                        join Category c
                                        on i.CategoryID = c.CategoryID
                                        join ItemStock its
                                        on its.ProductID = i.ProductID
                                        where i.ProductID = " + id;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    ItemDetailsViewModel data;
                    while (result.Read())
                    {
                        data = new ItemDetailsViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.ProductCategory = result.GetString(2);
                        data.Quantity = result.GetInt32(3);
                        lstData.Add(data);
                    }
                }
            }

            if (lstData == null)
            {
                return Json(new { });
            }

            return Json(new { item = lstData[0] });
            //Json(students, JsonRequestBehavior.AllowGet)
        }
    }
}
