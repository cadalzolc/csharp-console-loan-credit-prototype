namespace FinalProject;

public class ViewAgent
{
    public User CurrentUser { get; set; } = new User();

    public void Initialize()
    {

        Console.Clear();

        string? choice;

        do
        {
            Console.WriteLine();
            Console.WriteLine("*************************************");
            Console.WriteLine("Welcome to Agent Page [{0}]", CurrentUser.Name);

            Console.WriteLine();
            Console.WriteLine("[1] Serve");
            Console.WriteLine("[2] History");
            Console.WriteLine("[3] Logout");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1":
                    ServeCustomer();
                    break;
                case "2":
                    History();
                    break;
                case "3":
                    Console.WriteLine("Logging out...");
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

    private void ServeCustomer()
    {

        Console.WriteLine();
        Console.WriteLine("Serve Customer");

        var Customer = QueueController.QueueServe();

        string? choice;

        do
        {
            Console.WriteLine($"Now serving: {Customer.Name}");

            var Items = ItemController.GetItems().Where(r => r.Stock > 0);
            var Counter = 1;

            Console.WriteLine("# | ID | Item | Price | Installment");

            foreach (var Row in Items)
            {
                Console.WriteLine($"{Counter}.| {Row.ID} | {Row.Name} | {Row.Price} | {Row.Installment}");
                Counter++;
            }

            Console.WriteLine("***********************");

            Console.WriteLine();
            Console.WriteLine("[1] Select Item");
            Console.WriteLine("[2] Exit Serving");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1": 
                    choice = ProcessItem(Customer); 
                    break;
                case "2":
                    Console.WriteLine("Exiting agent board...");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        } while (choice != "2");

        Console.WriteLine();

        if (choice != "2")
        {
            Console.Write("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }

    private string ProcessItem(Customer CurrentCustomer)
    {
        Console.WriteLine();
        Console.Write("Enter item id: ");

        var id = Console.ReadLine() ?? "";

        if (id == "")
        {
            Console.WriteLine();
            Console.WriteLine("No item id provided");
            return "";
        }

        var itemInfo = ItemController.GetItems().Where(r => r.ID.Equals(id)).SingleOrDefault();

        if (itemInfo == null)
        {
            Console.WriteLine();
            Console.WriteLine("Item info not found;");
            return "";
        }

        Console.WriteLine("++++++++++++++++++++");
        Console.WriteLine("Item Information");
        Console.WriteLine("Item: {0}", itemInfo.Name);
        Console.WriteLine("Price: {0}", itemInfo.Price.ToString("P ###,##0.00"));
        Console.WriteLine("Installment: {0}", itemInfo.Installment);

        Console.WriteLine();
        Console.WriteLine("Downpayment");
        Console.Write("Enter amount: ");

        var strDownpayment = Console.ReadLine() ?? "";

        if (strDownpayment == "")
        {
            Console.WriteLine("No downpayment provided. Try again.");
            return "";
        }

        var IsPrice = double.TryParse(strDownpayment, out double downpayment);

        if (IsPrice == false)
        {
            Console.WriteLine("Downpayment is invalid. Try again.");
            return "";
        }

        if (downpayment < 0 || downpayment > itemInfo.Price)
        {
            Console.WriteLine("Downpayment is not allowed, less than 0 or greater than item price. Try again.");
            return "";
        }

        var Balance = itemInfo.Price - downpayment;

        var newPayment = new LoanPayment
        {
            ID = Guid.NewGuid().ToString(),
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            PaymentType = "DOWNPAYMENT",
            Amount = downpayment
        };

        var lstPayment = new List<LoanPayment>
        {
            newPayment
        };

        var newData = new Loan
        {
            ID = Guid.NewGuid().ToString(),
            AgentID = CurrentUser.ID,
            Agent = CurrentUser.Name,
            CustomerID = CurrentCustomer.ID,
            Customer = CurrentCustomer.Name,
            ItemID = itemInfo.ID,
            Item = itemInfo.Name,
            Price = itemInfo.Price,
            Installment = itemInfo.Installment,
            Payments = lstPayment.AsEnumerable(),
            Amortization = (Balance / itemInfo.Installment)
        };

        itemInfo.Stock--;

        ItemController.ItemUpdate(itemInfo);
        LoanController.LoanAdd(newData);


        Console.WriteLine();
        Console.WriteLine("++++++++++++++++++++");
        Console.WriteLine("Loan is Successful");
        Console.WriteLine("Loan #: {0}", newData.ID);
        Console.WriteLine("Installment: {0}", newData.Installment);
        Console.WriteLine("Monthly Amortization: {0}", newData.Amortization.ToString("P ###, ##0.00"));
       

        return "2";
    }

    private void History()
    {
        Console.WriteLine();
        Console.WriteLine("Serve Customer");

        var Lst = LoanController.GetLoans().Where(r => r.AgentID.Equals(CurrentUser.ID));

        Console.WriteLine("Server History");

        foreach(var Row in Lst)
        {
            Console.WriteLine("++++++++++++++++++");
            Console.WriteLine("Name: {0}", Row.Customer);
            Console.WriteLine("Item: {0}", Row.Item);
            Console.WriteLine("Amount: {0}", Row.Price.ToString("P ###, ##0.0"));
        }

        Console.WriteLine();
        Console.WriteLine("Total Sales: {0}", Lst.Sum(r => r.Price).ToString("P ###, ##0.0"));
        Console.WriteLine("######################");
        Console.WriteLine();
    }

}
