📝 My Todo App - Full Stack Görev Yönetimi
Bu proje, modern web teknolojileri kullanılarak geliştirilmiş, kullanıcı tabanlı bir görev (task) yönetim uygulamasıdır. Kullanıcılar hesap oluşturabilir, giriş yapabilir ve görevlerini Yapılacak, Yapılıyor, Bitti sütunları altında organize edebilirler.

✨ Öne Çıkan Özellikler
Kimlik Doğrulama (Auth): Kayıt olma ve giriş yapma özellikleri mevcuttur. Giriş sırasında kullanıcıyı bilgilendiren Loading Spinner desteği bulunur.

Görev Yönetimi (CRUD): Görev ekleme, listeleme, güncelleme ve silme işlemleri tam entegre çalışır.

Dinamik Arayüz: Görevler durumlarına göre sütunlara ayrılır ve anlık arama (Search) yapılabilir.

Hata Yönetimi: API tarafında oluşan doğrulama hataları ve teknik aksaklıklar (CORS, veritabanı kısıtlamaları) kullanıcıya bildirilmektedir.

Alan,Kullanılan Teknoloji
Frontend,"React, Axios, CSS3"
Backend,".NET Core WebAPI, Entity Framework Core"
Veritabanı,MS SQL Server
Araçlar,"Visual Studio, VS Code, Node.js"

📡 API Endpointleri
Uygulama, App.js dosyasında tanımlanan aşağıdaki temel endpointleri kullanmaktadır:

Auth API: https://localhost:7233/api/Auth (Login/Register)

Tasks API: https://localhost:7233/api/Tasks (Görev İşlemleri)

⚠️ Bilinen Hatalar ve Çözümleri
Validation Error (400): Görev eklerken Content alanının boş bırakılması durumunda oluşur.

DbUpdateException: Veritabanına NULL değer gönderildiğinde (özellikle Description/Content alanı) tetiklenir.

CORS Policy: React uygulaması port değiştirdiğinde (örn: 3001), backend bu yeni porta izin vermezse istekler engellenir.

📂 Bileşen Yapısı
Auth.js: Giriş ve kayıt formlarını yönetir.

TaskList.js: Görevleri sütunlar halinde listeler ve arama işlevini sunar.

TaskForm.js: Yeni görev ekleme ve düzenleme işlemlerini yapar.

TaskDetail.js: Bir göreve tıklandığında detaylı açıklamasını gösterir.