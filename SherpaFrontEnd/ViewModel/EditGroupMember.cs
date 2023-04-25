using System.ComponentModel.DataAnnotations;
using SherpaFrontEnd.Model;

namespace SherpaFrontEnd.ViewModel;

public class EditGroupMember
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



    public EditGroupMember(GroupMember? groupMember, List<string>? membersEmails)
    {
        Name = groupMember?.Name;
        LastName = groupMember?.LastName;
        Position = groupMember?.Position;
        Email = groupMember?.Email;
        ForbiddenEmails = membersEmails;
    }
}