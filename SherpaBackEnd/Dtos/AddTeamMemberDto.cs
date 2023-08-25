namespace SherpaBackEnd.Dtos;

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