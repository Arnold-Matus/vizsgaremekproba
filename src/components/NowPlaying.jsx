import { Music, Radio, Disc, Volume2 } from 'lucide-react';
import { useMusic } from '../contexts/MusicContext';

const NowPlaying = () => {
  const { currentlyPlaying, formatDuration } = useMusic();

  if (!currentlyPlaying) {
    return (
      <div className="now-playing empty">
        <div className="now-playing-content">
          <Radio size={32} className="now-playing-icon" aria-hidden="true" />
          <div className="now-playing-details">
            <span className="now-playing-label">Most játszuk</span>
            <h2 className="now-playing-title">Jelenleg nincs lejátszás</h2>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="now-playing" role="region" aria-label="Most játszott zene">
      <div className="now-playing-background">
        <div className="now-playing-gradient" />
      </div>
      
      <div className="now-playing-content">
        <div className="now-playing-visualizer">
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
        </div>

        <div className="now-playing-cover">
          {currentlyPlaying.coverUrl ? (
            <img src={currentlyPlaying.coverUrl} alt={`${currentlyPlaying.title} borító`} />
          ) : (
            <div className="now-playing-cover-placeholder">
              <Disc size={40} className="spinning" />
            </div>
          )}
        </div>

        <div className="now-playing-details">
          <span className="now-playing-label">
            <Volume2 size={14} className="pulse" />
            Most játszuk
          </span>
          <h2 className="now-playing-title">{currentlyPlaying.title}</h2>
          <p className="now-playing-artist">{currentlyPlaying.artist}</p>
          <div className="now-playing-meta">
            <span className="now-playing-album">{currentlyPlaying.album}</span>
            <span className="now-playing-separator">•</span>
            <span className="now-playing-duration">{formatDuration(currentlyPlaying.duration)}</span>
          </div>
          {currentlyPlaying.scheduleItem && (
            <p className="now-playing-requested">
              Kérte: <strong>{currentlyPlaying.scheduleItem.requestedBy}</strong>
            </p>
          )}
        </div>

        <div className="now-playing-visualizer right">
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
          <div className="visualizer-bar" />
        </div>
      </div>
    </div>
  );
};

export default NowPlaying;
