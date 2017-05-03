using AutofacDapperDemo.Models.Log;
using AutofacDapperDemo.Service.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutofacDapperDemo.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }
        // GET: Log
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var entity = _logService.LogList();
            var modelList = entity.Select(t => new LogModel()
            {
                Id = t.Id,
                Message = t.Message,
                CreateDate = t.CreateDate
            });
            return View(modelList);
        }


    }
}