import { useState } from 'react';
import { 
  PlusCircle, 
  Link as LinkIcon, 
  Upload, 
  Search, 
  Music, 
  Send, 
  CheckCircle, 
  AlertCircle,
  Info,
  Loader2
} from 'lucide-react';
import { useMusic } from '../contexts/MusicContext';
import { useUser } from '../contexts/UserContext';

const RequestPage = () => {
  const { songs, filteredSongs, searchQuery, setSearchQuery, selectedSongs, toggleSongSelection, requestSongs, formatDuration } = useMusic();
  const { user, isLoggedIn } = useUser();

  const [activeTab, setActiveTab] = useState('library');
  const [linkUrl, setLinkUrl] = useState('');
  const [linkTitle, setLinkTitle] = useState('');
  const [linkArtist, setLinkArtist] = useState('');
  const [message, setMessage] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submitResult, setSubmitResult] = useState(null);

  const handleLibraryRequest = async () => {
    if (!isLoggedIn) {
      setSubmitResult({ type: 'error', message: 'Kérlek jelentkezz be a zenék kéréséhez!' });
      return;
    }
    if (selectedSongs.length === 0) {
      setSubmitResult({ type: 'error', message: 'Válassz ki legalább egy zenét!' });
      return;
    }

    setIsSubmitting(true);
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1000));
    requestSongs(selectedSongs, user?.id);
    setSubmitResult({ type: 'success', message: `${selectedSongs.length} zene sikeresen kérve!` });
    setIsSubmitting(false);
  };

  const handleLinkRequest = async (e) => {
    e.preventDefault();
    if (!isLoggedIn) {
      setSubmitResult({ type: 'error', message: 'Kérlek jelentkezz be a zenék kéréséhez!' });
      return;
    }
    if (!linkUrl.trim()) {
      setSubmitResult({ type: 'error', message: 'Add meg a zene linkjét!' });
      return;
    }

    setIsSubmitting(true);
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1500));
    setSubmitResult({ type: 'success', message: 'Kérésed elküldve! Hamarosan feldolgozzuk.' });
    setLinkUrl('');
    setLinkTitle('');
    setLinkArtist('');
    setMessage('');
    setIsSubmitting(false);
  };

  const displayedSongs = filteredSongs();

  return (
    <div className="page request-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <PlusCircle size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Zene kérése</h1>
            <p className="page-subtitle">
              Kérj zenét a könyvtárból vagy küldj be egy linket!
            </p>
          </div>
        </div>
      </section>

      {/* Login warning */}
      {!isLoggedIn && (
        <div className="warning-banner" role="alert">
          <AlertCircle size={20} />
          <p>
            A zenék kéréséhez be kell jelentkezned. 
            <a href="/login"> Jelentkezz be</a> vagy 
            <a href="/register"> regisztrálj</a>!
          </p>
        </div>
      )}

      {/* Submit result */}
      {submitResult && (
        <div className={`result-banner ${submitResult.type}`} role="alert">
          {submitResult.type === 'success' ? <CheckCircle size={20} /> : <AlertCircle size={20} />}
          <p>{submitResult.message}</p>
          <button onClick={() => setSubmitResult(null)} aria-label="Bezárás">×</button>
        </div>
      )}

      {/* Tabs */}
      <div className="request-tabs" role="tablist">
        <button
          role="tab"
          aria-selected={activeTab === 'library'}
          className={`request-tab ${activeTab === 'library' ? 'active' : ''}`}
          onClick={() => setActiveTab('library')}
        >
          <Music size={18} />
          <span>Könyvtárból</span>
        </button>
        <button
          role="tab"
          aria-selected={activeTab === 'link'}
          className={`request-tab ${activeTab === 'link' ? 'active' : ''}`}
          onClick={() => setActiveTab('link')}
        >
          <LinkIcon size={18} />
          <span>Link beküldése</span>
        </button>
      </div>

      {/* Tab content */}
      <div className="request-content">
        {activeTab === 'library' && (
          <div className="library-request" role="tabpanel">
            <div className="library-header">
              <div className="search-container">
                <Search size={20} className="search-icon" />
                <input
                  type="search"
                  placeholder="Keresés a könyvtárban..."
                  value={searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                  className="search-input"
                  aria-label="Keresés"
                />
              </div>
              {selectedSongs.length > 0 && (
                <button 
                  className="submit-btn"
                  onClick={handleLibraryRequest}
                  disabled={isSubmitting || !isLoggedIn}
                >
                  {isSubmitting ? (
                    <Loader2 size={18} className="spinning" />
                  ) : (
                    <Send size={18} />
                  )}
                  <span>Kérés ({selectedSongs.length})</span>
                </button>
              )}
            </div>

            <div className="library-songs">
              {displayedSongs.length === 0 ? (
                <div className="no-songs">
                  <Music size={48} />
                  <p>Nincs találat</p>
                </div>
              ) : (
                <div className="songs-grid">
                  {displayedSongs.map(song => (
                    <div 
                      key={song.id}
                      className={`song-card ${selectedSongs.includes(song.id) ? 'selected' : ''}`}
                      onClick={() => toggleSongSelection(song.id)}
                      role="checkbox"
                      aria-checked={selectedSongs.includes(song.id)}
                      tabIndex={0}
                      onKeyDown={(e) => e.key === 'Enter' && toggleSongSelection(song.id)}
                    >
                      <div className="song-card-cover">
                        {song.coverUrl ? (
                          <img src={song.coverUrl} alt="" />
                        ) : (
                          <div className="cover-placeholder">
                            <Music size={32} />
                          </div>
                        )}
                        {selectedSongs.includes(song.id) && (
                          <div className="selected-overlay">
                            <CheckCircle size={32} />
                          </div>
                        )}
                      </div>
                      <div className="song-card-info">
                        <h3 className="song-card-title">{song.title}</h3>
                        <p className="song-card-artist">{song.artist}</p>
                        <span className="song-card-duration">{formatDuration(song.duration)}</span>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          </div>
        )}

        {activeTab === 'link' && (
          <div className="link-request" role="tabpanel">
            <form onSubmit={handleLinkRequest} className="link-form">
              <div className="form-group">
                <label htmlFor="link-url">
                  <LinkIcon size={16} />
                  Zene linkje *
                </label>
                <input
                  type="url"
                  id="link-url"
                  value={linkUrl}
                  onChange={(e) => setLinkUrl(e.target.value)}
                  placeholder="https://youtube.com/watch?v=..."
                  required
                  className="form-input"
                />
                <span className="form-hint">YouTube, Spotify vagy más streaming link</span>
              </div>

              <div className="form-row">
                <div className="form-group">
                  <label htmlFor="link-title">
                    <Music size={16} />
                    Zene címe
                  </label>
                  <input
                    type="text"
                    id="link-title"
                    value={linkTitle}
                    onChange={(e) => setLinkTitle(e.target.value)}
                    placeholder="Pl.: Bohemian Rhapsody"
                    className="form-input"
                  />
                </div>

                <div className="form-group">
                  <label htmlFor="link-artist">
                    <Music size={16} />
                    Előadó
                  </label>
                  <input
                    type="text"
                    id="link-artist"
                    value={linkArtist}
                    onChange={(e) => setLinkArtist(e.target.value)}
                    placeholder="Pl.: Queen"
                    className="form-input"
                  />
                </div>
              </div>

              <div className="form-group">
                <label htmlFor="message">
                  <Info size={16} />
                  Megjegyzés
                </label>
                <textarea
                  id="message"
                  value={message}
                  onChange={(e) => setMessage(e.target.value)}
                  placeholder="Opcionális üzenet a rádiósoknak..."
                  className="form-textarea"
                  rows={3}
                />
              </div>

              <button 
                type="submit" 
                className="submit-btn full-width"
                disabled={isSubmitting || !isLoggedIn}
              >
                {isSubmitting ? (
                  <Loader2 size={18} className="spinning" />
                ) : (
                  <Send size={18} />
                )}
                <span>Kérés elküldése</span>
              </button>
            </form>

            <div className="link-info">
              <h3><Info size={18} /> Tudnivalók</h3>
              <ul>
                <li>A linkkel beküldött zenéket a moderátorok ellenőrzik</li>
                <li>Nem minden zene engedélyezett (pl. explicit tartalom)</li>
                <li>A feldolgozás 1-2 napot vehet igénybe</li>
                <li>Értesítést kapsz, ha a zenéd elfogadva lett</li>
              </ul>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default RequestPage;
