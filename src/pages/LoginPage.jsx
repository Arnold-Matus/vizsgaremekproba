import { useState, useEffect } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { useUser } from '../contexts/UserContext';

const LoginPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { login, demoLogin, isLoggedIn } = useUser();

  const [identifier, setIdentifier] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    if (isLoggedIn) {
      navigate('/', { replace: true });
    }
  }, [isLoggedIn, navigate]);

  useEffect(() => {
    if (location.state?.message) {
      setSuccessMessage(location.state.message);
    }
  }, [location]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!identifier.trim() || !password) {
      setError('Kérlek töltsd ki az összes mezőt!');
      return;
    }
    
    setIsSubmitting(true);
    setError('');
    
    try {
      const result = await login(identifier, password);
      if (result.success) {
        navigate('/');
      } else {
        setError(result.error || 'Hibás bejelentkezési adatok!');
      }
    } catch {
      setError('Hiba történt a bejelentkezés során!');
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDemoLogin = async () => {
    setIsSubmitting(true);
    setError('');
    try {
      await demoLogin();
      navigate('/');
    } catch {
      setError('Hiba történt!');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="auth-wrapper">
      <div className="auth-card">
        <div className="auth-logo">
          <div className="auth-logo-icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <path d="M8 12a4 4 0 0 0 8 0"/>
              <path d="M12 8v.01"/>
            </svg>
          </div>
          <h1>SuliRadio</h1>
        </div>

        <h2 className="auth-title">Bejelentkezés</h2>
        <p className="auth-subtitle">Üdv újra! Kérlek jelentkezz be a fiókodba.</p>

        {successMessage && (
          <div className="auth-message success">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
              <polyline points="22,4 12,14.01 9,11.01"/>
            </svg>
            <span>{successMessage}</span>
          </div>
        )}

        {error && (
          <div className="auth-message error">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <line x1="12" y1="8" x2="12" y2="12"/>
              <line x1="12" y1="16" x2="12.01" y2="16"/>
            </svg>
            <span>{error}</span>
          </div>
        )}

        <form onSubmit={handleSubmit} className="auth-form">
          <div className="form-group">
            <label htmlFor="identifier">Email vagy oktatási azonosító</label>
            <input
              type="text"
              id="identifier"
              value={identifier}
              onChange={(e) => {
                setIdentifier(e.target.value);
                setError('');
              }}
              placeholder="pelda@nttbcs.hu vagy 72345678901"
              disabled={isSubmitting}
              autoComplete="username"
              autoFocus
            />
          </div>

          <div className="form-group">
            <div className="form-label-row">
              <label htmlFor="password">Jelszó</label>
              <Link to="/forgot-password" className="form-link">
                Elfelejtett jelszó?
              </Link>
            </div>
            <div className="password-input">
              <input
                type={showPassword ? 'text' : 'password'}
                id="password"
                value={password}
                onChange={(e) => {
                  setPassword(e.target.value);
                  setError('');
                }}
                placeholder="••••••••"
                disabled={isSubmitting}
                autoComplete="current-password"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => setShowPassword(!showPassword)}
                aria-label={showPassword ? 'Jelszó elrejtése' : 'Jelszó mutatása'}
              >
                {showPassword ? (
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <path d="M17.94 17.94A10.07 10.07 0 0 1 12 20c-7 0-11-8-11-8a18.45 18.45 0 0 1 5.06-5.94M9.9 4.24A9.12 9.12 0 0 1 12 4c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24"/>
                    <line x1="1" y1="1" x2="23" y2="23"/>
                  </svg>
                ) : (
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/>
                    <circle cx="12" cy="12" r="3"/>
                  </svg>
                )}
              </button>
            </div>
          </div>

          <button type="submit" className="auth-submit" disabled={isSubmitting}>
            {isSubmitting ? (
              <>
                <span className="spinner"></span>
                Bejelentkezés...
              </>
            ) : (
              'Bejelentkezés'
            )}
          </button>
        </form>

        <div className="auth-divider">
          <span>vagy</span>
        </div>

        <button 
          type="button"
          className="auth-demo-btn"
          onClick={handleDemoLogin}
          disabled={isSubmitting}
        >
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
            <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
            <circle cx="12" cy="7" r="4"/>
          </svg>
          Demo fiók kipróbálása
        </button>

        <p className="auth-footer">
          Még nincs fiókod?{' '}
          <Link to="/register">Regisztrálj most!</Link>
        </p>
      </div>
    </div>
  );
};

export default LoginPage;
