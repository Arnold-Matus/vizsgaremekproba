import { 
  HelpCircle, 
  Search, 
  ChevronDown, 
  ChevronUp,
  Music,
  User,
  Settings,
  Shield,
  Mail
} from 'lucide-react';
import { useState } from 'react';

const HelpPage = () => {
  const [openFaq, setOpenFaq] = useState(null);
  const [searchQuery, setSearchQuery] = useState('');

  const faqs = [
    {
      category: 'Zenekérés',
      icon: Music,
      questions: [
        {
          q: 'Hogyan kérhetek zenét?',
          a: 'A főoldalon vagy a "Zene kérése" oldalon böngészheted a könyvtárat. Jelöld ki a kívánt zenéket a checkbox-szal, majd kattints a "Kiválasztottak kérése" gombra. Link beküldésével is kérhetsz zenét, amit a moderátorok ellenőriznek.'
        },
        {
          q: 'Miért nem látom a zenémet a műsorrendben?',
          a: 'A zenekérések sorba kerülnek és a beérkezés sorrendjében játsszuk le őket. Ha sok kérés van, előfordulhat, hogy a te zenéd csak később kerül sorra. A műsorrend oldalon láthatod a várható időpontot.'
        },
        {
          q: 'Van-e korlátozás a zenekérésekre?',
          a: 'Igen, naponta maximum 3 zenét kérhetsz. Ez biztosítja, hogy mindenki hozzájuthasson a zenekéréshez. Az adminisztrátoroknak és tanároknak nincs ilyen korlátjuk.'
        },
        {
          q: 'Milyen zenéket NEM kérhetek?',
          a: 'Nem engedélyezett az explicit tartalmú (káromkodás, erőszak, stb.), a szerzői jogokat sértő, vagy az iskola értékeivel össze nem egyeztethető tartalom. A moderátorok ellenőrzik a beküldött linkeket.'
        }
      ]
    },
    {
      category: 'Fiók',
      icon: User,
      questions: [
        {
          q: 'Hogyan regisztrálhatok?',
          a: 'A regisztrációhoz iskolai email címre van szükség (@iskola.hu végződés). A regisztrációs oldalon add meg az adataidat és erősítsd meg az email címedet.'
        },
        {
          q: 'Elfelejtettem a jelszavam. Mit tegyek?',
          a: 'A bejelentkezési oldalon kattints az "Elfelejtett jelszó" linkre. Add meg az email címedet és küldünk egy jelszó-visszaállító linket.'
        },
        {
          q: 'Hogyan törölhetem a fiókomat?',
          a: 'A Beállítások oldalon a "Veszélyes zóna" résznél találod a "Fiók törlése" gombot. A törlés végleges és nem visszavonható!'
        }
      ]
    },
    {
      category: 'Beállítások',
      icon: Settings,
      questions: [
        {
          q: 'Hogyan kapcsolhatom be a sötét módot?',
          a: 'A navigációs sávban található hold/nap ikonra kattintva, vagy a Beállítások oldalon a Megjelenés résznél. A preferenciád mentésre kerül.'
        },
        {
          q: 'Hogyan növelhetem a betűméretet?',
          a: 'A Beállítások vagy Akadálymentesség oldalon találod a betűméret beállítást. 12px és 24px között választhatsz.'
        },
        {
          q: 'Hogyan módosíthatom a cookie beállításokat?',
          a: 'A Beállítások oldalon az Adatvédelem résznél vagy a láblécben található "Cookie beállítások" linkre kattintva.'
        }
      ]
    },
    {
      category: 'Adatvédelem',
      icon: Shield,
      questions: [
        {
          q: 'Milyen adatokat gyűjtötök rólam?',
          a: 'Regisztrációs adatokat (név, email, osztály), használati adatokat (zenekérések, kedvencek) és technikai adatokat (munkamenet). Részletesen az Adatvédelmi tájékoztatóban olvashatod.'
        },
        {
          q: 'Hogyan kérhetem az adataim törlését?',
          a: 'A Beállítások oldalon kérheted az összes adatod törlését, vagy írj nekünk az adatvedelem@iskola.hu címre.'
        },
        {
          q: 'Megosztjátok az adataimat harmadik féllel?',
          a: 'Nem, személyes adataidat nem osztjuk meg marketing vagy egyéb célokra. Csak jogszabályi kötelezettség esetén.'
        }
      ]
    }
  ];

  const filteredFaqs = faqs.map(category => ({
    ...category,
    questions: category.questions.filter(
      item => 
        item.q.toLowerCase().includes(searchQuery.toLowerCase()) ||
        item.a.toLowerCase().includes(searchQuery.toLowerCase())
    )
  })).filter(category => category.questions.length > 0);

  const toggleFaq = (categoryIndex, questionIndex) => {
    const key = `${categoryIndex}-${questionIndex}`;
    setOpenFaq(openFaq === key ? null : key);
  };

  return (
    <div className="page help-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <HelpCircle size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Súgó</h1>
            <p className="page-subtitle">
              Gyakran ismételt kérdések és válaszok
            </p>
          </div>
        </div>
      </section>

      <div className="help-content">
        {/* Search */}
        <div className="help-search">
          <Search size={20} className="search-icon" />
          <input
            type="search"
            placeholder="Keresés a kérdések között..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="search-input"
            aria-label="Keresés"
          />
        </div>

        {/* FAQs */}
        <div className="faq-sections">
          {filteredFaqs.length === 0 ? (
            <div className="no-results">
              <HelpCircle size={48} />
              <p>Nincs találat a keresési feltételeknek megfelelően.</p>
            </div>
          ) : (
            filteredFaqs.map((category, catIndex) => (
              <section key={catIndex} className="faq-section">
                <h2>
                  <category.icon size={20} />
                  {category.category}
                </h2>
                <div className="faq-list">
                  {category.questions.map((item, qIndex) => {
                    const isOpen = openFaq === `${catIndex}-${qIndex}`;
                    return (
                      <div key={qIndex} className={`faq-item ${isOpen ? 'open' : ''}`}>
                        <button
                          className="faq-question"
                          onClick={() => toggleFaq(catIndex, qIndex)}
                          aria-expanded={isOpen}
                        >
                          <span>{item.q}</span>
                          {isOpen ? <ChevronUp size={20} /> : <ChevronDown size={20} />}
                        </button>
                        {isOpen && (
                          <div className="faq-answer">
                            <p>{item.a}</p>
                          </div>
                        )}
                      </div>
                    );
                  })}
                </div>
              </section>
            ))
          )}
        </div>

        {/* Contact */}
        <section className="help-contact">
          <h2>Nem találtad meg a választ?</h2>
          <p>Írj nekünk és segítünk!</p>
          <a href="mailto:suliradio@iskola.hu" className="contact-btn">
            <Mail size={20} />
            suliradio@iskola.hu
          </a>
        </section>
      </div>
    </div>
  );
};

export default HelpPage;
