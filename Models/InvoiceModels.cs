using System.Collections.Generic;
using System.Linq;

namespace InvoiceApp.Models
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }

    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new();
        public double Total => Items.Sum(i => i.Price);
    }
}
