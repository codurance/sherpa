namespace SherpaFrontEnd.Services;

public interface ICachedResponseService
{
    Task<Dictionary<int, string>> GetBy(Guid surveyNotificationId);
    Task Save(Guid surveyNotificationId, Dictionary<int, string> responses);
}