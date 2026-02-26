import { createContext, useContext, useState, useEffect } from 'react';

const CookieContext = createContext();

export const useCookies = () => {
  const context = useContext(CookieContext);
  if (!context) {
    throw new Error('useCookies must be used within a CookieProvider');
  }
  return context;
};

export const CookieProvider = ({ children }) => {
  const [cookieConsent, setCookieConsent] = useState(() => {
    const saved = localStorage.getItem('suliradio-cookie-consent');
    return saved ? JSON.parse(saved) : null;
  });

  const [showBanner, setShowBanner] = useState(() => {
    return !localStorage.getItem('suliradio-cookie-consent');
  });

  useEffect(() => {
    if (cookieConsent !== null) {
      localStorage.setItem('suliradio-cookie-consent', JSON.stringify(cookieConsent));
    }
  }, [cookieConsent]);

  const acceptAll = () => {
    const consent = {
      necessary: true,
      preferences: true,
      statistics: true,
      marketing: false, // School project, no marketing
      timestamp: new Date().toISOString()
    };
    setCookieConsent(consent);
    setShowBanner(false);
  };

  const acceptNecessary = () => {
    const consent = {
      necessary: true,
      preferences: false,
      statistics: false,
      marketing: false,
      timestamp: new Date().toISOString()
    };
    setCookieConsent(consent);
    setShowBanner(false);
  };

  const updateConsent = (newConsent) => {
    setCookieConsent({
      ...newConsent,
      necessary: true, // Always required
      timestamp: new Date().toISOString()
    });
    setShowBanner(false);
  };

  const resetConsent = () => {
    localStorage.removeItem('suliradio-cookie-consent');
    setCookieConsent(null);
    setShowBanner(true);
  };

  const hasConsent = (type) => {
    if (!cookieConsent) return false;
    return cookieConsent[type] === true;
  };

  return (
    <CookieContext.Provider value={{
      cookieConsent,
      showBanner,
      setShowBanner,
      acceptAll,
      acceptNecessary,
      updateConsent,
      resetConsent,
      hasConsent
    }}>
      {children}
    </CookieContext.Provider>
  );
};
