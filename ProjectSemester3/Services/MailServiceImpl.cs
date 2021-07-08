using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class MailServiceImpl : IMailService
    {
        private static Random random = new Random();
        private IWebHostEnvironment webHostEnvironment;
        private DatabaseContext context;

        public MailServiceImpl(IWebHostEnvironment webHostEnvironment, DatabaseContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        // random number 
        public static string RandomCode(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Send code to confirm Forgot pass
        public string SendCodeForgotPass(string mail)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("Admin",
            "appchateproject@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User",
            mail);
            message.To.Add(to);

            message.Subject = "Change Password!";

            var bodyBuilder = new BodyBuilder();
            string header = "<h1>This is your code. It is avaible in 60 seconds. Please don't show to anyone!</h1>";
            string content = RandomCode(6);
            bodyBuilder.HtmlBody = header + "<br/>" + content;
            //bodyBuilder.TextBody = content;

            // send image
            //bodyBuilder.Attachments.Add(hostingEnvironment.WebRootPath + "\\images\\cute.jpg");

            message.Body = bodyBuilder.ToMessageBody();

            var client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("appchateproject@gmail.com", "Appchat12345");


            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return content;

        }

        public async Task Delete(int MailId)
        {
            var mail = context.Mail.Find(MailId);
            mail.Status = false;
            context.Entry(mail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public Mail Find(int mailid)
        {
            return context.Mail.SingleOrDefault(m => m.MailId == mailid);
        }

        public async Task<List<Mail>> FindAll() => await context.Mail.Where(m => m.Status == true).OrderByDescending(m => m.MailId).Take(10).ToListAsync();

        public bool Exists(int MailId) => context.Mail.Any(e => e.MailId == MailId && e.Status == true);

        public async Task<Mail> Update(Mail mail)
        {
            context.Entry(mail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return mail;
        }

        public string Reply(string mail, string title, string content)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("Admin",
            "appchateproject@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User",
            mail);
            message.To.Add(to);

            message.Subject = title;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = content;


            message.Body = bodyBuilder.ToMessageBody();

            var client = new MailKit.Net.Smtp.SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("appchateproject@gmail.com", "Appchat12345");


            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return content;
        }

        public Mail InsertIntoDb(Mail mail)
        {
            context.Entry(mail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return mail;
        }

        public List<Mail> Search(string searchMail)
        {
            var mails = context.Mail.AsQueryable();

            if (searchMail != null) mails = mails.Where(b => b.Title.Contains(searchMail) || b.EmailUser.Contains(searchMail) || b.Fullname.Contains(searchMail));

            var result = mails.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }
    }
}
