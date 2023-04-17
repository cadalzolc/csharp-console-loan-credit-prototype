namespace FinalProject;

public class ViewOwner
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
            Console.WriteLine("Welcome to Owner Page");

            Console.WriteLine();
            Console.WriteLine("[1] Agents");
            Console.WriteLine("[2] Items");
            Console.WriteLine("[3] Sales");
            Console.WriteLine("[4] Logout");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1":
                    AgentBoard();
                    break;
                case "2":
                    ItemsBoard();
                    break;
                case "3":
                    SalesBoard();
                    break;
                case "4":
                    Console.WriteLine("Logging out...");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }


        } while (choice != "4");

        Console.WriteLine();
    }

    #region " Board - Agent "

    private void AgentBoard()
    {
        string? choice;

        do
        {
            Console.WriteLine();
            Console.WriteLine("Agent Master List");

            Console.WriteLine("***********************");
            Console.WriteLine("List of agent");

            var Users = LoginController.GetUsers().Where(r => r.Username != "admin");
            var Counter = 1;

            Console.WriteLine("#   | Username   | Fullname      | Password");

            foreach (var Row in Users)
            {
                Console.WriteLine($"{Counter}.  | {Row.Username}    | {Row.Name}        | {Row.Password}");
                Counter++;
            }

            Console.WriteLine("***********************");

            Console.WriteLine();
            Console.WriteLine("[1] Add");
            Console.WriteLine("[2] Remove");
            Console.WriteLine("[3] Exit Board");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1": AgentAdd(); break;
                case "2": AgentRemove(); break;
                case "3":
                    Console.WriteLine("Exiting agent board...");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }


        } while (choice != "3");

        Console.WriteLine();
    }

    private void AgentAdd()
    {

        Console.WriteLine();

        //Agent Fullname =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.WriteLine("Agent account creation");
        Console.Write("Enter name: ");

        var name = Console.ReadLine() ?? "";

        if (name == "")
        {
            Console.WriteLine("No name provided. Try again.");
            return;
        }

        var IsNameExists = LoginController.GetUsers().Any(r => r.Name.Equals(name));

        if (IsNameExists)
        {
            Console.WriteLine("Name is already in used. Try again.");
            return;
        }

        //Agent Username =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.Write("Enter username: ");

        var username = Console.ReadLine() ?? "";

        if (username == "")
        {
            Console.WriteLine("No username provided. Try again.");
            return;
        }

        var IsUsernameExists = LoginController.GetUsers().Any(r => r.Username.Equals(username));

        if (IsUsernameExists)
        {
            Console.WriteLine("Username is already in used. Try again.");
            return;
        }

        //Agent Password =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.Write("Enter password: ");

        var password = Console.ReadLine() ?? "";

        if (password == "")
        {
            Console.WriteLine("No password provided. Try again.");
            return;
        }

        var newUser = new User
        {
            ID = Guid.NewGuid().ToString(),
            Name = name,
            Username = username,
            Password = password
        };

        LoginController.UserAdd(newUser);

        Console.WriteLine("Agent is successfully created");
        Console.WriteLine();
    }

    private void AgentRemove()
    {
        Console.WriteLine();
        Console.Write("Enter username to remove: ");

        var username = Console.ReadLine() ?? "";

        if (username == "owner")
        {
            Console.WriteLine("Sorry you cant remove the owner account.");
            return;
        }

        var user = LoginController.GetUsers().Where(r => r.Username.Equals(username)).SingleOrDefault();

        if (user == null)
        {
            Console.WriteLine("No agent is found");
            return;
        }

        LoginController.UserRemove(user);

        Console.WriteLine("Agent is successfully removed");
        Console.WriteLine();
    }

    #endregion

    #region " Board - Items "

    private void ItemsBoard()
    {
        string? choice;

        do
        {
            Console.WriteLine();
            Console.WriteLine("Items Master List");

            Console.WriteLine("***********************");

            var Items = ItemController.GetItems();
            var Counter = 1;

            Console.WriteLine("#| Id | Name | Price | Stock | Installment ");

            foreach (var Row in Items)
            {
                Console.WriteLine($"{Counter}. | {Row.ID} | {Row.Name} | {Row.Price:P ###,##0.00} | {Row.Stock} | {Row.Installment} ");
                Counter++;
            }

            Console.WriteLine("***********************");

            Console.WriteLine();
            Console.WriteLine("[1] Add");
            Console.WriteLine("[2] Update");
            Console.WriteLine("[3] Remove");
            Console.WriteLine("[4] Exit Items Board");

            Console.Write("Enter choice: ");

            choice = Console.ReadLine() ?? "";

            switch (choice.ToUpper())
            {
                case "1": ItemAdd(); break;
                case "2": ItemUpdate(); break;
                case "3": ItemRemove(); break;
                case "4":
                    Console.WriteLine("Exiting items board...");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }


        } while (choice != "4");

        Console.WriteLine();
    }

    private void ItemAdd()
    {
        Console.WriteLine();

        //Item ninfo =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.WriteLine("Item creation");
        Console.Write("Item name: ");

        var name = Console.ReadLine() ?? "";

        if (name == "")
        {
            Console.WriteLine("No name provided. Try again.");
            return;
        }

        var IsNameExists = ItemController.GetItems().Any(r => r.Name.Equals(name));

        if (IsNameExists)
        {
            Console.WriteLine("Item name is already in used. Try again.");
            return;
        }

        //Item price =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.Write("Enter price: ");

        var strPice = Console.ReadLine() ?? "";

        if (strPice == "")
        {
            Console.WriteLine("No price provided. Try again.");
            return;
        }

        var IsPrice = double.TryParse(strPice, out double price);

        if (IsPrice == false)
        {
            Console.WriteLine("Item price is invalid. Try again.");
            return;
        }

        if (price <= 0 || price >= 1000000)
        {
            Console.WriteLine("Item price is not allowed, less than 0 and greater than 1000000. Try again.");
            return;
        }

        //Item stock =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.Write("Enter stock: ");

        var strStock = Console.ReadLine() ?? "";

        if (strStock == "")
        {
            Console.WriteLine("No stock provided. Try again.");
            return;
        }

        var IsStock = int.TryParse(strStock, out int stock);

        if (IsStock == false)
        {
            Console.WriteLine("Item stock is invalid. Try again.");
            return;
        }

        if (stock <= 0 || stock >= 1000)
        {
            Console.WriteLine("Item stock is not allowed, less than 0 and greater than 100. Try again.");
            return;
        }

        //Item installment =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.Write("Enter installment: ");

        var strInstallment = Console.ReadLine() ?? "";

        if (strStock == "")
        {
            Console.WriteLine("No installment provided. Try again.");
            return;
        }

        var IsInstallment = int.TryParse(strInstallment, out int installment);

        if (IsInstallment == false)
        {
            Console.WriteLine("Item installment is invalid. Try again.");
            return;
        }

        if (installment <= 0 || installment >= 25)
        {
            Console.WriteLine("Item installment is not allowed, less than 0 and greater than 24. Try again.");
            return;
        }

        var newData = new Item
        {
            ID = Guid.NewGuid().ToString(),
            Name = name,
            Price = price,
            Stock = stock,
            Installment = installment
        };

        ItemController.ItemAdd(newData);

        Console.WriteLine("Item is successfully created");
        Console.WriteLine();
    }

    private void ItemUpdate()
    {
        Console.WriteLine();

        //Item info =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.WriteLine("Item update");
        Console.Write("Input item id: ");

        var id = Console.ReadLine() ?? "";

        if (id == "")
        {
            Console.WriteLine("No id provided. Try again.");
            return;
        }

        var itemInfo = ItemController.GetItems().Where(r => r.ID.Equals(id)).SingleOrDefault();

        if (itemInfo == null)
        {
            Console.WriteLine("No item found. Try again.");
            return;
        }


        //Item stock =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.Write("Update stock: ");

        var strStock = Console.ReadLine() ?? "";

        if (strStock == "")
        {
            Console.WriteLine("No stock provided. Try again.");
            return;
        }

        var IsStock = int.TryParse(strStock, out int stock);

        if (IsStock == false)
        {
            Console.WriteLine("Item stock is invalid. Try again.");
            return;
        }

        if (stock < 0 || stock >= 1000)
        {
            Console.WriteLine("Item stock is not allowed, less than 0 and greater than 100. Try again.");
            return;
        }

        itemInfo.Stock = stock;

        ItemController.ItemUpdate(itemInfo);

        Console.WriteLine("Item is successfully updated");
        Console.WriteLine();

    }

    private void ItemRemove()
    {
        Console.WriteLine();

        //Item info =>>>>>>>>>>>>>>>>>>>>>>>>>
        Console.WriteLine("Item removal");
        Console.Write("Input item id: ");

        var id = Console.ReadLine() ?? "";

        if (id == "")
        {
            Console.WriteLine("No id provided. Try again.");
            return;
        }

        var itemInfo = ItemController.GetItems().Where(r => r.ID.Equals(id)).SingleOrDefault();

        if (itemInfo == null)
        {
            Console.WriteLine("No item found. Try again.");
            return;
        }

        ItemController.ItemDelete(itemInfo);

        Console.WriteLine("Item is successfully removed");
        Console.WriteLine();
    }

    #endregion

    #region " Board - Sales "

    private void SalesBoard()
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------------------");
        Console.WriteLine("Sales Report");

        var Lst = LoanController.GetLoans();
        var Counter = 1;

        Console.WriteLine("--------------------------------------");
        Console.WriteLine("# | Customer | Item | Amount");
        foreach (var Row in Lst)
        {
            Console.WriteLine("{0} | {1} | {2} | {3}", Counter, Row.Customer, Row.Item, Row.Price);
            Counter++;
        }
        Console.WriteLine("--------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Total Sales: {0}", Lst.Sum(r => r.Price).ToString("P ###, ##0.0"));
        Console.WriteLine("######################");
        Console.WriteLine();
    }


    #endregion

}