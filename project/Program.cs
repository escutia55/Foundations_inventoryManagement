using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

class ProductManager
{
    private List<Product> inventory = new List<Product>();

    public void DisplayMenu()
    {
        Console.WriteLine("\nInventory Management Menu:");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Remove Product");
        Console.WriteLine("3. Update Existing Product Stock");
        Console.WriteLine("4. List Products");
        Console.WriteLine("5. Exit");
        Console.Write("Select an option: ");
    }

    public void AddProduct()
    {
        Console.Write("Enter product name: ");
        var name = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        if (inventory.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Product already exists.");
            return;
        }

        Console.Write("Enter price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        inventory.Add(new Product { Name = name, Price = price, Quantity = quantity });
        Console.WriteLine("Product added.");
    }

    public void RemoveProduct()
    {
        Console.Write("Enter product name to remove: ");
        var name = Console.ReadLine()?.Trim();
        var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter quantity to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        if (product.Quantity < quantity)
        {
            Console.WriteLine("Insufficient stock.");
            return;
        }

        product.Quantity -= quantity;
        if (product.Quantity == 0)
            inventory.Remove(product);

        Console.WriteLine("Product stock updated.");
    }

    public void UpdateProduct()
    {
        Console.Write("Enter product name to update: ");
        var name = Console.ReadLine()?.Trim();
        var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter new quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        product.Quantity += quantity;
        Console.WriteLine("Product quantity updated.");
    }

    public void ListProducts()
    {
        if (!inventory.Any())
        {
            Console.WriteLine("No products in inventory.");
            return;
        }

        Console.WriteLine("\n{0,-20} {1,10} {2,10}", "Name", "Price", "Quantity");
        Console.WriteLine(new string('-', 42));
        foreach (var p in inventory)
        {
            Console.WriteLine("{0,-20} {1,10:C} {2,10}", p.Name, p.Price, p.Quantity);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var manager = new ProductManager();

        while (true)
        {
            manager.DisplayMenu();
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    manager.AddProduct();
                    break;
                case "2":
                    manager.RemoveProduct();
                    break;
                case "3":
                    manager.UpdateProduct();
                    break;
                case "4":
                    manager.ListProducts();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}