using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class Quotations
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Must enter a quote!")]
        public string Quote { get; set; }

        [Required(ErrorMessage ="Must enter a quote author!")]
        public string Author { get; set; }

        public DateTime DateAdded { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

    }
}