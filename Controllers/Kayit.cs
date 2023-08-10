
using Microsoft.AspNetCore.Mvc;
using TelefonRehberi.Models.Context;
using TelefonRehberi.Models.Entities;
using Tesseract;


namespace TelefonRehberi.Controllers
{
    public class Kayit : Controller
    {
        RehberContext db = new RehberContext();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public Kayit(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string searchName)
        {
            var kisiler = db.Kisiler.ToList();

            if (!string.IsNullOrEmpty(searchName))
            {
                kisiler = kisiler.Where(k => k.Ad.Contains(searchName, StringComparison.OrdinalIgnoreCase) || k.Soyad.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(kisiler);
        }

        [HttpGet]
        public IActionResult KayitEkle()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult KayitEkle(Kisi kisi, IFormFile FotoDosyasi)
        {
            try
            {
                if (FotoDosyasi != null && FotoDosyasi.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + FotoDosyasi.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        FotoDosyasi.CopyTo(fileStream);
                    }

                    kisi.FotoYolu = "/uploads/" + uniqueFileName; 
                }

                db.Kisiler.Add(kisi);
                db.SaveChanges();
                TempData["BasariliMesaj"] = "Kayıt Başarılı";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["BasarisizMesaj"] = "Kayıt işlemi başarısız. Lütfen yeniden deneyin.";
                return RedirectToAction("KayitEkle");
            }
        }
     
        [HttpGet]
        public IActionResult Guncelle(int Id)
        {
            var kisi = db.Kisiler.Find(Id);
            if (kisi == null)
            {
                TempData["BasarisizMesaj"] = "Kayıt bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(kisi);
        }

        [HttpPost]
        public IActionResult Guncelle(Kisi kisi)
        {
           
            if (kisi == null)
            {
                TempData["BasarisizMesaj"] = "Geçersiz veri gönderildi. Lütfen tekrar deneyin.";
                return RedirectToAction("Index");
            }

            var eskiKayit = db.Kisiler.Find(kisi.Id);

           
            if (eskiKayit == null)
            {
                TempData["BasarisizMesaj"] = "Güncellenecek kayıt bulunamadı.";
                return RedirectToAction("Index");
            }

     
            eskiKayit.Ad = kisi.Ad;
            eskiKayit.Soyad = kisi.Soyad;
            eskiKayit.Numara = kisi.Numara;
            eskiKayit.Eposta = kisi.Eposta;
            eskiKayit.Adres = kisi.Adres;

            db.SaveChanges();
            TempData["BasariliMesaj"] = "Güncelleme Başarılı"; 
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Detay(int Id)
        {
            var kisi = db.Kisiler.Find(Id);
            if (kisi == null)
            {
                TempData["BasarisizMesaj"] = "Kayıt bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(kisi);
        }

        public IActionResult Sil(int Id)
        {
            var kisi = db.Kisiler.Find(Id);
            if (kisi == null)
            {
                TempData["BasarisizMesaj"] = "Kayıt bulunamadı.";
                return RedirectToAction("Index");
            }
            db.Kisiler.Remove(kisi);
            db.SaveChanges();
            TempData["BasariliMesaj"] = "Kayıt silindi.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Getir()
        {
            return View();
        }
  

        [HttpPost]
        public IActionResult Getir1(Kisi kisi, IFormFile FotoDosyasi)
        {

            try
            {
                if (FotoDosyasi != null && FotoDosyasi.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + FotoDosyasi.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        FotoDosyasi.CopyTo(fileStream);
                    }

                    kisi.FotoYolu = "/uploads/" + uniqueFileName;
                }

                db.Kisiler.Add(kisi);
                db.SaveChanges();
                TempData["BasariliMesaj"] = "Kayıt Başarılı";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["BasarisizMesaj"] = "Kayıt işlemi başarısız. Lütfen yeniden deneyin.";
                return RedirectToAction("Getir");
            }
        }

        [HttpPost]
        public IActionResult Getir(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Fotoğrafın yüklendiği geçici bir dosya oluşturun
                var imagePath = Path.GetTempFileName();

                // Yüklenen fotoğrafı geçici dosyaya kaydedin
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // Fotoğraftan metin çıkarımını gerçekleştirin
                var result = GetTextFromImage(imagePath);

                // Aldığınız Kisi nesnesini doğrudan gelen verilerle güncelleyin
                var kisi = new Kisi
                {
                    Ad = result.Ad,
                    Soyad = result.Soyad,
                    Numara = result.Numara,
                    Eposta = result.Eposta,
                    Website = result.Website,
                    Adres = result.Adres
                };

                return Json(kisi);
            }
            // Eğer fotoğraf yüklenmemişse, "Getir" sayfasını tekrar gösterin
            return View();
        }


        private Kisi GetTextFromImage(string imagePath)
        {
            using (var engine = new TesseractEngine("./tessdata", "tur+equ", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine(text);

                        string phoneNumber = string.Join(',', Extractor.ExtractPhoneNumber(text));
                        string emailAddress = string.Join(',', Extractor.ExtractEmailAddresses(text));
                        string websiteURL = Extractor.ExtractWebsiteURL(text);
                        string address = Extractor.ExtractAddress(text);
                        string name = Extractor.ExtractName(text);
                        string surname = Extractor.ExtractSurname(text);

                        Console.WriteLine("Name: " + name);
                        Console.WriteLine("Surname: " + surname);
                        Console.WriteLine("Phone Number: " + phoneNumber);
                        Console.WriteLine("Email Address: " + emailAddress);
                        Console.WriteLine("Website URL: " + websiteURL);
                        Console.WriteLine("Address: " + address);

                        var kisi = new Kisi
                        {
                            Ad = name,
                            Soyad = surname,
                            Website = websiteURL,
                            Numara = phoneNumber,
                            Eposta = emailAddress,
                            Adres = address
                        };

                        return kisi;
                    }
                }
            }
        }
    }
}
