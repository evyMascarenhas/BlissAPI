using Bliss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bliss.DTO
{
    public class Questions
    {
        public int id { get; set; }
        public string question { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public System.DateTime published_at { get; set; }

        public ICollection<Choices> questions_choices { get; set; }
    }
}