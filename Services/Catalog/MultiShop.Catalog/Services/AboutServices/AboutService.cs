using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entites;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IMongoCollection<About> _aboutCollection;
        private readonly IMapper _mapper;

        public AboutService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _aboutCollection = database.GetCollection<About>(databaseSettings.AboutCollectionName);
            _mapper = mapper;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var value = _mapper.Map<About>(createAboutDto);
            await _aboutCollection.InsertOneAsync(value);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _aboutCollection.DeleteOneAsync(x => x.AboutId == id);
        }

        public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
        {
            var value = await _aboutCollection
                .Find(x => x.AboutId == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<GetByIdAboutDto>(value);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var values = await _aboutCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultAboutDto>>(values);
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var value = _mapper.Map<About>(updateAboutDto);

            value.AboutId = updateAboutDto.AboutId;

            await _aboutCollection.FindOneAndReplaceAsync(
                x => x.AboutId == updateAboutDto.AboutId,
                value);
        }
    }
}
