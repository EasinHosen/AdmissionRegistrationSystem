using System.ComponentModel.DataAnnotations;

namespace AdmissionRegistrationSystem.Models
{
    public class PaymentInfoModel
    {
        public int Id { get; set; }

        public Guid? transactionId { get; set; }

        [StringLength(63)]
        public string paymentStatus { get; set; } = "Pending";   //completed && pending

        public int RegistrationId { get; set; }

        public RegistrationModel Registration { get; set; }
    }
}
