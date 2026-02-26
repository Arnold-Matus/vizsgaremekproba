import { useState } from 'react';
import { 
  Search, 
  SlidersHorizontal, 
  ArrowUpDown, 
  ArrowUp, 
  ArrowDown,
  CheckSquare,
  Square,
  Music,
  Clock,
  Calendar,
  TrendingUp,
  User,
  Play,
  Heart,
  MoreVertical,
  Send,
  X
} from 'lucide-react';
import { useMusic } from '../contexts/MusicContext';
import { useUser } from '../contexts/UserContext';

const SongList = () => {
  const {
    searchQuery,
    setSearchQuery,
    sortBy,
    setSortBy,
    sortOrder,
    setSortOrder,
    filterGenre,
    setFilterGenre,
    genres,
    filteredSongs,
    selectedSongs,
    toggleSongSelection,
    selectAllSongs,
    clearSelection,
    requestSongs,
    formatDuration
  } = useMusic();

  const { user, isLoggedIn, addToFavorites, removeFromFavorites } = useUser();
  const [showFilters, setShowFilters] = useState(false);
  const [requestMessage, setRequestMessage] = useState('');

  const songs = filteredSongs();
  const hasSelection = selectedSongs.length > 0;
  const allSelected = songs.length > 0 && selectedSongs.length === songs.length;

  const handleRequestSongs = () => {
    if (!isLoggedIn) {
      setRequestMessage('Kérlek jelentkezz be a zenék kéréséhez!');
      setTimeout(() => setRequestMessage(''), 3000);
      return;
    }
    if (selectedSongs.length === 0) {
      setRequestMessage('Válassz ki legalább egy zenét!');
      setTimeout(() => setRequestMessage(''), 3000);
      return;
    }
    requestSongs(selectedSongs, user?.id);
    setRequestMessage(`${selectedSongs.length} zene sikeresen kérve!`);
    setTimeout(() => setRequestMessage(''), 3000);
  };

  const toggleSort = (field) => {
    if (sortBy === field) {
      setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
    } else {
      setSortBy(field);
      setSortOrder('asc');
    }
  };

  const getSortIcon = (field) => {
    if (sortBy !== field) return <ArrowUpDown size={14} className="sort-icon inactive" />;
    return sortOrder === 'asc' 
      ? <ArrowUp size={14} className="sort-icon active" />
      : <ArrowDown size={14} className="sort-icon active" />;
  };

  const isFavorite = (songId) => {
    return user?.favorites?.includes(songId);
  };

  const handleFavoriteToggle = (e, songId) => {
    e.stopPropagation();
    if (!isLoggedIn) return;
    if (isFavorite(songId)) {
      removeFromFavorites(songId);
    } else {
      addToFavorites(songId);
    }
  };

  return (
    <div className="song-list-container">
      {/* Search and filter bar */}
      <div className="song-list-header">
        <div className="search-container">
          <Search size={20} className="search-icon" aria-hidden="true" />
          <input
            type="search"
            placeholder="Keresés cím, előadó, album vagy műfaj alapján..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="search-input"
            aria-label="Zenék keresése"
          />
          {searchQuery && (
            <button 
              className="search-clear"
              onClick={() => setSearchQuery('')}
              aria-label="Keresés törlése"
            >
              <X size={18} />
            </button>
          )}
        </div>

        <button
          className={`filter-toggle ${showFilters ? 'active' : ''}`}
          onClick={() => setShowFilters(!showFilters)}
          aria-expanded={showFilters}
          aria-label="Szűrők megjelenítése"
        >
          <SlidersHorizontal size={20} />
          <span>Szűrők</span>
        </button>
      </div>

      {/* Filters */}
      {showFilters && (
        <div className="filters-panel" role="region" aria-label="Szűrési beállítások">
          <div className="filter-group">
            <label htmlFor="genre-filter" className="filter-label">Műfaj</label>
            <select
              id="genre-filter"
              value={filterGenre}
              onChange={(e) => setFilterGenre(e.target.value)}
              className="filter-select"
            >
              <option value="all">Összes műfaj</option>
              {genres.map(genre => (
                <option key={genre} value={genre}>{genre}</option>
              ))}
            </select>
          </div>

          <div className="filter-group">
            <label htmlFor="sort-by" className="filter-label">Rendezés</label>
            <select
              id="sort-by"
              value={sortBy}
              onChange={(e) => setSortBy(e.target.value)}
              className="filter-select"
            >
              <option value="title">Cím</option>
              <option value="artist">Előadó</option>
              <option value="duration">Hossz</option>
              <option value="uploadDate">Feltöltés dátuma</option>
              <option value="requestCount">Népszerűség</option>
            </select>
          </div>

          <div className="filter-group">
            <label htmlFor="sort-order" className="filter-label">Sorrend</label>
            <select
              id="sort-order"
              value={sortOrder}
              onChange={(e) => setSortOrder(e.target.value)}
              className="filter-select"
            >
              <option value="asc">Növekvő</option>
              <option value="desc">Csökkenő</option>
            </select>
          </div>
        </div>
      )}

      {/* Selection toolbar */}
      {hasSelection && (
        <div className="selection-toolbar" role="toolbar" aria-label="Kiválasztott zenék műveletek">
          <div className="selection-info">
            <CheckSquare size={18} />
            <span>{selectedSongs.length} zene kiválasztva</span>
          </div>
          <div className="selection-actions">
            <button className="selection-btn" onClick={clearSelection}>
              <X size={18} />
              Kijelölés törlése
            </button>
            <button className="selection-btn primary" onClick={handleRequestSongs}>
              <Send size={18} />
              Kiválasztottak kérése
            </button>
          </div>
        </div>
      )}

      {/* Request message */}
      {requestMessage && (
        <div className={`request-message ${requestMessage.includes('sikeresen') ? 'success' : 'warning'}`} role="alert">
          {requestMessage}
        </div>
      )}

      {/* Song table */}
      <div className="song-table-wrapper">
        <table className="song-table" role="grid" aria-label="Zenék listája">
          <thead>
            <tr>
              <th className="th-checkbox">
                <button
                  className="select-all-btn"
                  onClick={allSelected ? clearSelection : selectAllSongs}
                  aria-label={allSelected ? 'Összes kijelölés törlése' : 'Összes kijelölése'}
                >
                  {allSelected ? <CheckSquare size={18} /> : <Square size={18} />}
                </button>
              </th>
              <th className="th-title">
                <button className="sort-btn" onClick={() => toggleSort('title')}>
                  <Music size={16} />
                  <span>Cím</span>
                  {getSortIcon('title')}
                </button>
              </th>
              <th className="th-artist">
                <button className="sort-btn" onClick={() => toggleSort('artist')}>
                  <User size={16} />
                  <span>Előadó</span>
                  {getSortIcon('artist')}
                </button>
              </th>
              <th className="th-duration">
                <button className="sort-btn" onClick={() => toggleSort('duration')}>
                  <Clock size={16} />
                  <span>Hossz</span>
                  {getSortIcon('duration')}
                </button>
              </th>
              <th className="th-genre">
                <span>Műfaj</span>
              </th>
              <th className="th-date">
                <button className="sort-btn" onClick={() => toggleSort('uploadDate')}>
                  <Calendar size={16} />
                  <span>Feltöltve</span>
                  {getSortIcon('uploadDate')}
                </button>
              </th>
              <th className="th-popularity">
                <button className="sort-btn" onClick={() => toggleSort('requestCount')}>
                  <TrendingUp size={16} />
                  <span>Kérések</span>
                  {getSortIcon('requestCount')}
                </button>
              </th>
              <th className="th-actions">
                <span className="visually-hidden">Műveletek</span>
              </th>
            </tr>
          </thead>
          <tbody>
            {songs.length === 0 ? (
              <tr>
                <td colSpan="8" className="no-results">
                  <Music size={48} />
                  <p>Nincs találat a keresési feltételeknek megfelelően.</p>
                </td>
              </tr>
            ) : (
              songs.map(song => (
                <tr 
                  key={song.id} 
                  className={`song-row ${selectedSongs.includes(song.id) ? 'selected' : ''}`}
                  onClick={() => toggleSongSelection(song.id)}
                >
                  <td className="td-checkbox">
                    <button
                      className="checkbox-btn"
                      onClick={(e) => {
                        e.stopPropagation();
                        toggleSongSelection(song.id);
                      }}
                      aria-label={selectedSongs.includes(song.id) ? `${song.title} kijelölés törlése` : `${song.title} kijelölése`}
                    >
                      {selectedSongs.includes(song.id) ? (
                        <CheckSquare size={18} className="checked" />
                      ) : (
                        <Square size={18} />
                      )}
                    </button>
                  </td>
                  <td className="td-title">
                    <div className="song-title-cell">
                      <div className="song-cover">
                        {song.coverUrl ? (
                          <img src={song.coverUrl} alt="" />
                        ) : (
                          <Music size={20} />
                        )}
                        <div className="song-cover-overlay">
                          <Play size={16} />
                        </div>
                      </div>
                      <div className="song-info">
                        <span className="song-title">{song.title}</span>
                        <span className="song-album">{song.album}</span>
                      </div>
                    </div>
                  </td>
                  <td className="td-artist">{song.artist}</td>
                  <td className="td-duration">{formatDuration(song.duration)}</td>
                  <td className="td-genre">
                    <span className="genre-badge">{song.genre}</span>
                  </td>
                  <td className="td-date">
                    {new Date(song.uploadDate).toLocaleDateString('hu-HU')}
                  </td>
                  <td className="td-popularity">
                    <div className="popularity-bar">
                      <div 
                        className="popularity-fill" 
                        style={{ width: `${Math.min(song.requestCount / 2.5, 100)}%` }}
                      />
                      <span>{song.requestCount}</span>
                    </div>
                  </td>
                  <td className="td-actions">
                    <button
                      className={`action-btn favorite ${isFavorite(song.id) ? 'active' : ''}`}
                      onClick={(e) => handleFavoriteToggle(e, song.id)}
                      aria-label={isFavorite(song.id) ? 'Eltávolítás a kedvencekből' : 'Hozzáadás a kedvencekhez'}
                      disabled={!isLoggedIn}
                    >
                      <Heart size={18} fill={isFavorite(song.id) ? 'currentColor' : 'none'} />
                    </button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* Results count */}
      <div className="song-list-footer">
        <p className="results-count">
          {songs.length} zene{songs.length !== 1 ? '' : ''} találat
          {searchQuery && ` "${searchQuery}" keresésre`}
        </p>
      </div>
    </div>
  );
};

export default SongList;
