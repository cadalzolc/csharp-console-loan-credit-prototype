using FinalProject;


JsonHelper.Init();

var Exit = "";

do
{

    Console.WriteLine("Welcome");
    Console.WriteLine();
    Console.WriteLine("Please select action from the selection below");
    Console.WriteLine("[1] Queue Customer");
    Console.WriteLine("[2] Payment Kiosk");
    Console.WriteLine("[3] Login");
    Console.WriteLine("[x] Exit Program");

    Console.Write("Enter action: ");

    var action = Console.ReadLine() ?? "";

    switch (action.ToUpper()) {
        case "1":
            var mgr = new ViewQueing();
            mgr.Initialize();
            break;
        case "2":
            var ksk = new ViewKiosk();
            ksk.Initialize();
            break;
        case "3":
            var lgn = new ViewLogin();
            lgn.Initialize();
            break;
        case "X":
            Exit = "X";
            break;
        default:
            Console.WriteLine("Invalid action");
            break;
    }

    if (Exit != "X")
    {
        Console.WriteLine();
        Console.Write("Press enter to continue");
        Console.ReadLine();
        Console.Clear();
    }

} while (Exit != "X");


Console.WriteLine("Program Terminated");