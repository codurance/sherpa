namespace SherpaFrontEnd.Services;

public interface ICachedResponseService
{
    Task<Dictionary<int, string>> GetBy(Guid surveyNotificationId);
}