import { useState, useRef, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { 
  Radio, 
  Home, 
  Calendar, 
  Info, 
  PlusCircle, 
  User, 
  Settings, 
  LogOut, 
  LogIn,
  Moon,
  Sun,
  Menu,
  X,
  ChevronDown,
  Bell,
  Heart,
  History,
  HelpCircle,
  Shield,
  Accessibility
} from 'lucide-react';
import { useTheme } from '../contexts/ThemeContext';
import { useUser } from '../contexts/UserContext';

const Navbar = () => {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [userMenuOpen, setUserMenuOpen] = useState(false);
  const userMenuRef = useRef(null);
  const location = useLocation();
  const { darkMode, toggleDarkMode } = useTheme();
  const { user, isLoggedIn, logout, demoLogin } = useUser();

  // Close user menu when clicking outside
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (userMenuRef.current && !userMenuRef.current.contains(event.target)) {
        setUserMenuOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  // Close mobile menu on route change
  useEffect(() => {
    setMobileMenuOpen(false);
  }, [location]);

  const navLinks = [
    { to: '/', label: 'Főoldal', icon: Home },
    { to: '/schedule', label: 'Műsorrend', icon: Calendar },
    { to: '/request', label: 'Zene kérése', icon: PlusCircle },
    { to: '/about', label: 'Rólunk', icon: Info },
  ];

  const isActive = (path) => location.pathname === path;

  return (
    <nav className="navbar" role="navigation" aria-label="Fő navigáció">
      <div className="navbar-container">
        {/* Logo */}
        <Link to="/" className="navbar-logo" aria-label="SuliRadio főoldal">
          <Radio className="navbar-logo-icon" aria-hidden="true" />
          <span className="navbar-logo-text">SuliRadio</span>
        </Link>

        {/* Desktop Navigation */}
        <div className="navbar-links">
          {navLinks.map(({ to, label, icon: Icon }) => (
            <Link
              key={to}
              to={to}
              className={`navbar-link ${isActive(to) ? 'active' : ''}`}
              aria-current={isActive(to) ? 'page' : undefined}
            >
              <Icon size={18} aria-hidden="true" />
              <span>{label}</span>
            </Link>
          ))}
        </div>

        {/* Right side controls */}
        <div className="navbar-controls">
          {/* Theme toggle */}
          <button
            className="navbar-icon-btn"
            onClick={toggleDarkMode}
            aria-label={darkMode ? 'Világos mód bekapcsolása' : 'Sötét mód bekapcsolása'}
            title={darkMode ? 'Világos mód' : 'Sötét mód'}
          >
            {darkMode ? <Sun size={20} /> : <Moon size={20} />}
          </button>

          {/* User menu */}
          <div className="navbar-user-menu" ref={userMenuRef}>
            <button
              className="navbar-user-btn"
              onClick={() => setUserMenuOpen(!userMenuOpen)}
              aria-expanded={userMenuOpen}
              aria-haspopup="true"
              aria-label="Felhasználói menü"
            >
              {isLoggedIn && user ? (
                <div className="navbar-avatar">
                  {user.avatar ? (
                    <img src={user.avatar} alt="" />
                  ) : (
                    <span>{user.name.charAt(0).toUpperCase()}</span>
                  )}
                </div>
              ) : (
                <User size={20} />
              )}
              <ChevronDown size={16} className={`chevron ${userMenuOpen ? 'open' : ''}`} />
            </button>

            {/* Dropdown menu */}
            {userMenuOpen && (
              <div className="navbar-dropdown" role="menu">
                {isLoggedIn && user ? (
                  <>
                    <div className="navbar-dropdown-header">
                      <div className="navbar-dropdown-avatar">
                        {user.avatar ? (
                          <img src={user.avatar} alt="" />
                        ) : (
                          <span>{user.name.charAt(0).toUpperCase()}</span>
                        )}
                      </div>
                      <div className="navbar-dropdown-info">
                        <span className="navbar-dropdown-name">{user.name}</span>
                        <span className="navbar-dropdown-email">{user.email}</span>
                        <span className="navbar-dropdown-class">{user.class}</span>
                      </div>
                    </div>
                    <div className="navbar-dropdown-divider" />
                    <Link to="/profile" className="navbar-dropdown-item" role="menuitem">
                      <User size={18} />
                      <span>Profil</span>
                    </Link>
                    <Link to="/favorites" className="navbar-dropdown-item" role="menuitem">
                      <Heart size={18} />
                      <span>Kedvencek</span>
                    </Link>
                    <Link to="/history" className="navbar-dropdown-item" role="menuitem">
                      <History size={18} />
                      <span>Előzmények</span>
                    </Link>
                    <Link to="/notifications" className="navbar-dropdown-item" role="menuitem">
                      <Bell size={18} />
                      <span>Értesítések</span>
                    </Link>
                    <div className="navbar-dropdown-divider" />
                    <Link to="/settings" className="navbar-dropdown-item" role="menuitem">
                      <Settings size={18} />
                      <span>Beállítások</span>
                    </Link>
                    <Link to="/accessibility" className="navbar-dropdown-item" role="menuitem">
                      <Accessibility size={18} />
                      <span>Akadálymentesség</span>
                    </Link>
                    <Link to="/help" className="navbar-dropdown-item" role="menuitem">
                      <HelpCircle size={18} />
                      <span>Súgó</span>
                    </Link>
                    <Link to="/privacy" className="navbar-dropdown-item" role="menuitem">
                      <Shield size={18} />
                      <span>Adatvédelem</span>
                    </Link>
                    <div className="navbar-dropdown-divider" />
                    <button className="navbar-dropdown-item logout" onClick={logout} role="menuitem">
                      <LogOut size={18} />
                      <span>Kijelentkezés</span>
                    </button>
                  </>
                ) : (
                  <>
                    <Link to="/login" className="navbar-dropdown-item" role="menuitem">
                      <LogIn size={18} />
                      <span>Bejelentkezés</span>
                    </Link>
                    <button className="navbar-dropdown-item" onClick={demoLogin} role="menuitem">
                      <User size={18} />
                      <span>Demo belépés</span>
                    </button>
                    <div className="navbar-dropdown-divider" />
                    <Link to="/accessibility" className="navbar-dropdown-item" role="menuitem">
                      <Accessibility size={18} />
                      <span>Akadálymentesség</span>
                    </Link>
                    <Link to="/help" className="navbar-dropdown-item" role="menuitem">
                      <HelpCircle size={18} />
                      <span>Súgó</span>
                    </Link>
                  </>
                )}
              </div>
            )}
          </div>

          {/* Mobile menu toggle */}
          <button
            className="navbar-mobile-toggle"
            onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
            aria-expanded={mobileMenuOpen}
            aria-label={mobileMenuOpen ? 'Menü bezárása' : 'Menü megnyitása'}
          >
            {mobileMenuOpen ? <X size={24} /> : <Menu size={24} />}
          </button>
        </div>
      </div>

      {/* Mobile menu */}
      {mobileMenuOpen && (
        <div className="navbar-mobile-menu" role="navigation" aria-label="Mobil navigáció">
          {navLinks.map(({ to, label, icon: Icon }) => (
            <Link
              key={to}
              to={to}
              className={`navbar-mobile-link ${isActive(to) ? 'active' : ''}`}
              aria-current={isActive(to) ? 'page' : undefined}
            >
              <Icon size={20} aria-hidden="true" />
              <span>{label}</span>
            </Link>
          ))}
        </div>
      )}
    </nav>
  );
};

export default Navbar;
