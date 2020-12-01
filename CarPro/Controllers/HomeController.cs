using CarPro.Context;
using CarPro.Models;
using CarPro.ViewModels;
using ceTe.DynamicPDF.HtmlConverter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPro.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _hosting;
        private readonly databaseContext _context;
        public HomeController(databaseContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }
        // GET: Get Customers
        public ActionResult GetCustomer()
        {
            return View(_context.Customers.ToList());
        }

        // GET: Get Offers
        public ActionResult GetOffer()
        {
            return View(_context.Offers.ToList());
        }

        // GET: Get Offers
        public ActionResult GetOrder()
        {
            return View(_context.Orders.ToList());
        }

        // GET: Add or Edit Offer
        public ActionResult EditOffer(int id = 0)
        {
            return View(_context.Offers.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOffer(Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(offer);
                _context.SaveChanges();
                return RedirectToAction(nameof(GetOffer));
            }
            return View(offer);
        }

        //GET: Delete Offer
        public ActionResult DeleteOffer(int? id)
        {
            var offer = _context.Offers.Find(id);
            _context.Offers.Remove(offer);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetOffer));
        }

        // GET: Create Offer
        public ActionResult CreateOffer()
        {
            //ViewBag.message = _context.Customers.ToList();
            return View();
        }

        // POST: Create Offer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOffer(IFormCollection collection, OfferVm offerVm)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var customer = new Customer
                    {
                        Id = 0,
                        Name = offerVm.Name,
                        Address = offerVm.Address,
                        Ac = offerVm.Ac
                    };
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    var offer = new Offer
                    {
                        Id = 0,
                        Type = offerVm.Type,
                        Chassis = offerVm.Chassis,
                        Engine = offerVm.Engine,
                        ManfYear = offerVm.ManfYear,
                        Cc = offerVm.Cc,
                        Colour = offerVm.Colour,
                        LoadCapacity = offerVm.LoadCapacity,
                        Accessories = offerVm.Accessories,
                        Price = offerVm.Price,
                        OfferDate = DateTime.Now,
                        Delivery = offerVm.Delivery,
                        Validity = offerVm.Validity,
                        CustomerId = customer.Id
                    };
                    _context.Offers.Add(offer);
                    _context.SaveChanges();

                    transaction.Commit();
                    
                    return RedirectToAction("GetOffer");
                }
                catch
                {
                    transaction.Rollback();
                    return View();
                }
            }
        }

        //GET: Get Particular Quatation
        public ActionResult GetData(int id)
        {
            var data = (from c in _context.Customers
                        join o in _context.Offers
                        on c.Id equals o.CustomerId
                        where o.Id == id
                        select new QuatationVm
                        {
                            CustomerName = c.Name,
                            Address = c.Address,
                            Ac = c.Ac,
                            OfferDate = o.OfferDate,
                            Type = o.Type,
                            Chassis = o.Chassis,
                            Engine = o.Engine,
                            ManfYear = o.ManfYear,
                            Cc = o.Cc,
                            Colour = o.Colour,
                            LoadCapacity = o.LoadCapacity,
                            Accessories = o.Accessories,
                            Delivery = o.Delivery,
                            Validity = o.Validity,
                            Price = o.Price
                        }).FirstOrDefault();

            string tkWord = NumberToWords(Convert.ToInt32(data.Price));
            ViewBag.word = tkWord;

            return View(data);
        }

        private string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        // GET: Print Particular Quatation
        public ActionResult PrintOffer(int id)
        {
            try
            {
                var report = Converter.Convert(new Uri("https://localhost:44373/Home/GetData/" + id));
                return File(report, "application/pdf");
            }
            catch (Exception)
            {
                return RedirectToAction("GetOffer");
            }
        }

        public ActionResult PlaceOrder(int id)
        {
            var offer = _context.Offers.Find(id);
            ViewBag.offerId = offer.Id;
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(Order order)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int challan = 300;
                    int bill = 500;
                    int receipt = 1300;

                    var existchallan = (from u in _context.Orders
                                        orderby u.ChallanNo descending
                                        select u).Take(1).FirstOrDefault();

                    if (existchallan != null)
                    {
                        challan = existchallan.ChallanNo + 1;
                    }

                    if (existchallan != null)
                    {
                        bill = existchallan.BillNo + 1;
                    }

                    if (existchallan != null)
                    {
                        receipt = existchallan.ReceiptNo+ 1;
                    }

                    var orderObj = new Order
                    {
                        Id = 0,
                        OfferId = order.OfferId,
                        Quantity = order.Quantity,
                        Remarks = order.Remarks,
                        ChallanNo = challan,
                        BillNo = bill,
                        ReceiptNo = receipt,
                        PaymentType = order.PaymentType,
                        OrderDate = DateTime.Now
                    };
                    _context.Orders.Add(orderObj);
                    _context.SaveChanges();

                    var offer = _context.Offers.Find(orderObj.OfferId);

                    Order ordervm = new Order();
                    ordervm = _context.Orders.Find(orderObj.Id);
                    ordervm.TotalPrice = offer.Price * orderObj.Quantity;
                    _context.Entry(ordervm).State = EntityState.Modified;
                    _context.SaveChanges();


                    transaction.Commit();
                    return RedirectToAction("GetOrder");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return View();
                }
            }
        }


        public ActionResult GetReceipt(int id)
        {
            var ch = (from o in _context.Offers
                      join c in _context.Customers
                      on o.CustomerId equals c.Id
                      join ord in _context.Orders
                      on o.Id equals ord.OfferId
                      where o.Id == id
                      select new ReceiptVm
                      {
                          BillNo = ord.BillNo,
                          CustomerName = c.Name,
                          OrderDate = DateTime.Now,
                          ReceiptlNo = ord.ReceiptNo,
                          TotalAmount = o.Price,
                          PaymentType = ord.PaymentType,
                          Address = c.Address,
                          ChallanNo = ord.ChallanNo,
                          Description = "hkhg",
                          Quantity = ord.Quantity,
                          Remarks = ord.Remarks,
                          TypeOfVehicle = o.Type,
                          ChassisNo = o.Chassis,
                          EngineNo = o.Engine,
                          ManfYear = o.ManfYear,
                          Cc = o.Cc,
                          Colour = o.Colour,
                          LoadCapacity = o.LoadCapacity,
                          Accessories = o.Accessories
                      }).FirstOrDefault();
            //return Content("<script>window.open('{url}','_blank')</script>");
            return View(ch);
        }


        public ActionResult PrintReceipt(int id)
        {
            try
            {
                var report = Converter.Convert(new Uri("https://localhost:44373/Home/GetReceipt/" + id));
                return File(report, "application/pdf");
            }
            catch (Exception)
            {
                return RedirectToAction("GetOffer");
            }
        }


































        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
