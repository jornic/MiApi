using System.ComponentModel.DataAnnotations;

namespace MiAPI.Models
{
    public class MfirstRegister:MLogin
    {
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
