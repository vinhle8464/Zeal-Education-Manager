using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Models
{
    public class EnquiryMetaData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public bool Status { get; set; }
    }
    [ModelMetadataType(typeof(EnquiryMetaData))]
    public partial class Enquiry
    {

    }
}
