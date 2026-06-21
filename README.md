# Library Management API

Bu proje, modern yazılım mimarisi standartlarına uygun olarak sıfırdan geliştirilmiş bir Kütüphane Yönetimi RESTful API sistemidir.

## 🚀 Kullanılan Teknolojiler

* **Framework:** ASP.NET Core 10.0
* **Veritabanı:** Microsoft SQL Server (LocalDB)
* **ORM:** Entity Framework Core 9.0
* **Kimlik Doğrulama:** JWT (JSON Web Token)
* **Dokümantasyon:** Swagger / OpenAPI

## 🏗️ Mimari Özellikler ve Kurumsal Yaklaşımlar

* **Repository Pattern:** Veritabanı işlemleri ile iş mantığı birbirinden ayrılarak temiz bir mimari (Clean Architecture) sağlandı. Spagetti kod engellendi.
* **Pagination (Sayfalama):** Büyük veri setleriyle başa çıkabilmek ve sunucu performansını korumak için kitap listeleme işlemlerine sayfalama (Skip & Take mantığı) entegre edildi.
* **Role-Based Authentication:** Sistemdeki CRUD işlemleri yetkilendirilerek, sadece geçerli bir JWT Token'a sahip adminlerin veri ekleme/güncelleme/silme işlemi yapabilmesi sağlandı.

## ⚙️ Uç Noktalar (Endpoints)

### Auth (Kimlik Doğrulama)
* `POST /api/Auth/login`: Admin girişi yapar ve JWT Token üretir.

### Books (Kitap İşlemleri)
* `GET /api/Books`: Tüm kitapları listeler (Sayfalama destekli: `?pageNumber=1&pageSize=10`). *[Herkese Açık]*
* `GET /api/Books/{id}`: ID'sine göre tek bir kitabı getirir. *[Herkese Açık]*
* `GET /api/Books/search/{authorName}`: Yazar adına göre kitapları filtreler. *[Herkese Açık]*
* `POST /api/Books`: Sisteme yeni kitap ekler. *[Kilitli - Token Gerektirir]*
* `PUT /api/Books/{id}`: Mevcut bir kitabın bilgilerini günceller. *[Kilitli - Token Gerektirir]*
* `DELETE /api/Books/{id}`: ID'si verilen kitabı sistemden siler. *[Kilitli - Token Gerektirir]*
