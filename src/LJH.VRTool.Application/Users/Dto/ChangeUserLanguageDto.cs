using System.ComponentModel.DataAnnotations;

namespace LJH.VRTool.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}