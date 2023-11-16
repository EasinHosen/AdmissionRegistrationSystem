using System.ComponentModel.DataAnnotations;

namespace AdmissionRegistrationSystem.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }

        public Guid regId { get; set; } 

        [StringLength(255)]
        public string Name { get; set; }
        
        public string PhotoUrl { get; set; }

        [StringLength(255)]
        public string FName { get; set; }

        [StringLength(255)]
        public string MName { get; set; }

        [StringLength(255)]
        public string School { get; set; }

        [StringLength(255)]
        public string College { get; set; }

        public int PermAddressId { get; set; }


        public AddressInfoModel PermAddress { get; set; }

        public int PresAddressId { get; set; }


        public AddressInfoModel PresAddress { get; set; }

        [StringLength(63)]
        public string DOB { get; set;}

        [StringLength(63)]
        public string Gender { get; set;}

        [StringLength(63)]
        public string Phone { get; set;}

        [StringLength(63)]
        public string Email { get; set;}

        [StringLength(63)]
        public string NID { get; set;}

        public int SSCId { get; set; }

        public PublicExamInfoModel SSC { get; set; }

        public int HSCId { get; set; }

        public PublicExamInfoModel HSC { get; set; }

    }
}
