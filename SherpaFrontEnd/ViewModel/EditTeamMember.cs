using System.ComponentModel.DataAnnotations;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.ViewModel;

public class EditTeamMember
{
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }
    
    [Required]
    [MinLength(2)]
    public string? LastName { get; set; }
    
    [Required]
    [MinLength(2)]
    public string? Position { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    private static List<string>? ForbiddenEmails { get; set; }



    public EditTeamMember(TeamMember? teamMember, List<string>? membersEmails)
    {
        Name = teamMember?.Name;
        LastName = teamMember?.LastName;
        Position = teamMember?.Position;
        Email = teamMember?.Email;
        ForbiddenEmails = membersEmails;
    }
}