namespace FinalProject;

public class QueueController
{

    public static void QueueAdd(Customer data)
    {
        JsonHelper.QueueAdd(data);
    }

    public static Customer QueueServe()
    {
        return JsonHelper.QueueServe();
    }

    public static Queue<Customer> GetQueues()
    {
        return JsonHelper.GetQueuesFromLocal();
    }

}