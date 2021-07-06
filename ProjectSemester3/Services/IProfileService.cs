using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IProfileService
    {
        Task<int> ChangePassword(string password, string username);
        Task<bool> GetPassword(string username, string currentPassword);
        Task<int> ForgotPassword(string password, string email);
        string SelectClass(string studentid);
        string SelectCourse(string studentid);
        public Account ChangeAvatar(Account account);
        public Account Edit(Account account);
    }
}
