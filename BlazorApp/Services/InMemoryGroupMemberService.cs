﻿using BlazorApp.Model;

namespace BlazorApp.Services;

public class InMemoryGroupMemberService : IGroupMemberService
{
    public List<GroupMember> GetMembers()
    {
        return new List<GroupMember>
        {
            new("Bob", "Smith", "CEO"),
            new("Tom", "Hardy", "CP")
        };
    }
}