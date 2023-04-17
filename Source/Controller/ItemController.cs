namespace FinalProject;

public class ItemController
{

    public static void ItemAdd(Item data)
    {
        JsonHelper.ItemAdd(data);
    }

    public static bool ItemUpdate(Item data)
    {
        return JsonHelper.ItemUpdate(data);
    }

    public static void ItemDelete(Item data)
    {
        JsonHelper.ItemRemove(data);
    }

    public static List<Item> GetItems()
    {
        return JsonHelper.GetItemsFromLocal();
    }

}