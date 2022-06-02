using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library
{
    public class ProductEFRepository : IProductRepository
    {
        private readonly CoolStoreContext context;

        public ProductEFRepository(CoolStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            context.Products.Add(product.ToEntity());
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var itemForDeletion = context.Products.Single(x => x.Id == product.Id);
            context.Products.Remove(itemForDeletion);
            context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.Select(x => x.ToModel());
        }

        public Product GetById(int id)
        {
            return context.Products.Single(prop => prop.Id == id).ToModel();
        }

        public void Update(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var oldProduct = context.Products.Single(prop => prop.Id == product.Id);
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.Weight = product.Weight;
            oldProduct.Height = product.Height;
            oldProduct.Width = product.Width;
            oldProduct.Length = product.Length;
            context.SaveChanges();
        }
    }
}
