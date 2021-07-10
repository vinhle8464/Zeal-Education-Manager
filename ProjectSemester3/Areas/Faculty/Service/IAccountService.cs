using Microsoft.AspNetCore.Http;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public interface IAccountService
    {
        List<Account> Findall();
        List<Role> Selectrole();
        Account getfaculty(string facultyid);
        Task<dynamic> Signup(IFormFile photo, Account account);
    }
}
