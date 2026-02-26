import { Radio, Music, Calendar, Users, Headphones } from 'lucide-react';
import NowPlaying from '../components/NowPlaying';
import SongList from '../components/SongList';
import { useMusic } from '../contexts/MusicContext';

const HomePage = () => {
  const { songs, queue } = useMusic();

  const stats = [
    { icon: Music, label: 'Elérhető zenék', value: songs.length },
    { icon: Calendar, label: 'Mai kérések', value: queue.length + 10 },
    { icon: Users, label: 'Aktív felhasználók', value: 42 },
    { icon: Headphones, label: 'Hallgatók most', value: 156 },
  ];

  return (
    <div className="page home-page">
      {/* Hero section */}
      <section className="hero" aria-labelledby="hero-title">
        <div className="hero-background">
          <div className="hero-gradient" />
          <div className="hero-pattern" />
        </div>
        <div className="hero-content">
          <div className="hero-icon">
            <Radio size={48} />
          </div>
          <h1 id="hero-title" className="hero-title">
            Üdvözlünk a <span className="highlight">SuliRadio</span>-ban!
          </h1>
          <p className="hero-subtitle">
            A Nemes Tihamér Technikum rádiója, ahol Te döntöd el, mi szóljon! 
            Kérj zenét, nézd meg a műsort, és légy részese a közösségnek.
          </p>
        </div>
      </section>

      {/* Now playing */}
      <section aria-label="Most játszott zene">
        <NowPlaying />
      </section>

      {/* Stats */}
      <section className="stats-section" aria-label="Statisztikák">
        <div className="stats-grid">
          {stats.map(({ icon: Icon, label, value }) => (
            <div key={label} className="stat-card">
              <Icon size={24} className="stat-icon" aria-hidden="true" />
              <div className="stat-info">
                <span className="stat-value">{value}</span>
                <span className="stat-label">{label}</span>
              </div>
            </div>
          ))}
        </div>
      </section>

      {/* Song list */}
      <section className="songs-section" aria-labelledby="songs-title">
        <div className="section-header">
          <h2 id="songs-title" className="section-title">
            <Music size={24} />
            Zenék böngészése
          </h2>
          <p className="section-subtitle">
            Válaszd ki a kedvenc zenéidet és kérd őket a rádiótól!
          </p>
        </div>
        <SongList />
      </section>
    </div>
  );
};

export default HomePage;