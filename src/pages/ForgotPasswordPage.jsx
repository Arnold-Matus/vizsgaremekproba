import { useState } from 'react';
import { Link } from 'react-router-dom';

const ForgotPasswordPage = () => {
  const [email, setEmail] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!email.trim()) {
      setError('Add meg az email címed!');
      return;
    }
    
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      setError('Érvénytelen email cím!');
      return;
    }
    
    setIsSubmitting(true);
    setError('');
    
    await new Promise(resolve => setTimeout(resolve, 1500));
    
    setIsSubmitting(false);
    setSuccess(true);
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

        <h2 className="auth-title">Elfelejtett jelszó</h2>
        <p className="auth-subtitle">Add meg az email címed és küldünk egy visszaállítási linket.</p>

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

        {success ? (
          <div className="auth-success-box">
            <div className="success-icon">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
                <polyline points="22,4 12,14.01 9,11.01"/>
              </svg>
            </div>
            <h3>Email elküldve!</h3>
            <p>Ha a megadott email cím szerepel a rendszerünkben, hamarosan kapsz egy linket a jelszó visszaállításához.</p>
            <Link to="/login" className="auth-submit" style={{ textDecoration: 'none', display: 'block', textAlign: 'center' }}>
              Vissza a bejelentkezéshez
            </Link>
          </div>
        ) : (
          <form onSubmit={handleSubmit} className="auth-form">
            <div className="form-group">
              <label htmlFor="email">Email cím</label>
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => {
                  setEmail(e.target.value);
                  setError('');
                }}
                placeholder="pelda@iskola.hu"
                disabled={isSubmitting}
                autoComplete="email"
                autoFocus
              />
            </div>

            <button type="submit" className="auth-submit" disabled={isSubmitting}>
              {isSubmitting ? (
                <>
                  <span className="spinner"></span>
                  Küldés...
                </>
              ) : (
                'Visszaállítási link küldése'
              )}
            </button>
          </form>
        )}

        <p className="auth-footer">
          <Link to="/login">← Vissza a bejelentkezéshez</Link>
        </p>
      </div>
    </div>
  );
};

export default ForgotPasswordPage;
