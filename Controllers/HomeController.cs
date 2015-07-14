using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JTLPortalV2.Models;
using System.Net.Mail;
using System.Text;
using System.Net;
using Microsoft.Azure;

namespace JTLPortalV2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            EmailInfoModel emailInfo = new EmailInfoModel();
            return View(emailInfo);
            
        }

        public ActionResult SendForm(EmailInfoModel emailInfo)
        {
            try
            {
                MailMessage msg = new MailMessage(CloudConfigurationManager.GetSetting("EmailAddr"), "jack.teng.li@gmail.com");
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {

                    Credentials = new NetworkCredential(CloudConfigurationManager.GetSetting("EmailAddr"), CloudConfigurationManager.GetSetting("EmailKey")),
                    EnableSsl = true
                };

                StringBuilder sb = new StringBuilder();
                msg.To.Add("jack.teng.li@gmail.com");
                msg.Subject = "Contact Us";
                msg.IsBodyHtml = false;

                sb.Append(Environment.NewLine);
                sb.Append("Email: " + emailInfo.Email);
                sb.Append(Environment.NewLine);
                sb.Append("Message: " + emailInfo.Message);

                msg.Body = sb.ToString();

                smtp.Send(msg);
                msg.Dispose();
                return RedirectToAction("Contact", "Home");
            }
            catch (Exception)
            {
                return View("Error");
            }   
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View();
        }
    }
}