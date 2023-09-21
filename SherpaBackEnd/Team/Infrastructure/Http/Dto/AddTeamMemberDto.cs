using SherpaBackEnd.Team.Domain;

namespace SherpaBackEnd.Team.Infrastructure.Http.Dto;

public class AddTeamMemberDto
{
    public Guid TeamId { get; set; }
    public TeamMember TeamMember { get; set; }

    public AddTeamMemberDto(Guid teamId, TeamMember teamMember)
    {
        TeamId = teamId;
        TeamMember = teamMember;
    }
}