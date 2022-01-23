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

namespace GoDam.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            return View(await _context.Purchase.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .FirstOrDefaultAsync(m => m.PurchaseID == id);

            List<PurchaseDetails> purchaseDetails = await _context.PurchaseDetails.Where(x => x.PurchaseID == id).ToListAsync();
            if (purchase == null)
            {
                return NotFound();
            }
            PurchaseViewModel pvm = new PurchaseViewModel();
            pvm.Purchase = purchase;
            pvm.ListPurchaseDetails = purchaseDetails;
            //ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductName");
            return View(pvm);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductName");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PurchaseID,BillNumber,VendorName,VendorAddress,PurchaseDate")] Purchase purchase,
            List<PurchaseDetails> ListPurchaseDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();

                foreach (PurchaseDetails element in ListPurchaseDetails)
                {
                    element.PurchaseID = purchase.PurchaseID;
                    //element.LineTotal = element.Amount * element.Quantity;
                    //element.Price = element.Quantity * element.LineTotal;

                    //add list of purchase details
                    _context.Add(element);
                    await _context.SaveChangesAsync();

                    int itemCount = _context.ProductStock.Where(x => x.ProductID == element.ProductID).Select(u => u.Quantity).First();
                    int qty = itemCount + element.Quantity;
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "UPDATE ProductStock SET Quantity=" + qty + " WHERE ProductID = " + element.ProductID;
                        _context.Database.OpenConnection();
                        using (var result = command.ExecuteReader()) { }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Product, "ProductID", "ProductName", ListPurchaseDetails[0].ProductID);
            PurchaseViewModel pvm = new PurchaseViewModel();
            pvm.Purchase = purchase;
            pvm.ListPurchaseDetails = ListPurchaseDetails;
            return View(pvm);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseID,BillNumber,VendorName,VendorAddress,PurchaseDate")] Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseID))
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
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.FindAsync(id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.PurchaseID == id);
        }


    }
}
