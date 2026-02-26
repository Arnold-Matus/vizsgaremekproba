import { useState } from 'react';
import { Link } from 'react-router-dom';
import { 
  Radio, 
  Mail, 
  MapPin, 
  Phone,
  Heart,
  Coffee
} from 'lucide-react';
import { useUser } from '../contexts/UserContext';

const Footer = () => {
  const { sessionToken, user } = useUser();
  const [showToken, setShowToken] = useState(false);

  const currentYear = new Date().getFullYear();
  
  // Raj ID - demo value
  const rajId = 'RAJ-2026-NTT-001';

  // Creators
  const creators = [
    'Rákóczi Botond',
    'Matus Arnold',
    'Sütő Zsolt Márk'
  ];

  return (
    <footer className="footer" role="contentinfo">
      {/* Marquee section */}
      <div className="footer-marquee" aria-label="Információs szalag">
        <div className="footer-marquee-content">
          <span>🎵 SuliRadio - Az iskola hangja</span>
          <span className="marquee-separator">•</span>
          <span>Készítették: {creators.join(', ')}</span>
          <span className="marquee-separator">•</span>
          <span>Vizsgamunka 2026</span>
          <span className="marquee-separator">•</span>
          <span>❤️ Szeretettel készült</span>
          <span className="marquee-separator">•</span>
          <span>🎵 SuliRadio - Az iskola hangja</span>
          <span className="marquee-separator">•</span>
          <span>Készítették: {creators.join(', ')}</span>
          <span className="marquee-separator">•</span>
          <span>Vizsgamunka 2026</span>
          <span className="marquee-separator">•</span>
          <span>❤️ Szeretettel készült</span>
        </div>
      </div>

      <div className="footer-main">
        <div className="footer-container">
          {/* Brand section */}
          <div className="footer-section footer-brand">
            <Link to="/" className="footer-logo">
              <Radio size={28} aria-hidden="true" />
              <span>SuliRadio</span>
            </Link>
            <p className="footer-description">
              A Békéscsabai Nemes Tihamér Technikum sulirádiója. 
              Te döntöd el, mi szóljon! Kérj zenét és légy részese a közösségnek.
            </p>
            <div className="footer-social">
            </div>
          </div>

          {/* Quick links */}
          <div className="footer-section">
            <h3 className="footer-section-title">Gyors linkek</h3>
            <nav aria-label="Lábléc navigáció">
              <ul className="footer-links">
                <li><Link to="/">Főoldal</Link></li>
                <li><Link to="/schedule">Műsorrend</Link></li>
                <li><Link to="/request">Zene kérése</Link></li>
                <li><Link to="/about">Rólunk</Link></li>
                <li><Link to="/help">Súgó</Link></li>
              </ul>
            </nav>
          </div>

          {/* Legal */}
          <div className="footer-section">
            <h3 className="footer-section-title">Információk</h3>
            <ul className="footer-links">
              <li><Link to="/privacy">Adatvédelem</Link></li>
              <li><Link to="/cookies">Cookie beállítások</Link></li>
              <li><Link to="/accessibility">Akadálymentesség</Link></li>
            </ul>
          </div>

          {/* Contact */}
          <div className="footer-section">
            <h3 className="footer-section-title">Kapcsolat</h3>
            <ul className="footer-contact">
              <li>
                <MapPin size={16} aria-hidden="true" />
                <span>5600 Békéscsaba, Kazinczy u. 7.</span>
              </li>
              <li>
                <Mail size={16} aria-hidden="true" />
                <a href="mailto:suliradio@nttbcs.hu">suliradio@nttbcs.hu</a>
              </li>
              <li>
                <Phone size={16} aria-hidden="true" />
                <span>+36 66 441 459</span>
              </li>
            </ul>
          </div>
        </div>

        {/* Session info */}
        <div className="footer-session">
          <div className="footer-session-item">
            <span className="footer-session-label">Raj ID:</span>
            <code className="footer-session-value">{rajId}</code>
          </div>
          {sessionToken && (
            <div className="footer-session-item">
              <span className="footer-session-label">Session:</span>
              <button 
                className="footer-session-toggle"
                onClick={() => setShowToken(!showToken)}
                aria-expanded={showToken}
              >
                {showToken ? (
                  <code className="footer-session-value">{sessionToken}</code>
                ) : (
                  <span>••••••••••••</span>
                )}
              </button>
            </div>
          )}
          {user && (
            <div className="footer-session-item">
              <span className="footer-session-label">User ID:</span>
              <code className="footer-session-value">{user.id}</code>
            </div>
          )}
        </div>
      </div>

      {/* Copyright */}
      <div className="footer-bottom">
        <div className="footer-container">
          <p className="footer-copyright">
            © {currentYear} SuliRadio - Nemes Tihamér Technikum
          </p>
          <p className="footer-made-with">
            Készült <Heart size={14} className="heart-icon" aria-label="szeretettel" /> és 
            <Coffee size={14} aria-label="kávéval" /> Békéscsaba, Magyarország
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
