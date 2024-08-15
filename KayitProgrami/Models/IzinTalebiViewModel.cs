using System;
using System.ComponentModel.DataAnnotations;

namespace KayitProgrami.Models
{
    public class IzinTalebiViewModel
    {
        [Required]
        [Display(Name = "İzin Başlangıç Tarihi")]
        public DateTime IzinTarihiBaslangic { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "İzin Bitiş Tarihi")]
        public DateTime IzinTarihiBitis { get; set; } = DateTime.Now;
        public bool IsValidDates()
        {
            return IzinTarihiBaslangic <= IzinTarihiBitis && IzinTarihiBaslangic >= DateTime.Now;
        }

        [Required]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
    }
}
