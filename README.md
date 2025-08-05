# FOV Plugin

![FOV Animation Demo 1](https://github.com/beradmc/CS2-Fov-Animation/raw/main/gif/fov.gif)
![FOV Animation Demo 2](https://github.com/beradmc/CS2-Fov-Animation/raw/main/gif/fov1.gif)

## Ne İşe Yarar?

Bu eklenti, Counter-Strike 2 sunucunuzda oyuncuların FOV (Görüş Alanı) ayarlarını özelleştirmelerine olanak tanır. Oyuncular 60-130 derece arasında FOV değeri ayarlayabilir.

## Özellikler

* ✅ Otomatik çalışır (komut gerekmez)
* ✅ Hem sohbet hem konsol komutları
* ✅ Yumuşak FOV geçiş animasyonları
* ✅ Veritabanında kalıcı kayıt
* ✅ İngilizce ve Türkçe dil desteği

## Kurulum

1. `Fov.dll` dosyasını sunucunuzun eklenti klasörüne kopyalayın:  
```  
csgo/addons/counterstrikesharp/plugins/Fov/  
```
2. Sunucunuzu yeniden başlatın
3. Eklenti otomatik olarak çalışmaya başlayacak

## Konfigürasyon

Eklenti ilk çalıştığında config dosyası şu konumda oluşturulur:
```
csgo/addons/counterstrikesharp/configs/plugins/Fov/Fov.json
```

**Örnek Config:**
```json
{
  "DefaultFov": 90,                    // Yeni oyuncular için varsayılan FOV değeri
  "Tag": "{GOLD}[FOV]{DEFAULT}",       // Sohbet mesajlarındaki etiket
  "DatabaseHost": "localhost",          // Veritabanı sunucu adresi
  "DatabaseName": "fovdb",             // Veritabanı adı
  "DatabaseUser": "fovuser",           // Veritabanı kullanıcı adı
  "DatabasePassword": "password",       // Veritabanı şifresi
  "FOVMin": 60,                        // Minimum FOV değeri
  "FOVMax": 130,                       // Maksimum FOV değeri
  "EnableFovAnimation": true,          // FOV animasyonlarını etkinleştir
  "FovAnimationDuration": 0.3          // Animasyon süresi (saniye)
}
```

## Komutlar

**Sohbet Komutları:**
- `!fov <değer>` - FOV'unu ayarla (60-130 aralığı)
- `!fovreset` - FOV'u varsayılan değere sıfırla

## Gereksinimler

* [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)

---