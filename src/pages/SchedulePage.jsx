import { useState, useEffect } from 'react';
import { 
  Calendar, 
  Clock, 
  Music, 
  User, 
  Play, 
  CheckCircle, 
  Circle,
  RefreshCw,
  Filter,
  ChevronLeft,
  ChevronRight
} from 'lucide-react';
import { useMusic } from '../contexts/MusicContext';

const SchedulePage = () => {
  const { getScheduleWithSongs, formatDuration } = useMusic();
  const [schedule, setSchedule] = useState([]);
  const [filterStatus, setFilterStatus] = useState('all');
  const [currentTime, setCurrentTime] = useState(new Date());
  const [selectedDate, setSelectedDate] = useState(new Date());

  useEffect(() => {
    setSchedule(getScheduleWithSongs());
  }, [getScheduleWithSongs]);

  // Update time every second
  useEffect(() => {
    const timer = setInterval(() => {
      setCurrentTime(new Date());
    }, 1000);
    return () => clearInterval(timer);
  }, []);

  const filteredSchedule = schedule.filter(item => {
    if (filterStatus === 'all') return true;
    return item.status === filterStatus;
  });

  const getStatusIcon = (status) => {
    switch (status) {
      case 'completed':
        return <CheckCircle size={18} className="status-icon completed" />;
      case 'playing':
        return <Play size={18} className="status-icon playing" />;
      default:
        return <Circle size={18} className="status-icon pending" />;
    }
  };

  const getStatusLabel = (status) => {
    switch (status) {
      case 'completed':
        return 'Lejátszva';
      case 'playing':
        return 'Most játszik';
      default:
        return 'Várakozik';
    }
  };

  const formatDate = (date) => {
    return date.toLocaleDateString('hu-HU', { 
      weekday: 'long', 
      year: 'numeric', 
      month: 'long', 
      day: 'numeric' 
    });
  };

  const changeDate = (days) => {
    const newDate = new Date(selectedDate);
    newDate.setDate(newDate.getDate() + days);
    setSelectedDate(newDate);
  };

  const isToday = selectedDate.toDateString() === new Date().toDateString();

  return (
    <div className="page schedule-page">
      {/* Header */}
      <section className="page-header">
        <div className="page-header-content">
          <Calendar size={32} className="page-icon" />
          <div>
            <h1 className="page-title">Műsorrend</h1>
            <p className="page-subtitle">
              Nézd meg, mikor mi fog szólni az iskolai rádióban!
            </p>
          </div>
        </div>
      </section>

      {/* Current time */}
      <div className="schedule-time-display">
        <Clock size={20} />
        <span className="current-time">
          {currentTime.toLocaleTimeString('hu-HU', { hour: '2-digit', minute: '2-digit', second: '2-digit' })}
        </span>
      </div>

      {/* Date navigation */}
      <div className="schedule-date-nav">
        <button 
          className="date-nav-btn"
          onClick={() => changeDate(-1)}
          aria-label="Előző nap"
        >
          <ChevronLeft size={20} />
        </button>
        <div className="date-display">
          <span className="date-text">{formatDate(selectedDate)}</span>
          {isToday && <span className="today-badge">Ma</span>}
        </div>
        <button 
          className="date-nav-btn"
          onClick={() => changeDate(1)}
          aria-label="Következő nap"
        >
          <ChevronRight size={20} />
        </button>
        {!isToday && (
          <button 
            className="today-btn"
            onClick={() => setSelectedDate(new Date())}
          >
            Mai nap
          </button>
        )}
      </div>

      {/* Filters */}
      <div className="schedule-filters">
        <div className="filter-group" style={{ flexDirection: 'row', alignItems: 'center' }}>
          <Filter size={18} />
          <label htmlFor="status-filter" className="visually-hidden">Állapot szűrő</label>
          <select
            id="status-filter"
            value={filterStatus}
            onChange={(e) => setFilterStatus(e.target.value)}
            className="filter-select"
          >
            <option value="all">Összes</option>
            <option value="completed">Lejátszott</option>
            <option value="playing">Most játszik</option>
            <option value="pending">Várakozó</option>
          </select>
        </div>
        <button className="refresh-btn" aria-label="Frissítés">
          <RefreshCw size={18} />
          <span>Frissítés</span>
        </button>
      </div>

      {/* Schedule table */}
      <div className="schedule-table-wrapper">
        <table className="schedule-table" role="grid" aria-label="Műsorrend táblázat">
          <thead>
            <tr>
              <th className="th-time">
                <Clock size={16} />
                <span>Időpont</span>
              </th>
              <th className="th-status">Állapot</th>
              <th className="th-song">
                <Music size={16} />
                <span>Zene</span>
              </th>
              <th className="th-duration">Hossz</th>
              <th className="th-requester">
                <User size={16} />
                <span>Kérte</span>
              </th>
            </tr>
          </thead>
          <tbody>
            {filteredSchedule.length === 0 ? (
              <tr>
                <td colSpan="5" className="no-results">
                  <Calendar size={48} />
                  <p>Nincs megjeleníthető műsorrend.</p>
                </td>
              </tr>
            ) : (
              filteredSchedule.map((item, index) => (
                <tr 
                  key={item.id} 
                  className={`schedule-row ${item.status}`}
                  aria-current={item.status === 'playing' ? 'true' : undefined}
                >
                  <td className="td-time">
                    <span className="time-badge">{item.time}</span>
                  </td>
                  <td className="td-status">
                    <div className={`status-badge ${item.status}`}>
                      {getStatusIcon(item.status)}
                      <span>{getStatusLabel(item.status)}</span>
                    </div>
                  </td>
                  <td className="td-song">
                    {item.song ? (
                      <div className="song-cell">
                        <div className="song-cover-small">
                          {item.song.coverUrl ? (
                            <img src={item.song.coverUrl} alt="" />
                          ) : (
                            <Music size={16} />
                          )}
                        </div>
                        <div className="song-info">
                          <span className="song-title">{item.song.title}</span>
                          <span className="song-artist">{item.song.artist}</span>
                        </div>
                      </div>
                    ) : (
                      <span className="unknown">Ismeretlen zene</span>
                    )}
                  </td>
                  <td className="td-duration">
                    {item.song ? formatDuration(item.song.duration) : '-'}
                  </td>
                  <td className="td-requester">
                    <div className="requester-cell">
                      <div className="requester-avatar">
                        {item.requestedBy.charAt(0)}
                      </div>
                      <span>{item.requestedBy}</span>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* Legend */}
      <div className="schedule-legend" role="note" aria-label="Jelmagyarázat">
        <h3 className="legend-title">Jelmagyarázat</h3>
        <div className="legend-items">
          <div className="legend-item">
            <CheckCircle size={16} className="completed" />
            <span>Lejátszott</span>
          </div>
          <div className="legend-item">
            <Play size={16} className="playing" />
            <span>Most játszik</span>
          </div>
          <div className="legend-item">
            <Circle size={16} className="pending" />
            <span>Várakozik</span>
          </div>
        </div>
      </div>

      {/* Info */}
      <div className="schedule-info">
        <p>
          A műsorrend automatikusan frissül. A zenék sorrendje a kérések beérkezésének 
          sorrendjében alakul. Ha szeretnél zenét kérni, látogass el a 
          <strong> Zene kérése</strong> oldalra!
        </p>
      </div>
    </div>
  );
};

export default SchedulePage;
