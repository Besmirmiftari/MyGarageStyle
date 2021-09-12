using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using MyGarageStyle.Data;
using MyGarageStyle.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyGarageStyle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var newestProductsImages =  context.PVCs.OrderByDescending(obj => obj.AddedDate).Take(3).ToList();
            ViewBag.newestProductsImages = newestProductsImages;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Products(int? pageNumber)
        {
            var products = from p in context.PVCs select p;
            int pageSize = 9;

            products = products.OrderByDescending(p => p.AddedDate);

            return View(await PaginatedList<PVC>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> ProductDetails(Guid Id) {

            var product = await context.PVCs.FindAsync(Id);
            var productImages = await context.PVCImages.Where(obj => obj.PVCId.Equals(Id)).ToListAsync();
        
            if(product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.productDetailsImages = productImages;
          
            return View(product);
        }

        [HttpPost]
        public IActionResult ContactUs(string Name, string Subject, string Email, string Message,string PhoneNumber, string Address)
        {

            if(Name == null || Name.Trim() == "" || Subject == null || Subject.Trim() == "" || Email == null || Email.Trim() == "" ||Message == null || Message.Trim() == "" || PhoneNumber == null || PhoneNumber.Trim() == "" || Address == null || Address.Trim() == "")
            {
                return RedirectToAction(nameof(Index));
            }

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("MyGarageStyleCustomer", "mygaragestylecustomers@gmail.com"));
            message.To.Add(MailboxAddress.Parse("besmirmiftari5@gmail.com"));
            //mygaragestyleca@gmail.com
            message.Subject = Subject;

            message.Body = new TextPart("html")
            {
                Text = "<h4> From: " + Name + "</h4><h3>Email: " + Email + "</h3><h3>Phone number: "+PhoneNumber+"</h3><h3>Address: "+Address+"</h3><br/>" + "Message: " + Message
            };



            SmtpClient client = new SmtpClient();

            try
            {

                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("mygaragestylecustomers@gmail.com", "[5bD&gz&'}u4W?Wh");
                client.Send(message);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return RedirectToAction(nameof(Index));
        }


    }
}
