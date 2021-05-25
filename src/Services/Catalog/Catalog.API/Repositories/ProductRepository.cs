using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Empty;
            var products = await _catalogContext
                                    .Products
                                    .Find(filter)
                                    .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var product = await _catalogContext
                                    .Products
                                    .Find(filter)
                                    .FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            var products = await _catalogContext
                                    .Products
                                    .Find(filter)
                                    .ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            var products = await _catalogContext
                                    .Products
                                    .Find(filter)
                                    .ToListAsync();
            return products;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id,
                                                                              replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
        }
    }
}
