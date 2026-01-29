import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import Cookies from 'js-cookie'
import './Auth.css'

function Register() {
  const navigate = useNavigate()
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    educationalId: '',
    password: '',
    passwordConfirm: ''
  })
  const [errors, setErrors] = useState({})

  const handleChange = (e) => {
    const { name, value } = e.target
    
    // Oktatási azonosító max 11 karakter
    if (name === 'educationalId' && value.length > 11) {
      return
    }
    
    setFormData({
      ...formData,
      [name]: value
    })
    
    // Töröljük a hibát ha javít
    if (errors[name]) {
      setErrors({ ...errors, [name]: '' })
    }
  }

  const validatePassword = (password) => {
    const errors = []
    if (password.length < 8) {
      errors.push('legalább 8 karakter hosszú')
    }
    if (!/[A-Z]/.test(password)) {
      errors.push('tartalmazzon nagybetűt')
    }
    if (!/[a-z]/.test(password)) {
      errors.push('tartalmazzon kisbetűt')
    }
    if (!/[0-9]/.test(password)) {
      errors.push('tartalmazzon számot')
    }
    return errors
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    const newErrors = {}
    
    // Név ellenőrzés
    if (formData.name.trim().length < 2) {
      newErrors.name = 'A név túl rövid'
    }
    
    // Email ellenőrzés
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    if (!emailRegex.test(formData.email)) {
      newErrors.email = 'Érvénytelen email cím'
    }
    
    // Oktatási azonosító ellenőrzés
    if (formData.educationalId.length !== 11) {
      newErrors.educationalId = 'Az oktatási azonosító pontosan 11 számjegyből kell álljon'
    }
    if (!/^\d+$/.test(formData.educationalId)) {
      newErrors.educationalId = 'Az oktatási azonosító csak számokat tartalmazhat'
    }
    
    // Jelszó követelmények
    const passwordErrors = validatePassword(formData.password)
    if (passwordErrors.length > 0) {
      newErrors.password = `A jelszónak ${passwordErrors.join(', ')} kell lennie`
    }
    
    // Jelszó egyezés
    if (formData.password !== formData.passwordConfirm) {
      newErrors.passwordConfirm = 'A jelszavak nem egyeznek'
    }
    
    // Ha vannak hibák, megjelenítjük őket
    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors)
      return
    }
    
    // Ellenőrizzük, hogy már van-e ilyen felhasználó
    const usersData = Cookies.get('users')
    const users = usersData ? JSON.parse(usersData) : []
    const existingUser = users.find(u => 
      u.educationalId === formData.educationalId || 
      u.email === formData.email
    )
    
    if (existingUser) {
      if (existingUser.educationalId === formData.educationalId) {
        setErrors({ educationalId: 'Ez az oktatási azonosító már regisztrálva van' })
      } else {
        setErrors({ email: 'Ez az email cím már regisztrálva van' })
      }
      return
    }
    
    // Mentjük az új felhasználót
    const newUser = {
      name: formData.name,
      email: formData.email,
      educationalId: formData.educationalId,
      password: formData.password
    }
    
    users.push(newUser)
    // Cookie mentése 365 napra
    Cookies.set('users', JSON.stringify(users), { expires: 365 })
    
    // Sikeres regisztráció -> Login oldalra
    alert('Sikeres regisztráció! Most már bejelentkezhetsz.')
    navigate('/login')
  }

  return (
    <div className="auth-container">
      <div className="auth-card">
        <div className="logo-section">
          <div className="logo-placeholder">ISKOLA LOGO</div>
        </div>
        
        <h1>Regisztráció</h1>
        
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="name">Felhasználónév</label>
            <input
              type="text"
              id="name"
              name="name"
              value={formData.name}
              onChange={handleChange}
              required
            />
            {errors.name && <span className="error-message">{errors.name}</span>}
          </div>

          <div className="form-group">
            <label htmlFor="email">Email cím</label>
            <input
              type="email"
              id="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              placeholder="pelda@email.com"
              required
            />
            {errors.email && <span className="error-message">{errors.email}</span>}
          </div>

          <div className="form-group">
            <label htmlFor="educationalId">Oktatási azonosító (11 számjegy)</label>
            <input
              type="text"
              id="educationalId"
              name="educationalId"
              value={formData.educationalId}
              onChange={handleChange}
              placeholder="12345678901"
              maxLength="11"
              required
            />
            {errors.educationalId && <span className="error-message">{errors.educationalId}</span>}
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
            <small className="password-hint">
              Min. 8 karakter, nagybetű, kisbetű és szám
            </small>
            {errors.password && <span className="error-message">{errors.password}</span>}
          </div>

          <div className="form-group">
            <label htmlFor="passwordConfirm">Jelszó újra</label>
            <input
              type="password"
              id="passwordConfirm"
              name="passwordConfirm"
              value={formData.passwordConfirm}
              onChange={handleChange}
              required
            />
            {errors.passwordConfirm && <span className="error-message">{errors.passwordConfirm}</span>}
          </div>

          <button type="submit" className="submit-btn">
            Regisztráció
          </button>
        </form>

        <p className="switch-auth">
          Van már fiókod? <Link to="/login">Jelentkezz be itt</Link>
        </p>
      </div>
    </div>
  )
}

export default Register
