using Npgsql;

namespace DataAccessLayer;

public class LoginRequest
{
    public string email { get; set; }
    public string password { get; set; }

    public LoginRequest(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}

public class UserDTO
{
    public int ID { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }
    public int type { get; set; }

    public UserDTO(
        int id,
        string firstname,
        string lastname,
        string email,
        string password,
        DateTime createdate,
        DateTime updatedate,
        int type
    )
    {
        this.ID = id;
        this.email = email;
        this.password = password;
        this.firstName = firstname;
        this.lastName = lastname;
        this.createDate = createdate;
        this.updateDate = updatedate;
        this.type = type;
    }
}

public class da_UsersData
{
    static string _connectionString = "Host=localhost;Username=user;Database=JewelryShop";

    public static UserDTO GetUserByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("SELECT * FROM users WHERE id = @id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserDTO(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("firstname")),
                            reader.GetString(reader.GetOrdinal("lastname")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password_hash")),
                            reader.GetDateTime(reader.GetOrdinal("created_at")),
                            reader.GetDateTime(reader.GetOrdinal("updated_at")),
                            reader.GetInt32(reader.GetOrdinal("type"))
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

    public static int AddNewUser(UserDTO user)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    @"INSERT INTO users (
                          firstname, lastname, email, password_hash, created_at,
                          updated_at, type
                          ) VALUES (
                            @firstname, @lastname, @email, @password_hash, @createdate,
                            @updatedate, @type
                          ) RETURNING ID;"
                )
            )
            {
                command.Parameters.AddWithValue("@firstname", user.firstName);
                command.Parameters.AddWithValue("@lastname", user.lastName);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@password_hash", user.password);
                command.Parameters.AddWithValue("@createdate", DateTime.UtcNow);
                command.Parameters.AddWithValue("@updatedate", DateTime.UtcNow);
                command.Parameters.AddWithValue("@type", user.type);

                var rowInserted = command.ExecuteScalar();

                return (int)rowInserted;
            }
        }
    }

    public static bool UpdateUserByID(UserDTO user)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "UPDATE users SET firstname = @firstname, lastname = @lastname, email = @email, password_hash = @password_hash, updated_at = @updatedate, type = @type WHERE id = @id;"
                )
            )
            {
                command.Parameters.AddWithValue("@id", user.ID);
                command.Parameters.AddWithValue("@firstname", user.firstName);
                command.Parameters.AddWithValue("@lastname", user.lastName);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@password_hash", user.password);
                command.Parameters.AddWithValue("@updatedate", DateTime.Now);
                command.Parameters.AddWithValue("@type", user.type);

                var rowAffected = command.ExecuteNonQuery();
                return true;
            }
        }
    }

    public static bool DeleteUserByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("DELETE FROM users WHERE ID=@id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                var rowAffected = command.ExecuteNonQuery();
                return rowAffected == 1;
            }
        }
    }

    public static UserDTO loginUser(string email, string password)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "SELECT * FROM users WHERE email=@email AND password_hash=@password;"
                )
            )
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserDTO(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("firstname")),
                            reader.GetString(reader.GetOrdinal("lastname")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("password_hash")),
                            reader.GetDateTime(reader.GetOrdinal("created_at")),
                            reader.GetDateTime(reader.GetOrdinal("updated_at")),
                            reader.GetInt32(reader.GetOrdinal("type"))
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
}
