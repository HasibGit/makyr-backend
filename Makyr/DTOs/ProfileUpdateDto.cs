using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class ProfileUpdateDto
{
    [Required]
    public required DateTime DateOfBirth { get; set; }
    [Required]
    public required string KnownAs { get; set; }
    [Required]
    public required string Gender { get; set; }
    [Required]
    public required string About { get; set; }
    [Required]
    public required string Interests { get; set; }
    [Required]
    public required string City { get; set; }
    [Required]
    public required string Country { get; set; }
}
