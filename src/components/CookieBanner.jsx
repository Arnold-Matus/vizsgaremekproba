import { useState } from 'react';
import { X, Cookie, Settings, Shield, Check } from 'lucide-react';
import { useCookies } from '../contexts/CookieContext';

const CookieBanner = () => {
  const { showBanner, acceptAll, acceptNecessary, updateConsent } = useCookies();
  const [showDetails, setShowDetails] = useState(false);
  const [preferences, setPreferences] = useState({
    necessary: true,
    preferences: true,
    statistics: true,
    marketing: false
  });

  if (!showBanner) return null;

  const handlePreferenceChange = (key) => {
    if (key === 'necessary') return; // Cannot disable necessary cookies
    setPreferences(prev => ({ ...prev, [key]: !prev[key] }));
  };

  const handleSavePreferences = () => {
    updateConsent(preferences);
  };

  return (
    <div className="cookie-banner" role="dialog" aria-labelledby="cookie-title" aria-modal="true">
      <div className="cookie-banner-content">
        <div className="cookie-banner-header">
          <Cookie size={24} className="cookie-icon" aria-hidden="true" />
          <h2 id="cookie-title" className="cookie-title">Cookie beállítások</h2>
        </div>

        {!showDetails ? (
          <>
            <p className="cookie-text">
              Ez a weboldal sütiket (cookie-kat) használ a jobb felhasználói élmény érdekében. 
              A sütik segítenek a bejelentkezésben, a beállítások mentésében és a weboldal 
              fejlesztésében. Az Európai Unió GDPR rendelete értelmében a beleegyezésedet kérjük.
            </p>
            <div className="cookie-actions">
              <button 
                className="cookie-btn cookie-btn-secondary"
                onClick={() => setShowDetails(true)}
              >
                <Settings size={18} />
                Testreszabás
              </button>
              <button 
                className="cookie-btn cookie-btn-outline"
                onClick={acceptNecessary}
              >
                Csak szükséges
              </button>
              <button 
                className="cookie-btn cookie-btn-primary"
                onClick={acceptAll}
              >
                <Check size={18} />
                Összes elfogadása
              </button>
            </div>
          </>
        ) : (
          <>
            <div className="cookie-details">
              <div className="cookie-category">
                <div className="cookie-category-header">
                  <div className="cookie-category-info">
                    <Shield size={20} aria-hidden="true" />
                    <div>
                      <h3>Szükséges sütik</h3>
                      <p>Ezek a sütik elengedhetetlenek a weboldal működéséhez.</p>
                    </div>
                  </div>
                  <label className="cookie-toggle disabled">
                    <input 
                      type="checkbox" 
                      checked={preferences.necessary}
                      disabled
                      aria-label="Szükséges sütik (mindig aktív)"
                    />
                    <span className="cookie-toggle-slider"></span>
                  </label>
                </div>
              </div>

              <div className="cookie-category">
                <div className="cookie-category-header">
                  <div className="cookie-category-info">
                    <Settings size={20} aria-hidden="true" />
                    <div>
                      <h3>Preferencia sütik</h3>
                      <p>Beállításaid mentéséhez, mint a sötét mód vagy betűméret.</p>
                    </div>
                  </div>
                  <label className="cookie-toggle">
                    <input 
                      type="checkbox" 
                      checked={preferences.preferences}
                      onChange={() => handlePreferenceChange('preferences')}
                      aria-label="Preferencia sütik engedélyezése"
                    />
                    <span className="cookie-toggle-slider"></span>
                  </label>
                </div>
              </div>

              <div className="cookie-category">
                <div className="cookie-category-header">
                  <div className="cookie-category-info">
                    <Cookie size={20} aria-hidden="true" />
                    <div>
                      <h3>Statisztikai sütik</h3>
                      <p>Segítenek megérteni, hogyan használják a látogatók a weboldalt.</p>
                    </div>
                  </div>
                  <label className="cookie-toggle">
                    <input 
                      type="checkbox" 
                      checked={preferences.statistics}
                      onChange={() => handlePreferenceChange('statistics')}
                      aria-label="Statisztikai sütik engedélyezése"
                    />
                    <span className="cookie-toggle-slider"></span>
                  </label>
                </div>
              </div>
            </div>

            <div className="cookie-actions">
              <button 
                className="cookie-btn cookie-btn-secondary"
                onClick={() => setShowDetails(false)}
              >
                Vissza
              </button>
              <button 
                className="cookie-btn cookie-btn-primary"
                onClick={handleSavePreferences}
              >
                <Check size={18} />
                Beállítások mentése
              </button>
            </div>
          </>
        )}

        <p className="cookie-privacy-link">
          További információ az <a href="/privacy">Adatvédelmi tájékoztatóban</a>.
        </p>
      </div>
    </div>
  );
};

export default CookieBanner;
