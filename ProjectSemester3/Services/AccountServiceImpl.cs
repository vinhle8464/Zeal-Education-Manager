using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ProjectSemester3.Models;
using ProjectSemester3.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly DatabaseContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMailService mailService;
        public AccountServiceImpl(DatabaseContext _context, IWebHostEnvironment webHostEnvironment, IMailService _mailService)
        {
            context = _context;
            _webHostEnvironment = webHostEnvironment;
            mailService = _mailService;
        }

        //Change Password method!
        public async Task<int> ChangePasswordByEmail(string password, string email)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(p => p.Email == email);
            account.Password = BCrypt.Net.BCrypt.HashString(password);
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public async Task<int> ChangePasswordByUsername(string password, string username)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(p => p.Username == username);
            account.Password = BCrypt.Net.BCrypt.HashString(password);
            account.Active = true;
            account.Status = true;
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public Account Find(string username)
        {
            return context.Accounts.SingleOrDefault(a => a.Username == username);
        }

        public Account FindID(string id)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == id);
        }
        public async Task<Account> FindEmail(string email)
        {
            var account = await context.Accounts.Where(a => a.Email == email && a.Status == true).FirstOrDefaultAsync();
            if (account == null)
            {
                return null;
            }
            else
            {
                return account;
            }
        }
        public string GetRole(string username)
        {
            return context.Roles.FirstOrDefault(m => m.RoleId == context.Accounts.Single(m => m.Username == username).RoleId).RoleName;
        }

        public async Task<bool> GetPassword(string username, string oldPassword)
        {
            bool result;
            var password = await context.Accounts.Where(a => a.Username == username).Select(n => n.Password).SingleOrDefaultAsync();
            if (BCrypt.Net.BCrypt.Verify(oldPassword, password))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;

        }

        //Signin method!
        public async Task<dynamic> Signin(string username, string password)
        {//Check for account existence and verify password!
            var account = await context.Accounts.SingleOrDefaultAsync(m => m.Username == username);
            if (account != null)
            {
                if (account.Active == true)
                {
                    if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
                    {
                        return "success";
                    }
                    else
                    {
                        return "fail";
                    }
                }
                else
                {
                    if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
                    {
                        
                        return account.Username;
                    }
                    else
                    {
                        return "fail";
                    }
                }
            }
            else
            {
                return "fail";
            }



        }
        //Signup method!
        public async Task<dynamic> Signup(IFormFile photo, [Bind(new[] { "Idacc,Username,Password,Fullname,Email,Phonenumber,Dob,Avartar,Status,Rolename" })] Account account)
        {
            try
            {//Check for duplicate accounts!
                if (context.Accounts.SingleOrDefault(m => m.Username == account.Username) != null)
                {
                    throw new InvalidOperationException("Username already exits!");
                }
                else
                {//Image encoding!
                    if (photo != null)
                    {
                        var fileName = System.Guid.NewGuid().ToString().Replace("-", "");
                        var ext = photo.ContentType.Split(new char[] { '/' })[1];
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName + "." + ext);
                        using (var file = new FileStream(path, FileMode.Create))
                        {
                            photo.CopyTo(file);
                        }
                        account.Avatar = fileName + "." + ext;
                    }
                    else
                    {
                        account.Avatar = "null.jpg";
                    }
                    //Password encoding!
                    account.Password = BCrypt.Net.BCrypt.HashString(account.Password);
                    //Add a new account to database!
                    context.Add(account);

                    return await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Account Update(Account account)
        {
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return account;
        }
        ///==================================================================
        ///

        public List<Account> Findall()
        {
            return context.Accounts.Where(p => p.Role.RoleId != "role01" && p.Status == true).ToList();
        }

        public async Task<int> Create(IFormFile photo, Account account)
        {
            //Check for duplicate accounts!
            if (context.Accounts.Any(m => m.Username == account.Username && m.Status == true) || context.Accounts.Any(m => m.Email == account.Email && m.Status == true))
            {
                return 0;
            }
            else
            {//Image encoding!
                if (photo != null)
                {
                    var fileName = System.Guid.NewGuid().ToString().Replace("-", "");
                    var ext = photo.ContentType.Split(new char[] { '/' })[1];
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName + "." + ext);
                    using (var file = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(file);
                    }
                    account.Avatar = fileName + "." + ext;
                }
                else
                {
                    account.Avatar = "null.jpg";
                }
                var password = RandomCode(8);
                // send username and password for user
                SendUesrnameAndPasswordActiveAccount(account.Email.Trim(), account.Username.Trim(), password);

                //Password encoding!
                account.Password = BCrypt.Net.BCrypt.HashString(password);
                //Add a new account to database!
                context.Add(account);
                return await context.SaveChangesAsync();
            }



        }

        public async Task<int> CountId()
        {
            return await context.Accounts.Where(p => p.Status == true).CountAsync();
        }

        public string GetNewestId()
        {

            return (from accounts in context.Accounts

                    orderby
                      accounts.AccountId descending
                    select accounts.AccountId).Take(1).SingleOrDefault();
        }

        private static Random random = new Random();

        // random number 
        public string RandomCode(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Send code to confirm Forgot pass
        public void SendUesrnameAndPasswordActiveAccount(string mail, string username, string pass)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("Admin",
            "appchateproject@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User",
            mail);
            message.To.Add(to);

            message.Subject = "Change Your New Password!";

            var bodyBuilder = new BodyBuilder();
            string header = "<h1>This is your Username and Password. Please don't show to anyone!</h1>";
            string userName = username;
            string passWord = pass;
            bodyBuilder.HtmlBody = header + "<br/>" + "Username: " + userName + "<br/>" + "Password: " + passWord + "<br/><br/> Link: http://localhost:58026/";
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

        }

        public string GetNewestUsername()
        {

            return (from accounts in context.Accounts
                    orderby
                      accounts.Username descending
                    select accounts.Username).Take(1).SingleOrDefault();
        }

        public async Task<Batch> GetBatch(string classId)
        {
            return await context.Batches.FirstOrDefaultAsync(m => m.ClassId == classId && m.Status == true);
        }
        public async Task<Course> GetCourse(string courseId)
        {
            return await context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId && c.Status == true);
        }

        public async Task<dynamic> CreatePay(string AccountId, Course course, string email)
        {
            string discount;

            var scholarshipId = await context.ScholarshipStudents.FirstOrDefaultAsync(c => c.AccountId == AccountId);
            if (scholarshipId == null)
            {
                discount = "0";
            }
            else
            {
                discount = context.Scholarships.FirstOrDefault(s => s.ScholarshipId == scholarshipId.ScholarshipId).Discount;
            }

            var pay = new Pay();
            pay.AccountId = AccountId;
            pay.Payment = "Paypal";
            pay.Title = "Coursefee";
            pay.Fee = (decimal)course.Fee;
            pay.Discount = (pay.Fee * Int32.Parse(discount)) / 100;
            pay.Total = pay.Fee - pay.Discount;
            pay.DateRequest = DateTime.Now;
            pay.DatePaid = null;
            pay.PayStatus = false;
            context.Pays.Add(pay);
            await context.SaveChangesAsync();
            string content = "<h2>Your course's fee is " + Convert.ToInt32(pay.Total) + ".</h2> <br/>" + "Please pay it in the shortest time!" + "<br/><br/>" + "Best regards," + "<br/>" + "<b>Zeal Education<b/>" + "<br/><br/> Link: http://localhost:58026/";
            mailService.Reply(email, pay.Title, content);
            return true;
        }

        public async Task CreateScholarshipStudent(string acccountId, string scholarshipId)
        {
            context.ScholarshipStudents.Add(new ScholarshipStudent { AccountId = acccountId, ScholarshipId = scholarshipId, Status = true });
            await context.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllClass(string keyword)
        {
            var listClassName = new List<string>();

            var listclass = await context.Classes.Where(c => c.ClassName.ToLower().Contains(keyword.ToLower()) && c.Status == true).ToListAsync();
            foreach (var item in listclass)
            {
                var result = context.Classes.FirstOrDefault(c => c.ClassId == item.ClassId && c.NumberOfStudent > context.Accounts.Where(a => a.ClassId == item.ClassId).Count() && c.Status == true);
                if (result != null)
                {
                    listClassName.Add(result.ClassName);
                }
            }
            return listClassName;
        }

        public async Task<List<string>> GetAllScholarship(string keyword)
        {
            var result = new List<string>();

            var listScholarship = await context.Scholarships.Where(c => c.ScholarshipName.ToLower().Contains(keyword.ToLower()) && c.Status == true).ToListAsync();
            foreach (var item in listScholarship)
            {
                result.Add(item.ScholarshipName);
            }
            return result;
        }

   

        public async Task<Account> FindAjax(string accountId)
        {
            return await context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId && a.Status == true);
        }


        public async Task<List<string>> GetKeyWordByKeyword(string keyword)
        {
            var list = new List<string>();

            var listFullname = await context.Accounts
                .Where(c => c.Fullname.ToLower().Contains(keyword.ToLower()) && c.Status == true)
                .Select(c => c.Fullname).ToListAsync();
            foreach (var item in listFullname)
            {
                list.Add(item);
            }

            var listPhone = await context.Accounts
                .Where(c => c.Phone.ToLower().Contains(keyword.ToLower()) && c.Status == true)
                .Select(c => c.Phone).ToListAsync();
            foreach (var item in listPhone)
            {
                list.Add(item);
            }

            var listEmail = await context.Accounts
             .Where(c => c.Email.ToLower().Contains(keyword.ToLower()) && c.Status == true)
             .Select(c => c.Email).ToListAsync();
            foreach (var item in listEmail)
            {
                list.Add(item);
            }

            return list;
        }

        public List<Account> Search(string searchKeyword, string roleKeyword, string genderKeyword, string statusKeyword)
        {

            var accounts = context.Accounts.AsQueryable();
            if (roleKeyword != null) accounts = accounts.Where(s => s.Role.RoleName.Contains(roleKeyword));
            if (genderKeyword != null) accounts = accounts.Where(s => s.Gender == Boolean.Parse(genderKeyword));
            if (statusKeyword != null) accounts = accounts.Where(s => s.Active == Boolean.Parse(statusKeyword));

            if (searchKeyword != null) accounts = accounts.Where(b => b.Fullname.Contains(searchKeyword) || b.Phone.Contains(searchKeyword) || b.Email.Contains(searchKeyword));

            var result = accounts.Where(a => a.RoleId != "role01" && a.Status == true).ToList(); // execute query

            return result;
        }
    }
}