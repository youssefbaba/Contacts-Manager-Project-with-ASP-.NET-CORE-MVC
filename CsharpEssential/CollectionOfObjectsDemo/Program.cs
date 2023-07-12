using ClassLibraryFour;

// Create an empty collection
List<Product> products = new List<Product>();
string? choice = string.Empty;
do
{
    Console.Write("Enter ProductID : ");
    int productID = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter ProductName : ");
    string? productName = Console.ReadLine();
    Console.Write("Enter Price : ");
    double price = Convert.ToDouble(Console.ReadLine());
    Console.Write("Enter Date Of Manufacture {yyyy-MM-dd} : ");
    DateTime dateOfManufacture = Convert.ToDateTime(Console.ReadLine());

    // Create a new object of Product class
    Product product = new Product() { ProductID = productID, ProductName = productName, Price = price, DateOfManufacture = dateOfManufacture };

    // Add Product object to the collection
    products.Add(product);

    // Ask the user to continue
    Console.WriteLine("Product Added.");
    Console.WriteLine("Do you want to continue to next product? (Yes/No)");
    choice = Console.ReadLine();

} while (choice != "no" && choice != "No" && choice != "n" && choice != "N");


Console.WriteLine("Products : ");
foreach (var product in products)
{
    Console.WriteLine($"ProductID : {product.ProductID} , ProductName : {product.ProductName} , Price : {product.Price} , DateOfManufacture : {product.DateOfManufacture.ToString("yyyy-MM-dd")}");
}
