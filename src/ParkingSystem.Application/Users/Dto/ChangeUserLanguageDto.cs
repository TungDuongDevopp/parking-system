using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}