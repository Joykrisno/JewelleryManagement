using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JewelleryManagement.Models;
using System;

namespace JewelleryManagement.Controllers
{
    public class BuySellController : Controller
    {
        private static List<GoldRate> _goldRates = new List<GoldRate>
        {
            new GoldRate { Karat = "24K", BuyingRate = 6100, SellingRate = 6200, Change = 25 },
            new GoldRate { Karat = "22K", BuyingRate = 5850, SellingRate = 5950, Change = 20 },
            new GoldRate { Karat = "21K", BuyingRate = 5600, SellingRate = 5700, Change = 15 },
            new GoldRate { Karat = "20K", BuyingRate = 5350, SellingRate = 5450, Change = 12 },
            new GoldRate { Karat = "19K", BuyingRate = 5100, SellingRate = 5200, Change = 10 },
            new GoldRate { Karat = "18K", BuyingRate = 4900, SellingRate = 5000, Change = 10 },
            new GoldRate { Karat = "17K", BuyingRate = 4700, SellingRate = 4800, Change = 8 },
            new GoldRate { Karat = "16K", BuyingRate = 4500, SellingRate = 4600, Change = 8 },
            new GoldRate { Karat = "15K", BuyingRate = 4300, SellingRate = 4400, Change = 5 },
            new GoldRate { Karat = "14K", BuyingRate = 4100, SellingRate = 4200, Change = 5 }
        };

        public IActionResult Index()
        {
            ViewBag.GoldRates = _goldRates;
            return View();
        }
        public IActionResult Sell()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult GetGoldRates()
        {
            return Json(_goldRates);
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = new List<object>
            {
                new { Id = 1, Name = "রহিম মিয়া", Phone = "01712-345678" },
                new { Id = 2, Name = "করিমা বেগম", Phone = "01812-345679" },
                new { Id = 3, Name = "শফিকুল ইসলাম", Phone = "01912-345680" },
                new { Id = 4, Name = "নাসরিন আক্তার", Phone = "01612-345681" },
                new { Id = 5, Name = "জাহিদ হাসান", Phone = "01512-345682" },
                new { Id = 6, Name = "ফাতেমা বেগম", Phone = "01312-345683" }
            };
            return Json(customers);
        }
    }

    public class GoldRate
    {
        public string Karat { get; set; }
        public decimal BuyingRate { get; set; }
        public decimal SellingRate { get; set; }
        public decimal Change { get; set; }
    }
} 
 