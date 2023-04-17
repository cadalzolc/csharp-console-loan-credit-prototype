namespace FinalProject;

public class Loan
{
    public string ID { get; set; } = "";
    public string Agent { get; set; } = "";
    public string AgentID { get; set; } = "";
    public string CustomerID { get; set; } = "";
    public string Customer { get; set; } = "";
    public string ItemID { get; set; } = "";
    public string Item { get; set; } = "";
    public double Price { get; set; } = 0;
    public int Installment { get; set; } = 0;
    public double Balance { get; set; } = 0;
    public double Amortization { get; set; } = 0;
    public IEnumerable<LoanPayment> Payments { get; set; } = new List<LoanPayment>();
}

public class LoanPayment
{
    public string ID { get; set; } = "";
    public string Date { get; set; } = "";
    public string PaymentType { get; set; } = "";
    public double Amount { get; set; } = 0;
}