using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Public
{
    public class UserNameDto
    {
        [Required]
        public string AccessTokenUser { get; set; }
        //[Required]
        //public string Status { get; set; }
        //[Required]
        //public string AccessToken { get; set; }

    }
}
