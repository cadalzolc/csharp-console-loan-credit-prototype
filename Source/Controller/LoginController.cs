using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace FinalProject;

public class LoginController
{

    public LoginResponse Login()
    {
        Console.WriteLine();
        Console.WriteLine("Enter login credentials");

        Console.Write("Username: ");
        var username = Console.ReadLine() ?? "";

        Console.Write("Password: ");

        var password = Console.ReadLine();

        Console.WriteLine();

        if (username == "")
        {
            return new LoginResponse("Invalid username");
        }

        if (password == "")
        {
            return new LoginResponse("Invalid password");
        }

        var Users = JsonHelper.GetUsersFromLocal();
        var User = Users.Where(r => r.Username == username && r.Password == r.Password).SingleOrDefault();

        if (User == null)
        {
            return new LoginResponse("No user was found.Due to invalid username or password");
        }

        return new LoginResponse()
        {
            Success = true,
            Message = "Successfully login",
            Info = User
        };

    }

    public static void UserAdd(User info)
    {
        JsonHelper.UserAdd(info);
    }

    public static void UserRemove(User info)
    {
        JsonHelper.UserDelete(info);
    }

    public static IEnumerable<User> GetUsers()
    {
        return JsonHelper.GetUsersFromLocal();
    }

}

public class LoginResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = "";
    public User Info { get; set; } = new User();

    public LoginResponse() { }

    public LoginResponse(string message)
    {
        Message = message;
    }
}