using Moq;
using SherpaBackEnd.Model.Template;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Services;

public class TemplateServiceTest
{
    private readonly Mock<ITemplateRepository> _repository;

    public TemplateServiceTest()
    {
         _repository = new Mock<ITemplateRepository>();
    }

    [Fact]
    public async void Should_return_templates_returned_by_the_repository()
    {
        var actualResponse = new[]
        {
            new Template("test", Array.Empty<IQuestion>(), 1)
        };
        
        _repository.Setup(templateRepository => templateRepository.GetAllTemplates()).ReturnsAsync(actualResponse);
        var templateService = new TemplateService(_repository.Object);

        var expectedResponse = await templateService.GetAllTemplates();
        
        Assert.Equal(expectedResponse, actualResponse);
    }
}