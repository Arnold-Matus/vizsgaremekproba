import { Link } from 'react-router-dom';
import { 
  History, 
  Music, 
  Clock, 
  Calendar,
  CheckCircle,
  XCircle,
  Loader2
} from 'lucide-react';
import { useUser } from '../contexts/UserContext';
import { useMusic } from '../contexts/MusicContext';

const HistoryPage = () => {
  const { user, isLoggedIn } = useUser();
  const { getSongById, formatDuration } = useMusic();

  if (!isLoggedIn || !user) {
    return (
      <div className="page">
        <div className="not-logged-in">
          <History size={64} />
          <h2>Nincs bejelentkezve</h2>
          <p>Az előzmények megtekintéséhez jelentkezz be.</p>
          <Link to="/login" className="btn btn-primary">Bejelentkezés</Link>
        </div>
      </div>
    );
  }

  // Mock history data (in real app this would come from user.requestHistory)
  const mockHistory = [
    { id: 1, songId: 2, requestedAt: '2026-02-05T10:30:00Z', status: 'played' },
    { id: 2, songId: 5, requestedAt: '2026-02-05T09:15:00Z', status: 'played' },
    { id: 3, songId: 7, requestedAt: '2026-02-04T14:20:00Z', status: 'played' },
    { id: 4, songId: 1, requestedAt: '2026-02-04T11:00:00Z', status: 'rejected' },
    { id: 5, songId: 10, requestedAt: '2026-02-03T16:45:00Z', status: 'played' },
    { id: 6, songId: 3, requestedAt: '2026-02-03T13:30:00Z', status: 'pending' },
  ];

  const history = (user.requestHistory?.length > 0 ? user.requestHistory : mockHistory)
    .map(item => ({
      ...item,
      song: getSongById(item.songId)
    }))
    .filter(item => item.song != null);

  const getStatusIcon = (status) => {
    switch (status) {
      case 'played':
        return <CheckCircle size={18} style={{ color: 'var(--color-success)' }} />;
      case 'rejected':
        return <XCircle size={18} style={{ color: 'var(--color-error)' }} />;
      case 'pending':
        return <Loader2 size={18} style={{ color: 'var(--color-warning)' }} className="spinning" />;
      default:
        return <Clock size={18} />;
    }
  };

  const getStatusLabel = (status) => {
    switch (status) {
      case 'played': return 'Lejátszva';
      case 'rejected': return 'Elutasítva';
      case 'pending': return 'Függőben';
      default: return 'Ismeretlen';
    }
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('hu-HU', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  return (
    <div className="page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <History size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Előzmények</h1>
            <p className="page-subtitle">
              Korábbi zenekéréseid
            </p>
          </div>
        </div>
      </section>

      {history.length === 0 ? (
        <div className="not-logged-in">
          <History size={64} />
          <h2>Még nincsenek kéréseid</h2>
          <p>Kérj zenéket és itt fognak megjelenni!</p>
          <Link to="/request" className="btn btn-primary">Zene kérése</Link>
        </div>
      ) : (
        <div className="song-list-container">
          <div style={{ overflowX: 'auto' }}>
            <table className="song-table">
              <thead>
                <tr>
                  <th style={{ width: '40%' }}>Zene</th>
                  <th>Dátum</th>
                  <th>Állapot</th>
                </tr>
              </thead>
              <tbody>
                {history.map(item => (
                  <tr key={item.id}>
                    <td>
                      <div className="song-info">
                        <div className="song-cover">
                          <Music size={20} />
                        </div>
                        <div className="song-details">
                          <span className="song-title">{item.song.title}</span>
                          <span className="song-artist">{item.song.artist}</span>
                        </div>
                      </div>
                    </td>
                    <td>
                      <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem', color: 'var(--color-text-secondary)' }}>
                        <Calendar size={16} />
                        {formatDate(item.requestedAt)}
                      </div>
                    </td>
                    <td>
                      <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                        {getStatusIcon(item.status)}
                        <span>{getStatusLabel(item.status)}</span>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
};

export default HistoryPage;
