using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Models;
using ProjectSemester3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface AccountService
    {
        Task<dynamic> Signup(IFormFile photo, [Bind("Idacc,Username,Password,Fullname,Email,Phonenumber,Dob,Avartar,Status,Rolename")] Account account);
        Task<dynamic> Signin(string username, string password);
        Task<int> ChangePasswordByEmail(string password, string email);
        Task<int> ChangePasswordByUsername(string password, string username);
        Task<bool> GetPassword(string username, string oldPassword);
        Account Find(string username);
        Account FindID(string id);
        Account Update(Account account);
    }
}
