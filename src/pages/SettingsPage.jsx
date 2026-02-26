import { useState } from 'react';
import { 
  Settings as SettingsIcon, 
  User, 
  Bell, 
  Volume2, 
  Moon, 
  Sun, 
  Globe, 
  Shield, 
  Trash2,
  Save,
  RefreshCw
} from 'lucide-react';
import { useTheme } from '../contexts/ThemeContext';
import { useUser } from '../contexts/UserContext';
import { useCookies } from '../contexts/CookieContext';

const SettingsPage = () => {
  const { darkMode, toggleDarkMode, highContrast, toggleHighContrast, fontSize, setFontSize, resetFontSize } = useTheme();
  const { user, isLoggedIn, updatePreferences, logout } = useUser();
  const { resetConsent } = useCookies();

  const [settings, setSettings] = useState({
    notifications: user?.preferences?.notifications ?? true,
    emailNotifications: user?.preferences?.emailNotifications ?? false,
    autoplay: user?.preferences?.autoplay ?? false,
    volume: user?.preferences?.volume ?? 80,
    language: 'hu',
    reduceMotion: false,
  });

  const [saved, setSaved] = useState(false);

  const handleChange = (key, value) => {
    setSettings(prev => ({ ...prev, [key]: value }));
    setSaved(false);
  };

  const handleSave = () => {
    if (isLoggedIn) {
      updatePreferences(settings);
    }
    setSaved(true);
    setTimeout(() => setSaved(false), 3000);
  };

  const handleResetAll = () => {
    if (confirm('Biztosan visszaállítod az összes beállítást az alapértelmezettre?')) {
      setSettings({
        notifications: true,
        emailNotifications: false,
        autoplay: false,
        volume: 80,
        language: 'hu',
        reduceMotion: false,
      });
      resetFontSize();
      if (darkMode) toggleDarkMode();
      if (highContrast) toggleHighContrast();
      resetConsent();
    }
  };

  const handleDeleteAccount = () => {
    if (confirm('Biztosan törölni szeretnéd a fiókodat? Ez a művelet nem visszavonható!')) {
      logout();
      // Here would be the actual account deletion API call
    }
  };

  return (
    <div className="page settings-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <SettingsIcon size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Beállítások</h1>
            <p className="page-subtitle">
              Személyre szabhatod az alkalmazás működését
            </p>
          </div>
        </div>
      </section>

      {/* Success message */}
      {saved && (
        <div className="success-message" role="alert">
          <Save size={18} />
          <span>Beállítások mentve!</span>
        </div>
      )}

      <div className="settings-content">
        {/* Appearance */}
        <section className="settings-section">
          <h2>
            <Moon size={20} />
            Megjelenés
          </h2>
          
          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="dark-mode">Sötét mód</label>
              <p>Sötét háttér a szemkímélő használatért</p>
            </div>
            <label className="toggle">
              <input
                type="checkbox"
                id="dark-mode"
                checked={darkMode}
                onChange={toggleDarkMode}
              />
              <span className="toggle-slider"></span>
            </label>
          </div>

          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="high-contrast">Magas kontraszt</label>
              <p>Nagyobb kontraszt a jobb olvashatóságért</p>
            </div>
            <label className="toggle">
              <input
                type="checkbox"
                id="high-contrast"
                checked={highContrast}
                onChange={toggleHighContrast}
              />
              <span className="toggle-slider"></span>
            </label>
          </div>

          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="font-size">Betűméret</label>
              <p>Jelenleg: {fontSize}px</p>
            </div>
            <div className="font-size-controls">
              <button 
                onClick={() => setFontSize(Math.max(12, fontSize - 2))}
                disabled={fontSize <= 12}
                aria-label="Betűméret csökkentése"
              >
                A-
              </button>
              <input
                type="range"
                id="font-size"
                min="12"
                max="24"
                value={fontSize}
                onChange={(e) => setFontSize(parseInt(e.target.value))}
              />
              <button 
                onClick={() => setFontSize(Math.min(24, fontSize + 2))}
                disabled={fontSize >= 24}
                aria-label="Betűméret növelése"
              >
                A+
              </button>
            </div>
          </div>

          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="reduce-motion">Csökkentett mozgás</label>
              <p>Animációk kikapcsolása</p>
            </div>
            <label className="toggle">
              <input
                type="checkbox"
                id="reduce-motion"
                checked={settings.reduceMotion}
                onChange={(e) => handleChange('reduceMotion', e.target.checked)}
              />
              <span className="toggle-slider"></span>
            </label>
          </div>
        </section>

        {/* Notifications */}
        <section className="settings-section">
          <h2>
            <Bell size={20} />
            Értesítések
          </h2>
          
          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="notifications">Push értesítések</label>
              <p>Értesítések a zenekérésekről</p>
            </div>
            <label className="toggle">
              <input
                type="checkbox"
                id="notifications"
                checked={settings.notifications}
                onChange={(e) => handleChange('notifications', e.target.checked)}
              />
              <span className="toggle-slider"></span>
            </label>
          </div>

          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="email-notifications">Email értesítések</label>
              <p>Heti összefoglaló emailben</p>
            </div>
            <label className="toggle">
              <input
                type="checkbox"
                id="email-notifications"
                checked={settings.emailNotifications}
                onChange={(e) => handleChange('emailNotifications', e.target.checked)}
              />
              <span className="toggle-slider"></span>
            </label>
          </div>
        </section>

        {/* Audio */}
        <section className="settings-section">
          <h2>
            <Volume2 size={20} />
            Hang
          </h2>
          
          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="autoplay">Automatikus lejátszás</label>
              <p>Előnézet automatikus lejátszása</p>
            </div>
            <label className="toggle">
              <input
                type="checkbox"
                id="autoplay"
                checked={settings.autoplay}
                onChange={(e) => handleChange('autoplay', e.target.checked)}
              />
              <span className="toggle-slider"></span>
            </label>
          </div>

          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="volume">Alapértelmezett hangerő</label>
              <p>{settings.volume}%</p>
            </div>
            <input
              type="range"
              id="volume"
              min="0"
              max="100"
              value={settings.volume}
              onChange={(e) => handleChange('volume', parseInt(e.target.value))}
              className="volume-slider"
            />
          </div>
        </section>

        {/* Language */}
        <section className="settings-section">
          <h2>
            <Globe size={20} />
            Nyelv és régió
          </h2>
          
          <div className="setting-item">
            <div className="setting-info">
              <label htmlFor="language">Nyelv</label>
              <p>Az alkalmazás nyelve</p>
            </div>
            <select
              id="language"
              value={settings.language}
              onChange={(e) => handleChange('language', e.target.value)}
              className="setting-select"
            >
              <option value="hu">Magyar</option>
              <option value="en">English</option>
            </select>
          </div>
        </section>

        {/* Privacy */}
        <section className="settings-section">
          <h2>
            <Shield size={20} />
            Adatvédelem
          </h2>
          
          <div className="setting-item">
            <div className="setting-info">
              <label>Cookie beállítások</label>
              <p>Sütik kezelésének módosítása</p>
            </div>
            <button className="btn btn-secondary" onClick={resetConsent}>
              Módosítás
            </button>
          </div>

          <div className="setting-item">
            <div className="setting-info">
              <label>Adataim letöltése</label>
              <p>Összes személyes adat exportálása</p>
            </div>
            <button className="btn btn-secondary">
              Letöltés
            </button>
          </div>
        </section>

        {/* Danger zone */}
        <section className="settings-section danger-zone">
          <h2>
            <Trash2 size={20} />
            Veszélyes zóna
          </h2>
          
          <div className="setting-item">
            <div className="setting-info">
              <label>Beállítások visszaállítása</label>
              <p>Minden beállítás alapértelmezettre állítása</p>
            </div>
            <button className="btn btn-warning" onClick={handleResetAll}>
              <RefreshCw size={18} />
              Visszaállítás
            </button>
          </div>

          {isLoggedIn && (
            <div className="setting-item">
              <div className="setting-info">
                <label>Fiók törlése</label>
                <p>Fiókod és minden adatod végleges törlése</p>
              </div>
              <button className="btn btn-danger" onClick={handleDeleteAccount}>
                <Trash2 size={18} />
                Fiók törlése
              </button>
            </div>
          )}
        </section>

        {/* Save button */}
        <div className="settings-actions">
          <button className="btn btn-primary" onClick={handleSave}>
            <Save size={18} />
            Beállítások mentése
          </button>
        </div>
      </div>
    </div>
  );
};

export default SettingsPage;
