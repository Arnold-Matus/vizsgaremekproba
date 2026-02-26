import { useState } from 'react';
import { Link } from 'react-router-dom';
import { 
  Heart, 
  Music, 
  Play, 
  Trash2, 
  Search,
  X,
  AlertCircle
} from 'lucide-react';
import { useUser } from '../contexts/UserContext';
import { useMusic } from '../contexts/MusicContext';

const FavoritesPage = () => {
  const { user, isLoggedIn, removeFromFavorites } = useUser();
  const { getSongById, formatDuration, requestSongs } = useMusic();
  const [searchQuery, setSearchQuery] = useState('');
  const [message, setMessage] = useState(null);

  if (!isLoggedIn || !user) {
    return (
      <div className="page">
        <div className="not-logged-in">
          <Heart size={64} />
          <h2>Nincs bejelentkezve</h2>
          <p>A kedvencek megtekintéséhez jelentkezz be.</p>
          <Link to="/login" className="btn btn-primary">Bejelentkezés</Link>
        </div>
      </div>
    );
  }

  const favorites = (user.favorites || [])
    .map(id => getSongById(id))
    .filter(song => song != null);

  const filteredFavorites = favorites.filter(song => {
    if (!searchQuery) return true;
    const query = searchQuery.toLowerCase();
    return (
      song.title.toLowerCase().includes(query) ||
      song.artist.toLowerCase().includes(query)
    );
  });

  const handleRemove = (songId) => {
    removeFromFavorites(songId);
    setMessage({ type: 'success', text: 'Eltávolítva a kedvencekből!' });
    setTimeout(() => setMessage(null), 3000);
  };

  const handleRequest = (songId) => {
    requestSongs([songId], user.id);
    setMessage({ type: 'success', text: 'Zene sikeresen kérve!' });
    setTimeout(() => setMessage(null), 3000);
  };

  return (
    <div className="page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <Heart size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Kedvencek</h1>
            <p className="page-subtitle">
              {favorites.length} kedvenc zene
            </p>
          </div>
        </div>
      </section>

      {/* Message */}
      {message && (
        <div className={`result-banner ${message.type}`} role="alert">
          {message.type === 'success' ? <Heart size={20} /> : <AlertCircle size={20} />}
          <p>{message.text}</p>
          <button onClick={() => setMessage(null)}>×</button>
        </div>
      )}

      {favorites.length === 0 ? (
        <div className="not-logged-in">
          <Heart size={64} />
          <h2>Még nincsenek kedvenceid</h2>
          <p>Böngéssz a zenék között és jelöld meg a kedvenceidet!</p>
          <Link to="/" className="btn btn-primary">Zenék böngészése</Link>
        </div>
      ) : (
        <>
          {/* Search */}
          <div className="song-list-header" style={{ marginBottom: '1rem' }}>
            <div className="search-container">
              <Search size={20} className="search-icon" />
              <input
                type="search"
                placeholder="Keresés a kedvencek között..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="search-input"
              />
              {searchQuery && (
                <button 
                  className="search-clear"
                  onClick={() => setSearchQuery('')}
                >
                  <X size={18} />
                </button>
              )}
            </div>
          </div>

          {/* Favorites list */}
          <div className="song-list-container">
            <div style={{ overflowX: 'auto' }}>
              <table className="song-table">
                <thead>
                  <tr>
                    <th style={{ width: '50%' }}>Zene</th>
                    <th>Hossz</th>
                    <th>Műfaj</th>
                    <th style={{ width: '120px' }}>Műveletek</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredFavorites.map(song => (
                    <tr key={song.id}>
                      <td>
                        <div className="song-info">
                          <div className="song-cover">
                            <Music size={20} />
                          </div>
                          <div className="song-details">
                            <span className="song-title">{song.title}</span>
                            <span className="song-artist">{song.artist}</span>
                          </div>
                        </div>
                      </td>
                      <td>{formatDuration(song.duration)}</td>
                      <td>{song.genre}</td>
                      <td>
                        <div className="song-actions">
                          <button 
                            className="song-action-btn"
                            onClick={() => handleRequest(song.id)}
                            title="Zene kérése"
                          >
                            <Play size={18} />
                          </button>
                          <button 
                            className="song-action-btn"
                            onClick={() => handleRemove(song.id)}
                            title="Eltávolítás"
                            style={{ color: 'var(--color-error)' }}
                          >
                            <Trash2 size={18} />
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            {filteredFavorites.length === 0 && searchQuery && (
              <div className="song-list-empty">
                <Search size={48} />
                <p>Nincs találat a keresésre: "{searchQuery}"</p>
              </div>
            )}
          </div>
        </>
      )}
    </div>
  );
};

export default FavoritesPage;
