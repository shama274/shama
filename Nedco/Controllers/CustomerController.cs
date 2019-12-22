using Nedco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nedco.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Get()
        {
            int rowsCount;
            ViewBag.customers = Customer.GetCustomers(new CustomerParameters { }, out rowsCount);
            return View();
        }
        [HttpPost]
        public ActionResult Save(string name, string address, string mobile, string iban)
        {
            Customer customer = new Customer();
            customer.Address = address;
            customer.IBAN = iban;
            customer.Mobile = mobile;
            customer.Name = name;
            customer.SaveData();

            return View();
        }
    }
}