using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TedBank.Models
{
    public class TedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Topic { get; set; }
        public string Uploaded { get; set; }
        public string Speaker { get; set; }
    }
}
