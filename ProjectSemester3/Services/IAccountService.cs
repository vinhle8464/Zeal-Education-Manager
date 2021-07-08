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
    public interface IAccountService
    {
        //Task<dynamic> Signup(IFormFile photo, [Bind("Idacc,Username,Password,Fullname,Email,Phonenumber,Dob,Avartar,Status,Rolename")] Account account);
        Task<dynamic> Signin(string username, string password);
        Task<int> ChangePasswordByEmail(string password, string email);
        Task<int> ChangePasswordByUsername(string password, string username);
        Task<bool> GetPassword(string username, string oldPassword);
        Account Find(string username);
        Account FindID(string id);
        Task<Account> FindEmail(string email);

        Account Update(Account account);
        String GetRole(string username);

        ////////////
        ///  
        List<Account> Findall();
        Task<List<string>> GetAllClass(string keyword);
        Task<List<string>> GetAllScholarship(string keyword);
        Task<Batch> GetBatch(string classId);
        Task<Course> GetCourse(string courseId);

        Task<int> Create(IFormFile photo, Account account);
        Task<int> CountId();
        string GetNewestId();
        string GetNewestUsername();
        string RandomCode(int n);
        Task<dynamic> CreatePay(string AccountId, Course course, string email);
        Task CreateScholarshipStudent(string acccountId, string scholarshipId);

        Task<Account> FindAjax(string accountId);

        public Task<List<string>> GetKeyWordByKeyword(string keyword);
        public List<Account> Search(string searchKeyword, string roleKeyword, string genderKeyword, string statusKeyword);

    }
}
