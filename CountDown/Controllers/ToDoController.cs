﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class ToDoController : Controller
    {

//        public ActionResult Index()
//        {
//            return View();
//        }

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult List()
        {
            return View("List");
        }
    }
}