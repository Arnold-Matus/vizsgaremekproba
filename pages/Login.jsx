import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import Cookies from 'js-cookie'
import './Auth.css'

function Login() {
  const navigate = useNavigate()
  const [formData, setFormData] = useState({
    identifier: '',
    password: ''
  })
  const [error, setError] = useState('')

  const handleChange = (e) => {
    const { name, value } = e.target
    
    // Ha az identifier mezőt módosítjuk, validáljuk
    if (name === 'identifier') {
      // Ellenőrizzük, hogy csak számokat tartalmaz-e (oktatási azonosító)
      const isEducationalId = /^\d+$/.test(value)
      
      // Ha oktatási azonosító (csak számok), akkor max 11 karakter
      if (isEducationalId && value.length > 11) {
        setError('Az oktatási azonosító maximum 11 karakter lehet')
        return
      }
    }
    
    setFormData({
      ...formData,
      [name]: value
    })
    // Töröljük a hibát ha javít
    if (error) setError('')
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    
    // Végső validáció beküldéskor
    const isEducationalId = /^\d+$/.test(formData.identifier)
    
    if (isEducationalId && formData.identifier.length !== 11) {
      setError('Az oktatási azonosító pontosan 11 karakter hosszú kell legyen')
      return
    }
    
    // Lekérjük a regisztrált felhasználókat cookie-ból
    const usersData = Cookies.get('users')
    const users = usersData ? JSON.parse(usersData) : []
    
    // Keresünk a név, email vagy oktatási azonosító alapján
    const user = users.find(u => 
      (u.name === formData.identifier || 
       u.email === formData.identifier || 
       u.educationalId === formData.identifier) && 
      u.password === formData.password
    )
    
    if (user) {
      // Sikeres bejelentkezés - cookie mentése 7 napra
      Cookies.set('currentUser', JSON.stringify(user), { expires: 7 })
      navigate('/kezdolap')
    } else {
      setError('Hibás bejelentkezési adatok')
    }
  }

  return (
    <div className="auth-container">
      <div className="auth-card">
        <div className="logo-section">
          <div className="logo-placeholder">ISKOLA LOGO</div>
        </div>
        
        <h1>Bejelentkezés</h1>
        
        {error && <div className="error-banner">{error}</div>}
        
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="identifier">Felhasználónév, Email vagy Oktatási azonosító</label>
            <input
              type="text"
              id="identifier"
              name="identifier"
              value={formData.identifier}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="password">Jelszó</label>
            <input
              type="password"
              id="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              required
            />
          </div>

          <button type="submit" className="submit-btn">
            Bejelentkezés
          </button>
        </form>

        <p className="switch-auth">
          Még nincs fiókod? <Link to="/register">Regisztrálj itt</Link>
        </p>
      </div>
    </div>
  )
}

export default Login
