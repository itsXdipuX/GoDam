using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoDam.Data;
using GoDam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoDam.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using GoDam.Models;

namespace GoDam.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ItemDetailsViewModel> lstData = new List<ItemDetailsViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {

                command.CommandText = @"select i.ProductID,i.ProductName,c.CategoryName Category, its.Quantity,pd.Amount PricePerPiece,p.VendorName
                                            from Product i
                                            join Category c on c.CategoryID = i.CategoryID
                                            join ProductStock its
                                            on its.ProductID = i.ProductID
                                            join PurchaseDetails pd on pd.ProductID = i.ProductID
                                            join Purchase p on p.PurchaseID = pd.PurchaseID
                                            WHERE i.ProductID =" + id;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    ItemDetailsViewModel data;
                    while (result.Read())
                    {
                        data = new ItemDetailsViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.ProductCategory = result.GetString(3);
                        data.Quantity = result.GetInt32(4);
                        data.PricePerPiece = result.GetInt32(5);
                        data.VendorName = result.GetString(6);
                        lstData.Add(data);
                    }
                }
            }
            if (lstData == null)
            {
                return NotFound();
            }

            return View(lstData);
        }

        public IActionResult StockListReport(string searchKey)
        {
            List<ProductStockViewModel> lstData = new List<ProductStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand()) { 
                if(searchKey == null)
                {
                    command.CommandText = "SELECT p.ProductID as ProductId,p.ProductName as ProductName,ps.Quantity from Product p inner join ProductStock ps on p.ProductID=ps.ProductID";
                }
                else
                {
                    command.CommandText = "SELECT p.ProductID as ProductId,p.ProductName as ProductName,ps.Quantity from Product p inner join ProductStock ps on p.ProductID=ps.ProductID WHERE p.ProductName like '%" + searchKey + "%'";
                }

                
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    
                    ProductStockViewModel data;
                    while (result.Read())
                    {
                        data = new ProductStockViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        public IActionResult CustomerPurchaseReport(int? id)
        {
            List<CustomerPurchaseViewModel> lstData = new List<CustomerPurchaseViewModel>();
            if (id != null)
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"select s.SalesID,s.CustomerID,s.BillNumber,p.ProductID,p.ProductName,sd.Quantity,sd.Price,DATEDIFF(DAY,  s.SalesDate,GETDATE()) purchased_days_before 
                                            from Sales s
                                            join SalesDetails sd
                                            on sd.SalesID = s.SalesID
                                            join product p
                                            on p.ProductID = sd.ProductID
                                            where DATEDIFF(DAY, s.SalesDate, GETDATE()) <= 31 
                                            and s.CustomerID = " + id;
                    _context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        CustomerPurchaseViewModel data;
                        while (result.Read())
                        {
                            data = new CustomerPurchaseViewModel();
                            data.SalesID = result.GetInt32(0);
                            data.CustomerID = result.GetInt32(1);
                            data.BillNumber = result.GetInt32(2);
                            data.ProductID = result.GetInt32(3);
                            data.ProductName = result.GetString(4);
                            data.Quantity = result.GetInt32(5);
                            data.Price = result.GetInt32(6);
                            data.DaysBeforePurchsed = result.GetInt32(7);
                            lstData.Add(data);
                        }
                    }
                }
            }
            ViewData["CustomerID"] = new SelectList(_context.Set<Customer>(), "CustomerID", "Name");
            return View(lstData);
        }

        public IActionResult InactiveCustomer()
        {
            List<InactiveCustomerViewModel> lstData = new List<InactiveCustomerViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"select c.CustomerID,c.Name,ISNULL(s.SalesDate,cast('1900-01-01' as DATE)) from Customer c
                                            left join Sales s
                                            on s.CustomerID = c.CustomerID
                                            where s.SalesDate IS NULL OR 
                                            DATEDIFF(DAY, s.SalesDate,GETDATE()) >= 31";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    InactiveCustomerViewModel data;
                    while (result.Read())
                    {
                        data = new InactiveCustomerViewModel();
                        data.CustomerID = result.GetInt32(0);
                        data.Name = result.GetString(1);
                        data.LastPurchasedON = result.GetDateTime(2).ToString() == "1/1/1900 12:00:00 AM" ? "N/A" : result.GetDateTime(2).ToString();
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        public IActionResult InactiveItem()
        {
            List<InactiveItemViewModel> lstData = new List<InactiveItemViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {


                command.CommandText = @"select p.ProductID,p.ProductName,ps.Quantity,ISNULL(s.SalesDate,cast('1900-01-01' as DATE)) from Product p
                                        left join ProductStock ps
                                        on p.ProductID = ps.ProductID
                                        left join SalesDetails sd
                                        on sd.ProductID = p.ProductID
                                        left join Sales s
                                        on s.SalesID = sd.SalesID
                                        where ps.Quantity > 0 AND
                                        (s.SalesDate IS NULL OR DATEDIFF(DAY, s.SalesDate,GETDATE()) >= 31)";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    InactiveItemViewModel data;
                    while (result.Read())
                    {
                        data = new InactiveItemViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        data.LastSalesDate = result.GetDateTime(3).ToString() == "1/1/1900 12:00:00 AM" ? "N/A" : result.GetDateTime(3).ToString();
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        public IActionResult ItemsOutOfStock(string? filterOption)
        {
            List<ItemDetailsViewModel> itemData = new List<ItemDetailsViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {

                if (filterOption == "Name")
                {
                    command.CommandText = @"select p.ProductID,p.ProductName,ps.Quantity,pu.PurchaseDate from Product p
                                        left join ProductStock ps
                                        on p.ProductID = ps.ProductID
                                        left join PurchaseDetails pd
                                        on pd.ProductID = p.ProductID
                                        left join Purchase pu 
                                        on pu.PurchaseID = pd.PurchaseID
                                        where ps.Quantity < 10 and pu.PurchaseDate is not null 
                                        ORDER BY p.ProductName ASC
                                        ;";
                }
                else if (filterOption == "qty")
                {
                    command.CommandText = @"select p.ProductID,p.ProductName,ps.Quantity,pu.PurchaseDate from Product p
                                        join ProductStock ps
                                        on p.ProductID = ps.ProductID
                                        join PurchaseDetails pd
                                        on pd.ProductID = p.ProductID
                                        join Purchase pu 
                                        on pu.PurchaseID = pd.PurchaseID
                                        where ps.Quantity < 10 and pu.PurchaseDate is not null 
                                        ORDER BY ps.Quantity DESC
                                        ;";
                }
                else if (filterOption == "date")
                {
                    command.CommandText = @"select p.ProductID,p.ProductName,ps.Quantity,pu.PurchaseDate from Product p
                                        left join ProductStock ps
                                        on p.ProductID = ps.ProductID
                                        left join PurchaseDetails pd
                                        on pd.ProductID = p.ProductID
                                        left join Purchase pu 
                                        on pu.PurchaseID = pd.PurchaseID
                                        where ps.Quantity < 10 and pu.PurchaseDate is not null 
                                        ORDER BY pu.PurchaseDate DESC
                                        ;";
                }
                else
                {

                    command.CommandText = @"select p.ProductID,p.ProductName,ps.Quantity,pu.PurchaseDate from Product p
                                        join ProductStock ps
                                        on p.ProductID = ps.ProductID
                                        join PurchaseDetails pd
                                        on pd.ProductID = p.ProductID
                                        join Purchase pu 
                                        on pu.PurchaseID = pd.PurchaseID
                                        where ps.Quantity < 10 and pu.PurchaseDate is not null;";


                }
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    ItemDetailsViewModel data;
                    while (result.Read())
                    {
                        data = new ItemDetailsViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        data.PurchaseDate = result.GetDateTime(3).ToString();
                        itemData.Add(data);
                    }
                }
            }
            return View(itemData);
        }

        public IActionResult StockedForLongTime()
        {
            List<Product> itemData = new List<Product>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"select p.ProductID, p.ProductName, p.CategoryID from Product p
                                        left join ProductStock ps
                                        on p.ProductID = ps.ProductID
                                        left join PurchaseDetails pd
                                        on pd.ProductID = p.ProductID
                                        left join Purchase pu 
                                        on pu.PurchaseID = pd.PurchaseID
                                        where pu.PurchaseDate is null;";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    Product data;
                    while (result.Read())
                    {
                        data = new Product();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.CategoryID = result.GetInt32(2);
                        itemData.Add(data);
                    }
                }
            }
            return View(itemData);
        }

    }
}
