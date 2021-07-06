using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Mail
    {
        public int MailId { get; set; }
        public string Title { get; set; }
        public string EmailUser { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Content { get; set; }
        public string ReplyContent { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReplyDate { get; set; }
        public bool? Check { get; set; }
        public bool Status { get; set; }
    }
}
