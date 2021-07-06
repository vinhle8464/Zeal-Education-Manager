using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class IAccountServiceImpl : AccountService
    {
        private readonly DatabaseContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public IAccountServiceImpl(DatabaseContext _context, IWebHostEnvironment webHostEnvironment)
        {
            context = _context;
            _webHostEnvironment = webHostEnvironment;
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
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await context.SaveChangesAsync();
        }
        public string GetRole(string username)
        {
            return context.Roles.FirstOrDefault(m => m.RoleId == context.Accounts.Single(m => m.Username == username).RoleId).RoleName;
        }
        public Account Find(string username)
        {
            return context.Accounts.SingleOrDefault(a => a.Username == username);
        }

        public Account FindID(string id)
        {
            return context.Accounts.FirstOrDefault(m=>m.AccountId==id);
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
            var account = await context.Accounts.SingleAsync(m => m.Username == username);
            if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                return account;
            }
            else
            {
                throw new InvalidOperationException("Wrong username or password!");
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
    }
}
