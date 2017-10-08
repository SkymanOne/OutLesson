using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.WebUI.Models
{
    public class PostViewModel
    {
        public string Title { get; set; }

        public int Id { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public DateTime Time { get; set; }
        public virtual ApplicationUser Autor { get; set; }

        public string ShortUrl { get; set; }
    }
}