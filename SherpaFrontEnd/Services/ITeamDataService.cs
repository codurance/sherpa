using System.Net;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public interface ITeamDataService
{
    public Task<List<Team>> GetAllTeams();
    Task<Team> GetTeamById(Guid guid);
    Task<HttpStatusCode> DeleteTeam(Guid guid);
    Task PutTeam(Team team);
    Task AddTeam(Team team);
}