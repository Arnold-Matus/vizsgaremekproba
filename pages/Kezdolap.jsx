import { useNavigate } from 'react-router-dom'
import { useEffect, useState } from 'react'
import Cookies from 'js-cookie'

function Kezdolap() {
  const navigate = useNavigate()
  const [user, setUser] = useState(null)

  useEffect(() => {
    // Ellenőrizzük, hogy be van-e jelentkezve cookie-ból
    const currentUserData = Cookies.get('currentUser')
    if (!currentUserData) {
      navigate('/login')
      return
    }
    setUser(JSON.parse(currentUserData))
  }, [navigate])

  const handleLogout = () => {
    Cookies.remove('currentUser')
    navigate('/login')
  }

  if (!user) return null

  return (
    <div style={{ padding: '20px' }}>
      <h1>Üdvözöljük, {user.name}!</h1>
      <p>Sikeres bejelentkezés.</p>
      <button onClick={handleLogout}>Kijelentkezés</button>
    </div>
  )
}

export default Kezdolap
