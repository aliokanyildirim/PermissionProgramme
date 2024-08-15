using Microsoft.AspNetCore.Identity;

namespace KayitProgrami.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Ekstra özellikler buraya eklenebilir
        public ICollection<IzinTalebi> IzinTalepleri { get; set; }
        public UserRole Role { get; set; } // Kullanıcının rolü
        public int AnnualLeave { get; set; } = 14;
        public int UsedLeave { get; set; } = 0;
        public ICollection<Permission> Permissions { get; set; }
    }
}
    