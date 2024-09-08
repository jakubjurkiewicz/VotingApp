using System.ComponentModel.DataAnnotations;

namespace VotingApp.API.Options;

public class DatabaseOptions
{
    [Required(ErrorMessage = "Database name is required.")]
    [MinLength(3, ErrorMessage = "Database name cannot be empty.")]
    public string DbName { get; set; } = string.Empty;
}