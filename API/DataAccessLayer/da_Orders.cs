using Npgsql;

namespace DataAccessLayer;

public class OrderDTO
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public string status { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }

    public OrderDTO(
        int ID,
        int UserID,
        string status,
        string ShippingAddress,
        DateTime createDate,
        DateTime updateDate
    )
    {
        this.ID = ID;
        this.UserID = UserID;
        this.status = status;
        this.ShippingAddress = ShippingAddress;
        this.createDate = createDate;
        this.updateDate = updateDate;
    }
}

public class da_OrderData
{
    static string _connectionString = "Host=localhost;Username=user;Database=JewelryShop";

    public static List<OrderDTO> Get_All_ProductID_Orders_For_UserID(int id, int userID)
    {
        var OrdersList = new List<OrderDTO>();

        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "SELECT o.*, i.product_id FROM orders o inner join order_items i on o.id=i.order_id WHERE product_id = @id and o.user_id =@userID"
                )
            )
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userID", userID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrdersList.Add(
                            new OrderDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("user_id")),
                                reader.GetString(reader.GetOrdinal("status")),
                                reader.GetString(reader.GetOrdinal("shipping_address")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetDateTime(reader.GetOrdinal("updated_at"))
                            )
                        );
                    }
                }
            }
        }

        return OrdersList;
    }

    public static List<OrderDTO> GetAllOrdersForUserID(int id)
    {
        var OrdersList = new List<OrderDTO>();

        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("SELECT * FROM orders WHERE user_id=@id"))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrdersList.Add(
                            new OrderDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("user_id")),
                                reader.GetString(reader.GetOrdinal("status")),
                                reader.GetString(reader.GetOrdinal("shipping_address")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetDateTime(reader.GetOrdinal("updated_at"))
                            )
                        );
                    }
                }
            }
        }

        return OrdersList;
    }

    public static List<OrderDTO> GetAllOrdersForProductID(int id)
    {
        var OrdersList = new List<OrderDTO>();

        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "SELECT o.*, i.product_id FROM orders o inner join order_items i on o.id=i.order_id WHERE product_id =@id"
                )
            )
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrdersList.Add(
                            new OrderDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("user_id")),
                                reader.GetString(reader.GetOrdinal("status")),
                                reader.GetString(reader.GetOrdinal("shipping_address")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetDateTime(reader.GetOrdinal("updated_at"))
                            )
                        );
                    }
                }
            }
        }

        return OrdersList;
    }

    public static OrderDTO GetOrderByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("SELECT * FROM orders WHERE id = @id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OrderDTO(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("user_id")),
                            reader.GetString(reader.GetOrdinal("status")),
                            reader.GetString(reader.GetOrdinal("shipping_address")),
                            reader.GetDateTime(reader.GetOrdinal("created_at")),
                            reader.GetDateTime(reader.GetOrdinal("updated_at"))
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }

    public static int AddNewOrder(OrderDTO order)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    @"INSERT INTO orders (
                          user_id, status, shipping_address, created_at
                          ) VALUES (
                            @user_id, @status, @shipping_address, @created_at
                          ) RETURNING ID;"
                )
            )
            {
                command.Parameters.AddWithValue("@user_id", order.UserID);
                command.Parameters.AddWithValue("@status", order.status);
                command.Parameters.AddWithValue("@shipping_address", order.ShippingAddress);
                command.Parameters.AddWithValue("@createdate", DateTime.UtcNow);

                var rowInserted = command.ExecuteScalar();

                return (int)rowInserted;
            }
        }
    }

    public static bool UpdateOrderByID(OrderDTO order)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "UPDATE orders SET user_id=@user_id, status=@status, shipping_address=@shipping_address, created_at=@createdate, updated_at=@updateDate WHERE ID=@id;"
                )
            )
            {
                command.Parameters.AddWithValue("@user_id", order.UserID);
                command.Parameters.AddWithValue("@status", order.status);
                command.Parameters.AddWithValue("@shipping_address", order.ShippingAddress);
                command.Parameters.AddWithValue("@createdate", DateTime.UtcNow);
                command.Parameters.AddWithValue("@updateDate", DateTime.UtcNow);

                var rowAffected = command.ExecuteNonQuery();
                return true;
            }
        }
    }

    public static bool DeleteOrderByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("DELETE FROM orders WHERE ID=@id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                var rowAffected = command.ExecuteNonQuery();
                return rowAffected == 1;
            }
        }
    }
}
