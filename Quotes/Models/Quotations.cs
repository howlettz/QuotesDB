using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class Quotations
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public DateTime DateAdded { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

    }
}