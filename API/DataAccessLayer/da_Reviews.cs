using Npgsql;

namespace DataAccessLayer;

public class ReviewDTO
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
    public int rating { get; set; }
    public string comment { get; set; }
    public DateTime createDate { get; set; }

    public ReviewDTO(
        int id,
        int ProductID,
        int UserID,
        int rating,
        string comment,
        DateTime createdate
    )
    {
        this.ID = id;
        this.UserID = UserID;
        this.ProductID = ProductID;
        this.rating = rating;
        this.comment = comment;
        this.createDate = createdate;
    }
}

public class AllReviewDTO : ReviewDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }

    public AllReviewDTO(
        int id,
        int ProductID,
        int UserID,
        int rating,
        string comment,
        DateTime createdate,
        string firstName,
        string lastName
    )
        : base(id, ProductID, UserID, rating, comment, createdate)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}

public class da_ReviewData
{
    static string _connectionString = "Host=localhost;Username=user;Database=JewelryShop";

    public static List<AllReviewDTO> GetAllReviewsForProductID(int id)
    {
        var ReivewsList = new List<AllReviewDTO>();

        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "SELECT r.*, u.firstname, u.lastname FROM reviews r INNER JOIN users u ON r.user_id=u.id WHERE product_id = @id"
                )
            )
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReivewsList.Add(
                            new AllReviewDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("product_id")),
                                reader.GetInt32(reader.GetOrdinal("user_id")),
                                reader.GetInt32(reader.GetOrdinal("rating")),
                                reader.GetString(reader.GetOrdinal("comment")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetString(reader.GetOrdinal("firstName")),
                                reader.GetString(reader.GetOrdinal("lastName"))
                            )
                        );
                    }
                }
            }
        }

        return ReivewsList;
    }

    public static ReviewDTO GetReviewByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("SELECT * FROM reviews WHERE id = @id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ReviewDTO(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetInt32(reader.GetOrdinal("product_id")),
                            reader.GetInt32(reader.GetOrdinal("user_id")),
                            reader.GetInt32(reader.GetOrdinal("rating")),
                            reader.GetString(reader.GetOrdinal("comment")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))
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

    public static int AddNewReview(ReviewDTO review)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    @"INSERT INTO reviews (
                          product_id, user_id, rating, comment, created_at
                          ) VALUES (
                            @product_id, @user_id, @rating, @comment, @createdate
                          ) RETURNING ID;"
                )
            )
            {
                command.Parameters.AddWithValue("@product_id", review.ProductID);
                command.Parameters.AddWithValue("@user_id", review.UserID);
                command.Parameters.AddWithValue("@rating", review.rating);
                command.Parameters.AddWithValue("@comment", review.comment);
                command.Parameters.AddWithValue("@createdate", DateTime.UtcNow);

                var rowInserted = command.ExecuteScalar();

                return (int)rowInserted;
            }
        }
    }

    public static bool UpdateReviewByID(ReviewDTO review)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "UPDATE reviews SET product_id=@product_id, user_id=@user_id, rating=@rating, comment=@comment, created_at=@createdate WHERE ID=@id;"
                )
            )
            {
                command.Parameters.AddWithValue("@product_id", review.ProductID);
                command.Parameters.AddWithValue("@user_id", review.UserID);
                command.Parameters.AddWithValue("@rating", review.rating);
                command.Parameters.AddWithValue("@comment", review.comment);
                command.Parameters.AddWithValue("@createdate", DateTime.UtcNow);

                var rowAffected = command.ExecuteNonQuery();
                return true;
            }
        }
    }

    public static bool DeleteReviewByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("DELETE FROM reviews WHERE ID=@id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                var rowAffected = command.ExecuteNonQuery();
                return rowAffected == 1;
            }
        }
    }
}
