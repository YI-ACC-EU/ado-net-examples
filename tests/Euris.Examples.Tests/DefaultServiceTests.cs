using Euris.Examples.Business;
using Euris.Examples.Common.Models.Requests;
using Euris.Examples.Common.Repositories;
using Moq;
using Serilog;

namespace Euris.Examples.Tests
{
    public class DefaultServiceTests
    {
        private MovieService _sut;
        private readonly Mock<IMovieRepository> _defaultRepositoryMoq = new();
        private readonly Mock<ILogger> _loggerMoq = new();

        [SetUp]
        public void Setup()
        {
            _sut = new MovieService(
                _loggerMoq.Object,
                _defaultRepositoryMoq.Object);
        }

        [Test]
        public async Task GetDefaultModel_ShouldCallRepositoryMethod_WhenInvoked()
        {
            _ = await _sut.GetMovieById(new MovieByIdRequest() {Id = 1});
            _defaultRepositoryMoq.Verify(x=>x.GetMovieById(1), Times.Once);
        }
    }
}