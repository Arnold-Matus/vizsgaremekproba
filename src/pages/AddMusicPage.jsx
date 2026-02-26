import { useState } from 'react';
import { Link } from 'react-router-dom';
import { 
  PlusCircle, 
  Link as LinkIcon, 
  Upload, 
  Music, 
  Send, 
  CheckCircle, 
  AlertCircle,
  Info,
  Loader2,
  User,
  Clock,
  FileAudio
} from 'lucide-react';
import { useUser } from '../contexts/UserContext';

const AddMusicPage = () => {
  const { user, isLoggedIn } = useUser();

  const [activeTab, setActiveTab] = useState('link');
  const [linkData, setLinkData] = useState({
    url: '',
    title: '',
    artist: '',
    message: ''
  });
  const [fileData, setFileData] = useState({
    file: null,
    title: '',
    artist: '',
    message: ''
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submitResult, setSubmitResult] = useState(null);

  const handleLinkChange = (e) => {
    const { name, value } = e.target;
    setLinkData(prev => ({ ...prev, [name]: value }));
  };

  const handleFileChange = (e) => {
    const { name, value, files } = e.target;
    if (name === 'file') {
      setFileData(prev => ({ ...prev, file: files[0] }));
    } else {
      setFileData(prev => ({ ...prev, [name]: value }));
    }
  };

  const handleLinkSubmit = async (e) => {
    e.preventDefault();
    
    if (!isLoggedIn) {
      setSubmitResult({ type: 'error', message: 'Kérlek jelentkezz be a zene hozzáadásához!' });
      return;
    }
    
    if (!linkData.url.trim()) {
      setSubmitResult({ type: 'error', message: 'Add meg a zene linkjét!' });
      return;
    }

    setIsSubmitting(true);
    setSubmitResult(null);

    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1500));

    setSubmitResult({ 
      type: 'success', 
      message: 'Zenekérésed sikeresen elküldve! A moderátorok hamarosan átnézik.' 
    });
    setLinkData({ url: '', title: '', artist: '', message: '' });
    setIsSubmitting(false);
  };

  const handleFileSubmit = async (e) => {
    e.preventDefault();
    
    if (!isLoggedIn) {
      setSubmitResult({ type: 'error', message: 'Kérlek jelentkezz be a zene feltöltéséhez!' });
      return;
    }
    
    if (!fileData.file) {
      setSubmitResult({ type: 'error', message: 'Válassz ki egy hangfájlt!' });
      return;
    }

    setIsSubmitting(true);
    setSubmitResult(null);

    // Simulate file upload
    await new Promise(resolve => setTimeout(resolve, 2000));

    setSubmitResult({ 
      type: 'success', 
      message: 'A zene sikeresen feltöltve! A moderátorok hamarosan átnézik.' 
    });
    setFileData({ file: null, title: '', artist: '', message: '' });
    setIsSubmitting(false);
  };

  return (
    <div className="page request-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <PlusCircle size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Zene hozzáadása</h1>
            <p className="page-subtitle">
              Adj hozzá új zenét a könyvtárhoz link beküldésével vagy fájl feltöltésével!
            </p>
          </div>
        </div>
      </section>

      {/* Login warning */}
      {!isLoggedIn && (
        <div className="warning-banner" role="alert">
          <AlertCircle size={20} />
          <p>
            Zene hozzáadásához be kell jelentkezned. 
            <Link to="/login"> Jelentkezz be</Link> vagy 
            <Link to="/register"> regisztrálj</Link>!
          </p>
        </div>
      )}

      {/* Info banner */}
      <div className="result-banner" style={{ 
        background: 'var(--color-info-bg)', 
        border: '1px solid var(--color-info)',
        color: 'var(--color-info)'
      }}>
        <Info size={20} />
        <p style={{ color: 'var(--color-text)' }}>
          A feltöltött zenéket moderátoraink átnézik mielőtt a könyvtárba kerülnének. 
          Kérjük, csak megfelelő tartalmú zenéket tölts fel!
        </p>
      </div>

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
          aria-selected={activeTab === 'link'}
          className={`request-tab ${activeTab === 'link' ? 'active' : ''}`}
          onClick={() => setActiveTab('link')}
        >
          <LinkIcon size={18} />
          <span>Link beküldése</span>
        </button>
        <button
          role="tab"
          aria-selected={activeTab === 'file'}
          className={`request-tab ${activeTab === 'file' ? 'active' : ''}`}
          onClick={() => setActiveTab('file')}
        >
          <Upload size={18} />
          <span>Fájl feltöltése</span>
        </button>
      </div>

      {/* Tab content */}
      <div className="request-content">
        {activeTab === 'link' && (
          <div className="library-request" role="tabpanel">
            <form onSubmit={handleLinkSubmit} className="link-request-form">
              <div className="form-group">
                <label htmlFor="url">
                  <LinkIcon size={16} />
                  Zene linkje (YouTube, Spotify, stb.) *
                </label>
                <input
                  type="url"
                  id="url"
                  name="url"
                  value={linkData.url}
                  onChange={handleLinkChange}
                  placeholder="https://youtube.com/watch?v=..."
                  className="form-input"
                  disabled={isSubmitting}
                  required
                />
              </div>

              <div className="form-group">
                <label htmlFor="title">
                  <Music size={16} />
                  Zene címe <span className="optional">(opcionális)</span>
                </label>
                <input
                  type="text"
                  id="title"
                  name="title"
                  value={linkData.title}
                  onChange={handleLinkChange}
                  placeholder="Pl.: Bohemian Rhapsody"
                  className="form-input"
                  disabled={isSubmitting}
                />
              </div>

              <div className="form-group">
                <label htmlFor="artist">
                  <User size={16} />
                  Előadó <span className="optional">(opcionális)</span>
                </label>
                <input
                  type="text"
                  id="artist"
                  name="artist"
                  value={linkData.artist}
                  onChange={handleLinkChange}
                  placeholder="Pl.: Queen"
                  className="form-input"
                  disabled={isSubmitting}
                />
              </div>

              <div className="form-group">
                <label htmlFor="message">
                  <Info size={16} />
                  Üzenet a moderátoroknak <span className="optional">(opcionális)</span>
                </label>
                <textarea
                  id="message"
                  name="message"
                  value={linkData.message}
                  onChange={handleLinkChange}
                  placeholder="Ha van valami amit tudnunk kell a zenéről..."
                  className="form-input"
                  rows={3}
                  disabled={isSubmitting}
                  style={{ resize: 'vertical' }}
                />
              </div>

              <button 
                type="submit" 
                className="submit-btn"
                disabled={isSubmitting || !isLoggedIn}
              >
                {isSubmitting ? (
                  <>
                    <Loader2 size={18} className="spinning" />
                    Küldés...
                  </>
                ) : (
                  <>
                    <Send size={18} />
                    Link beküldése
                  </>
                )}
              </button>
            </form>
          </div>
        )}

        {activeTab === 'file' && (
          <div className="library-request" role="tabpanel">
            <form onSubmit={handleFileSubmit} className="link-request-form">
              <div className="form-group">
                <label htmlFor="file">
                  <FileAudio size={16} />
                  Hangfájl (MP3, WAV, OGG) *
                </label>
                <input
                  type="file"
                  id="file"
                  name="file"
                  onChange={handleFileChange}
                  accept="audio/*"
                  className="form-input"
                  disabled={isSubmitting}
                  required
                  style={{ padding: '0.75rem' }}
                />
                {fileData.file && (
                  <small style={{ color: 'var(--color-text-secondary)', marginTop: '0.5rem', display: 'block' }}>
                    Kiválasztva: {fileData.file.name} ({(fileData.file.size / 1024 / 1024).toFixed(2)} MB)
                  </small>
                )}
              </div>

              <div className="form-group">
                <label htmlFor="file-title">
                  <Music size={16} />
                  Zene címe *
                </label>
                <input
                  type="text"
                  id="file-title"
                  name="title"
                  value={fileData.title}
                  onChange={handleFileChange}
                  placeholder="Pl.: Bohemian Rhapsody"
                  className="form-input"
                  disabled={isSubmitting}
                  required
                />
              </div>

              <div className="form-group">
                <label htmlFor="file-artist">
                  <User size={16} />
                  Előadó *
                </label>
                <input
                  type="text"
                  id="file-artist"
                  name="artist"
                  value={fileData.artist}
                  onChange={handleFileChange}
                  placeholder="Pl.: Queen"
                  className="form-input"
                  disabled={isSubmitting}
                  required
                />
              </div>

              <div className="form-group">
                <label htmlFor="file-message">
                  <Info size={16} />
                  Üzenet a moderátoroknak <span className="optional">(opcionális)</span>
                </label>
                <textarea
                  id="file-message"
                  name="message"
                  value={fileData.message}
                  onChange={handleFileChange}
                  placeholder="Ha van valami amit tudnunk kell a zenéről..."
                  className="form-input"
                  rows={3}
                  disabled={isSubmitting}
                  style={{ resize: 'vertical' }}
                />
              </div>

              <button 
                type="submit" 
                className="submit-btn"
                disabled={isSubmitting || !isLoggedIn}
              >
                {isSubmitting ? (
                  <>
                    <Loader2 size={18} className="spinning" />
                    Feltöltés...
                  </>
                ) : (
                  <>
                    <Upload size={18} />
                    Fájl feltöltése
                  </>
                )}
              </button>
            </form>
          </div>
        )}
      </div>

      {/* Guidelines */}
      <section style={{ marginTop: '2rem' }}>
        <h2 style={{ marginBottom: '1rem', display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
          <Info size={20} />
          Irányelvek
        </h2>
        <div style={{ 
          background: 'var(--color-surface)', 
          border: '1px solid var(--color-border)',
          borderRadius: 'var(--radius-lg)',
          padding: '1.5rem'
        }}>
          <ul style={{ 
            listStyle: 'disc', 
            paddingLeft: '1.5rem',
            display: 'grid',
            gap: '0.5rem',
            color: 'var(--color-text-secondary)'
          }}>
            <li>Csak olyan zenéket tölts fel, amiknek a lejátszása jogszerű</li>
            <li>Kerüld a trágár vagy sértő tartalmú zenéket</li>
            <li>A zenék hossza maximum 10 perc lehet</li>
            <li>A fájlok mérete maximum 50 MB lehet</li>
            <li>A moderátorok fenntartják a jogot a zenék elutasítására</li>
          </ul>
        </div>
      </section>
    </div>
  );
};

export default AddMusicPage;
