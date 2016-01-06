using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using ModelSite.Models;

namespace ModelSite.Controllers
{
    public class HomeController : Controller
    {

        
        String address = "lapalapaluza@gmail.com";
        List<string> extensions = new List<string>() { ".png", ".jpg", ".jpeg", ".gif" };
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using ( var sw = new System.IO.StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }




        [HttpGet]
        public ActionResult Model()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Model(JoinViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool fileError = false;
                if (model.FullPhoto == null || model.FullPhoto.ContentLength <= 0 || !extensions.Contains(Path.GetExtension(model.FullPhoto.FileName)))
                {
                    ViewBag.SecondPhotoError = "Невалидный файл";
                    fileError = true;
                }

                if (model.HalfPhoto == null || model.HalfPhoto.ContentLength <= 0 || !extensions.Contains(Path.GetExtension(model.FullPhoto.FileName)))
                {
                    ViewBag.FirstPhotoError = "Невалидный файл";
                    fileError = true;
                }

                if (fileError)
                    return View();


                var message = new MailMessage();
                message.To.Add(new MailAddress(address));  
                message.Subject = "Заявка модели";
                message.IsBodyHtml = true;
                message.Body = model.CreateMessage();

                message.Attachments.Add(new Attachment(model.FullPhoto.InputStream, Path.GetFileName(model.FullPhoto.FileName)));
                message.Attachments.Add(new Attachment(model.HalfPhoto.InputStream, Path.GetFileName(model.HalfPhoto.FileName)));

                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        ViewBag.MailResult = "Анкета отправлена успешно";
                    }
                }
                catch
                {
                    ViewBag.MailResult = "Ошибка при отправке анкеты";
                }

                return View("MailSendResult");
            }
            return View();
        }



        [HttpGet]
        public ActionResult Scout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Scout(ScoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool fileError = false;
                if (model.FullPhoto == null || model.FullPhoto.ContentLength <= 0 || !extensions.Contains(Path.GetExtension(model.FullPhoto.FileName)))
                {
                    ViewBag.FirstPhotoError = "Невалидный файл";
                    fileError = true;
                }

                if (model.HalfPhoto == null || model.HalfPhoto.ContentLength <= 0 || !extensions.Contains(Path.GetExtension(model.FullPhoto.FileName)))
                {
                    ViewBag.SecondPhotoError = "Невалидный файл";
                    fileError = true;
                }

                if (fileError)
                    return View();


                var message = new MailMessage();
                message.To.Add(new MailAddress(address));  
                message.Subject = "Заявка скаута";
                message.IsBodyHtml = true;
                message.Body = model.CreateMessage();

                message.Attachments.Add(new Attachment(model.FullPhoto.InputStream, Path.GetFileName(model.FullPhoto.FileName)));
                message.Attachments.Add(new Attachment(model.HalfPhoto.InputStream, Path.GetFileName(model.HalfPhoto.FileName)));
                try
                {
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        ViewBag.MailResult = "Анкета отправлена успешно";
                    }
                }
                catch
                {
                    ViewBag.MailResult = "Ошибка при отправке анкеты";
                }

                return View("MailSendResult");

            }
            return View();
        }

    }





}

/* var credential = new NetworkCredential
                    {
                        UserName = "lapalapaluza@gmail.com",  // replace with valid value
                        Password = "qwaszx579"  // replace with valid value
                    };*/
// smtp.UseDefaultCredentials = false;
//smtp.Credentials = credential;
//smtp.Host = "smtp.gmail.com";
//smtp.Port = 587;
//smtp.EnableSsl = true;