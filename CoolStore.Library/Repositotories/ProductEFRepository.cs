﻿using CoolStore.Library.Helpers;
using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData.EntityFramework;

namespace CoolStore.Library.Repositotories
{
    public class ProductEFRepository : IProductRepository
    {
        private readonly CoolStoreContext context;

        public ProductEFRepository(CoolStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(Product product)
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
            oldProduct.Weight = 1000;
            oldProduct.Height = product.Height;
            oldProduct.Width = product.Width;
            oldProduct.Length = product.Length;
            context.SaveChanges();
        }
    }
}
