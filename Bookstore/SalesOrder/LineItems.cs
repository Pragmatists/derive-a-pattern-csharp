using System.Collections.Generic;
using System.Linq;

namespace Bookstore.SalesOrder
{
    public class LineItems
    {
        private readonly List<LineItem> lineItems = new List<LineItem>();

        public void Add(LineItem lineItem)
        {
            lineItems.Add(lineItem);
        }

        public int TotalPrice()
        {
            return lineItems.Select(x => x.Price).Sum();
        }

        public bool OnlyBooks()
        {
            return lineItems.TrueForAll(x => x.IsBook);
        }

        public double TotalWeight()
        {
            return lineItems.Select(x => x.Weight).Sum();
        }
    }
}