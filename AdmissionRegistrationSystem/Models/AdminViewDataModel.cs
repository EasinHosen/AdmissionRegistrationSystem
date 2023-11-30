namespace AdmissionRegistrationSystem.Models
{
    public class AdminViewDataModel
    {
        public RegistrationModel registrations { get; set; }
        public string paymentStatus { get; set; }

        public Guid? transaactionId { get; set; }
    }
}
