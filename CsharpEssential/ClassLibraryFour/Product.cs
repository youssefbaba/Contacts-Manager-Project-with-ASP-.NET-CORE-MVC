namespace ClassLibraryFour
{
    /// <summary>
    /// Represents a Product in E-Commerce application
    /// </summary>
    public class Product
    {
        // Properties
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public DateTime DateOfManufacture { get; set; }
    }
}