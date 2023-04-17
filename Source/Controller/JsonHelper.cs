using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace FinalProject;

public class JsonHelper
{
    private static string PathQueues => Path.Combine(AppContext.BaseDirectory, "json", "queues.json");
    private static string PathUsers => Path.Combine(AppContext.BaseDirectory, "json", "users.json");
    private static string PathItems => Path.Combine(AppContext.BaseDirectory, "json", "items.json");
    private static string PathLoans => Path.Combine(AppContext.BaseDirectory, "json", "loans.json");

    public static void Init()
    {
        var DirJson = Path.Combine(AppContext.BaseDirectory, "json");

        if (!Directory.Exists(DirJson)) {
            Directory.CreateDirectory(DirJson);
        }

        if (!File.Exists(PathQueues))
        {
            var lstQueue = new Queue<Customer>();
            var dataQueue = JsonSerializer.Serialize(lstQueue);
            File.WriteAllText(PathQueues, dataQueue);
        }

        if (!File.Exists(PathUsers))
        {
            var lstUser = new List<User> { new User(Guid.NewGuid().ToString(), "owner", "888888", "Owner") };
            var dataUser = JsonSerializer.Serialize(lstUser);
            File.WriteAllText(PathUsers, dataUser);
        }

        if (!File.Exists(PathItems))
        {
            var lstItem = new List<Item>();
            var dataItem = JsonSerializer.Serialize(lstItem);
            File.WriteAllText(PathItems, dataItem);
        }

        if (!File.Exists(PathLoans))
        {
            var lstLoan = new List<Loan>();
            var dataLoan = JsonSerializer.Serialize(lstLoan);
            File.WriteAllText(PathLoans, dataLoan);
        }
    }

    #region " Queues"

    public static Queue<Customer> GetQueuesFromLocal()
    {

        if (!File.Exists(PathQueues))
        {
            var lst = new Queue<Customer>();
            var data = JsonSerializer.Serialize(lst);

            File.WriteAllText(PathQueues, data);
        }

        var text = File.ReadAllText(PathQueues) ?? "";

        if (text == "") return new Queue<Customer>();

        var curList = JsonSerializer.Deserialize<Queue<Customer>>(text);

        return curList ?? new Queue<Customer>();
    }

    public static void QueueAdd(Customer userInfo)
    {
        var lst = GetQueuesFromLocal();

        lst.Enqueue(userInfo);

        if (File.Exists(PathQueues)) File.Delete(PathQueues);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathQueues, data);
    }

    public static Customer QueueServe()
    {
        var lst = GetQueuesFromLocal();
        var  info = lst.Dequeue();

        if (File.Exists(PathQueues)) File.Delete(PathQueues);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathQueues, data);

        return info;

    }

    #endregion

    #region " Users "

    public static List<User> GetUsersFromLocal()
    {
        if (!File.Exists(PathUsers))
        {
            var lst = new List<User> { new User(Guid.NewGuid().ToString(), "owner", "888888", "Owner") };
            var data = JsonSerializer.Serialize(lst);

            File.WriteAllText(PathUsers, data);
        }

        var text = File.ReadAllText(PathUsers) ?? "";

        if (text == "") return new List<User>();

        var curList = JsonSerializer.Deserialize<List<User>>(text);

        return curList ?? new List<User>();
    }

    public static void UserAdd(User userInfo)
    {
        var lst = GetUsersFromLocal();

        lst.Add(userInfo);

        if (File.Exists(PathUsers)) File.Delete(PathUsers);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathUsers, data);
    }

    public static void UserDelete(User info)
    {
        var lst = GetUsersFromLocal();

        var i = lst.FindIndex(r => r.ID.Equals(info.ID));

        if (i == -1) return;

        lst.RemoveAt(i);

        if (File.Exists(PathUsers)) File.Delete(PathUsers);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathUsers, data);
    }

    #endregion

    #region " Items "

    public static List<Item> GetItemsFromLocal()
    {
        if (!File.Exists(PathItems))
        {
            var lst = new List<Item>();
            var data = JsonSerializer.Serialize(lst);

            File.WriteAllText(PathItems, data);
        }

        var text = File.ReadAllText(PathItems) ?? "";

        if (text == "") return new List<Item>();

        var curList = JsonSerializer.Deserialize<List<Item>>(text);

        return curList ?? new List<Item>();
    }

    public static void ItemAdd(Item info)
    {
        var lst = GetItemsFromLocal();

        lst.Add(info);

        if (File.Exists(PathItems)) File.Delete(PathItems);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathItems, data);
    }

    public static bool ItemUpdate(Item info)
    {
        var lst = GetItemsFromLocal();
        var i = lst.FindIndex(r => r.ID.Equals(info.ID));

        if (i == -1) return false;

        lst[i].Name = info.Name;
        lst[i].Stock = info.Stock;
        lst[i].Price = info.Price;
        lst[i].Installment = info.Installment;

        if (File.Exists(PathItems)) File.Delete(PathItems);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathItems, data);

        return true;
    }

    public static void ItemRemove(Item info)
    {
        var lst = GetItemsFromLocal();
        var i = lst.FindIndex(r => r.ID.Equals(info.ID));

        if (i == -1) return;

        lst.RemoveAt(i);

        if (File.Exists(PathItems)) File.Delete(PathItems);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathItems, data);
    }

    #endregion

    #region " Loans "

    public static List<Loan> GetLoansFromLocal()
    {
        if (!File.Exists(PathLoans))
        {
            var lst = new List<Loan>();
            var data = JsonSerializer.Serialize(lst);

            File.WriteAllText(PathLoans, data);
        }

        var text = File.ReadAllText(PathLoans) ?? "";

        if (text == "") return new List<Loan>();

        var curList = JsonSerializer.Deserialize<List<Loan>>(text);

        return curList ?? new List<Loan>();
    }

    public static void LoanAdd(Loan info)
    {
        var lst = GetLoansFromLocal();

        lst.Add(info);

        if (File.Exists(PathLoans)) File.Delete(PathLoans);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathLoans, data);
    }

    public static bool LoanUpdate(Loan info)
    {
        var lst = GetLoansFromLocal();
        var i = lst.FindIndex(r => r.ID.Equals(info.ID));

        if (i == -1) return false;

        lst[i].Balance = info.Balance;
        lst[i].Payments = info.Payments;

        if (File.Exists(PathLoans)) File.Delete(PathLoans);

        var data = JsonSerializer.Serialize(lst);

        File.WriteAllText(PathLoans, data);

        return true;
    }

    #endregion

}