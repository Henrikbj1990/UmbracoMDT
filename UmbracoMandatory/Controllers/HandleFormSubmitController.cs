using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoMandatory.ViewModels;
using Umbraco.Core.Models;

namespace UmbracoMandatory.Controllers
{
    public class HandleFormSubmitController : SurfaceController
    {
        // GET: HandleFormSubmit
        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {

            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }

            MailMessage message = new MailMessage();
            message.To.Add("henrikbj1990@hotmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("henrikbj1990@gmail.com", "hbj270590");
                smtp.EnableSsl = true;
                // send mail
                smtp.Send(message);
                TempData["success"] = true;
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}