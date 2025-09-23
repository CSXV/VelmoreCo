using DataAccessLayer;

namespace BusinessLayer;

public class bl_Orders
{
    public enum enMode
    {
        addNew = 0,
        Update = 1,
    };

    public enMode Mode = enMode.addNew;

    public OrderDTO ODTO
    {
        get
        {
            return (
                new OrderDTO(
                    this.ID,
                    this.UserID,
                    this.status,
                    this.ShippingAddress,
                    this.createDate,
                    this.updateDate
                )
            );
        }
    }

    public int ID { get; set; }
    public int UserID { get; set; }
    public string status { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }

    public bl_Orders(OrderDTO ODTO, enMode cMode = enMode.addNew)
    {
        this.ID = ODTO.ID;
        this.UserID = ODTO.UserID;
        this.status = ODTO.status;
        this.ShippingAddress = ODTO.ShippingAddress;
        this.createDate = ODTO.createDate;
        this.updateDate = ODTO.updateDate;

        this.Mode = cMode;
    }

    public static List<OrderDTO> GetAllOrdersForProductID(int id)
    {
        return da_OrderData.GetAllOrdersForProductID(id);
    }

    public static List<OrderDTO> GetAllOrdersForUserID(int id)
    {
        return da_OrderData.GetAllOrdersForUserID(id);
    }

    public static List<OrderDTO> Get_All_ProductID_Orders_For_UserID(int id, int UserID)
    {
        return da_OrderData.Get_All_ProductID_Orders_For_UserID(id, UserID);
    }

    public static bl_Orders Find(int id)
    {
        OrderDTO ODTO = da_OrderData.GetOrderByID(id);

        if (ODTO != null)
        {
            return new bl_Orders(ODTO, enMode.Update);
        }
        else
        {
            return null;
        }
    }

    private bool _AddNewOrder()
    {
        this.ID = da_OrderData.AddNewOrder(ODTO);
        return (this.ID != -1);
    }

    private bool _UpdateOrderByID()
    {
        return da_OrderData.UpdateOrderByID(ODTO);
    }

    public static bool DeleteOrder(int id)
    {
        return da_OrderData.DeleteOrderByID(id);
    }

    public bool Save()
    {
        switch (Mode)
        {
            case enMode.addNew:
                if (_AddNewOrder())
                {
                    Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            case enMode.Update:
                return _UpdateOrderByID();
        }

        return false;
    }
}
