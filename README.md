## 🚀 Mission System Projesi

Bu proje, görev takip ve yönetim sistemine dair temel CRUD işlemlerini içeren bir uygulamadır. Projede Entity Framework Core ve LINQ kullanılarak veri işlemleri yapılmıştır.

---

### 🛠 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core
- LINQ
- SQL Server
- Bootstrap

---

### 📁 Proje Yapısı

- `Models/`: Veritabanı modelleri
- `Controllers/`: CRUD işlemlerinin yapıldığı controller sınıfları
- `Views/`: Razor ile yazılmış kullanıcı arayüzü sayfaları
- `Data/`: DbContext sınıfı

---

### 🎯 Temel Özellikler

- Görev ekleme
- Görev listeleme
- Görev güncelleme
- Görev silme

---

### 💻 Örnek Kodlar

**Görev Listeleme (LINQ ile):**
```csharp
var missions = _context.Missions.ToList();
return View(missions);
```

**Yeni Görev Ekleme:**
```csharp
[HttpPost]
public IActionResult AddMission(Mission mission)
{
    if(ModelState.IsValid)
    {
        _context.Missions.Add(mission);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(mission);
}
```

---

### 🧠 Öğrendiklerim

- Entity Framework ile veritabanı işlemleri
- LINQ sorguları ile veri çekme
- MVC tasarım deseni kullanımı
- View-Model-Controller yapısı
- Temel Bootstrap ile sayfa düzeni

---

### 📌 Notlar

Bu proje eğitim amaçlı geliştirilmiştir. Geliştirme sürecinde temel yazılım mimarisi, katmanlı yapı, LINQ kullanımı gibi konular pekiştirilmiştir.

---

### 📫 İletişim

Eğer projeyle ilgili sorularınız varsa veya iletişime geçmek isterseniz:

[📧 mertagralii@gmail.com](mailto:mertagralii@gmail.com)

---

Teşekkürler! 🙌
