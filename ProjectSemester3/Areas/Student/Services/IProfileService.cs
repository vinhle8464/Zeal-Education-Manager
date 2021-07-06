using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Services
{
    public interface IProfileService
    {
        Task<int> ChangePassword(string password, string username);
        Task<bool> GetPassword(string username, string currentPassword);
        Task<int> ForgotPassword(string password, string email);

    }
}
