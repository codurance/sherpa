using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class SurveysControllerTest
{
    private SurveysController _surveysController;
    private Mock<ISurveysService> _mockSurveyRepository;

    public SurveysControllerTest()
    {
        _mockSurveyRepository = new Mock<ISurveysService>();
        _surveysController = new SurveysController(_mockSurveyRepository.Object);
    }
}