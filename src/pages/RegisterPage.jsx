import { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useUser } from '../contexts/UserContext';

const RegisterPage = () => {
  const navigate = useNavigate();
  const { register, isLoggedIn } = useUser();

  const [formData, setFormData] = useState({
    name: '',
    email: '',
    educationalId: '',
    classGroup: '',
    password: '',
    passwordConfirm: '',
    acceptTerms: false,
    acceptPrivacy: false
  });
  const [showPassword, setShowPassword] = useState(false);
  const [showPasswordConfirm, setShowPasswordConfirm] = useState(false);
  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [registerError, setRegisterError] = useState('');
  const [registerSuccess, setRegisterSuccess] = useState(false);

  useEffect(() => {
    if (isLoggedIn) {
      navigate('/', { replace: true });
    }
  }, [isLoggedIn, navigate]);

  const passwordRequirements = [
    { id: 'length', label: 'Legalább 8 karakter', test: (p) => p.length >= 8 },
    { id: 'upper', label: 'Nagybetű (A-Z)', test: (p) => /[A-Z]/.test(p) },
    { id: 'lower', label: 'Kisbetű (a-z)', test: (p) => /[a-z]/.test(p) },
    { id: 'number', label: 'Szám (0-9)', test: (p) => /[0-9]/.test(p) },
  ];

  const passwordStrength = passwordRequirements.filter(req => req.test(formData.password)).length;

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    
    if (name === 'educationalId') {
      if (value.length > 11 || (value && !/^\d*$/.test(value))) return;
    }
    
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
    
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: '' }));
    }
    if (registerError) {
      setRegisterError('');
    }
  };

  const validateForm = () => {
    const newErrors = {};
    
    if (!formData.name.trim()) newErrors.name = 'Add meg a neved!';
    
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!formData.email.trim()) {
      newErrors.email = 'Add meg az email címed!';
    } else if (!emailRegex.test(formData.email)) {
      newErrors.email = 'Érvénytelen email cím!';
    }
    
    if (!formData.educationalId) {
      newErrors.educationalId = 'Add meg az oktatási azonosítód!';
    } else if (formData.educationalId.length !== 11) {
      newErrors.educationalId = 'Pontosan 11 számjegy!';
    }
    
    if (!formData.classGroup.trim()) newErrors.classGroup = 'Add meg az osztályod!';
    
    if (!formData.password) {
      newErrors.password = 'Add meg a jelszavad!';
    } else if (passwordStrength < 4) {
      newErrors.password = 'A jelszó nem elég erős!';
    }
    
    if (!formData.passwordConfirm) {
      newErrors.passwordConfirm = 'Erősítsd meg a jelszavad!';
    } else if (formData.password !== formData.passwordConfirm) {
      newErrors.passwordConfirm = 'A jelszavak nem egyeznek!';
    }
    
    if (!formData.acceptTerms) newErrors.acceptTerms = 'Kötelező elfogadni!';
    if (!formData.acceptPrivacy) newErrors.acceptPrivacy = 'Kötelező elfogadni!';
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) return;
    
    setIsSubmitting(true);
    setRegisterError('');
    
    try {
      const result = await register({
        name: formData.name,
        email: formData.email,
        educationalId: formData.educationalId,
        classGroup: formData.classGroup,
        password: formData.password
      });
      
      if (result.success) {
        setRegisterSuccess(true);
        setTimeout(() => {
          navigate('/login', { state: { message: 'Sikeres regisztráció! Most már bejelentkezhetsz.' }});
        }, 1500);
      } else {
        setRegisterError(result.error || 'Hiba történt a regisztráció során!');
      }
    } catch {
      setRegisterError('Hiba történt. Próbáld újra később!');
    } finally {
      setIsSubmitting(false);
    }
  };

  const getStrengthColor = () => {
    if (passwordStrength <= 1) return '#ef4444';
    if (passwordStrength === 2) return '#f97316';
    if (passwordStrength === 3) return '#eab308';
    return '#22c55e';
  };

  const getStrengthText = () => {
    if (passwordStrength <= 1) return 'Gyenge';
    if (passwordStrength === 2) return 'Közepes';
    if (passwordStrength === 3) return 'Jó';
    return 'Erős';
  };

  return (
    <div className="auth-wrapper">
      <div className="auth-card auth-card-wide">
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

        <h2 className="auth-title">Regisztráció</h2>
        <p className="auth-subtitle">Hozd létre a fiókodat és csatlakozz hozzánk!</p>

        {registerError && (
          <div className="auth-message error">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <line x1="12" y1="8" x2="12" y2="12"/>
              <line x1="12" y1="16" x2="12.01" y2="16"/>
            </svg>
            <span>{registerError}</span>
          </div>
        )}

        {registerSuccess && (
          <div className="auth-message success">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
              <polyline points="22,4 12,14.01 9,11.01"/>
            </svg>
            <span>Sikeres regisztráció! Átirányítás...</span>
          </div>
        )}

        <form onSubmit={handleSubmit} className="auth-form">
          <div className={`form-group ${errors.name ? 'has-error' : ''}`}>
            <label htmlFor="name">Teljes név</label>
            <input
              type="text"
              id="name"
              name="name"
              value={formData.name}
              onChange={handleChange}
              placeholder="Kovács Péter"
              disabled={isSubmitting}
              autoComplete="name"
            />
            {errors.name && <span className="error-text">{errors.name}</span>}
          </div>

          <div className={`form-group ${errors.email ? 'has-error' : ''}`}>
            <label htmlFor="email">Email cím</label>
            <input
              type="email"
              id="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              placeholder="pelda@nttbcs.hu"
              disabled={isSubmitting}
              autoComplete="email"
            />
            {errors.email && <span className="error-text">{errors.email}</span>}
          </div>

          <div className="form-row">
            <div className={`form-group ${errors.educationalId ? 'has-error' : ''}`}>
              <label htmlFor="educationalId">Oktatási azonosító</label>
              <input
                type="text"
                id="educationalId"
                name="educationalId"
                value={formData.educationalId}
                onChange={handleChange}
                placeholder="72345678901"
                maxLength={11}
                disabled={isSubmitting}
              />
              {errors.educationalId && <span className="error-text">{errors.educationalId}</span>}
            </div>

            <div className={`form-group ${errors.classGroup ? 'has-error' : ''}`}>
              <label htmlFor="classGroup">Osztály</label>
              <input
                type="text"
                id="classGroup"
                name="classGroup"
                value={formData.classGroup}
                onChange={handleChange}
                placeholder="12.A"
                disabled={isSubmitting}
              />
              {errors.classGroup && <span className="error-text">{errors.classGroup}</span>}
            </div>
          </div>

          <div className={`form-group ${errors.password ? 'has-error' : ''}`}>
            <label htmlFor="password">Jelszó</label>
            <div className="password-input">
              <input
                type={showPassword ? 'text' : 'password'}
                id="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                placeholder="••••••••"
                disabled={isSubmitting}
                autoComplete="new-password"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => setShowPassword(!showPassword)}
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

            {formData.password && (
              <div className="password-strength">
                <div className="strength-bar">
                  <div 
                    className="strength-fill"
                    style={{ 
                      width: `${(passwordStrength / 4) * 100}%`,
                      backgroundColor: getStrengthColor()
                    }}
                  />
                </div>
                <span style={{ color: getStrengthColor() }}>{getStrengthText()}</span>
              </div>
            )}

            <div className="password-requirements">
              {passwordRequirements.map((req) => (
                <div key={req.id} className={`requirement ${req.test(formData.password) ? 'met' : ''}`}>
                  {req.test(formData.password) ? (
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                      <polyline points="20,6 9,17 4,12"/>
                    </svg>
                  ) : (
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                      <line x1="18" y1="6" x2="6" y2="18"/>
                      <line x1="6" y1="6" x2="18" y2="18"/>
                    </svg>
                  )}
                  <span>{req.label}</span>
                </div>
              ))}
            </div>
            {errors.password && <span className="error-text">{errors.password}</span>}
          </div>

          <div className={`form-group ${errors.passwordConfirm ? 'has-error' : ''}`}>
            <label htmlFor="passwordConfirm">Jelszó megerősítése</label>
            <div className="password-input">
              <input
                type={showPasswordConfirm ? 'text' : 'password'}
                id="passwordConfirm"
                name="passwordConfirm"
                value={formData.passwordConfirm}
                onChange={handleChange}
                placeholder="••••••••"
                disabled={isSubmitting}
                autoComplete="new-password"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => setShowPasswordConfirm(!showPasswordConfirm)}
              >
                {showPasswordConfirm ? (
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
            {formData.passwordConfirm && formData.password === formData.passwordConfirm && (
              <span className="success-text">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <polyline points="20,6 9,17 4,12"/>
                </svg>
                A jelszavak egyeznek
              </span>
            )}
            {errors.passwordConfirm && <span className="error-text">{errors.passwordConfirm}</span>}
          </div>

          <div className="form-checkboxes">
            <label className={`checkbox-label ${errors.acceptTerms ? 'has-error' : ''}`}>
              <input
                type="checkbox"
                name="acceptTerms"
                checked={formData.acceptTerms}
                onChange={handleChange}
                disabled={isSubmitting}
              />
              <span className="checkbox-custom"></span>
              <span>Elfogadom a <Link to="/terms" target="_blank">használati szabályokat</Link></span>
            </label>

            <label className={`checkbox-label ${errors.acceptPrivacy ? 'has-error' : ''}`}>
              <input
                type="checkbox"
                name="acceptPrivacy"
                checked={formData.acceptPrivacy}
                onChange={handleChange}
                disabled={isSubmitting}
              />
              <span className="checkbox-custom"></span>
              <span>Elfogadom az <Link to="/privacy" target="_blank">adatkezelési tájékoztatót</Link></span>
            </label>
          </div>

          <button type="submit" className="auth-submit" disabled={isSubmitting || registerSuccess}>
            {isSubmitting ? (
              <>
                <span className="spinner"></span>
                Regisztráció...
              </>
            ) : registerSuccess ? (
              'Sikeres regisztráció!'
            ) : (
              'Regisztráció'
            )}
          </button>
        </form>

        <p className="auth-footer">
          Már van fiókod?{' '}
          <Link to="/login">Jelentkezz be!</Link>
        </p>
      </div>
    </div>
  );
};

export default RegisterPage;
