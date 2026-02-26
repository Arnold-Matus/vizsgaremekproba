import { 
  Accessibility as AccessibilityIcon, 
  Eye, 
  Type, 
  Palette, 
  MousePointer, 
  Volume2,
  Keyboard,
  Monitor
} from 'lucide-react';
import { useTheme } from '../contexts/ThemeContext';

const AccessibilityPage = () => {
  const { 
    darkMode, 
    toggleDarkMode, 
    highContrast, 
    toggleHighContrast, 
    fontSize, 
    increaseFontSize, 
    decreaseFontSize, 
    resetFontSize 
  } = useTheme();

  const accessibilityFeatures = [
    {
      icon: Eye,
      title: 'Látás',
      features: [
        'Sötét mód a szemkímélő használatért',
        'Magas kontraszt mód',
        'Állítható betűméret (12-24px)',
        'Színvak-barát színek',
      ]
    },
    {
      icon: Keyboard,
      title: 'Billentyűzet navigáció',
      features: [
        'Teljes billentyűzet-támogatás',
        'Tab navigáció minden elemhez',
        'Skip linkek a gyors navigációhoz',
        'Fókusz indikátorok',
      ]
    },
    {
      icon: Volume2,
      title: 'Képernyőolvasók',
      features: [
        'ARIA címkék minden elemhez',
        'Strukturált HTML5 elemek',
        'Alt szövegek a képekhez',
        'Szerepkör meghatározások',
      ]
    },
    {
      icon: Monitor,
      title: 'Megjelenítés',
      features: [
        'Reszponzív design minden eszközre',
        'Csökkentett mozgás opció',
        'Nagyítható tartalom',
        '200% zoom támogatás',
      ]
    },
  ];

  const shortcuts = [
    { keys: ['Tab'], description: 'Következő elemre ugrás' },
    { keys: ['Shift', 'Tab'], description: 'Előző elemre ugrás' },
    { keys: ['Enter'], description: 'Elem aktiválása' },
    { keys: ['Esc'], description: 'Menü/dialog bezárása' },
    { keys: ['Alt', 'S'], description: 'Keresés megnyitása' },
    { keys: ['Alt', 'M'], description: 'Menü megnyitása' },
  ];

  return (
    <div className="page accessibility-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <AccessibilityIcon size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Akadálymentesség</h1>
            <p className="page-subtitle">
              A SuliRadio mindenki számára elérhető
            </p>
          </div>
        </div>
      </section>

      <div className="accessibility-content">
        {/* Quick settings */}
        <section className="accessibility-quick-settings">
          <h2>Gyors beállítások</h2>
          <div className="quick-settings-grid">
            <div className="quick-setting-card">
              <Palette size={24} />
              <h3>Sötét mód</h3>
              <p>{darkMode ? 'Bekapcsolva' : 'Kikapcsolva'}</p>
              <button 
                className={`toggle-btn ${darkMode ? 'active' : ''}`}
                onClick={toggleDarkMode}
                aria-pressed={darkMode}
              >
                {darkMode ? 'Kikapcsolás' : 'Bekapcsolás'}
              </button>
            </div>

            <div className="quick-setting-card">
              <Eye size={24} />
              <h3>Magas kontraszt</h3>
              <p>{highContrast ? 'Bekapcsolva' : 'Kikapcsolva'}</p>
              <button 
                className={`toggle-btn ${highContrast ? 'active' : ''}`}
                onClick={toggleHighContrast}
                aria-pressed={highContrast}
              >
                {highContrast ? 'Kikapcsolás' : 'Bekapcsolás'}
              </button>
            </div>

            <div className="quick-setting-card">
              <Type size={24} />
              <h3>Betűméret</h3>
              <p>{fontSize}px</p>
              <div className="font-controls">
                <button 
                  onClick={decreaseFontSize}
                  disabled={fontSize <= 12}
                  aria-label="Betűméret csökkentése"
                >
                  A-
                </button>
                <button onClick={resetFontSize} aria-label="Alapértelmezett betűméret">
                  Alapértelmezett
                </button>
                <button 
                  onClick={increaseFontSize}
                  disabled={fontSize >= 24}
                  aria-label="Betűméret növelése"
                >
                  A+
                </button>
              </div>
            </div>
          </div>
        </section>

        {/* Features */}
        <section className="accessibility-features">
          <h2>Akadálymentességi funkciók</h2>
          <div className="features-grid">
            {accessibilityFeatures.map(({ icon: Icon, title, features }) => (
              <div key={title} className="feature-card">
                <Icon size={32} />
                <h3>{title}</h3>
                <ul>
                  {features.map((feature, index) => (
                    <li key={index}>{feature}</li>
                  ))}
                </ul>
              </div>
            ))}
          </div>
        </section>

        {/* Keyboard shortcuts */}
        <section className="accessibility-shortcuts">
          <h2>
            <Keyboard size={20} />
            Billentyűparancsok
          </h2>
          <div className="shortcuts-table">
            <table>
              <thead>
                <tr>
                  <th>Billentyű</th>
                  <th>Művelet</th>
                </tr>
              </thead>
              <tbody>
                {shortcuts.map(({ keys, description }, index) => (
                  <tr key={index}>
                    <td>
                      {keys.map((key, i) => (
                        <span key={i}>
                          <kbd>{key}</kbd>
                          {i < keys.length - 1 && ' + '}
                        </span>
                      ))}
                    </td>
                    <td>{description}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </section>

        {/* Standards */}
        <section className="accessibility-standards">
          <h2>Megfelelőség</h2>
          <div className="standards-content">
            <p>
              A SuliRadio megfelel a következő akadálymentességi szabványoknak:
            </p>
            <ul>
              <li>
                <strong>WCAG 2.1 Level AA</strong> - Web Content Accessibility Guidelines
              </li>
              <li>
                <strong>EU Akadálymentességi Irányelv</strong> (2016/2102)
              </li>
              <li>
                <strong>Esélyegyenlőségi törvény</strong> (2007. évi XXVI. törvény)
              </li>
            </ul>
            <p>
              Folyamatosan dolgozunk azon, hogy weboldalunk mindenki számára 
              használható legyen. Ha bármilyen akadálymentességi problémát 
              tapasztalsz, kérjük jelezd nekünk!
            </p>
          </div>
        </section>

        {/* Contact */}
        <section className="accessibility-contact">
          <h2>Segítségkérés</h2>
          <p>
            Ha akadálymentességi problémát tapasztalsz vagy segítségre van szükséged:
          </p>
          <ul>
            <li>Email: <a href="mailto:suliradio@iskola.hu">suliradio@iskola.hu</a></li>
            <li>Telefon: +36 1 234 5678</li>
          </ul>
        </section>
      </div>
    </div>
  );
};

export default AccessibilityPage;
