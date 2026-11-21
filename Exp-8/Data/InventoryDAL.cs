using Microsoft.Data.SqlClient;

public class InventoryDAL
{
    private readonly string _connectionString;

    public InventoryDAL(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        EnsureDatabaseAndTable();
    }

    private void EnsureDatabaseAndTable()
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);
        string dbName = builder.InitialCatalog;
        builder.InitialCatalog = "master";

        using (var masterCon = new SqlConnection(builder.ConnectionString))
        {
            masterCon.Open();
            using var createDbCmd = new SqlCommand(
                $"IF DB_ID('{dbName}') IS NULL CREATE DATABASE [{dbName}];", masterCon);
            createDbCmd.ExecuteNonQuery();
        }

        using (var con = new SqlConnection(_connectionString))
        {
            con.Open();
            string createTableSql = @"
                IF OBJECT_ID('dbo.Products', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Products (
                        ProductId INT PRIMARY KEY IDENTITY(1,1),
                        ProductName NVARCHAR(100) NOT NULL,
                        Category NVARCHAR(50) NOT NULL,
                        Quantity INT NOT NULL,
                        UnitPrice DECIMAL(10, 2) NOT NULL
                    );
                END";
            using var createTableCmd = new SqlCommand(createTableSql, con);
            createTableCmd.ExecuteNonQuery();
        }
    }

    // READ ALL
    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT ProductId, ProductName, Category, Quantity, UnitPrice FROM Products ORDER BY ProductId DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString()!,
                        Category = reader["Category"].ToString()!,
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                    });
                }
            }
        }
        return products;
    }

    // READ BY ID
    public Product? GetProductById(int id)
    {
        Product? product = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT ProductId, ProductName, Category, Quantity, UnitPrice FROM Products WHERE ProductId = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    product = new Product
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString()!,
                        Category = reader["Category"].ToString()!,
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        UnitPrice = Convert.ToDecimal(reader["UnitPrice"])
                    };
                }
            }
        }
        return product;
    }

    // CREATE
    public void AddProduct(Product product)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Products (ProductName, Category, Quantity, UnitPrice) 
                             VALUES (@Name, @Category, @Quantity, @Price)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", product.ProductName);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@Price", product.UnitPrice);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // UPDATE
    public void UpdateProduct(Product product)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"UPDATE Products 
                             SET ProductName = @Name, Category = @Category, Quantity = @Quantity, UnitPrice = @Price 
                             WHERE ProductId = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", product.ProductId);
            cmd.Parameters.AddWithValue("@Name", product.ProductName);
            cmd.Parameters.AddWithValue("@Category", product.Category);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@Price", product.UnitPrice);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // DELETE
    public void DeleteProduct(int id)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Products WHERE ProductId = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
