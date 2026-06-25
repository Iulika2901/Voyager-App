import { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import TripList from './componets/TripList';
import TripDetails from './componets/TripDetails';
import { Button } from 'primereact/button';
import './App.css';
import fundalDark from './fundal2.jpg';
import fundalLight from './fundal.jpg';
import { Dropdown } from 'primereact/dropdown';

function App() {
  const [isDarkMode, setIsDarkMode] = useState(false);

  const appStyle = {
    backgroundImage: `url(${isDarkMode ? fundalDark : fundalLight})`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    minHeight: '100vh',
    display: 'flex',
    flexDirection: 'column' as const
  };

  return (
    <Router>
      <div className={`app-container ${isDarkMode ? 'dark-theme' : 'light-theme'}`} style={appStyle}>
        
        <div style={{ position: 'relative', textAlign: 'center', marginBottom: '40px', maxWidth: '900px', margin: '0 auto', padding: '20px 0', width: '100%' }}>
          
          <h1 className="fancy-title">Voyager Planner</h1>
          
          <div style={{ position: 'absolute', right: '0', top: '50%', transform: 'translateY(-50%)' }}>
            <Button 
              icon={isDarkMode ? "pi pi-sun" : "pi pi-moon"} 
              rounded 
              severity={isDarkMode ? "warning" : "secondary"}
              onClick={() => setIsDarkMode(!isDarkMode)} 
            />
          </div>
        </div>

        <div style={{ maxWidth: '900px', margin: '0 auto', flex: 1, width: '100%' }}>
          <Routes>
            <Route path="/" element={<TripList />} />
            <Route path="/trip/:id" element={<TripDetails />} />
          </Routes>
        </div>

        <div style={{ display: 'flex', justifyContent: 'center', gap: '15px', marginTop: '50px', paddingBottom: '30px' }}>
          <a href="https://facebook.com" target="_blank" rel="noreferrer" style={{ textDecoration: 'none' }}>
            <Button icon="pi pi-facebook" label="Facebook" rounded style={{ backgroundColor: 'rgba(59, 130, 246, 0.25)', borderColor: 'transparent', color: '#fff', backdropFilter: 'blur(4px)' }} />
          </a>
          <a href="https://discord.com" target="_blank" rel="noreferrer" style={{ textDecoration: 'none' }}>
            <Button icon="pi pi-discord" label="Discord" rounded style={{ backgroundColor: 'rgba(139, 92, 246, 0.25)', borderColor: 'transparent', color: '#fff', backdropFilter: 'blur(4px)' }} />
          </a>
          <a href="https://whatsapp.com" target="_blank" rel="noreferrer" style={{ textDecoration: 'none' }}>
            <Button icon="pi pi-whatsapp" label="WhatsApp" rounded style={{ backgroundColor: 'rgba(34, 197, 94, 0.25)', borderColor: 'transparent', color: '#fff', backdropFilter: 'blur(4px)' }} />
          </a>
        </div>

      </div>
    </Router>
  );
}

export default App;