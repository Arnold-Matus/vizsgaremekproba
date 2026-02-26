# 📻 SuliRadio - Iskolai Rádió Weboldal

Modern, reszponzív iskolai rádió weboldal React-tel készítve.

![React](https://img.shields.io/badge/React-19.2.0-blue)
![Vite](https://img.shields.io/badge/Vite-7.2.4-purple)
![License](https://img.shields.io/badge/License-Apache%202.0-green)

## 🌟 Funkciók

### 👤 Felhasználói funkciók
- **Bejelentkezés/Regisztráció** - Oktatási azonosítóval vagy email címmel
- **Profil kezelés** - Személyes adatok módosítása
- **Kedvencek** - Kedvenc zenék mentése és kezelése
- **Előzmények** - Korábbi zenekérések megtekintése
- **Értesítések** - Valós idejű értesítések

### 🎵 Zene funkciók
- **Zenelista** - Böngészhető zenei könyvtár
- **Keresés** - Cím, előadó, album alapján
- **Szűrés** - Műfaj szerinti szűrés
- **Rendezés** - Többféle rendezési lehetőség
- **Zenekérés** - Zenék kérése a rádiókba
- **Zene hozzáadása** - Új zenék javaslása link vagy fájl formájában

### 📅 Információk
- **Műsorrend** - Aktuális és jövőbeli műsor
- **Most szól** - Aktuálisan lejátszott zene
- **Rólunk** - Csapat és iskola bemutatása
- **Segítség** - GYIK és súgó

### ⚙️ Beállítások
- **Sötét mód** - Automatikus és manuális témaváltás
- **Magas kontraszt** - Akadálymentesített mód
- **Cookie kezelés** - GDPR kompatibilis
- **Értesítések** - Testreszabható értesítések

## 🚀 Telepítés

```bash
# Függőségek telepítése
npm install

# Fejlesztői szerver indítása
npm run dev

# Éles build készítése
npm run build

# Build előnézete
npm run preview
```

## 📁 Projekt struktúra

```
src/
├── components/          # Újrahasználható komponensek
│   ├── Navbar.jsx      # Navigációs sáv
│   ├── Footer.jsx      # Lábléc
│   ├── SongList.jsx    # Zenelista
│   ├── NowPlaying.jsx  # Most szól widget
│   └── CookieBanner.jsx # Cookie banner
├── contexts/           # React Context-ek
│   ├── ThemeContext.jsx
│   ├── UserContext.jsx
│   ├── MusicContext.jsx
│   └── CookieContext.jsx
├── pages/              # Oldalak
│   ├── HomePage.jsx
│   ├── LoginPage.jsx
│   ├── RegisterPage.jsx
│   ├── SchedulePage.jsx
│   ├── RequestPage.jsx
│   ├── AboutPage.jsx
│   ├── ProfilePage.jsx
│   ├── SettingsPage.jsx
│   ├── FavoritesPage.jsx
│   ├── HistoryPage.jsx
│   ├── NotificationsPage.jsx
│   ├── AddMusicPage.jsx
│   ├── AccessibilityPage.jsx
│   ├── PrivacyPage.jsx
│   ├── TermsPage.jsx
│   ├── HelpPage.jsx
│   └── NotFoundPage.jsx
├── styles/
│   └── main.css        # Fő stíluslap
└── App.jsx             # Fő alkalmazás
```

## 🛠️ Technológiák

- **React 19** - UI könyvtár
- **React Router 7** - Navigáció
- **Vite 7** - Build eszköz
- **Lucide React** - Ikonok
- **CSS Custom Properties** - Testreszabható témák

## ♿ Akadálymentesítés

- ARIA címkék minden interaktív elemhez
- Billentyűzet navigáció támogatás
- Magas kontraszt mód
- Skip link a fő tartalomhoz
- Képernyőolvasó kompatibilitás

## 🔒 Adatvédelem

- GDPR kompatibilis cookie kezelés
- Adatvédelmi tájékoztató
- Felhasználási feltételek
- Biztonságos felhasználói adattárolás

## 📜 Licenc

Apache 2.0 - Lásd a [LICENSE](LICENSE) fájlt.

## 👥 Készítők

SuliRadio csapat - Vizsgaremek projekt

---

*Készült ❤️-vel diákoknak diákoktól*
