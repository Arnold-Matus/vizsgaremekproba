import { 
  Shield, 
  Database, 
  Lock, 
  Eye, 
  Mail,
  FileText,
  Clock
} from 'lucide-react';

const PrivacyPage = () => {
  const lastUpdated = '2026. január 15.';

  return (
    <div className="page privacy-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <Shield size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Adatvédelem</h1>
            <p className="page-subtitle">
              Hogyan kezeljük az adataidat
            </p>
          </div>
        </div>
      </section>

      <div className="privacy-content">
        <div className="privacy-meta">
          <Clock size={16} />
          <span>Frissítve: {lastUpdated}</span>
        </div>

        <section className="privacy-section">
          <h2>
            <FileText size={20} />
            Röviden
          </h2>
          <p>
            A SuliRadio a Békéscsabai Nemes Tihamér Technikum diákrádiója. 
            Az oldalon tárolt adataidat biztonságban kezeljük és harmadik félnek nem adjuk ki.
          </p>
        </section>

        <section className="privacy-section">
          <h2>
            <Database size={20} />
            Mit tárolunk?
          </h2>
          <ul>
            <li>Neved és email címed</li>
            <li>Oktatási azonosítód és osztályod</li>
            <li>Zenekéréseid és kedvenceid</li>
            <li>Beállításaid (pl. téma, értesítések)</li>
          </ul>
        </section>

        <section className="privacy-section">
          <h2>
            <Eye size={20} />
            Mire használjuk?
          </h2>
          <ul>
            <li>Bejelentkezéshez és azonosításhoz</li>
            <li>Zenekérések feldolgozásához</li>
            <li>A felhasználói élmény javításához</li>
          </ul>
        </section>

        <section className="privacy-section">
          <h2>
            <Lock size={20} />
            Biztonság
          </h2>
          <p>
            Az adataid védelmére modern titkosítást használunk. 
            Jelszavadat hash-elve tároljuk, még mi sem tudjuk elolvasni.
          </p>
        </section>

        <section className="privacy-section">
          <h2>
            <Mail size={20} />
            Kapcsolat
          </h2>
          <p>
            Ha kérdésed van az adatkezeléssel kapcsolatban, írj nekünk:
          </p>
          <ul>
            <li>Email: <a href="mailto:suliradio@nttbcs.hu">suliradio@nttbcs.hu</a></li>
          </ul>
        </section>
      </div>
    </div>
  );
};

export default PrivacyPage;