import { createContext, useContext, useState, useEffect, useCallback } from 'react';

const MusicContext = createContext();

export const useMusic = () => {
  const context = useContext(MusicContext);
  if (!context) {
    throw new Error('useMusic must be used within a MusicProvider');
  }
  return context;
};

// Mock music data
const mockSongs = [
  {
    id: 1,
    title: "Bohemian Rhapsody",
    artist: "Queen",
    album: "A Night at the Opera",
    duration: 354,
    genre: "Rock",
    uploadDate: "2025-09-15T10:30:00Z",
    requestCount: 156,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 2,
    title: "Blinding Lights",
    artist: "The Weeknd",
    album: "After Hours",
    duration: 200,
    genre: "Pop",
    uploadDate: "2025-10-01T14:20:00Z",
    requestCount: 243,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 3,
    title: "Shape of You",
    artist: "Ed Sheeran",
    album: "÷ (Divide)",
    duration: 233,
    genre: "Pop",
    uploadDate: "2025-08-20T09:00:00Z",
    requestCount: 189,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 4,
    title: "Smells Like Teen Spirit",
    artist: "Nirvana",
    album: "Nevermind",
    duration: 301,
    genre: "Grunge",
    uploadDate: "2025-09-05T11:45:00Z",
    requestCount: 98,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 5,
    title: "Bad Guy",
    artist: "Billie Eilish",
    album: "When We All Fall Asleep, Where Do We Go?",
    duration: 194,
    genre: "Pop",
    uploadDate: "2025-11-10T16:00:00Z",
    requestCount: 167,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 6,
    title: "Valami Amerika",
    artist: "Hooligans",
    album: "Best of Hooligans",
    duration: 245,
    genre: "Magyar Pop",
    uploadDate: "2025-10-15T08:30:00Z",
    requestCount: 134,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 7,
    title: "Még Mindig",
    artist: "Halott Pénz",
    album: "Ki van itt?",
    duration: 218,
    genre: "Magyar Rap",
    uploadDate: "2025-09-25T12:00:00Z",
    requestCount: 201,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 8,
    title: "Uptown Funk",
    artist: "Mark Ronson ft. Bruno Mars",
    album: "Uptown Special",
    duration: 270,
    genre: "Funk",
    uploadDate: "2025-08-01T10:00:00Z",
    requestCount: 178,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 9,
    title: "Sweet Child O' Mine",
    artist: "Guns N' Roses",
    album: "Appetite for Destruction",
    duration: 356,
    genre: "Rock",
    uploadDate: "2025-07-20T14:30:00Z",
    requestCount: 112,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 10,
    title: "Levitating",
    artist: "Dua Lipa",
    album: "Future Nostalgia",
    duration: 203,
    genre: "Pop",
    uploadDate: "2025-12-01T09:15:00Z",
    requestCount: 225,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 11,
    title: "Imagine",
    artist: "John Lennon",
    album: "Imagine",
    duration: 187,
    genre: "Rock",
    uploadDate: "2025-06-15T11:00:00Z",
    requestCount: 89,
    isAvailable: true,
    coverUrl: null
  },
  {
    id: 12,
    title: "Szerelem",
    artist: "Ákos",
    album: "Best of Ákos",
    duration: 276,
    genre: "Magyar Pop",
    uploadDate: "2025-11-20T13:45:00Z",
    requestCount: 156,
    isAvailable: true,
    coverUrl: null
  }
];

// Mock schedule data
const mockSchedule = [
  { id: 1, songId: 2, time: "07:30", status: "completed", requestedBy: "Kovács Anna" },
  { id: 2, songId: 5, time: "07:45", status: "completed", requestedBy: "Nagy Péter" },
  { id: 3, songId: 7, time: "08:00", status: "playing", requestedBy: "Kiss Béla" },
  { id: 4, songId: 1, time: "08:15", status: "pending", requestedBy: "Szabó Emma" },
  { id: 5, songId: 10, time: "08:30", status: "pending", requestedBy: "Tóth Gábor" },
  { id: 6, songId: 3, time: "08:45", status: "pending", requestedBy: "Molnár Kata" },
  { id: 7, songId: 8, time: "09:00", status: "pending", requestedBy: "Horváth Dávid" },
  { id: 8, songId: 12, time: "09:15", status: "pending", requestedBy: "Varga Laura" },
  { id: 9, songId: 4, time: "09:30", status: "pending", requestedBy: "Fekete Márk" },
  { id: 10, songId: 6, time: "09:45", status: "pending", requestedBy: "Simon Réka" },
];

