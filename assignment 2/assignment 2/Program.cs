using System;
using System.Collections.Generic;
using System.IO;

class Item
{
    public int Id;
    public string Name;
    public int Quantity;
    public double Price;
    public double Total;
}
class Program
{
    static List<Item> items = new List<Item>();
    const double VAT = 0.16;

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n----MENU----");
            Console.WriteLine("1.load file");
            Console.WriteLine("2.display items");
            Console.WriteLine("3.calculate totals");
            Console.WriteLine("4.print receipt");
            Console.WriteLine("5.save to file");
            Console.WriteLine("6.exit");
            Console.Write("enter choice:");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {

                case 1:
                    LoadFile();
                    break;
                case 2:
                    DisplayItems();
                    break;
                case 3:
                    CalculateTotals(ref items);
                    break;
                case 4:
                    PrintReceipt();
                    break;
                case 5:
                    SaveToFile();
                    break;
            }
        } while (choice != 6);
}
    // load file using stream reader
    static void LoadFile()
    {
        Console.Write("enter file path:");
        string path= Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found!");
            return;
        }
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    Item item = new Item();
                    item.Id = int.Parse(parts[0]);
                    item.Name = parts[1];
                    item.Quantity = int.Parse(parts[2]);
                    item.Price = double.Parse(parts[3]);

                    items.Add(item);
                }
            
            }
            Console.WriteLine("file loaded successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }
    }
    //display items
    static void DisplayItems()
    {
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} - Qty: {item.Quantity}, Price: {item.Price}");
        }
    }
    //calculate ttl
    static void CalculateTotals(ref List<Item> items)
    {
        foreach (var item in items)
        {
            item.Total = item.Quantity * item.Price;
        }

        Console.WriteLine("Totals calculated!");
    }
    //print receipt
    static void PrintReceipt()
    {
        double subtotal = 0;

        Console.WriteLine("\n--- RECEIPT ---");

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} | {item.Quantity} x {item.Price} = {item.Total}");
            subtotal += item.Total;
        }

        double tax = subtotal * VAT;
        double grandTotal = subtotal + tax;

        Console.WriteLine("---------------------------");
        Console.WriteLine($"Subtotal: {subtotal}");
        Console.WriteLine($"Tax (16%): {tax}");
        Console.WriteLine($"Grand Total: {grandTotal}");
    }
    //saving ile using stream writer
    static void SaveToFile()
    {
        string path = "output.txt";

        try
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                double subtotal = 0;

                foreach (var item in items)
                {
                    writer.WriteLine($"{item.Name},{item.Quantity},{item.Price},{item.Total}");
                    subtotal += item.Total;
                }

                double tax = subtotal * VAT;
                double grandTotal = subtotal + tax;

                writer.WriteLine($"Subtotal: {subtotal}");
                writer.WriteLine($"Tax: {tax}");
                writer.WriteLine($"Grand Total: {grandTotal}");
            }

            Console.WriteLine("Data saved to file!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing file: " + ex.Message);
        }
    }
}