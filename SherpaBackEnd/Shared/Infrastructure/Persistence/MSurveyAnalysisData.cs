using SherpaBackEnd.Analysis.Domain;

namespace SherpaBackEnd.Shared.Infrastructure.Persistence;

public class MSurveyAnalysisData
{
    public static SurveyAnalysisData<string> ToSurveyAnalysisData(MSurvey mSurvey, TemplateAnalysis template)
    {
        var participants = mSurvey.Responses.Select(r =>
            new Participant<string>(r.Answers.Select(a =>
                new Response<string>(
                    template.Questions[a.Number].Category,
                    a.Answer,
                    template.Questions[a.Number].Reverse,
                    template.Questions[a.Number].Options
                )
            ).ToList())
        ).ToList();

        return new SurveyAnalysisData<string>(mSurvey.Title, participants);
    }
}