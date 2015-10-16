using System.ComponentModel;

namespace Quotes.Models
{
    public class Category
    {
        public int ID { get; set; }
        [DisplayName("Category")]
        public string Name { get; set;}


    }
}