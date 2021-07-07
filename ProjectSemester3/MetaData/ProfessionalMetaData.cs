using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Models
{
    public class ProfessionalMetaData
    {

        public string FacultyId { get; set; }
        public string SubjectId { get; set; }
        public bool Status { get; set; }

     
    }
    [ModelMetadataType(typeof(ProfessionalMetaData))]
    public partial class Professional
    {

    }
}
