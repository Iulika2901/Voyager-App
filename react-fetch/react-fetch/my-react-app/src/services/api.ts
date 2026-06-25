import { type Trip } from '../types/Trip';

const API_URL = 'http://localhost:5260/api/Trip';

export const tripService = {
  
  getAll: async (filters?: { status?: string }) => {
    // AICI ERA PROBLEMA: Acum folosim API_URL-ul tău real
    let url = API_URL; 
    
    if (filters && filters.status) {
      const params = new URLSearchParams();
      params.append('Status', filters.status);
      url += `?${params.toString()}`;
    }

    const response = await fetch(url);
    if (!response.ok) throw new Error('Failed to fetch trips');
    return response.json();
  },

  getById: async (id: number): Promise<Trip> => {
    const res = await fetch(`${API_URL}/${id}`);
    if (!res.ok) throw new Error('Eroare la încărcarea excursiei.');
    return res.json();
  },
  
  create: async (trip: any): Promise<Trip> => {
    const res = await fetch(API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(trip)
    });
    if (!res.ok) throw new Error('Eroare la crearea excursiei.');
    return res.json();
  },

  update: async (id: number, trip: any): Promise<Trip> => {
    const res = await fetch(`${API_URL}/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ ...trip, id })  
    });
    if (!res.ok) throw new Error('Eroare la actualizare.');
    return res.json();
  },

  delete: async (id: number): Promise<void> => {
    const res = await fetch(`${API_URL}/${id}`, { method: 'DELETE' });
    if (!res.ok) throw new Error('Eroare la ștergere.');
  }
};