import { useState } from 'react';
import { Link } from 'react-router-dom';
import { 
  Bell, 
  CheckCircle, 
  Music, 
  Info,
  Trash2,
  Check,
  AlertCircle
} from 'lucide-react';
import { useUser } from '../contexts/UserContext';

const NotificationsPage = () => {
  const { user, isLoggedIn } = useUser();

  // Mock notifications
  const [notifications, setNotifications] = useState([
    {
      id: 1,
      type: 'played',
      title: 'Zenéd lejátszva!',
      message: 'A "Bohemian Rhapsody" című kérésed lejátszásra került.',
      time: '10 perce',
      read: false
    },
    {
      id: 2,
      type: 'info',
      title: 'Új funkció!',
      message: 'Mostantól fájlokat is feltölthetsz a zene hozzáadása oldalon.',
      time: '1 órája',
      read: false
    },
    {
      id: 3,
      type: 'played',
      title: 'Zenéd lejátszva!',
      message: 'A "Blinding Lights" című kérésed lejátszásra került.',
      time: '2 órája',
      read: true
    },
    {
      id: 4,
      type: 'alert',
      title: 'Zene elutasítva',
      message: 'A kért zene nem felelt meg az irányelveinknek.',
      time: 'Tegnap',
      read: true
    },
    {
      id: 5,
      type: 'info',
      title: 'Üdvözlünk!',
      message: 'Köszönjük, hogy csatlakoztál a SuliRadio közösséghez!',
      time: '1 hete',
      read: true
    }
  ]);

  const markAsRead = (id) => {
    setNotifications(prev => 
      prev.map(n => n.id === id ? { ...n, read: true } : n)
    );
  };

  const markAllAsRead = () => {
    setNotifications(prev => prev.map(n => ({ ...n, read: true })));
  };

  const deleteNotification = (id) => {
    setNotifications(prev => prev.filter(n => n.id !== id));
  };

  const clearAll = () => {
    setNotifications([]);
  };

  const unreadCount = notifications.filter(n => !n.read).length;

  const getIcon = (type) => {
    switch (type) {
      case 'played':
        return <Music size={20} style={{ color: 'var(--color-success)' }} />;
      case 'alert':
        return <AlertCircle size={20} style={{ color: 'var(--color-error)' }} />;
      default:
        return <Info size={20} style={{ color: 'var(--color-primary)' }} />;
    }
  };

  if (!isLoggedIn || !user) {
    return (
      <div className="page">
        <div className="not-logged-in">
          <Bell size={64} />
          <h2>Nincs bejelentkezve</h2>
          <p>Az értesítések megtekintéséhez jelentkezz be.</p>
          <Link to="/login" className="btn btn-primary">Bejelentkezés</Link>
        </div>
      </div>
    );
  }

  return (
    <div className="page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <Bell size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Értesítések</h1>
            <p className="page-subtitle">
              {unreadCount > 0 ? `${unreadCount} olvasatlan értesítés` : 'Minden értesítést elolvastál'}
            </p>
          </div>
        </div>
      </section>

      {/* Actions */}
      {notifications.length > 0 && (
        <div style={{ 
          display: 'flex', 
          justifyContent: 'flex-end', 
          gap: '0.5rem',
          marginBottom: '1rem'
        }}>
          {unreadCount > 0 && (
            <button className="btn btn-secondary" onClick={markAllAsRead}>
              <Check size={18} />
              Mind olvasottnak jelölése
            </button>
          )}
          <button 
            className="btn btn-secondary" 
            onClick={clearAll}
            style={{ color: 'var(--color-error)' }}
          >
            <Trash2 size={18} />
            Összes törlése
          </button>
        </div>
      )}

      {notifications.length === 0 ? (
        <div className="not-logged-in">
          <Bell size={64} />
          <h2>Nincsenek értesítéseid</h2>
          <p>Ha történik valami fontos, itt fogsz értesülni róla.</p>
        </div>
      ) : (
        <div style={{ display: 'grid', gap: '0.75rem' }}>
          {notifications.map(notification => (
            <div 
              key={notification.id}
              style={{
                display: 'flex',
                alignItems: 'flex-start',
                gap: '1rem',
                padding: '1rem',
                background: notification.read ? 'var(--color-surface)' : 'var(--color-primary-bg)',
                border: `1px solid ${notification.read ? 'var(--color-border)' : 'var(--color-primary)'}`,
                borderRadius: 'var(--radius-lg)',
                cursor: 'pointer',
                transition: 'all var(--transition-fast)'
              }}
              onClick={() => markAsRead(notification.id)}
            >
              <div style={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                width: '2.5rem',
                height: '2.5rem',
                borderRadius: 'var(--radius-full)',
                background: 'var(--color-bg-secondary)',
                flexShrink: 0
              }}>
                {getIcon(notification.type)}
              </div>
              
              <div style={{ flex: 1, minWidth: 0 }}>
                <div style={{ 
                  display: 'flex', 
                  justifyContent: 'space-between',
                  alignItems: 'flex-start',
                  gap: '0.5rem',
                  marginBottom: '0.25rem'
                }}>
                  <h3 style={{ 
                    fontSize: '1rem', 
                    fontWeight: notification.read ? 500 : 600,
                    margin: 0
                  }}>
                    {notification.title}
                  </h3>
                  <span style={{ 
                    fontSize: '0.75rem', 
                    color: 'var(--color-text-muted)',
                    whiteSpace: 'nowrap'
                  }}>
                    {notification.time}
                  </span>
                </div>
                <p style={{ 
                  margin: 0, 
                  color: 'var(--color-text-secondary)',
                  fontSize: '0.875rem'
                }}>
                  {notification.message}
                </p>
              </div>

              <button
                onClick={(e) => {
                  e.stopPropagation();
                  deleteNotification(notification.id);
                }}
                style={{
                  background: 'none',
                  border: 'none',
                  padding: '0.5rem',
                  cursor: 'pointer',
                  color: 'var(--color-text-muted)',
                  borderRadius: 'var(--radius-sm)',
                  transition: 'all var(--transition-fast)'
                }}
                title="Értesítés törlése"
              >
                <Trash2 size={16} />
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default NotificationsPage;
