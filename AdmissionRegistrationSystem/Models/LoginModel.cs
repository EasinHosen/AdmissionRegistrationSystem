using System.ComponentModel.DataAnnotations;

namespace AdmissionRegistrationSystem.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        
        [StringLength(255)]
        public string UserName { get; set; }

        [StringLength(63)]
        public string Password { get; set; }

        [StringLength(63)]
        public string UserType { get; set; }

    }
}
