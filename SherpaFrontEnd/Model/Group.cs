using System.ComponentModel.DataAnnotations;

namespace SherpaFrontEnd.Model;

public class Group
{
    [Required]
    [MinLength(2)]
    public string? Name { get; set; }
    
    public Guid Id { get; init; }
    
    public List<GroupMember> Members { get; set; }
    
public Group()
    {
        Id = Guid.NewGuid();
        Members = new List<GroupMember>();
    }
}