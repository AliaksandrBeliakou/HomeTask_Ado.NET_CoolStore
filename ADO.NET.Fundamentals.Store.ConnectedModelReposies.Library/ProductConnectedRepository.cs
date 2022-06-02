using ADO.NET.Fundamentals.Store.ConnectedModelReposies.Library.Helpers;
using ADO.NET.Fundamentals.Store.ConnectedModelReposies.Library.Interfaces;
using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET.Fundamentals.Store.ConnectedModelReposies.Library;

public class ProductConnectedRepository : IProductRepository
{
    private readonly SqlExecuter executer;

    public ProductConnectedRepository(string connectionString, IConnectedDbActorsFactory factory)
    {
        this.executer = new SqlExecuter(
            connectionString ?? throw new ArgumentNullException(nameof(connectionString)),
            factory ?? throw new ArgumentNullException(nameof(factory)));
    }

    public void Add(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var sqlParameters = GetSqlParametersFromProduct(product, false);
        this.executer.GetNothing("INSERT INTO [dbo].[Products] VALUES(@Name, @Description, @Weight, @Height, @Width, @Length)", CommandType.Text, sqlParameters);
    }

    public void Delete(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = product.Id }};
        this.executer.GetNothing("DELETE FROM [dbo].[Products] WHERE Id = @Id", CommandType.Text, parameters);
    }

    public IEnumerable<Product> GetAll()
    {
        return this.executer.GetList("SELECT * FROM [dbo].[Products]", CommandType.Text, null, Mapper.GetProduct);
    }

    public Product GetById(int id)
    {
        var sqlParameters = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }.ToEnumerable();
        return this.executer.GetSingle("SELECT * FROM [dbo].[Products] WHERE Id = @Id", CommandType.Text, sqlParameters, Mapper.GetProduct);
    }

    public void Update(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var sqlParameters = GetSqlParametersFromProduct(product, true);
        this.executer.GetNothing("UPDATE [dbo].[Products] SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id", CommandType.Text, sqlParameters);
    }

    private static IEnumerable<SqlParameter> GetSqlParametersFromProduct(Product product, bool withIdFlag)
    {
        var result = new List<SqlParameter>
        {
            new SqlParameter { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Size = 128, Value = product.Name },
            new SqlParameter { ParameterName = "@Description", SqlDbType = SqlDbType.Text, Value = product.Description },
            new SqlParameter { ParameterName = "@Weight", SqlDbType = SqlDbType.Int, Value = product.Weight },
            new SqlParameter { ParameterName = "@Height", SqlDbType = SqlDbType.Int, Value = product.Height },
            new SqlParameter { ParameterName = "@Width", SqlDbType = SqlDbType.Int, Value = product.Width },
            new SqlParameter { ParameterName = "@Length", SqlDbType = SqlDbType.Int, Value = product.Length },
        };
        if (withIdFlag)
        {
            result.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = product.Id });
        }

        return result;
    }
}
