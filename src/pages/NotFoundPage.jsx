import { Link } from 'react-router-dom';
import { Home, AlertTriangle } from 'lucide-react';

const NotFoundPage = () => {
  return (
    <div className="page not-found-page">
      <div className="not-found-content">
        <AlertTriangle size={80} className="not-found-icon" />
        <h1>404</h1>
        <h2>Az oldal nem található</h2>
        <p>
          A keresett oldal nem létezik vagy áthelyezésre került.
          Ellenőrizd az URL-t vagy térj vissza a főoldalra.
        </p>
        <Link to="/" className="btn btn-primary">
          <Home size={20} />
          Vissza a főoldalra
        </Link>
      </div>
    </div>
  );
};

export default NotFoundPage;
