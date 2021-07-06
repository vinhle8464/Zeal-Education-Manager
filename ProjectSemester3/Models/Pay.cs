using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Pay
    {
        public int PayId { get; set; }
        public string AccountId { get; set; }
        public string Payment { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? DatePaid { get; set; }
        public bool PayStatus { get; set; }

        public virtual Account Account { get; set; }
    }
}
