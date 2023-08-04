﻿using System.ComponentModel.DataAnnotations;

namespace SherpaFrontEnd.Model;

public class Team
{
    [Required]
    [MinLength(2)]
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
}