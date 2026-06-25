import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { type Trip } from '../types/Trip';
import { tripService } from '../services/api';
import { Button } from 'primereact/button';

export default function TripDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [trip, setTrip] = useState<Trip | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchTrip = async () => {
      try {
        const data = await tripService.getById(Number(id));
        setTrip(data);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    fetchTrip();
  }, [id]);

  if (loading) return <div style={{ textAlign: 'center', padding: '50px' }}><i className="pi pi-spin pi-spinner" style={{ fontSize: '2rem' }}></i></div>;
  if (!trip) return <div style={{ textAlign: 'center', padding: '50px' }}>Trip not found.</div>;

  return (
    <div className="card" style={{ padding: '30px', borderRadius: '12px', boxShadow: '0 4px 8px rgba(0,0,0,0.1)', maxWidth: '800px', margin: '40px auto' }}>
      <Button icon="pi pi-arrow-left" label="Back" className="p-button-text" style={{ marginBottom: '20px' }} onClick={() => navigate('/')} />
      <h2 style={{ fontSize: '2rem', marginBottom: '10px', marginTop: 0 }}>{trip.name}</h2>
      
      <div style={{ display: 'grid', gap: '15px', marginTop: '20px' }}>
        <p style={{ margin: 0 }}><strong>Destination:</strong> {trip.destination.name}, {trip.destination.country}</p>
        <p style={{ margin: 0 }}><strong>Period:</strong> {new Date(trip.startDate).toLocaleDateString()} - {new Date(trip.endDate).toLocaleDateString()}</p>
        <p style={{ margin: 0 }}><strong>Budget:</strong> {trip.budget} €</p>
        <p style={{ margin: 0 }}><strong>Status:</strong> {trip.status}</p>
      </div>
    </div>
  );
}