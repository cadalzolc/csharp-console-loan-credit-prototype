namespace FinalProject;

public class ViewKiosk
{

    public void Initialize()
    {
        string? choice;

        do
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("*********************************");
            Console.WriteLine("Welcome to payment Kiosk.");

            Console.WriteLine("[1] Loan Payment");
            Console.WriteLine("[2] Exit Kiosk");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1": ProcessPayment(); break;
                case "2":
                    Console.WriteLine("Exiting Kiosk...");
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

    private void ProcessPayment()
    {
        Console.WriteLine();
        Console.Write("Enter loan no: ");
        var no = Console.ReadLine() ?? "";

        if (no == "")
        {
            Console.WriteLine();
            Console.WriteLine("No load number provided. Try again");
            return;
        }

        var loan = LoanController.GetLoans().Where(r => r.ID.Equals(no)).SingleOrDefault();

        if (loan == null)
        {
            Console.WriteLine();
            Console.WriteLine("Loan account is not found. Try again");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("******************");
        Console.WriteLine("Loan Information");
        Console.WriteLine("Loan No: {0}", loan.ID);
        Console.WriteLine("Loan Balance: {0}", loan.Balance);
        Console.WriteLine("Loan Amortization: {0}", loan.Amortization);

        if (loan.Balance <= 0)
        {
            Console.WriteLine();
            Console.WriteLine("Loan is fully paid");
            return;
        }

        Console.WriteLine();
        Console.Write("Enter payment :");

        var strPayment = Console.ReadLine() ?? "";

        if (strPayment == "")
        {
            Console.WriteLine("No payment provided. Try again.");
            return;
        }

        var IsPrice = double.TryParse(strPayment, out double payment);

        if (IsPrice == false)
        {
            Console.WriteLine("Payment is invalid. Try again.");
            return;
        }

        if (payment <= 0 || payment > loan.Balance)
        {
            Console.WriteLine("Payment is not allowed, less than 0 or greater than balance. Try again.");
            return;
        }

        var Balance = loan.Balance - payment;
        var newPayment = new LoanPayment
        {
            ID = Guid.NewGuid().ToString(),
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            PaymentType = "PAYMENT",
            Amount = payment
        };

        var lst = loan.Payments.ToList();

        lst.Add(newPayment);

        loan.Balance = Balance;
        loan.Payments = lst.AsEnumerable();


        LoanController.LoanUpdate(loan);

        if (Balance <= 0)
        {
            Console.WriteLine();
            Console.WriteLine("Payment is fully paid. Thank you");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Thank you for you payment");
        Console.WriteLine("Your remaninng balance is: {0}", loan.Balance);
        Console.WriteLine();
    }

}