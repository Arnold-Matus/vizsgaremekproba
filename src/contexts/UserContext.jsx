import { createContext, useContext, useState, useEffect, useCallback } from 'react';

const UserContext = createContext();

export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error('useUser must be used within a UserProvider');
  }
  return context;
};

// Cookie helper functions
const setCookie = (name, value, days = 7) => {
  const expires = new Date(Date.now() + days * 864e5).toUTCString();
  document.cookie = `${name}=${encodeURIComponent(JSON.stringify(value))}; expires=${expires}; path=/; SameSite=Strict`;
};

const getCookie = (name) => {
  const value = document.cookie.split('; ').find(row => row.startsWith(name + '='));
  if (value) {
    try {
      return JSON.parse(decodeURIComponent(value.split('=')[1]));
    } catch {
      return null;
    }
  }
  return null;
};

const deleteCookie = (name) => {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/`;
};

// Default user template
const createUserObject = (data) => ({
  id: data.id || 'usr_' + Math.random().toString(36).substring(2, 15),
  name: data.name,
  email: data.email,
  educationalId: data.educationalId || '',
  avatar: data.avatar || null,
  role: data.role || 'student',
  class: data.classGroup || data.class || '',
  preferences: data.preferences || {
    notifications: true,
    emailNotifications: false,
    autoplay: false,
    volume: 80,
  },
  favorites: data.favorites || [],
  requestHistory: data.requestHistory || [],
  createdAt: data.createdAt || new Date().toISOString(),
});

// Demo user for testing
const demoUser = createUserObject({
  id: 'usr_demo_123',
  name: 'Demo Diák',
  email: 'demo@nttbcs.hu',
  educationalId: '72345678901',
  role: 'student',
  class: '13.E',
  createdAt: '2025-09-01T08:00:00Z',
});

export const UserProvider = ({ children }) => {
  const [user, setUser] = useState(() => {
    // Try to get user from cookie first, then localStorage
    const cookieUser = getCookie('suliradio-user');
    if (cookieUser) return cookieUser;
    
    const saved = localStorage.getItem('suliradio-user');
    return saved ? JSON.parse(saved) : null;
  });

  const [isLoggedIn, setIsLoggedIn] = useState(() => {
    return getCookie('suliradio-loggedin') === true || 
           localStorage.getItem('suliradio-loggedin') === 'true';
  });

  const [sessionToken, setSessionToken] = useState(() => {
    return getCookie('suliradio-session') || 
           localStorage.getItem('suliradio-session') || null;
  });

  // Sync to both localStorage and cookies
  useEffect(() => {
    if (user) {
      localStorage.setItem('suliradio-user', JSON.stringify(user));
      setCookie('suliradio-user', user, 7);
    } else {
      localStorage.removeItem('suliradio-user');
      deleteCookie('suliradio-user');
    }
  }, [user]);

  useEffect(() => {
    localStorage.setItem('suliradio-loggedin', isLoggedIn.toString());
    setCookie('suliradio-loggedin', isLoggedIn, 7);
  }, [isLoggedIn]);

  useEffect(() => {
    if (sessionToken) {
      localStorage.setItem('suliradio-session', sessionToken);
      setCookie('suliradio-session', sessionToken, 7);
    } else {
      localStorage.removeItem('suliradio-session');
      deleteCookie('suliradio-session');
    }
  }, [sessionToken]);

  // Get all registered users
  const getUsers = useCallback(() => {
    const cookieUsers = getCookie('suliradio-users');
    if (cookieUsers) return cookieUsers;
    
    const localUsers = localStorage.getItem('suliradio-users');
    return localUsers ? JSON.parse(localUsers) : [];
  }, []);

  // Save users
  const saveUsers = useCallback((users) => {
    localStorage.setItem('suliradio-users', JSON.stringify(users));
    setCookie('suliradio-users', users, 365);
  }, []);

  // Register new user
  const register = async (userData) => {
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 800));
    
    const users = getUsers();
    
    // Check if user already exists
    const existingUser = users.find(u => 
      u.educationalId === userData.educationalId || 
      u.email === userData.email
    );
    
    if (existingUser) {
      if (existingUser.educationalId === userData.educationalId) {
        return { success: false, error: 'Ez az oktatási azonosító már regisztrálva van!' };
      }
      return { success: false, error: 'Ez az email cím már regisztrálva van!' };
    }
    
    // Create new user
    const newUser = createUserObject({
      ...userData,
      password: userData.password // In real app, this would be hashed
    });
    
    users.push({ ...newUser, password: userData.password });
    saveUsers(users);
    
    return { success: true, user: newUser };
  };

  // Login
  const login = async (identifier, password) => {
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const users = getUsers();
    
    // Find user by name, email, or educational ID
    const foundUser = users.find(u => 
      (u.name === identifier || 
       u.email === identifier || 
       u.educationalId === identifier) && 
      u.password === password
    );
    
    if (foundUser) {
      const token = 'sess_' + Math.random().toString(36).substring(2, 15);
      const { password: _, ...userWithoutPassword } = foundUser;
      
      setSessionToken(token);
      setUser(userWithoutPassword);
      setIsLoggedIn(true);
      
      return { success: true, user: userWithoutPassword };
    }
    
    return { success: false, error: 'Hibás bejelentkezési adatok!' };
  };

  // Logout
  const logout = useCallback(() => {
    setUser(null);
    setIsLoggedIn(false);
    setSessionToken(null);
    
    // Clear localStorage
    localStorage.removeItem('suliradio-user');
    localStorage.removeItem('suliradio-loggedin');
    localStorage.removeItem('suliradio-session');
    
    // Clear cookies
    deleteCookie('suliradio-user');
    deleteCookie('suliradio-loggedin');
    deleteCookie('suliradio-session');
  }, []);

  // Demo login for testing
  const demoLogin = useCallback(async () => {
    await new Promise(resolve => setTimeout(resolve, 300));
    
    const token = 'sess_demo_' + Math.random().toString(36).substring(2, 15);
    setSessionToken(token);
    setUser(demoUser);
    setIsLoggedIn(true);
    
    return { success: true, user: demoUser };
  }, []);

  // Update user data
  const updateUser = useCallback((updates) => {
    setUser(prev => {
      const updated = { ...prev, ...updates };
      
      // Also update in users list
      const users = getUsers();
      const index = users.findIndex(u => u.id === updated.id);
      if (index !== -1) {
        users[index] = { ...users[index], ...updates };
        saveUsers(users);
      }
      
      return updated;
    });
  }, [getUsers, saveUsers]);

  // Update preferences
  const updatePreferences = useCallback((preferences) => {
    setUser(prev => ({
      ...prev,
      preferences: { ...prev?.preferences, ...preferences }
    }));
  }, []);

  // Favorites management
  const addToFavorites = useCallback((songId) => {
    setUser(prev => {
      if (!prev) return prev;
      const newFavorites = [...(prev.favorites || []), songId];
      return { ...prev, favorites: newFavorites };
    });
  }, []);

  const removeFromFavorites = useCallback((songId) => {
    setUser(prev => {
      if (!prev) return prev;
      return { 
        ...prev, 
        favorites: (prev.favorites || []).filter(id => id !== songId) 
      };
    });
  }, []);

  // History management
  const addToHistory = useCallback((request) => {
    setUser(prev => {
      if (!prev) return prev;
      const newHistory = [request, ...(prev.requestHistory || [])].slice(0, 50);
      return { ...prev, requestHistory: newHistory };
    });
  }, []);

  return (
    <UserContext.Provider value={{
      user,
      isLoggedIn,
      sessionToken,
      register,
      login,
      logout,
      demoLogin,
      updateUser,
      updatePreferences,
      addToFavorites,
      removeFromFavorites,
      addToHistory,
      getUsers
    }}>
      {children}
    </UserContext.Provider>
  );
};
