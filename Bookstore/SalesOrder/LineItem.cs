namespace Bookstore.SalesOrder
{
    public class LineItem
    {
        public int Price { get; set; }
        public double Weight { get; set; }
        public ProductType Type { get; set; }
        public bool IsBook { get { return Type.Equals(ProductType.Book); } }
    }
}