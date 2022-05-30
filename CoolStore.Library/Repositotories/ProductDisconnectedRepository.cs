using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData.AdoNetDisconectedModel;
using CoolStore.Library.SqlData.AdoNetDisconectedModel.Interfaces;
using System.Data;

namespace CoolStore.Library.Repositotories
{
    public class ProductDisconnectedRepository : IProductRepository
    {
        private readonly CoolStoreDataSet dataset;
        private readonly ICoolStoreDbProvider provider;


        public ProductDisconnectedRepository(CoolStoreDataSet dataset, ICoolStoreDbProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.dataset = dataset ?? throw new ArgumentNullException(nameof(dataset));
        }

        public void Create(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.dataset.Products.AddProductsRow(product.Name, product.Description, product.Weight, product.Height, product.Width, product.Length);
            SaveChangesToDatabase();
        }

        public void Delete(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.dataset.Products.FindById(product.Id)?.Delete();
            SaveChangesToDatabase();
        }

        public IEnumerable<Product> GetAll()
        {
            return dataset.Products
                .Select(row => new Product(row.Id, row.Name, row.Description, row.Weight, row.Height, row.Width, row.Length));
        }

        public Product GetById(int id)
        {
            var row = dataset.Products.FindById(id) ?? throw new ArgumentOutOfRangeException($"Priduct with id equal to {id} is not exist");
            return new Product(row.Id, row.Name, row.Description, row.Weight, row.Height, row.Width, row.Length);
        }

        public void Update(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var productDataRow = dataset.Products.FindById(product.Id) ?? throw new ArgumentOutOfRangeException($"Priduct with id equal to {product.Id} is not exist");
            productDataRow.Name = product.Name;
            productDataRow.Description = product.Description;
            productDataRow.Weight = product.Weight;
            productDataRow.Height = product.Height;
            productDataRow.Width = product.Width;
            productDataRow.Length = product.Length;
            SaveChangesToDatabase();
        }

        private void SaveChangesToDatabase()
        {
            this.provider.Update(this.dataset.Products);
        }
    }
}
