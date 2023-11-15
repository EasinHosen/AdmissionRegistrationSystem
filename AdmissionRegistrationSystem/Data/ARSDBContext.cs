using Microsoft.EntityFrameworkCore;
using AdmissionRegistrationSystem.Models;

namespace AdmissionRegistrationSystem.Data
{
    public class ARSDBContext : DbContext
    {
        public ARSDBContext (DbContextOptions<ARSDBContext> options) : base(options) { }

        public DbSet<AdmissionRegistrationSystem.Models.LoginModel> Logins { get; set; }
        public DbSet<AdmissionRegistrationSystem.Models.RegistrationModel> Registrations { get; set; }
        public DbSet<AdmissionRegistrationSystem.Models.AddressInfoModel> Addresses { get; set; }
        public DbSet<AdmissionRegistrationSystem.Models.PublicExamInfoModel> ExamInfos { get; set; }
    }
}