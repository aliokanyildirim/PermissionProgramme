using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KayitProgrami.Models
{
        public class IzinTalebi
        {
            public int Id { get; set; }
            [Required]
            public string KullaniciId { get; set; } // KullaniciId türü string olmalı

            [Required]
            [Display(Name = "İzin Başlangıç Tarihi")]
            public DateTime IzinTarihiBaslangic { get; set; }

            [Required]
            [Display(Name = "İzin Bitiş Tarihi")]
            public DateTime IzinTarihiBitis { get; set; }

            [Required]
            [Display(Name = "Açıklama")]
            public string Aciklama { get; set; }
            public RequestStatus Status { get; set; } //izin talebinin durumu

            public virtual ApplicationUser AspNetUsers { get; set; }

        // Kullanıcının adını tutan property
        [NotMapped] // Bu alan veritabanında saklanmaz, sadece görüntüleme amaçlıdır
        public string KullaniciAdi => AspNetUsers?.UserName;
        public int AnnualLeave => AspNetUsers?.AnnualLeave ?? 0 ; 
        public int UsedLeave => AspNetUsers?.UsedLeave ?? 0;

    }
    }
