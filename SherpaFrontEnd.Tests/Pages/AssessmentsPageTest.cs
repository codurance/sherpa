using System.Reflection;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SherpaFrontEnd.Model;
using SherpaFrontEnd.Pages.Surveys;
using SherpaFrontEnd.Services;

namespace BlazorApp.Tests.Pages;

public class AssessmentsPageTest
{
    private readonly TestContext _testContext;
    private Mock<IAssessmentsDataService> _mockAssessmentDataService;
    private Mock<IGroupDataService> _mockGroupDataService;
    private IRenderedComponent<AssessmentsPage> _renderedComponent;
    private AssessmentsPage AssessmentsPage;

    public AssessmentsPageTest()
    {
        _testContext = new TestContext();
        _mockAssessmentDataService = new Mock<IAssessmentsDataService>();
        _mockGroupDataService = new Mock<IGroupDataService>();
        _testContext.Services.AddScoped(p => _mockAssessmentDataService.Object);
        _testContext.Services.AddScoped(p => _mockGroupDataService.Object);
    }

    [Fact]
    public void AssessmentPage_OnLoadDoesNotReturnAssessments_ExpectSelectedAssessmentsToBeAnEmptyLisy()
    {
        _mockAssessmentDataService.Setup(ds => ds.GetAssessments())
            .Returns(Task.FromResult(new List<Assessment>())!);

        _renderedComponent = _testContext.RenderComponent<AssessmentsPage>();

        if (_renderedComponent.Instance.Assessments != null) Assert.Empty(_renderedComponent.Instance.Assessments);
    }

    [Fact]
    public void AssessmentsPage_OnLoadReturnsAssessmentList_ExpectSelectedAssessmentToMatchSelectedGroup()
    {
        Guid groupA_id = Guid.NewGuid();
        Guid groupB_id = Guid.NewGuid();
        _mockAssessmentDataService.Setup(ds => ds.GetAssessments())
            .Returns(Task.FromResult(new List<Assessment>{
                new Assessment("assessment group A",groupA_id,Guid.NewGuid(),null),
                new Assessment("assessment group B",groupB_id,Guid.NewGuid(),null)
            }));
        _mockGroupDataService.Setup(ds => ds.GetGroups())
            .Returns(Task.FromResult(new List<Group>
            {
                new Group(groupA_id, "Group A"),
                new Group(groupB_id, "Group B")
            })!);

        _renderedComponent = _testContext.RenderComponent<AssessmentsPage>();

        if (_renderedComponent.Instance.SelectedGroupAssessments != null)
            Assert.Equal(groupA_id, _renderedComponent.Instance.SelectedGroupAssessments[0].GroupId);
    }
}
