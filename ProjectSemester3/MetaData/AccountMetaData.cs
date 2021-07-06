using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectSemester3.Models
{
    public class AccountMetaData
    {
        public string AccountId { get; set; }
        public string RoleId { get; set; }
        public string ClassId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
    }
    [ModelMetadataType(typeof(AccountMetaData))]
    public partial class Account
    {

    }
}