export const MusicProvider = ({ children }) => {
  const [songs, setSongs] = useState(mockSongs);
  const [schedule, setSchedule] = useState(mockSchedule);
  const [queue, setQueue] = useState([]);
  const [currentlyPlaying, setCurrentlyPlaying] = useState(null);
  const [searchQuery, setSearchQuery] = useState('');
  const [sortBy, setSortBy] = useState('title');
  const [sortOrder, setSortOrder] = useState('asc');
  const [filterGenre, setFilterGenre] = useState('all');
  const [selectedSongs, setSelectedSongs] = useState([]);

  // Get unique genres
  const genres = [...new Set(songs.map(song => song.genre))];

  // Filter and sort songs
  const filteredSongs = useCallback(() => {
    let result = [...songs];

    // Search filter
    if (searchQuery) {
      const query = searchQuery.toLowerCase();
      result = result.filter(song =>
        song.title.toLowerCase().includes(query) ||
        song.artist.toLowerCase().includes(query) ||
        song.album.toLowerCase().includes(query) ||
        song.genre.toLowerCase().includes(query)
      );
    }

    // Genre filter
    if (filterGenre !== 'all') {
      result = result.filter(song => song.genre === filterGenre);
    }

    // Sort
    result.sort((a, b) => {
      let comparison = 0;
      switch (sortBy) {
        case 'title':
          comparison = a.title.localeCompare(b.title, 'hu');
          break;
        case 'artist':
          comparison = a.artist.localeCompare(b.artist, 'hu');
          break;
        case 'duration':
          comparison = a.duration - b.duration;
          break;
        case 'uploadDate':
          comparison = new Date(a.uploadDate) - new Date(b.uploadDate);
          break;
        case 'requestCount':
          comparison = a.requestCount - b.requestCount;
          break;
        default:
          comparison = 0;
      }
      return sortOrder === 'asc' ? comparison : -comparison;
    });

    return result;
  }, [songs, searchQuery, sortBy, sortOrder, filterGenre]);

  // Get currently playing from schedule
  useEffect(() => {
    const playing = schedule.find(item => item.status === 'playing');
    if (playing) {
      const song = songs.find(s => s.id === playing.songId);
      setCurrentlyPlaying(song ? { ...song, scheduleItem: playing } : null);
    }
  }, [schedule, songs]);

  // Toggle song selection
  const toggleSongSelection = (songId) => {
    setSelectedSongs(prev =>
      prev.includes(songId)
        ? prev.filter(id => id !== songId)
        : [...prev, songId]
    );
  };

  // Select all songs
  const selectAllSongs = () => {
    const allIds = filteredSongs().map(song => song.id);
    setSelectedSongs(allIds);
  };

  // Clear selection
  const clearSelection = () => {
    setSelectedSongs([]);
  };

  // Request songs (add to queue)
  const requestSongs = (songIds, userId) => {
    const newRequests = songIds.map((songId, index) => ({
      id: Date.now() + index,
      songId,
      requestedBy: userId,
      requestedAt: new Date().toISOString(),
      status: 'pending'
    }));
    setQueue(prev => [...prev, ...newRequests]);
    clearSelection();
    return newRequests;
  };

  // Add song to library (for admin)
  const addSong = (songData) => {
    const newSong = {
      id: Math.max(...songs.map(s => s.id)) + 1,
      ...songData,
      uploadDate: new Date().toISOString(),
      requestCount: 0,
      isAvailable: true
    };
    setSongs(prev => [...prev, newSong]);
    return newSong;
  };

  // Remove song from library (for admin)
  const removeSong = (songId) => {
    setSongs(prev => prev.filter(s => s.id !== songId));
  };

  // Update song (for admin)
  const updateSong = (songId, updates) => {
    setSongs(prev => prev.map(s =>
      s.id === songId ? { ...s, ...updates } : s
    ));
  };

  // Get song by ID
  const getSongById = (id) => {
    return songs.find(s => s.id === id);
  };

  // Get schedule with song details
  const getScheduleWithSongs = () => {
    return schedule.map(item => ({
      ...item,
      song: getSongById(item.songId)
    }));
  };

  // Format duration
  const formatDuration = (seconds) => {
    const mins = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${mins}:${secs.toString().padStart(2, '0')}`;
  };

  return (
    <MusicContext.Provider value={{
      songs,
      schedule,
      queue,
      currentlyPlaying,
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
      addSong,
      removeSong,
      updateSong,
      getSongById,
      getScheduleWithSongs,
      formatDuration
    }}>
      {children}
    </MusicContext.Provider>
  );
};
