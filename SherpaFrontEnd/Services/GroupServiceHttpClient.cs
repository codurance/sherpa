using System.Net.Http.Json;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.Services;

public class GroupServiceHttpClient : IGroupDataService
{

    private HttpClient HttpClient;

    public GroupServiceHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public List<Group>? getGroups()
    {
        var response = HttpClient.GetFromJsonAsync<IEnumerable<Group>>("https://localhost:7281/groups");
        return response.Result.ToList();
        // return new List<Group> { new Group("Group A"), new Group("Group B") };
    }
}