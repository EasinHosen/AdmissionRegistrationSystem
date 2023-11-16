using System.ComponentModel.DataAnnotations;

namespace AdmissionRegistrationSystem.Models
{
    public class PublicExamInfoModel
    {
        public int Id { get; set; }

        [StringLength(63)]
        public string ExamType { get; set; }

        [StringLength(63)]
        public string Section { get; set; }

        [StringLength(63)]
        public string Roll { get; set; }

        [StringLength(63)]
        public string Result { get; set; }

        [StringLength(63)]
        public string Board { get; set; }

        [StringLength(63)]
        public string PassingYear { get; set; }
    }
}
