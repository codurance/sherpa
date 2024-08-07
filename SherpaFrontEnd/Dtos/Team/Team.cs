﻿using System.ComponentModel.DataAnnotations;

namespace SherpaFrontEnd.Dtos.Team;

public class Team
{
    [Required(ErrorMessage = "This field is mandatory")]
    public string? Name { get; set; }

    public Guid Id { get; init; }

    public List<TeamMember> Members { get; set; } = new();

    public Team()
    {
        Id = Guid.NewGuid();
    }

    public Team(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Team(Guid id, string name, List<TeamMember> members)
    {
        Id = id;
        Name = name;
        Members = members;
    }
}