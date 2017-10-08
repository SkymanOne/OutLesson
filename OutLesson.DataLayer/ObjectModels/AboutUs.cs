using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OutLesson.DataLayer.ObjectModels
{
    public class AboutUs
    {
        public int Id { get; set; }
        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime TimeChange { get; set; }
    }
}