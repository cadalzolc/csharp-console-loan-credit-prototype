namespace FinalProject;

public class User
{
    public string ID { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Name { get; set; } = "";

    public User() { }
    public User(string id, string username, string password, string name)
    {
        ID = id;
        Username = username;
        Password = password;
        Name = name;
    }

}