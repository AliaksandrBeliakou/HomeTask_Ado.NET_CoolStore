using ADO.NET.Fundamentals.Store.DapperReposies.Library.Interfaces;
using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;
using Dapper;

namespace ADO.NET.Fundamentals.Store.DapperReposies.Library
{
    public class ProductDapperRepository : IProductRepository
    {
        private readonly IConnectionBuilder connectionBuilder;

        public ProductDapperRepository(IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder ?? throw new ArgumentNullException(nameof(connectionBuilder));
        }

        public void Add(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute("INSERT INTO [dbo].[Products] VALUES(@Name, @Description, @Weight, @Height, @Width, @Length)",
                new { product.Name, product.Description, product.Weight, product.Height, product.Width, product.Length });
        }

        public void Delete(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute("DELETE FROM [dbo].[Products] WHERE Id = @Id", new { product.Id });
        }

        public IEnumerable<Product> GetAll()
        {
            using var connection = connectionBuilder.Build();
            connection.Open();
            return connection.Query<Product>("SELECT * FROM [dbo].[Products]").ToList();
        }

        public Product GetById(int id)
        {
            using var connection = connectionBuilder.Build();
            connection.Open();
            return connection.QuerySingle<Product>("SELECT * FROM [dbo].[Products] WHERE Id = @Id", new { Id = id });
        }

        public void Update(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute("UPDATE [dbo].[Products] SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id",
                new { product.Id, product.Name, product.Description, product.Weight, product.Height, product.Width, product.Length });
        }
    }
}
