using Microsoft.AspNetCore.Mvc;
using Graduation.Infrastructure.Repositories.Abstract;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation.Controllers
{

    public class EmailFormModel
    {
        
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Message { get; set; }
    }
    public class BaseController : Controller
    {
        protected ILoggingRepository _logRepo;

        public BaseController(ILoggingRepository logRepo)
        {
            _logRepo = logRepo;
        }
        

        //[HttpPost]
        //public async Task<ActionResult> Contact(EmailFormModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //        var message = new MailMessage();
        //        message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
        //        message.From = new MailAddress("sender@outlook.com");  // replace with valid value
        //        message.Subject = "Your email subject";
        //        message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
        //        message.IsBodyHtml = true;

        //        using (var smtp = new SmtpClient())
        //        {
        //            var credential = new NetworkCredential
        //            {
        //                UserName = "user@outlook.com",  // replace with valid value
        //                Password = "password"  // replace with valid value
        //            };
        //            smtp.Credentials = credential;
        //            smtp.Host = "smtp-mail.outlook.com";
        //            smtp.Port = 587;
        //            smtp.EnableSsl = true;
        //            await smtp.SendMailAsync(message);
        //            return RedirectToAction("Sent");
        //        }
        //    }
        //    return View(model);
        //}
    }
}
