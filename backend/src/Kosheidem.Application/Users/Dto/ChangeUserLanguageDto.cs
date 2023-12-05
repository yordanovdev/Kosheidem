using System.ComponentModel.DataAnnotations;

namespace Kosheidem.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}