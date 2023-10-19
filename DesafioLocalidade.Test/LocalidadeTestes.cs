using DesafioLocalidade.Application.Services.IBGEServices;
using DesafioLocalidade.Data.Context;
using DesafioLocalidade.Domain.Models;
using DesafioLocalidade.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DesafioLocalidade.Test
{
    public class LocalidadeTests : IDisposable
    {
        private DbContextOptions<AppDbContext> _options;
        private AppDbContext _appDbContext;
        private IBGEQueriesService _queriesService;
        private IBGECommandsService _commandsService;

        public LocalidadeTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _appDbContext = new AppDbContext(_options);
            _queriesService = new IBGEQueriesService(_appDbContext);
            _commandsService = new IBGECommandsService(_appDbContext);
        }

        [Fact]
        public async Task BuscarTodasLocalidades_Sucesso()
        {
            List<IBGE> ibgeListMock = new()
            {
                new IBGE("3844550","MG","Araguari"),
                new IBGE("7777777", "MG","Uberlândia")
            };

            List<IBGEViewModel> ibgeVmExpected = new()
            {
                new IBGEViewModel("3844550", "Araguari", "MG"),
                new IBGEViewModel("7777777", "Uberlândia", "MG")
            };

            _appDbContext.IBGE.AddRange(ibgeListMock);
            await _appDbContext.SaveChangesAsync();

            var result = await _queriesService.GetAll(1, 5);

            Assert.NotNull(result);
            Assert.True(result.Count() == 2);
            Assert.Collection(result,
                                r => Assert.Equal(ibgeVmExpected[0].Id, r.Id),
                                r => Assert.Equal(ibgeVmExpected[1].Id, r.Id)
                              );
            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task BuscarPorUF_Sucesso()
        {
            List<IBGE> ibgeListMock = new()
            {
                new IBGE("3844550","MG","Araguari"),
                new IBGE("7777777", "MG","Uberlândia"),
                new IBGE("3333333", "MG","Ituiutaba"),
                new IBGE("9999999", "MG","Roça do Zé"),
            };

            List<IBGEViewModel> ibgeVmExpected = new()
            {
                new IBGEViewModel("3844550", "Araguari", "MG"),
                new IBGEViewModel("7777777", "Uberlândia", "MG"),
                new IBGEViewModel("3333333", "Ituiutaba", "MG"),
                new IBGEViewModel("9999999", "Roça do Zé", "MG")
            };

            _appDbContext.IBGE.AddRange(ibgeListMock);
            await _appDbContext.SaveChangesAsync();

            var result = await _queriesService.GetByUF("MG");

            Assert.NotNull(result);
            Assert.True(result.Count() == 4);
            Assert.Collection(result,
                                r => Assert.Equal(ibgeVmExpected[0].Id, r.Id),
                                r => Assert.Equal(ibgeVmExpected[1].Id, r.Id),
                                r => Assert.Equal(ibgeVmExpected[2].Id, r.Id),
                                r => Assert.Equal(ibgeVmExpected[3].Id, r.Id)
                              );
            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task BuscarPorCodigo_Sucesso()
        {
            // Arrange
            var entity = new IBGE("0000000", "MG", "Tuiutaba");
            await _appDbContext.IBGE.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            // Act
            var retrievedEntity = await _queriesService.GetByIBGE(entity.Id);

            // Assert
            Assert.NotNull(retrievedEntity);
            Assert.Equal(entity.Id, retrievedEntity.Id);
            Assert.Equal(entity.State, retrievedEntity.State);
            Assert.Equal(entity.City, retrievedEntity.City);
            _appDbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task BuscarPorCidade_Sucesso()
        {
            // Arrange
            var entity = new IBGE("0000000", "NY", "Gothan City");
            await _appDbContext.IBGE.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            // Act
            var retrievedEntity = await _queriesService.GetByCity(entity.City);

            // Assert
            Assert.NotNull(retrievedEntity);
            Assert.Equal(entity.Id, retrievedEntity.Id);
            Assert.Equal(entity.State, retrievedEntity.State);
            Assert.Equal(entity.City, retrievedEntity.City);
            _appDbContext.Database.EnsureDeleted();
        }
        void IDisposable.Dispose()
        {
            _appDbContext.Dispose();
        }
    }

}
