// See https://aka.ms/new-console-template for more information


using ClassLibraryOne;
using ExtensionMethodsDemo;

// Create object
Product product = new Product() { ProductCost = 1000, DiscountPercentage = 10 };

// Call the extension method
Console.WriteLine(product.GetDiscount());

