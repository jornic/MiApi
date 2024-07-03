using System.ComponentModel.DataAnnotations;

namespace MiAPI.Models
{
    public class MPerson
    {
        public int IdPerson { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
