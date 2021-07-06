using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class PayDetail
    {
        public int PayDetailId { get; set; }
        public int PayId { get; set; }
        public string TimesPay { get; set; }
        public string Payment { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public decimal? Paid { get; set; }
        public DateTime? DateRequest { get; set; }
        public DateTime? DatePaid { get; set; }
        public byte? NumberDateLate { get; set; }
        public decimal? Total { get; set; }
        public bool Status { get; set; }

        public virtual Pay Pay { get; set; }
    }
}
