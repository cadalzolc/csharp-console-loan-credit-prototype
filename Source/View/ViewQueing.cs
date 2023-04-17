namespace FinalProject;

public class ViewQueing
{

    public void Initialize()
    {

        Console.Clear();

        string? choice;

        do
        {


            Console.WriteLine();
            Console.WriteLine("*********************************");
            Console.WriteLine("Welcome to customer queing.");

            Console.WriteLine("[1] Add");
            Console.WriteLine("[2] View");
            Console.WriteLine("[3] Exit queing");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1":
                    Add();
                    break;
                case "2":
                    View();
                    break;
                case "3":
                    Console.WriteLine("Exiting queueing");
                    break;
                default: 
                    Console.WriteLine("Invalid choice"); 
                    break;
            }

        } while (choice != "3");

        Console.WriteLine();

        if (choice != "3")
        {
            Console.Write("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }

    private void Add()
    {
        Console.WriteLine();
        Console.Write("Please enter customer name: ");

        var name = Console.ReadLine();

        if (name == null)
        {
            Console.WriteLine("No customer was created");
            return;
        }

        var customer = new Customer
        {
            ID = Guid.NewGuid().ToString(),
            Name = name
        };

        QueueController.QueueAdd(customer);

        Console.WriteLine("Customer was added to queue");
        Console.WriteLine();
    }

    private void View()
    {
        Console.WriteLine();
        Console.WriteLine("********************");
        Console.WriteLine("List of customers in queue");

        var Rows = QueueController.GetQueues().AsEnumerable();
        var Cnt = Rows.Count();

        Console.WriteLine();
        Console.WriteLine($"{Cnt} records.");

        if (Cnt <= 0)
        {
            Console.WriteLine();
            Console.WriteLine("********************");
            Console.WriteLine();
            return;
        }

        foreach (var Row in Rows)
        {
            Console.WriteLine();
            Console.WriteLine("+++++++++++++++++++");
            Console.WriteLine("Customer Info......");
            Console.WriteLine($"Id: {Row.ID}");
            Console.WriteLine($"Name: {Row.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("********************");
        Console.WriteLine();

    }
}