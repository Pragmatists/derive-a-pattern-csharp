using Bookstore.Common;

namespace Bookstore.SalesOrder
{
    public class SalesOrder
    {
        private readonly LineItems lineItems = new LineItems();

        public Country DeliveryCountry { get; set; }

        public int GetTotal()
        {
            return lineItems.TotalPrice()
                   +
                   ShippingCost();
        }

        public void AddItem(LineItem lineItem)
        {
            lineItems.Add(lineItem);
        }


        public int ShippingCost()
        {
            if (!DeliveryCountry.Equals(Country.Poland))
            {
                if (TotalWeight() > 10.0)
                {
                    return 70;
                }
                return 50;
            }
            if (lineItems.OnlyBooks())
            {
                if (lineItems.TotalPrice() > 200)
                {
                    return 0;
                }
                return 5;
            }
            return 15;
        }

        private double TotalWeight()
        {
            return lineItems.TotalWeight();
        }
    }
}