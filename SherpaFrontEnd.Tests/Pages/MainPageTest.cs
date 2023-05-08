using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class MainPageTest
{
    private readonly TestContext _testContext;
    private Mock<IAssessmentsDataService> _mockAssessmentDataService;
    private Mock<IGroupDataService> _mockGroupDataService;
    private IRenderedComponent<MainPage> _renderedComponent;
    private MainPage MainPage;

    public MainPageTest()
    {
        _testContext = new TestContext();
        _mockAssessmentDataService = new Mock<IAssessmentsDataService>();
        _mockGroupDataService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(p => _mockAssessmentDataService.Object);
        _testContext.Services.AddScoped(p => _mockGroupDataService.Object);
    }

    [Fact]
    public void MainPage_OnLoadDoesNotReturnAssessments_ExpectSelectedAssessmentsToBeAnEmptyLisy()
    {
        _mockAssessmentDataService.Setup(ds => ds.GetAssessments())
            .Returns(Task.FromResult(new List<Assessment>())!);

        _renderedComponent = _testContext.RenderComponent<MainPage>();

        if (_renderedComponent.Instance.Assessments != null) Assert.Empty(_renderedComponent.Instance.Assessments);
    }

    [Fact]
    public void MainPage_OnLoadReturnsAssessmentList_ExpectSelectedAssessmentToMatchSelectedGroup()
    {
        var groupA_id = Guid.NewGuid();
        var groupB_id = Guid.NewGuid();
        _mockAssessmentDataService.Setup(ds => ds.GetAssessments())
            .Returns(Task.FromResult(new List<Assessment>{
                new ("assessment group A", groupA_id,Guid.NewGuid()),
                new ("assessment group B", groupB_id,Guid.NewGuid())
            })!);
        _mockGroupDataService.Setup(ds => ds.GetGroups())
            .Returns(Task.FromResult(new List<Group>
            {
                new Group
                {
                    Id = groupA_id,
                    Name = "Group A",
                    Members = new List<GroupMember>
                    {
                        new ("Name 1", "LastName 1", "Position 1", "email1@gmail.com"),
                        new ("Name 2", "LastName 2", "Position 2", "email2@gmail.com")
                    }
                },
                new Group
                {
                    Id = groupB_id,
                    Name = "Group B",
                    Members = new List<GroupMember>
                    {
                        new ("Name 1", "LastName 1", "Position 1", "email1@gmail.com"),
                        new ("Name 2", "LastName 2", "Position 2", "email2@gmail.com")
                    }
                }
            })!);

        _renderedComponent = _testContext.RenderComponent<MainPage>();

        if (_renderedComponent.Instance.SelectedGroupAssessments != null)
            Assert.Equal(groupA_id, _renderedComponent.Instance.SelectedGroupAssessments[0].GroupId);
    }
}
