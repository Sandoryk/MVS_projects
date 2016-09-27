﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowSpy.Models;
using WorkFlowManager;
using WorkFlowManager.ModelsWFM;
using WorkFlowSpy.Tools;
using System.Xml.Linq;

namespace WorkFlowSpy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Diagram()
        {
            DiagramAdapter DAdapter = new DiagramAdapter();
            JsonResult json = new JsonResult();

            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                IEnumerable<TaskWFM> gottenTasks = wfs.GetAllTasks();
                List<TaskViewModel> viewTasks = new List<TaskViewModel>();
                List<LinkViewModel> viewLinks = new List<LinkViewModel>();
                TaskViewModel taskView = null;
                LinkViewModel linkView = null;

                if (gottenTasks != null)
                {
                    foreach (var task in gottenTasks)
                    {
                        taskView = DataMapperView.DoMapping<TaskWFM, TaskViewModel>(task);
                        if (taskView != null)
                        {
                            viewTasks.Add(taskView);
                        }
                    }
                }

                json = DAdapter.CreateJson(viewTasks, viewLinks);
            }
            return View(json);
        }

        /// <summary>
        /// Update Diagram tasks/links: insert/update/delete 
        /// </summary>
        /// <param name="form">Diagram data</param>
        /// <returns>XML response</returns>
        [HttpPost]
        public ContentResult SaveDiagramChanges(FormCollection form)
        {
            DiagramAdapter DAdapter = new DiagramAdapter();
            DAdapter.ParseJson(form, Request.QueryString["gantt_mode"]);
            
            using (WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection"))
            {
                DAdapter.MakeUpdate(wfs);
            }

            return Content(DAdapter.ResposeToDiagram().ToString(), "text/xml");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}