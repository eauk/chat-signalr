using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chat.Models;
using chat.Repository;

namespace chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepo _repository;

        public HomeController()
        {
            _repository = new UserRepo(new DataContext());
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Enter(EnterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _repository.CheckUser(model.Name, model.Email);

                if (user != null)
                {
                  //alert error
                }
                else
                {
                    var newUser = new User()
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        IsOnline = true
                    };
                    _repository.Add(newUser);

                    return RedirectToAction("Index");
                }
            }

            return Json("Error", JsonRequestBehavior.AllowGet);
        }


        //// This action handles the form POST and the upload
        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase file)
        //{
        //    // Verify that the user selected a file
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        // extract only the fielname
        //        var fileName = Path.GetFileName(file.FileName);
        //        var key = Guid.NewGuid().ToString().Replace("-", "");
        //        // store the file inside ~/App_Data/uploads folder
        //        var path = Path.Combine(Server.MapPath("~/Content/uploads"), key + ".png");

        //        file.SaveAs(path);
        //    }
        //    // redirect back to the index action to show the form once again
        //    return RedirectToAction("Index");
        //}
	}
}