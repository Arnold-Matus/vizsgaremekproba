import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import { ThemeProvider } from './contexts/ThemeContext';
import { UserProvider } from './contexts/UserContext';
import { MusicProvider } from './contexts/MusicContext';
import { CookieProvider } from './contexts/CookieContext';

import Navbar from './components/Navbar';
import Footer from './components/Footer';
import CookieBanner from './components/CookieBanner';

import HomePage from './pages/HomePage';
import SchedulePage from './pages/SchedulePage';
import RequestPage from './pages/RequestPage';
import AboutPage from './pages/AboutPage';
import ProfilePage from './pages/ProfilePage';
import SettingsPage from './pages/SettingsPage';
import AccessibilityPage from './pages/AccessibilityPage';
import PrivacyPage from './pages/PrivacyPage';
import HelpPage from './pages/HelpPage';
import NotFoundPage from './pages/NotFoundPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ForgotPasswordPage from './pages/ForgotPasswordPage';
import AddMusicPage from './pages/AddMusicPage';
import FavoritesPage from './pages/FavoritesPage';
import HistoryPage from './pages/HistoryPage';
import NotificationsPage from './pages/NotificationsPage';
import TermsPage from './pages/TermsPage';

import './styles/main.css';

// Layout wrapper to conditionally show navbar/footer
const AppLayout = () => {
  const location = useLocation();
  const isAuthPage = ['/login', '/register', '/forgot-password'].includes(location.pathname);

  return (
    <div className="app">
      <a href="#main-content" className="skip-link">
        Ugrás a tartalomhoz
      </a>
      {!isAuthPage && <Navbar />}
      <main id="main-content" className={`main-content${isAuthPage ? ' auth-page' : ''}`} style={isAuthPage ? { paddingTop: 0, padding: 0 } : {}}>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/schedule" element={<SchedulePage />} />
          <Route path="/request" element={<RequestPage />} />
          <Route path="/about" element={<AboutPage />} />
          <Route path="/profile" element={<ProfilePage />} />
          <Route path="/settings" element={<SettingsPage />} />
          <Route path="/accessibility" element={<AccessibilityPage />} />
          <Route path="/privacy" element={<PrivacyPage />} />
          <Route path="/cookies" element={<PrivacyPage />} />
          <Route path="/terms" element={<TermsPage />} />
          <Route path="/help" element={<HelpPage />} />
          <Route path="/favorites" element={<FavoritesPage />} />
          <Route path="/history" element={<HistoryPage />} />
          <Route path="/notifications" element={<NotificationsPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/forgot-password" element={<ForgotPasswordPage />} />
          <Route path="/add-music" element={<AddMusicPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </main>
      {!isAuthPage && <Footer />}
      <CookieBanner />
    </div>
  );
};

function App() {
  return (
    <ThemeProvider>
      <UserProvider>
        <MusicProvider>
          <CookieProvider>
            <Router>
              <AppLayout />
            </Router>
          </CookieProvider>
        </MusicProvider>
      </UserProvider>
    </ThemeProvider>
  );
}

export default App;
