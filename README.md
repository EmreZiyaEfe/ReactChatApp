# ChatApp

## 📌 Proje Özeti
Kullanıcıların mesajlaşabildiği ve mesajlara **AI tabanlı duygu analizi** uygulandığı basit bir web + mobil uygulama geliştirildi.  
Amaç, **frontend (React / React Native), backend (.NET Core) ve AI servisi (Python + Hugging Face)** teknolojilerini uçtan uca bir zincir içinde çalıştırmak ve **ücretsiz platformlarda deploy** etmektir.  

## 🚀 Temel Özellikler (MVP)
- **React Web (Vercel)**  
  - Basit chat ekranı  
  - Kullanıcı metin yazar → Mesaj listesi + anlık duygu skoru  
- **React Native CLI (Mobil)**  
  - Aynı chat ekranının mobil sürümü (geliştirilmeye başlandı, tamamlanamadı)  
- **.NET Core API (Render)**  
  - Kullanıcı kaydı (rumuz)  
  - Mesajların veritabanına kaydı (SQLite)  
- **Python AI Servisi (Hugging Face Spaces)**  
  - Hugging Face üzerinde sentiment analysis modeli çalıştırıldı (pozitif/nötr/negatif)  

## ⚙️ Teknoloji ve Hosting
- **Frontend:** React (web) → Vercel’de deploy edildi  
- **Mobil:** React Native CLI (APK build denemeleri yapıldı, tamamlanamadı)  
- **Backend:** .NET Core + SQLite → Render üzerinde deploy denendi (Canlıya alındı fakat frontend ile bağlantı kurulamadı)  
- **AI:** Python + Hugging Face Transformers → Hugging Face Spaces üzerinde test edildi  

## 📂 Klasör Yapısı
- /frontend → React web uygulaması
- /backend → .NET Core API (SQLite DB)
- /ai-service → Python AI servisi (Hugging Face API)

  ## 📝 Eksikler ve Notlar

- Backend (.NET + Render): API tam olarak frontend ile haberleşemedi.

- React Native CLI: Mobil sürüm API/AI entegrasyonu nedeniyle tamamlanamadı.

- Hugging Face Spaces: Model başarıyla çalıştırıldı ve kullanılabilir durumda fakat gradio api problemi çıktı.

  
## 🔗 Demo Linkleri
- React Web (Vercel): [https://react-chat-app-sage-two.vercel.app/]  
- Backend API (Render): [https://reactchatapp-backend-gfyv.onrender.com]  
- AI Service (Hugging Face): [https://huggingface.co/spaces/emre1111/chatapp-ai-service]  

## 📑 Kurulum

### Backend (.NET Core)
1. `cd backend`
2. `dotnet restore`
3. `dotnet run`

### Frontend (React)
1. `cd frontend`
2. `npm install`
3. `npm start`

### AI Service (Python)
1. `cd ai-service`
2. `pip install -r requirements.txt`
3. `python app.py`


