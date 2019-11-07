using Product.WebApi.Domain.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.WebApi.Domain.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;

        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public List<Entities.Product> GetAll()
        {
            return _productContext.Products.ToList();
        }

        public Entities.Product Get(int id)
        {
            return _productContext.Products.FirstOrDefault(d => d.Id == id);
        }

        public EntityEntry<Entities.Product> Add(Entities.Product product)
        {
            var productWithId = _productContext.Products.Add(product);

            _productContext.SaveChanges();

            return productWithId;
        }

        public EntityEntry<Entities.Product> Update(Entities.Product product)
        {
            var productUpdated = _productContext.Products.Update(product);

            _productContext.SaveChanges();

            return productUpdated;
        }

        public bool Remove(int id)
        {
            try
            {
                var productToRemove = _productContext.Products.FirstOrDefault(d => d.Id == id);
                if (productToRemove != null)
                {
                    _productContext.Products.Remove(productToRemove);

                    _productContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // LOGUEAR
                return false;
                //throw;
            }

        }
    }
}
