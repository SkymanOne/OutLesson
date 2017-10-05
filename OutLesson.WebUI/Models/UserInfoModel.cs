using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.WebUI.Models
{
    public class UserInfoModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Post> UserPosts { get; set; }
    }
}