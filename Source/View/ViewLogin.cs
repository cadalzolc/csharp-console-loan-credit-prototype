namespace FinalProject;

public class ViewLogin
{
    public void Initialize()
    {

        Console.Clear();

        var lgr = new LoginController();
        var res = lgr.Login();

        if (res.Success == false)
        {
            Console.WriteLine(res.Message);
            return;
        }

        var user = res.Info;

        if (user.Username.Equals("owner"))
        {
            var VwOwner = new ViewOwner
            {
                CurrentUser = user
            };
            VwOwner.Initialize();
            return;
        }

        var VwAgent = new ViewAgent
        {
            CurrentUser = user
        };
        VwAgent.Initialize();
    }

}