using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KayitProgrami.Models;
using KayitProgrami.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net;

public class IzinTalebiController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IzinTalebiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    [Authorize(Roles = "Admin")] 
    public IActionResult AdminIndex(string status = "Pending")
    {
        
        ViewBag.SelectedStatus = status;
        IQueryable<IzinTalebi> izinTalepleri = _context.IzinTalepleri.Include(i => i.AspNetUsers).AsQueryable();
        if (!string.IsNullOrEmpty(status))
        {
            if (status == "Pending")
            {
                izinTalepleri = izinTalepleri.Where(i => i.Status == RequestStatus.Pending);
            }
            else if (status == "Accepted")
            {
                izinTalepleri = izinTalepleri.Where(i => i.Status == RequestStatus.Accepted);
            }
            else if (status == "Rejected")
            {
                izinTalepleri = izinTalepleri.Where(i => i.Status == RequestStatus.Rejected);
            }

            ViewBag.SelectedStatus = status; // Seçilen durumu View'e gönderir
        }
        return View(izinTalepleri.ToList());
    }

    public async Task<IActionResult> Accept(int id)
    {
      

        var request = await _context.IzinTalepleri.FindAsync(id);
        
        if(request.Status != null && request.Status == RequestStatus.Pending || request.Status == RequestStatus.Rejected )
        {         

            //izin gün sayısı
            var gunSayisi = (request.IzinTarihiBitis - request.IzinTarihiBaslangic).Days + 1;

            //kullanıcının kalan izinleri güncelle
            var kullanici = await _userManager.FindByIdAsync(request.KullaniciId);
           

            if (kullanici != null)
            {
                 kullanici.UsedLeave -= gunSayisi;
                   kullanici.AnnualLeave = kullanici.AnnualLeave - gunSayisi;
                // Yeni bir Permission kaydı oluştur
                var yeniPermission = new Permission
                {
                    KullaniciId = kullanici.Id,
                    BaslangicTarihi = request.IzinTarihiBaslangic,
                    BitisTarihi = request.IzinTarihiBitis,
                    KullanilanIzin = kullanici.UsedLeave,
                    KalanIzin = kullanici.AnnualLeave,
                    Name = kullanici.UserName
                };

                _context.Permissions.Add(yeniPermission);
                await _context.SaveChangesAsync();
                //     kullanici.UsedLeave += gunSayisi;
                //     kullanici.AnnualLeave = kullanici.AnnualLeave - gunSayisi;

                //     var userPermission = await _context.Permissions
                //.FirstOrDefaultAsync(p => p.Name == kullanici.UserName);

                //     if(userPermission != null)
                //     {
                //         userPermission.KullanilanIzin += ((request.IzinTarihiBitis - request.IzinTarihiBaslangic).Days + 1);
                //         userPermission.KalanIzin -= (request.IzinTarihiBitis - request.IzinTarihiBaslangic).Days +1 ;

                //         _context.Update(userPermission);
                //         await _context.SaveChangesAsync();
                //     }

                if (kullanici.AnnualLeave < 0)
                {
                    // izin hakkı aşılmışsa yapılacak işlemler 
                    request.Status = RequestStatus.Rejected;
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "İzin talebi reddedildi. Yıllık izin hakkınız tükenmiş.";
                }
                else
                {
                    request.Status = RequestStatus.Accepted;
                    _context.Update(kullanici);
                    await _context.SaveChangesAsync();
                }
            }
            //request.Status = RequestStatus.Accepted;
            //_context.Update(request);
            //await _context.SaveChangesAsync();

            //        // ----


            // Kullanıcıyı al


            // Kullanıcının ilk kabul edilen izin talebini bul
            //var enSonEklenenIzin = await _context.Permissions
            //      .Where(p => p.KullaniciId == kullanici.Id)
            //      .OrderByDescending(p => p.BaslangicTarihi)
            //      .FirstOrDefaultAsync();
            //var ilkKabulEdilenIzin = await _context.IzinTalepleri
            //        .Where(t => t.KullaniciId == kullanici.Id && t.Status == RequestStatus.Accepted)
            //        .OrderBy(t => t.IzinTarihiBaslangic)
            //        .FirstOrDefaultAsync();

            //    if (ilkKabulEdilenIzin != null)
            //    {
            //        // İlk kabul edilen iznin başlangıç tarihini alın
            //        var baslangicTarihi = ilkKabulEdilenIzin.IzinTarihiBaslangic;
            //        var suankiTarih = DateTime.Now;
            //    var bitisTarihi = enSonEklenenIzin.BaslangicTarihi - ilkKabulEdilenIzin.IzinTarihiBaslangic;

            //    // İlk kabul edilen tarihten itibaren kaç yıl geçtiğini hesapla
            //    int gecenYilSayisi = enSonEklenenIzin.BaslangicTarihi.Year - ilkKabulEdilenIzin.IzinTarihiBaslangic.Year ;

            //        // Yıllık izinlerin daha önce eklenip eklenmediğini kontrol etmek için

            //    if ( enSonEklenenIzin.BaslangicTarihi >= ilkKabulEdilenIzin.IzinTarihiBaslangic)
            //        // Eğer bu yıl için izin eklenmemişse, ekleyin

            //            if (enSonEklenenIzin.BaslangicTarihi <= baslangicTarihi.AddYears(gecenYilSayisi))
            //            {
            //                // Sadece bir kez 14 gün ekle
            //                kullanici.UsedLeave += 14;

            //                // Yeni bir izin kaydı ekle (opsiyonel, ekleme işlemini takip etmek için)
            //                var yeniPermission = new Permission
            //                {
            //                    KullaniciId = kullanici.Id,
            //                    BaslangicTarihi = suankiTarih,
            //                    BitisTarihi = suankiTarih,
            //                    KullanilanIzin = 0, // Bu izin için kullanılan gün sayısı yok
            //                    KalanIzin = kullanici.AnnualLeave,
            //                    Name = kullanici.UserName
            //                };
            //                _context.Permissions.Add(yeniPermission);

            //                // Kullanıcının izin bilgilerini güncelleyin
            //                _context.Update(kullanici);
            //                await _context.SaveChangesAsync();
            //            }

            //    }

            //    return RedirectToAction("Index");
            var ilkKabulEdilenIzin = await _context.IzinTalepleri
          .Where(t => t.KullaniciId == t.AspNetUsers.Id && t.Status == RequestStatus.Accepted)
          .OrderBy(t => t.IzinTarihiBaslangic)
          .FirstOrDefaultAsync();

            var sonKabulEdilenIzin = await _context.Permissions
    .Where(t => t.KullaniciId == kullanici.Id /*&& request.Status == RequestStatus.Accepted*/)
    .OrderByDescending(t => t.BaslangicTarihi)
    .FirstOrDefaultAsync();

            if (ilkKabulEdilenIzin == null)
            {
                return BadRequest("Kullanıcının kabul edilen bir izin talebi yok."); // Eğer yoksa hata döner
            }

            // İlk kabul edilen iznin üzerinden bir yıl geçmiş mi?
            if (sonKabulEdilenIzin.BaslangicTarihi >= ilkKabulEdilenIzin.IzinTarihiBaslangic.AddYears(1))
            {
                // Kullanıcının yıllık izin haklarını güncelle
                kullanici.UsedLeave += 14;
                await _userManager.UpdateAsync(kullanici);

                // Yeni bir izin kaydı oluşturun
                var yeniPermission = new Permission
                {
                    KullaniciId = kullanici.Id,
                    BaslangicTarihi = DateTime.Now,
                    BitisTarihi = DateTime.Now.AddYears(1), // Bu izin 1 yıl geçerli olur
                    KullanilanIzin = kullanici.UsedLeave,  // Toplam kullanılan izin gününü ekleriz
                    KalanIzin = kullanici.AnnualLeave,
                    Name = kullanici.UserName
                };

                _context.Permissions.Add(yeniPermission);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Kullanıcının izin hakları güncellendi ve +14 gün eklendi.";
            }
            else
            {
                TempData["Message"] = "Kullanıcının ilk kabul edilen izni üzerinden henüz bir yıl geçmemiş.";
            }

            return RedirectToAction(nameof(AdminIndex)); // Admin sayfasına yönlendirin


            // ----
        }
        return RedirectToAction(nameof(AdminIndex));
    }

    public async Task<IActionResult> Reject(int id)
    {
        var request = await _context.IzinTalepleri.FindAsync(id);
        if(request.Status != null && request.Status == RequestStatus.Pending || request.Status == RequestStatus.Accepted)
        {
            request.Status = RequestStatus.Rejected;
            _context.Update(request);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(AdminIndex));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var delete = await _context.IzinTalepleri.FindAsync(id);
        if (delete != null)
        {
            _context.Remove(delete);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(AdminIndex));
    }


    [Authorize]
    public async Task<IActionResult> Index()
    {
        var kullanici = await _userManager.GetUserAsync(User);
        var kullaniciId = kullanici.Id;

        // kullanıcının rolünü kontrol et
        var roles = await _userManager.GetRolesAsync(kullanici);

        if (roles.Contains("Admin"))
        {
            return RedirectToAction("AdminIndex");
        }
        else
        {
            var userId = _userManager.GetUserId(User);
            List<IzinTalebi> izinTalebi = _context.IzinTalepleri.Include(a => a.AspNetUsers).Where(x => x.KullaniciId == userId).ToList();

            //List<IzinTalebi> izinTalebi = _context.IzinTalepleri.Include(x=> x.AspNetUsers).ToList();
            return View(izinTalebi);
        }
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        var model = new IzinTalebiViewModel
        {
            IzinTarihiBaslangic = DateTime.Today,
            IzinTarihiBitis = DateTime.Today
        };
        return View(model);
    }
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IzinTalebiViewModel model)
    {
      
        if (ModelState.IsValid)
        {
            if (model.IsValidDates())
            {
                var kullanici = await _userManager.GetUserAsync(User);
                if (kullanici != null)
                {
                    //var permission = new Permission
                    //{
                    //    KullaniciId = kullanici.Id,
                    //    Name = kullanici.UserName,
                    //    BaslangicTarihi = model.IzinTarihiBaslangic,
                    //    BitisTarihi = model.IzinTarihiBitis,
                    //    KalanIzin = 14,
                    //    KullanilanIzin = 0
                    //};

                    //_context.Permissions.Add(permission);
                    //await _context.SaveChangesAsync();

                    var yeniIzinTalebi = new IzinTalebi
                    {
                        //Id = Guid.NewGuid().ToString(),                 

                        KullaniciId = kullanici.Id , // 2c1389da-60d9-48d4-bf49-fece6b641e3f
                        IzinTarihiBaslangic = model.IzinTarihiBaslangic,
                        IzinTarihiBitis = model.IzinTarihiBitis,
                        Aciklama = model.Aciklama,
                    };

                    _context.IzinTalepleri.Add(yeniIzinTalebi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index"); // Başarılı işlemden sonra yönlendirme
                }
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
            }else
            {
                TempData["CreateLeaveMessage"] = "Başlangıç tarihi geçerli bir tarihte olmalı ve bitiş tarihi başlangıç tarihinden sonra olmalı.";
            }

        }
        return View(model);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Permission()
    {
        var izinTalepleri = await _userManager.Users
      .Include(u => u.Permissions)
      .ToListAsync();

        //var user = await _userManager.GetUserAsync(User);
        //var roles = await _userManager.GetRolesAsync(user);


        //List<ApplicationUser> izinTalepleri = _userManager.Users.Include(a => a.IzinTalepleri).ToList();

 
        return View(izinTalepleri); // Verileri View'e gönder



    }
}
