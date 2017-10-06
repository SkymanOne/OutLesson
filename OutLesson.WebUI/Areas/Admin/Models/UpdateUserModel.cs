using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.WebUI.Areas.Admin.Models
{
    public class UpdateUserModel : ApplicationUser
    {
        [DataType(DataType.Password)]
        public string NewPasssword { get; set; }
    }
}