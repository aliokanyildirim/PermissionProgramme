using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KayitProgrami.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ıd otomatik artan 
        public int Id { get; set; }
        public string KullaniciId { get; set; }
        public string Name { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int KalanIzin { get; set; }
        public int KullanilanIzin { get; set; }
      

        //relational prop
        public virtual ApplicationUser Kullanici { get; set; }
    }
}
