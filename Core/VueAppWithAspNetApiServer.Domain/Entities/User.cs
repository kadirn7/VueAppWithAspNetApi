
using System.ComponentModel.DataAnnotations;


namespace VueAppWithAspNetApiServer.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }


    }
}
