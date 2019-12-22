using Nedco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nedco.Controllers
{
    public class MeterController : Controller
    {
        // GET: Meter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Save(int? id, int? customer_id, int? prev_value, int? current_value, int? ceil)
        {
            Meter meter = new Meter();
            meter.ceil = ceil;
            meter.Current_value = current_value;
            meter.Customer_id = customer_id;
            meter.Id = id;
            meter.Prev_value = prev_value;

            meter.SaveData();
            return View();
        }
    }
}