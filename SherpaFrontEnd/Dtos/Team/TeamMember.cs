using System.ComponentModel.DataAnnotations;

namespace SherpaFrontEnd.Dtos.Team;

public class TeamMember
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "This field is mandatory")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "This field is mandatory")]
    public string Position { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address"),
     Required(ErrorMessage = "This field is mandatory")
    ]
    public string Email { get; set; }

    public TeamMember(Guid id, string fullName, string position, string email)
    {
        Id = id;
        Position = position;
        Email = email;
        FullName = fullName;
    }
}