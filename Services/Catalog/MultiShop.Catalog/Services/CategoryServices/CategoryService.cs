using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entites;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(
                databaseSettings.CategoryCollectionName);

            _productCollection = database.GetCollection<Product>(
                databaseSettings.ProductCollectionName);

            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var value = await _categoryCollection
                .Find(x => x.CategoryId == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<GetByIdCategoryDto>(value);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryWithProductCountAsync()
        {
            var categories = await _categoryCollection.Find(x => true).ToListAsync();

            var result = categories.Select(c => new ResultCategoryDto
            {
                CategoryID = c.CategoryId,
                CategoryName = c.CategoryName,
                ImageUrl = c.ImageUrl,
                ProductCount = _productCollection.CountDocuments(
                                    x => x.CategoryId == c.CategoryId)
            }).ToList();

            return result;
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);

            value.CategoryId = updateCategoryDto.CategoryID;

            await _categoryCollection.FindOneAndReplaceAsync(
                x => x.CategoryId == updateCategoryDto.CategoryID,
                value);
        }
    }
}
