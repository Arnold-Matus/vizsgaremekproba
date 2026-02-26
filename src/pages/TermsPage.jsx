import { 
  FileText, 
  CheckCircle,
  XCircle,
  Mail
} from 'lucide-react';

const TermsPage = () => {
  return (
    <div className="page privacy-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <FileText size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Használati útmutató</h1>
            <p className="page-subtitle">
              A SuliRadio használatának egyszerű szabályai
            </p>
          </div>
        </div>
      </section>

      <div className="privacy-content">
        <section className="privacy-section">
          <h2>
            <FileText size={20} />
            Mi ez az oldal?
          </h2>
          <p>
            A SuliRadio a Békéscsabai Nemes Tihamér Technikum diákrádiója. 
            Itt tudsz zenét kérni, ami szünetekben szólni fog az iskola hangszóróin!
          </p>
        </section>

        <section className="privacy-section">
          <h2>
            <CheckCircle size={20} />
            Ezt csinálhatod
          </h2>
          <ul>
            <li>Böngészheted a zenéket és kedvenceket menthetsz</li>
            <li>Zenéket kérhetsz a rádióba</li>
            <li>Új zenéket javasolhatsz</li>
            <li>Megnézheted a műsorrendet</li>
          </ul>
        </section>

        <section className="privacy-section">
          <h2>
            <XCircle size={20} />
            Ez nem oké
          </h2>
          <ul>
            <li>Trágár, sértő zenék kérése</li>
            <li>Más diákok zaklatása</li>
            <li>Spam küldése</li>
            <li>Hamis adatok megadása</li>
          </ul>
          <p>
            Ha valaki nem tartja be a szabályokat, a fiókját ideiglenesen vagy 
            véglegesen letilthatjuk.
          </p>
        </section>

        <section className="privacy-section">
          <h2>
            <Mail size={20} />
            Kérdésed van?
          </h2>
          <p>
            Írj nekünk: <a href="mailto:suliradio@nttbcs.hu">suliradio@nttbcs.hu</a>
          </p>
        </section>
      </div>
    </div>
  );
};

export default TermsPage;