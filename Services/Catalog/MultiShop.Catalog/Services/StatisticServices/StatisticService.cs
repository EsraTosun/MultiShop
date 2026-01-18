using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Entites;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Brand> _brandCollection;

        public StatisticService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _brandCollection = database.GetCollection<Brand>(databaseSettings.BrandCollectionName);
        }

        public async Task<long> GetBrandCount()
        {
            return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty);
        }

        public async Task<long> GetCategoryCount()
        {
            return await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty);
        }

        public async Task<long> GetProductCount()
        {
            return await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }

        public async Task<string?> GetMaxPriceProductName()
        {
            var product = await _productCollection
                .Find(FilterDefinition<Product>.Empty)
                .SortByDescending(x => x.ProductPrice)
                .Limit(1)
                .FirstOrDefaultAsync();

            return product?.ProductName;
        }

        public async Task<string?> GetMinPriceProductName()
        {
            var product = await _productCollection
                .Find(FilterDefinition<Product>.Empty)
                .SortBy(x => x.ProductPrice)
                .Limit(1)
                .FirstOrDefaultAsync();

            return product?.ProductName;
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$group",
                    new BsonDocument
                    {
                        { "_id", BsonNull.Value },
                        { "averagePrice", new BsonDocument("$avg", "$ProductPrice") }
                    })
            };

            var result = await _productCollection.AggregateAsync<BsonDocument>(pipeline);
            var document = result.FirstOrDefault();

            return document == null
                ? 0
                : document.GetValue("averagePrice", 0).ToDecimal();
        }
    }
}
