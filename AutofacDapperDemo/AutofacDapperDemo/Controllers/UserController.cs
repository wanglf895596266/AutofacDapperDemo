using AutofacDapperDemo.Core.Log;
using AutofacDapperDemo.Core.User;
using AutofacDapperDemo.Models.Users;
using AutofacDapperDemo.Service.Log;
using AutofacDapperDemo.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutofacDapperDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public UserController(IUserService userService,ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public async Task<ActionResult> List()
        {
            var entity = await _userService.UserList();
            var model = new List<UserModel>();
            model = entity.Select(x => new UserModel()
            {
                Id = x.Id,
                Name = x.Name,
                Adress = x.Adress,
                Tel = x.Tel,
                Age = x.Age,
                CreateDate = x.CreateDate
            }).ToList();
            return View(model);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var entity = await _userService.GetById(id);
            var model = new UserModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Adress = entity.Adress,
                Age = entity.Age,
                Tel = entity.Tel,
                CreateDate = entity.CreateDate
            };
            return View(model);
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var model = new UserModel();
            return View(model);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            try
            {
                // TODO: Add insert logic here
                var user = new User()
                {
                    Name = model.Name,
                    Adress = model.Adress,
                    Age = model.Age,
                    Tel = model.Tel,
                    CreateDate = DateTime.Now
                };
                _userService.Insert(user);
                var log = new Logs();
                log.Message = DateTime.Now + ":创建用户" + user.Name;
                _logService.Insert(log);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var entity = await _userService.GetById(id);
            var model = new UserModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Adress = entity.Adress,
                Age = entity.Age,
                Tel = entity.Tel,
                CreateDate = entity.CreateDate
            };
            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserModel model)
        {
            try
            {
                // TODO: Add update logic here
                var entity = await _userService.GetById(model.Id.ToString());
                entity.Adress = model.Adress;
                entity.Age = model.Age;
                entity.Tel = model.Tel;
                entity.Name = model.Name;
                _userService.Update(entity);
 
                var log = new Logs();
                log.Message = DateTime.Now + ":编辑用户" + entity.Name;
                _logService.Insert(log);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var entity = await _userService.GetById(id);
            var model = new UserModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Adress = entity.Adress,
                Age = entity.Age,
                Tel = entity.Tel,
                CreateDate = entity.CreateDate
            };
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(UserModel model)
        {
            try
            {
                // TODO: Add delete logic here
                var entity = await _userService.GetById(model.Id.ToString());

                _userService.Delete(entity);

                var log = new Logs();
                log.Message = DateTime.Now + ":删除用户" + entity.Name;
                _logService.Insert(log);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
