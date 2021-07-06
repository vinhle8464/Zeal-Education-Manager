using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Models
{
    public class ClassMetaData
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        
        public string Desc { get; set; }
        public bool Status { get; set; }
    }
    [ModelMetadataType(typeof(ClassMetaData))]
    public partial class Class
    {

    }
}
