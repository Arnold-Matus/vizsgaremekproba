import { useState, useEffect, useRef } from 'react';
import { 
  Info, 
  Users, 
  Target, 
  Heart, 
  MapPin, 
  Mail, 
  Phone, 
  Radio,
  Music,
  Headphones,
  Volume2
} from 'lucide-react';

const AboutPage = () => {
  const [activeSection, setActiveSection] = useState('about');
  const [speakerWaves, setSpeakerWaves] = useState([]);
  const mapRef = useRef(null);

  // Simulate speaker waves animation
  useEffect(() => {
    if (activeSection === 'map') {
      const interval = setInterval(() => {
        const newWave = {
          id: Date.now(),
          x: Math.random() * 100,
          y: Math.random() * 100,
        };
        setSpeakerWaves(prev => [...prev.slice(-5), newWave]);
      }, 2000);
      return () => clearInterval(interval);
    }
  }, [activeSection]);

  const teamMembers = [
    {
      name: 'Rákóczi Botond',
      role: 'Fejlesztő',
      description: 'Adatbázis és backend fejlesztés',
      class: '13.B'
    },
    {
      name: 'Matus Arnold',
      role: 'Fejlesztő',
      description: 'Rendszertervezés és backend fejlesztés',
      class: '13.B'
    },
    {
      name: 'Sütő Zsolt Márk',
      role: 'Fejlesztő',
      description: 'UI/UX tervezés és frontend fejlesztés',
      class: '13.B'
    }
  ];

  const speakers = [
    { id: 1, name: 'Aula', x: 50, y: 30 },
    { id: 2, name: 'Folyosó A', x: 20, y: 50 },
    { id: 3, name: 'Folyosó B', x: 80, y: 50 },
    { id: 4, name: 'Kantine', x: 35, y: 70 },
    { id: 5, name: 'Sportpálya', x: 65, y: 85 },
    { id: 6, name: 'Könyvtár', x: 15, y: 25 },
  ];

  return (
    <div className="page about-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <Info size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Rólunk</h1>
            <p className="page-subtitle">
              Ismerd meg a SuliRadio csapatát és az iskolát!
            </p>
          </div>
        </div>
      </section>

      {/* Navigation tabs */}
      <div className="about-tabs" role="tablist">
        <button
          role="tab"
          aria-selected={activeSection === 'about'}
          className={`about-tab ${activeSection === 'about' ? 'active' : ''}`}
          onClick={() => setActiveSection('about')}
        >
          <Radio size={18} />
          <span>A SuliRadióról</span>
        </button>
        <button
          role="tab"
          aria-selected={activeSection === 'team'}
          className={`about-tab ${activeSection === 'team' ? 'active' : ''}`}
          onClick={() => setActiveSection('team')}
        >
          <Users size={18} />
          <span>Csapatunk</span>
        </button>
        <button
          role="tab"
          aria-selected={activeSection === 'school'}
          className={`about-tab ${activeSection === 'school' ? 'active' : ''}`}
          onClick={() => setActiveSection('school')}
        >
          <Target size={18} />
          <span>Az iskoláról</span>
        </button>
        <button
          role="tab"
          aria-selected={activeSection === 'map'}
          className={`about-tab ${activeSection === 'map' ? 'active' : ''}`}
          onClick={() => setActiveSection('map')}
        >
          <MapPin size={18} />
          <span>Hangszóró térkép</span>
        </button>
      </div>

      {/* Content sections */}
      <div className="about-content">
        {activeSection === 'about' && (
          <div className="about-section" role="tabpanel">
            <div className="about-hero">
              <div className="about-hero-icon">
                <Radio size={64} />
              </div>
              <h2>Mi az a SuliRadio?</h2>
              <p className="about-lead">
                A SuliRadio az iskolánk hivatalos belső rádiója, amelyet diákok készítenek diákoknak.
              </p>
            </div>

            <div className="about-features">
              <div className="feature-card">
                <Music size={32} />
                <h3>Zenekérés</h3>
                <p>
                  Te döntöd el, mi szóljon! Válassz a könyvtárból vagy küldj be saját javaslatot.
                </p>
              </div>
              <div className="feature-card">
                <Headphones size={32} />
                <h3>Közösségi élmény</h3>
                <p>
                  Hallgasd együtt az iskolával a kedvenc zenéidet szünetekben és rendezvényeken.
                </p>
              </div>
              <div className="feature-card">
                <Volume2 size={32} />
                <h3>Minőségi hangzás</h3>
                <p>
                  Modern hangrendszer az iskola minden pontján a legjobb hangélményért.
                </p>
              </div>
            </div>

            <div className="about-story">
              <h3>A projekt története</h3>
              <p>
                A SuliRadio ötlete 2025 őszén született, amikor egy csoport lelkes diák 
                elhatározta, hogy életet lehel az iskola régi rádiójába. A projekt 
                vizsgamunkaként indult, de hamar az egész iskola közösségének kedvence lett.
              </p>
              <p>
                Célunk, hogy egy olyan platformot hozzunk létre, ahol mindenki hallathatja 
                a hangját - szó szerint! A zenekérés demokratikus, a műsor változatos, 
                és az egész rendszert mi, diákok fejlesztjük és üzemeltetjük.
              </p>
            </div>

            <div className="about-values">
              <h3>Értékeink</h3>
              <div className="values-grid">
                <div className="value-item">
                  <Heart size={24} />
                  <h4>Közösség</h4>
                  <p>Összekötjük az iskola diákjait a zene erejével.</p>
                </div>
                <div className="value-item">
                  <Target size={24} />
                  <h4>Minőség</h4>
                  <p>Csak a legjobb zenék és a legjobb hangzás.</p>
                </div>
                <div className="value-item">
                  <Users size={24} />
                  <h4>Részvétel</h4>
                  <p>Mindenki beleszólhat a műsorba.</p>
                </div>
              </div>
            </div>
          </div>
        )}

        {activeSection === 'team' && (
          <div className="team-section" role="tabpanel">
            <h2>Ismerd meg a csapatot!</h2>
            <p className="team-intro">
              Mi vagyunk a SuliRadio mögött álló lelkes diákok, akik vizsgamunkaként 
              álmodták meg és valósították meg ezt a projektet.
            </p>

            <div className="team-grid">
              {teamMembers.map((member, index) => (
                <div key={index} className="team-card">
                  <div className="team-avatar">
                    {member.name.charAt(0)}
                  </div>
                  <h3 className="team-name">{member.name}</h3>
                  <span className="team-role">{member.role}</span>
                  <span className="team-class">{member.class}</span>
                  <p className="team-description">{member.description}</p>
                </div>
              ))}
            </div>

            <div className="team-contact">
              <h3>Kapcsolat</h3>
              <p>Kérdésed van? Írj nekünk!</p>
              <div className="contact-links">
                <a href="mailto:suliradio@nttbcs.hu" className="contact-link">
                  <Mail size={18} />
                  <span>suliradio@nttbcs.hu</span>
                </a>
              </div>
            </div>
          </div>
        )}

        {activeSection === 'school' && (
          <div className="school-section" role="tabpanel">
            <h2>Az iskolánkról</h2>
            
            <div className="school-info">
              <div className="school-card">
                <h3>Békéscsabai Nemes Tihamér Technikum</h3>
                <p>
                  Iskolánk Békéscsaba egyik legrangosabb informatikai és műszaki képzést nyújtó 
                  oktatási intézménye. Nevét Nemes Tihamér matematikusról, a számítógép 
                  egyik magyar úttörőjéről kapta.
                </p>
              </div>

              <div className="school-stats">
                <div className="school-stat">
                  <span className="stat-number">600+</span>
                  <span className="stat-label">Diák</span>
                </div>
                <div className="school-stat">
                  <span className="stat-number">50+</span>
                  <span className="stat-label">Tanár</span>
                </div>
                <div className="school-stat">
                  <span className="stat-number">3</span>
                  <span className="stat-label">Emelet</span>
                </div>
                <div className="school-stat">
                  <span className="stat-number">6</span>
                  <span className="stat-label">Hangszóró</span>
                </div>
              </div>

              <div className="school-details">
                <h3>Elérhetőségek</h3>
                <ul className="school-contact">
                  <li>
                    <MapPin size={18} />
                    <span>5600 Békéscsaba, Kazinczy utca 7.</span>
                  </li>
                  <li>
                    <Phone size={18} />
                    <span>+36 66 441 459</span>
                  </li>
                  <li>
                    <Mail size={18} />
                    <a href="mailto:titkarsag@nttbcs.hu">titkarsag@nttbcs.hu</a>
                  </li>
                </ul>
              </div>

              <div className="school-programs">
                <h3>Képzéseink</h3>
                <ul>
                  <li>Szoftverfejlesztő és -tesztelő technikus</li>
                  <li>Hálózati rendszerüzemeltető technikus</li>
                  <li>Épület- és szerkezetlakatos technikus</li>
                  <li>Gépi forgácsoló technikus</li>
                  <li>Mechatronikai technikus</li>
                </ul>
              </div>
            </div>
          </div>
        )}

        {activeSection === 'map' && (
          <div className="map-section" role="tabpanel">
            <h2>Hangszóró térkép</h2>
            <p className="map-intro">
              Nézd meg, hol találhatók az iskolai rádió hangszórói! 
              Amikor zene szól, a hangszórók helyéről hullámok indulnak ki.
            </p>

            <div className="school-map" ref={mapRef}>
              <div className="map-background">
                {/* School layout representation */}
                <div className="map-floor">
                  <div className="map-room aula">Aula</div>
                  <div className="map-room library">Könyvtár</div>
                  <div className="map-room corridor-a">Folyosó A</div>
                  <div className="map-room corridor-b">Folyosó B</div>
                  <div className="map-room canteen">Kantine</div>
                  <div className="map-room sports">Sportpálya</div>
                </div>

                {/* Speakers with waves */}
                {speakers.map(speaker => (
                  <div 
                    key={speaker.id}
                    className="speaker-marker"
                    style={{ left: `${speaker.x}%`, top: `${speaker.y}%` }}
                    title={speaker.name}
                  >
                    <div className="speaker-icon">
                      <Volume2 size={20} />
                    </div>
                    <div className="speaker-wave wave-1" />
                    <div className="speaker-wave wave-2" />
                    <div className="speaker-wave wave-3" />
                    <span className="speaker-label">{speaker.name}</span>
                  </div>
                ))}
              </div>

              <div className="map-legend">
                <h4>Jelmagyarázat</h4>
                <div className="legend-item">
                  <Volume2 size={16} />
                  <span>Hangszóró helye</span>
                </div>
                <div className="legend-item">
                  <div className="wave-sample" />
                  <span>Hanghullám (ha zene szól)</span>
                </div>
              </div>
            </div>

            <div className="map-info">
              <h3>Tudnivalók a hangrendszerről</h3>
              <ul>
                <li>6 db professzionális hangszóró az iskola stratégiai pontjain</li>
                <li>Központi vezérlés a rádió stúdióból</li>
                <li>Automatikus hangerő-szabályozás a napszak függvényében</li>
                <li>Vészjelző funkció beépítve</li>
              </ul>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default AboutPage;
