using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Services
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly DatabaseContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProfileServiceImpl(DatabaseContext _context, IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            webHostEnvironment = _webHostEnvironment;
        }

        public async Task<int> ChangePassword(string password, string username)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(p => p.Username == username);
            account.Password = BCrypt.Net.BCrypt.HashString(password);
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public async Task<int> ForgotPassword(string password, string email)
        {
            var account = await context.Accounts.SingleOrDefaultAsync(p => p.Email == email);
            account.Password = BCrypt.Net.BCrypt.HashString(password);
            context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public async Task<bool> GetPassword(string username, string currentPassword)
        {
            bool result;
            var password = await context.Accounts.Where(a => a.Username == username).Select(n => n.Password).SingleOrDefaultAsync();
            if (BCrypt.Net.BCrypt.Verify(currentPassword, password))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
