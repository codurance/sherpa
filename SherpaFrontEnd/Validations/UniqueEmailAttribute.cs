using System.ComponentModel.DataAnnotations;

namespace SherpaFrontEnd.Validations;

public class UniqueEmailAttribute : ValidationAttribute
{
    public UniqueEmailAttribute(List<string> forbiddenEmails)
    {
        ForbiddenEmails = forbiddenEmails;
    }

    private List<string> ForbiddenEmails { get; }

    protected override ValidationResult? IsValid(object? emailToCheck, ValidationContext validationContext)
    {
        if (ForbiddenEmails.Any(e => e == (string)emailToCheck!))
        {
            return new ValidationResult("Email should be unique");
        }

        return ValidationResult.Success;
    }
}