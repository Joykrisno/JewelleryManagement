using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JewelleryManagement.Models;
using System;

namespace JewelleryManagement.Controllers
{
    public class BuySellController : Controller
    {
        // ডামি ডাটা স্টোর করার জন্য স্ট্যাটিক লিস্ট
        private static List<Transaction> _transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = 1,
                CustomerName = "রহিম মিয়া",
                CustomerId = 1,
                Type = "বিক্রয়",
                ProductType = "সোনার চেইন",
                Weight = 12.5m,
                Karat = "22K",
                Price = 5850,
                TotalAmount = 73125,
                Date = DateTime.Now.AddDays(-1),
                PaymentMethod = "নগদ",
                Status = "সম্পন্ন",
                GoldRate = 5850
            },
            new Transaction
            {
                Id = 2,
                CustomerName = "করিমা বেগম",
                CustomerId = 2,
                Type = "ক্রয়",
                ProductType = "গহনা",
                Weight = 25.0m,
                Karat = "21K",
                Price = 5600,
                TotalAmount = 140000,
                Date = DateTime.Now.AddDays(-2),
                PaymentMethod = "বিকাশ",
                Status = "সম্পন্ন",
                GoldRate = 5600
            },
            new Transaction
            {
                Id = 3,
                CustomerName = "শফিকুল ইসলাম",
                CustomerId = 3,
                Type = "বিক্রয়",
                ProductType = "সোনার বালা",
                Weight = 18.0m,
                Karat = "22K",
                Price = 5850,
                TotalAmount = 105300,
                Date = DateTime.Now.AddDays(-3),
                PaymentMethod = "নগদ",
                Status = "সম্পন্ন",
                GoldRate = 5850
            },
            new Transaction
            {
                Id = 4,
                CustomerName = "নাসরিন আক্তার",
                CustomerId = 4,
                Type = "বিক্রয়",
                ProductType = "সোনার নেকলেস",
                Weight = 30.0m,
                Karat = "22K",
                Price = 5850,
                TotalAmount = 175500,
                Date = DateTime.Now,
                PaymentMethod = "ক্রেডিট কার্ড",
                Status = "পেন্ডিং",
                GoldRate = 5850
            }
        };

        private static List<GoldRate> _goldRates = new List<GoldRate>
        {
            new GoldRate { Karat = "24K", BuyingRate = 6000, SellingRate = 6100, Change = +25 },
            new GoldRate { Karat = "22K", BuyingRate = 5750, SellingRate = 5850, Change = +20 },
            new GoldRate { Karat = "21K", BuyingRate = 5500, SellingRate = 5600, Change = +15 },
            new GoldRate { Karat = "18K", BuyingRate = 4800, SellingRate = 4900, Change = +10 }
        };

        public IActionResult Index()
        {
            ViewBag.GoldRates = _goldRates;
            return View(_transactions);
        }

        // AJAX: সোনার দর আনার জন্য
        [HttpGet]
        public IActionResult GetGoldRates()
        {
            return Json(_goldRates);
        }

        // AJAX: ট্রানজেকশন তালিকা আনার জন্য
        [HttpGet]
        public IActionResult GetTransactions(string type = "", string fromDate = "", string toDate = "")
        {
            var query = _transactions.AsQueryable();

            if (!string.IsNullOrEmpty(type) && type != "সব")
            {
                query = query.Where(t => t.Type == type);
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                var from = DateTime.Parse(fromDate);
                query = query.Where(t => t.Date.Date >= from.Date);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                var to = DateTime.Parse(toDate);
                query = query.Where(t => t.Date.Date <= to.Date);
            }

            return Json(query.OrderByDescending(t => t.Date).ToList());
        }

        // AJAX: নতুন ট্রানজেকশন যোগ করার জন্য
        [HttpPost]
        public IActionResult AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
                return BadRequest("Invalid transaction data");

            transaction.Id = _transactions.Count > 0 ? _transactions.Max(t => t.Id) + 1 : 1;
            transaction.Date = DateTime.Now;
            transaction.TotalAmount = transaction.Weight * transaction.Price;

            _transactions.Insert(0, transaction);

            return Json(new
            {
                success = true,
                message = $"{transaction.Type} সফলভাবে সম্পন্ন হয়েছে!",
                transaction = transaction
            });
        }

        // AJAX: ট্রানজেকশন আপডেট করার জন্য
        [HttpPost]
        public IActionResult UpdateTransaction([FromBody] Transaction transaction)
        {
            var existing = _transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (existing == null)
                return NotFound();

            existing.CustomerName = transaction.CustomerName;
            existing.ProductType = transaction.ProductType;
            existing.Weight = transaction.Weight;
            existing.Karat = transaction.Karat;
            existing.Price = transaction.Price;
            existing.TotalAmount = transaction.Weight * transaction.Price;
            existing.PaymentMethod = transaction.PaymentMethod;
            existing.Status = transaction.Status;

            return Json(new
            {
                success = true,
                message = "ট্রানজেকশন আপডেট করা হয়েছে!"
            });
        }

        // AJAX: ট্রানজেকশন ডিলিট করার জন্য
        [HttpPost]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
                return NotFound();

            _transactions.Remove(transaction);

            return Json(new
            {
                success = true,
                message = "ট্রানজেকশন ডিলিট করা হয়েছে!"
            });
        }

        // AJAX: আজকের সামারি আনার জন্য
        [HttpGet]
        public IActionResult GetTodaySummary()
        {
            var today = DateTime.Now.Date;
            var todayTransactions = _transactions.Where(t => t.Date.Date == today);

            var summary = new
            {
                TotalSell = todayTransactions.Where(t => t.Type == "বিক্রয়").Sum(t => t.TotalAmount),
                TotalBuy = todayTransactions.Where(t => t.Type == "ক্রয়").Sum(t => t.TotalAmount),
                SellCount = todayTransactions.Count(t => t.Type == "বিক্রয়"),
                BuyCount = todayTransactions.Count(t => t.Type == "ক্রয়"),
                PendingCount = todayTransactions.Count(t => t.Status == "পেন্ডিং"),
                TotalTransactions = todayTransactions.Count()
            };

            return Json(summary);
        }

        // AJAX: মোট সামারি আনার জন্য
        [HttpGet]
        public IActionResult GetTotalSummary()
        {
            var summary = new
            {
                TotalSell = _transactions.Where(t => t.Type == "বিক্রয়").Sum(t => t.TotalAmount),
                TotalBuy = _transactions.Where(t => t.Type == "ক্রয়").Sum(t => t.TotalAmount),
                TotalWeight = _transactions.Sum(t => t.Weight),
                TotalTransactions = _transactions.Count,
                AverageRate = _transactions.Average(t => t.Price)
            };

            return Json(summary);
        }

        // AJAX: গ্রাহক লিস্ট আনার জন্য (ক্রয়/বিক্রয় ফর্মে ব্যবহারের জন্য)
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = new List<object>
            {
                new { Id = 1, Name = "রহিম মিয়া" },
                new { Id = 2, Name = "করিমা বেগম" },
                new { Id = 3, Name = "শফিকুল ইসলাম" },
                new { Id = 4, Name = "নাসরিন আক্তার" }
            };

            return Json(customers);
        }

        // AJAX: চার্ট ডাটা আনার জন্য
        [HttpGet]
        public IActionResult GetChartData()
        {
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .OrderBy(d => d)
                .ToList();

            var chartData = last7Days.Select(date => new
            {
                Date = date.ToString("dd MMM"),
                Sell = _transactions
                    .Where(t => t.Type == "বিক্রয়" && t.Date.Date == date)
                    .Sum(t => t.TotalAmount),
                Buy = _transactions
                    .Where(t => t.Type == "ক্রয়" && t.Date.Date == date)
                    .Sum(t => t.TotalAmount)
            });

            return Json(chartData);
        }
    }

    // Transaction Model
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Type { get; set; } // ক্রয় / বিক্রয়
        public string ProductType { get; set; }
        public decimal Weight { get; set; }
        public string Karat { get; set; }
        public decimal Price { get; set; } // প্রতি গ্রাম দর
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public decimal GoldRate { get; set; }
        public string Notes { get; set; }
    }

    // Gold Rate Model
    public class GoldRate
    {
        public string Karat { get; set; }
        public decimal BuyingRate { get; set; }
        public decimal SellingRate { get; set; }
        public decimal Change { get; set; }
    }
}