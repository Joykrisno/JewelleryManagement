using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using JewelleryManagement.Models; // যদি Models ফোল্ডার ব্যবহার করেন

namespace JewelleryManagement.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            // Dummy Customer List তৈরি করা হলো
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "রহিম মিয়া",
                    Phone = "01712-345678",
                    Email = "rahim@gmail.com",
                    TotalPurchase = 125.5m,
                    LastPurchaseDate = "২০২৬-০৩-১৪",
                    Status = "সক্রিয়",
                    ProfileImage = "👨‍🦱"
                },
                new Customer
                {
                    Id = 2,
                    Name = "করিমা বেগম",
                    Phone = "01812-345679",
                    Email = "karima@gmail.com",
                    TotalPurchase = 85.0m,
                    LastPurchaseDate = "২০২৬-০৩-১৩",
                    Status = "সক্রিয়",
                    ProfileImage = "👩‍🦰"
                },
                new Customer
                {
                    Id = 3,
                    Name = "শফিকুল ইসলাম",
                    Phone = "01912-345680",
                    Email = "shafiq@gmail.com",
                    TotalPurchase = 250.0m,
                    LastPurchaseDate = "২০২৬-০৩-১২",
                    Status = "প্রিমিয়াম",
                    ProfileImage = "👨‍🦳"
                },
                new Customer
                {
                    Id = 4,
                    Name = "নাসরিন আক্তার",
                    Phone = "01612-345681",
                    Email = "nasrin@gmail.com",
                    TotalPurchase = 45.5m,
                    LastPurchaseDate = "২০২৬-০৩-১০",
                    Status = "নতুন",
                    ProfileImage = "👩"
                },
                new Customer
                {
                    Id = 5,
                    Name = "জাহিদ হাসান",
                    Phone = "01512-345682",
                    Email = "zahid@gmail.com",
                    TotalPurchase = 180.0m,
                    LastPurchaseDate = "২০২৬-০৩-০৮",
                    Status = "সক্রিয়",
                    ProfileImage = "👨"
                },
                new Customer
                {
                    Id = 6,
                    Name = "ফাতেমা বেগম",
                    Phone = "01312-345683",
                    Email = "fatema@gmail.com",
                    TotalPurchase = 320.0m,
                    LastPurchaseDate = "২০২৬-০৩-০৫",
                    Status = "প্রিমিয়াম",
                    ProfileImage = "👩‍🦱"
                }
            };

            return View(customers);
        }

        // Customer Details Action
        public IActionResult Details(int id)
        {
            // ডিটেলস এর জন্য ডামি ডাটা
            var customer = new Customer
            {
                Id = id,
                Name = "রহিম মিয়া",
                Phone = "01712-345678",
                Email = "rahim@gmail.com",
                Address = "১২/৩, গুলশান এভিনিউ, ঢাকা",
                TotalPurchase = 125.5m,
                LastPurchaseDate = "২০২৬-০৩-১৪",
                Status = "সক্রিয়",
                ProfileImage = "👨‍🦱",
                TotalOrders = 8,
                PendingOrders = 2,
                TotalSpent = 525000,
                MemberSince = "২০২৫-০১-১৫"
            };

            return View(customer);
        }

        // Customer Create Action (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Customer Create Action (POST)
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // এখানে ডাটাবেজে সেভ করবেন
                TempData["Success"] = "গ্রাহক সফলভাবে যোগ করা হয়েছে!";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Customer Edit Action
        public IActionResult Edit(int id)
        {
            // ডামি ডাটা
            var customer = new Customer
            {
                Id = id,
                Name = "রহিম মিয়া",
                Phone = "01712-345678",
                Email = "rahim@gmail.com",
                Address = "১২/৩, গুলশান এভিনিউ, ঢাকা",
                Status = "সক্রিয়"
            };

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "গ্রাহকের তথ্য আপডেট করা হয়েছে!";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Customer Delete Action
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // ডিলিট লজিক
            TempData["Success"] = "গ্রাহক ডিলিট করা হয়েছে!";
            return RedirectToAction("Index");
        }

        // Search Customer Action
        public IActionResult Search(string searchTerm)
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "রহিম মিয়া", Phone = "01712-345678" },
                new Customer { Id = 2, Name = "করিমা বেগম", Phone = "01812-345679" },
                new Customer { Id = 3, Name = "শফিকুল ইসলাম", Phone = "01912-345680" }
            };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                customers = customers.Where(c =>
                    c.Name.Contains(searchTerm) ||
                    c.Phone.Contains(searchTerm)).ToList();
            }

            return PartialView("_CustomerSearchResults", customers);
        }
    }
}

 