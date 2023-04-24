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


    public EditGroupMember(GroupMember member)
    {
        Name = member.Name;
        LastName = member.LastName;
        Position = member.Position;
        Email = member.Email;
    }
}