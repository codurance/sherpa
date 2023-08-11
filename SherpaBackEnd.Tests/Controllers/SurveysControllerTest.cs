﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using SherpaBackEnd.Controllers;
using SherpaBackEnd.Model;
using SherpaBackEnd.Services;

namespace SherpaBackEnd.Tests.Controllers;

public class SurveysControllerTest
{
    private DeprecatedSurveysController _deprecatedSurveysController;
    private Mock<ISurveyService> _mockSurveyRepository;

    public SurveysControllerTest()
    {
        _mockSurveyRepository = new Mock<ISurveyService>();
        _deprecatedSurveysController = new DeprecatedSurveysController(_mockSurveyRepository.Object);
    }
}