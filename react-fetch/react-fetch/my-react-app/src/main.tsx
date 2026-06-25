import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'

import "primereact/resources/themes/lara-light-cyan/theme.css"; 
import "primeicons/primeicons.css";
import { PrimeReactProvider } from 'primereact/api';

createRoot(document.getElementById('root')!).render(
  <PrimeReactProvider>
    <App/>
  </PrimeReactProvider>,
)