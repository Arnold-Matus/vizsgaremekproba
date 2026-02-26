import { useState } from 'react';
import { 
  User, 
  Mail, 
  Shield, 
  Bell, 
  History, 
  Heart, 
  Edit2, 
  Save, 
  X,
  Music,
  Calendar,
  Clock,
  Award
} from 'lucide-react';
import { useUser } from '../contexts/UserContext';
import { useMusic } from '../contexts/MusicContext';

const ProfilePage = () => {
  const { user, isLoggedIn, updateUser } = useUser();
  const { getSongById, formatDuration } = useMusic();
  const [isEditing, setIsEditing] = useState(false);
  const [editData, setEditData] = useState({
    name: user?.name || '',
    email: user?.email || '',
    class: user?.class || ''
  });

  if (!isLoggedIn || !user) {
    return (
      <div className="page profile-page">
        <div className="not-logged-in">
          <User size={64} />
          <h2>Nincs bejelentkezve</h2>
          <p>A profil megtekintéséhez jelentkezz be.</p>
          <a href="/login" className="btn btn-primary">Bejelentkezés</a>
        </div>
      </div>
    );
  }

  const handleSave = () => {
    updateUser(editData);
    setIsEditing(false);
  };

  const handleCancel = () => {
    setEditData({
      name: user.name,
      email: user.email,
      class: user.class
    });
    setIsEditing(false);
  };

  const stats = [
    { icon: Music, label: 'Kért zenék', value: user.requestHistory?.length || 0 },
    { icon: Heart, label: 'Kedvencek', value: user.favorites?.length || 0 },
    { icon: Calendar, label: 'Tag óta', value: new Date(user.createdAt).toLocaleDateString('hu-HU') },
  ];

  const roleLabels = {
    admin: 'Adminisztrátor',
    teacher: 'Tanár',
    student: 'Diák'
  };

  return (
    <div className="page profile-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <User size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Profil</h1>
            <p className="page-subtitle">
              Személyes adataid és statisztikáid
            </p>
          </div>
        </div>
      </section>

      <div className="profile-content">
        {/* Profile card */}
        <div className="profile-card">
          <div className="profile-header">
            <div className="profile-avatar-large">
              {user.avatar ? (
                <img src={user.avatar} alt={user.name} />
              ) : (
                <span>{user.name.charAt(0).toUpperCase()}</span>
              )}
            </div>
            <div className="profile-actions">
              {isEditing ? (
                <>
                  <button className="btn btn-secondary" onClick={handleCancel}>
                    <X size={18} />
                    Mégse
                  </button>
                  <button className="btn btn-primary" onClick={handleSave}>
                    <Save size={18} />
                    Mentés
                  </button>
                </>
              ) : (
                <button className="btn btn-secondary" onClick={() => setIsEditing(true)}>
                  <Edit2 size={18} />
                  Szerkesztés
                </button>
              )}
            </div>
          </div>

          <div className="profile-info">
            {isEditing ? (
              <div className="profile-form">
                <div className="form-group">
                  <label htmlFor="name">
                    <User size={16} />
                    Név
                  </label>
                  <input
                    type="text"
                    id="name"
                    value={editData.name}
                    onChange={(e) => setEditData({ ...editData, name: e.target.value })}
                    className="form-input"
                  />
                </div>
                <div className="form-group">
                  <label htmlFor="email">
                    <Mail size={16} />
                    Email
                  </label>
                  <input
                    type="email"
                    id="email"
                    value={editData.email}
                    onChange={(e) => setEditData({ ...editData, email: e.target.value })}
                    className="form-input"
                  />
                </div>
                <div className="form-group">
                  <label htmlFor="class">
                    <Award size={16} />
                    Osztály
                  </label>
                  <input
                    type="text"
                    id="class"
                    value={editData.class}
                    onChange={(e) => setEditData({ ...editData, class: e.target.value })}
                    className="form-input"
                  />
                </div>
              </div>
            ) : (
              <div className="profile-details">
                <div className="profile-detail">
                  <User size={18} />
                  <span className="detail-label">Név:</span>
                  <span className="detail-value">{user.name}</span>
                </div>
                <div className="profile-detail">
                  <Mail size={18} />
                  <span className="detail-label">Email:</span>
                  <span className="detail-value">{user.email}</span>
                </div>
                <div className="profile-detail">
                  <Award size={18} />
                  <span className="detail-label">Osztály:</span>
                  <span className="detail-value">{user.class}</span>
                </div>
                <div className="profile-detail">
                  <Shield size={18} />
                  <span className="detail-label">Szerepkör:</span>
                  <span className="detail-value role-badge">{roleLabels[user.role]}</span>
                </div>
              </div>
            )}
          </div>
        </div>

        {/* Stats */}
        <div className="profile-stats">
          {stats.map(({ icon: Icon, label, value }) => (
            <div key={label} className="stat-card">
              <Icon size={24} className="stat-icon" />
              <div className="stat-info">
                <span className="stat-value">{value}</span>
                <span className="stat-label">{label}</span>
              </div>
            </div>
          ))}
        </div>

        {/* Favorites */}
        {user.favorites?.length > 0 && (
          <div className="profile-section">
            <h2>
              <Heart size={20} />
              Kedvenc zenék
            </h2>
            <div className="favorites-list">
              {user.favorites.slice(0, 5).map(songId => {
                const song = getSongById(songId);
                return song ? (
                  <div key={songId} className="favorite-item">
                    <div className="favorite-cover">
                      <Music size={20} />
                    </div>
                    <div className="favorite-info">
                      <span className="favorite-title">{song.title}</span>
                      <span className="favorite-artist">{song.artist}</span>
                    </div>
                    <span className="favorite-duration">{formatDuration(song.duration)}</span>
                  </div>
                ) : null;
              })}
            </div>
          </div>
        )}

        {/* Request history */}
        {user.requestHistory?.length > 0 && (
          <div className="profile-section">
            <h2>
              <History size={20} />
              Legutóbbi kérések
            </h2>
            <div className="history-list">
              {user.requestHistory.slice(0, 5).map((request, index) => (
                <div key={index} className="history-item">
                  <Clock size={16} />
                  <span>{new Date(request.requestedAt).toLocaleString('hu-HU')}</span>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default ProfilePage;
