using Npgsql;

namespace DataAccessLayer;

public class ProductDTO
{
    // public ProductDTO() { }

    public ProductDTO(
        int ID,
        string name,
        string description,
        int categoryID,
        decimal price,
        string material,
        string gemstone,
        decimal carat,
        string clarity,
        string cut,
        string certification,
        int stock,
        bool isFeatured,
        DateTime createDate,
        DateTime updateDate
    )
    {
        this.ID = ID;
        this.name = name;
        this.description = description;
        this.categoryID = categoryID;
        this.price = price;
        this.material = material;
        this.gemstone = gemstone;
        this.carat = carat;
        this.clarity = clarity;
        this.cut = cut;
        this.certification = certification;
        this.stock = stock;
        this.isFeatured = isFeatured;
        this.createDate = createDate;
        this.updateDate = updateDate;
    }

    public int ID { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int categoryID { get; set; }
    public decimal price { get; set; }
    public string material { get; set; }
    public string gemstone { get; set; }
    public decimal carat { get; set; }
    public string clarity { get; set; }
    public string cut { get; set; }
    public string certification { get; set; }
    public int stock { get; set; }
    public bool isFeatured { get; set; }
    public DateTime createDate { get; set; }
    public DateTime updateDate { get; set; }
}

public class da_ProductsData
{
    static string _connectionString = "Host=localhost;Username=user;Database=JewelryShop";

    public static List<ProductDTO> GetAllProducts()
    {
        var ProductsList = new List<ProductDTO>();

        using (var dataSource = NpgsqlDataSource.Create(_connectionString))
        {
            using (var cmd = dataSource.CreateCommand("SELECT * FROM products;"))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductsList.Add(
                            new ProductDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("name")),
                                reader.GetString(reader.GetOrdinal("description")),
                                reader.GetInt32(reader.GetOrdinal("category_id")),
                                reader.GetDecimal(reader.GetOrdinal("price")),
                                reader.GetString(reader.GetOrdinal("material")),
                                reader.GetString(reader.GetOrdinal("gemstone")),
                                reader.GetDecimal(reader.GetOrdinal("carat")),
                                reader.GetString(reader.GetOrdinal("clarity")),
                                reader.GetString(reader.GetOrdinal("cut")),
                                reader.GetString(reader.GetOrdinal("certification")),
                                reader.GetInt32(reader.GetOrdinal("stock")),
                                reader.GetBoolean(reader.GetOrdinal("is_Featured")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetDateTime(reader.GetOrdinal("updated_at"))
                            )
                        );
                    }
                }
            }
        }

        return ProductsList;
    }

    public static ProductDTO GetProductByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("SELECT * FROM products WHERE id=@id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProductDTO(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("name")),
                            reader.GetString(reader.GetOrdinal("description")),
                            reader.GetInt32(reader.GetOrdinal("category_id")),
                            reader.GetDecimal(reader.GetOrdinal("price")),
                            reader.GetString(reader.GetOrdinal("material")),
                            reader.GetString(reader.GetOrdinal("gemstone")),
                            reader.GetDecimal(reader.GetOrdinal("carat")),
                            reader.GetString(reader.GetOrdinal("clarity")),
                            reader.GetString(reader.GetOrdinal("cut")),
                            reader.GetString(reader.GetOrdinal("certification")),
                            reader.GetInt32(reader.GetOrdinal("stock")),
                            reader.GetBoolean(reader.GetOrdinal("is_Featured")),
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

    public static int AddNewProduct(ProductDTO product)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "INSERT INTO products(name, description, category_id, price, material, gemstone, carat, clarity, cut, certification, stock, is_Featured, created_at, updated_at) VALUES (@name, @description, @category_id, @price, @material, @gemstone, @carat, @clarity, @cut, @certification, @stock, @is_Featured, @created_at, @updated_at) RETURNING ID;"
                )
            )
            {
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@description", product.description);
                command.Parameters.AddWithValue("@category_id", product.categoryID);
                command.Parameters.AddWithValue("@price", product.price);
                command.Parameters.AddWithValue("@material", product.material);
                command.Parameters.AddWithValue("@gemstone", product.gemstone);
                command.Parameters.AddWithValue("@carat", product.carat);
                command.Parameters.AddWithValue("@clarity", product.clarity);
                command.Parameters.AddWithValue("@cut", product.cut);
                command.Parameters.AddWithValue("@certification", product.certification);
                command.Parameters.AddWithValue("@stock", product.stock);
                command.Parameters.AddWithValue("@is_Featured", product.isFeatured);
                command.Parameters.AddWithValue("@created_at", DateTime.Now);
                command.Parameters.AddWithValue("@updated_at", DateTime.Now);

                var rowInserted = command.ExecuteScalar();

                return (int)rowInserted;
            }
        }
    }

    public static bool UpdateProductByID(ProductDTO product)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (
                var command = conn.CreateCommand(
                    "UPDATE products SET name=@name, description=@description, category_id=@category_id, price=@price, material=@material, gemstone=@gemstone, carat=@carat, clarity=@clarity, cut=@cut, certification=@certification, stock=@stock, is_Featured=@is_Featured, updated_at=@updated_at WHERE ID=@id;"
                )
            )
            {
                command.Parameters.AddWithValue("@ID", product.ID);
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@description", product.description);
                command.Parameters.AddWithValue("@category_id", product.categoryID);
                command.Parameters.AddWithValue("@price", product.price);
                command.Parameters.AddWithValue("@material", product.material);
                command.Parameters.AddWithValue("@gemstone", product.gemstone);
                command.Parameters.AddWithValue("@carat", product.carat);
                command.Parameters.AddWithValue("@clarity", product.clarity);
                command.Parameters.AddWithValue("@cut", product.cut);
                command.Parameters.AddWithValue("@certification", product.certification);
                command.Parameters.AddWithValue("@stock", product.stock);
                command.Parameters.AddWithValue("@is_Featured", product.isFeatured);
                command.Parameters.AddWithValue("@updated_at", DateTime.Now);

                var rowAffected = command.ExecuteNonQuery();
                return true;
            }
        }
    }

    public static bool DeleteProductByID(int id)
    {
        using (var conn = NpgsqlDataSource.Create(_connectionString))
        {
            using (var command = conn.CreateCommand("DELETE FROM products WHERE ID=@id;"))
            {
                command.Parameters.AddWithValue("@id", id);

                var rowAffected = command.ExecuteNonQuery();
                return rowAffected == 1;
            }
        }
    }
}
