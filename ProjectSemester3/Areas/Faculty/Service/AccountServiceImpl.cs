using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountServiceImpl(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Account> Findall()
        {
            return _context.Accounts.ToList();
        }

        public Account getfaculty(string facultyid)
        {
            return _context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public List<Role> Selectrole()
        {
            return _context.Roles.ToList();
        }

        public async Task<dynamic> Signup(IFormFile photo, Account account)
        {
            try
            {//Check for duplicate accounts!
                if (_context.Accounts.SingleOrDefault(m => m.Username == account.Username) != null)
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
                    _context.Add(account);

                    return await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
