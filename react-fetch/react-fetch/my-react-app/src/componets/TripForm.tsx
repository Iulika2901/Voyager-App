import { useState, useEffect } from 'react';
import { type Trip, TripStatus, type TripStatusType } from '../types/Trip';
import { InputText } from 'primereact/inputtext';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';

interface Props {
  trip?: Trip | null;
  onSave: (data: any) => void;
  onCancel: () => void;
}

export default function TripForm({ trip, onSave, onCancel }: Props) {
  const destinationImages = [
    "https://images.unsplash.com/photo-1499856871958-5b9627545d1a?auto=format&fit=crop&w=300&q=80",
    "https://images.unsplash.com/photo-1513635269975-59663e0ac1ad?auto=format&fit=crop&w=300&q=80",
    "https://images.unsplash.com/photo-1523906834658-6e24ef2386f9?auto=format&fit=crop&w=300&q=80",
    "https://images.unsplash.com/photo-1506973035872-a4ec16b8e8d9?auto=format&fit=crop&w=300&q=80",
    "https://images.unsplash.com/photo-1540959733332-eab4deabeeaf?auto=format&fit=crop&w=300&q=80",
  ];

  const carouselImages = [...destinationImages, ...destinationImages];

  const [formData, setFormData] = useState({
    name: '',
    destName: '',
    destCountry: '',
    start: '',
    end: '',
    budget: 0,
    status: TripStatus.Planning as TripStatusType
  });

  useEffect(() => {
    if (trip) {
      setFormData({
        name: trip.name,
        destName: trip.destination.name,
        destCountry: trip.destination.country,
        start: trip.startDate.split('T')[0],
        end: trip.endDate.split('T')[0],
        budget: trip.budget,
        status: typeof trip.status === 'string' ? (TripStatus as any)[trip.status] : trip.status
      });
    }
  }, [trip]);

  const statusOptions = Object.keys(TripStatus).map(key => ({ label: key, value: (TripStatus as any)[key] }));

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave({
      name: formData.name,
      destinationName: formData.destName,
      destinationCountry: formData.destCountry,
      startDate: new Date(formData.start).toISOString(),
      endDate: new Date(formData.end).toISOString(),
      budget: Number(formData.budget),
      status: formData.status
    });
  };

  return (
    <form onSubmit={handleSubmit} className="p-fluid">
      <h3 style={{ marginTop: 0 }}>{trip ? 'Edit Trip' : 'Add New Trip'}</h3>
      
      <div className="p-field" style={{ marginBottom: '15px' }}>
        <InputText placeholder="Trip Name" value={formData.name} onChange={(e) => setFormData({...formData, name: e.target.value})} required />
      </div>
      
      <div style={{ display: 'flex', gap: '15px', marginBottom: '15px' }}>
        <InputText placeholder="City" style={{ flex: 1 }} value={formData.destName} onChange={(e) => setFormData({...formData, destName: e.target.value})} required />
        <InputText placeholder="Country" style={{ flex: 1 }} value={formData.destCountry} onChange={(e) => setFormData({...formData, destCountry: e.target.value})} required />
      </div>
      
      <div style={{ display: 'flex', gap: '15px', marginBottom: '15px' }}>
        <div style={{ flex: 1 }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>Start Date</label>
            <InputText type="date" value={formData.start} onChange={(e) => setFormData({...formData, start: e.target.value})} required style={{ width: '100%' }} />
        </div>
        <div style={{ flex: 1 }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>End Date</label>
            <InputText type="date" value={formData.end} onChange={(e) => setFormData({...formData, end: e.target.value})} required style={{ width: '100%' }} />
        </div>
      </div>
      
      <div style={{ display: 'flex', gap: '15px', marginBottom: '25px', alignItems: 'flex-end' }}>
        <div style={{ flex: 1 }}>
            <label style={{ display: 'block', marginBottom: '5px' }}>Budget (€)</label>
            <InputText type="number" value={formData.budget.toString()} onChange={(e) => setFormData({...formData, budget: Number(e.target.value)})} required style={{ width: '100%' }} />
        </div>
        <Dropdown style={{ flex: 1 }} value={formData.status} options={statusOptions} onChange={(e) => setFormData({...formData, status: e.value})} placeholder="Status" />
      </div>

      <div className="destination-carousel-wrapper">
        <div className="destination-title">Choose your next destination ✈️</div>
        <div className="carousel-track">
            {carouselImages.map((src, index) => (
                <img key={index} src={src} alt={`Destination ${index}`} className="carousel-image" />
            ))}
        </div>
      </div>
      
      <div style={{ display: 'flex', gap: '15px' }}>
        <Button label="Save" icon="pi pi-check" type="submit" severity="success" />
        <Button label="Cancel" icon="pi pi-times" type="button" severity="secondary" onClick={onCancel} />
      </div>
    </form>
  );
}