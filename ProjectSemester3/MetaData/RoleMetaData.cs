using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Models
{
    public class RoleMetaData
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

   
    }
    [ModelMetadataType(typeof(RoleMetaData))]
    public partial class Role
    {

    }
}
