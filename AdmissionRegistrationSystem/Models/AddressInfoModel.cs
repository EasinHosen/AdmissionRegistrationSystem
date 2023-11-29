using System.ComponentModel.DataAnnotations;

namespace AdmissionRegistrationSystem.Models
{
    public class AddressInfoModel
    {
        public int Id { get; set; }

        [StringLength(63)]
        public string AddressType { get; set; }

        [StringLength(255)]
        public string Provience { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string Area { get; set; }

        //public int ZipCode { get; set; }

    }
}
