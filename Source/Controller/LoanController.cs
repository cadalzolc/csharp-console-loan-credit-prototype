namespace FinalProject;

public class LoanController
{

    public static void LoanAdd(Loan data)
    {
        JsonHelper.LoanAdd(data);
    }

    public static bool ItemUpdate(Loan data)
    {
        return JsonHelper.LoanUpdate(data);
    }

    public static List<Loan> GetLoans()
    {
        return JsonHelper.GetLoansFromLocal();
    }

}