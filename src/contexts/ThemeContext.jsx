import { createContext, useContext, useState, useEffect } from 'react';

const ThemeContext = createContext();

export const useTheme = () => {
  const context = useContext(ThemeContext);
  if (!context) {
    throw new Error('useTheme must be used within a ThemeProvider');
  }
  return context;
};

export const ThemeProvider = ({ children }) => {
  const [darkMode, setDarkMode] = useState(() => {
    const saved = localStorage.getItem('suliradio-darkmode');
    if (saved !== null) {
      return JSON.parse(saved);
    }
    return window.matchMedia('(prefers-color-scheme: dark)').matches;
  });

  const [highContrast, setHighContrast] = useState(() => {
    const saved = localStorage.getItem('suliradio-highcontrast');
    return saved ? JSON.parse(saved) : false;
  });

  const [fontSize, setFontSize] = useState(() => {
    const saved = localStorage.getItem('suliradio-fontsize');
    return saved ? parseInt(saved) : 16;
  });

  useEffect(() => {
    localStorage.setItem('suliradio-darkmode', JSON.stringify(darkMode));
    document.documentElement.classList.toggle('dark', darkMode);
  }, [darkMode]);

  useEffect(() => {
    localStorage.setItem('suliradio-highcontrast', JSON.stringify(highContrast));
    document.documentElement.classList.toggle('high-contrast', highContrast);
  }, [highContrast]);

  useEffect(() => {
    localStorage.setItem('suliradio-fontsize', fontSize.toString());
    document.documentElement.style.fontSize = `${fontSize}px`;
  }, [fontSize]);

  const toggleDarkMode = () => setDarkMode(prev => !prev);
  const toggleHighContrast = () => setHighContrast(prev => !prev);
  const increaseFontSize = () => setFontSize(prev => Math.min(prev + 2, 24));
  const decreaseFontSize = () => setFontSize(prev => Math.max(prev - 2, 12));
  const resetFontSize = () => setFontSize(16);

  return (
    <ThemeContext.Provider value={{
      darkMode,
      highContrast,
      fontSize,
      toggleDarkMode,
      toggleHighContrast,
      increaseFontSize,
      decreaseFontSize,
      resetFontSize,
      setFontSize
    }}>
      {children}
    </ThemeContext.Provider>
  );
};
