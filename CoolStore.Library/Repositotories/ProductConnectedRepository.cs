using CoolStore.Library.Helpers;
using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData;
using CoolStore.Library.SqlData.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CoolStore.Library.Repositotories
{
    public class ProductConnectedRepository : IProductRepository
    {
        private readonly SqlExecuter executer;

        public ProductConnectedRepository(string connectionString, IConnectedDbActorsFactory factory)
        {
            this.executer = new SqlExecuter(
                connectionString ?? throw new ArgumentNullException(nameof(connectionString)),
                factory ?? throw new ArgumentNullException(nameof(factory)));
        }

        public void Create(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            var sqlParameters = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }.ToEnumerable();
            return this.executer.GetSingleRow<Product>("SELECT * FROM [dbo].[Products] WHERE Id = @Id", sqlParameters, Mapper.GetProduct);
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
