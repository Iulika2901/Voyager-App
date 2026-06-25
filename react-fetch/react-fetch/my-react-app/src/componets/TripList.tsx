import { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { type Trip } from '../types/Trip';
import { tripService } from '../services/api';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';
import { Toast } from 'primereact/toast';
import { InputText } from 'primereact/inputtext';
import { Dropdown } from 'primereact/dropdown';
import TripForm from './TripForm';

export default function TripList() {
  const navigate = useNavigate();
  const toast = useRef<Toast>(null);
  
  const [trips, setTrips] = useState<Trip[]>([]);
  const [filteredTrips, setFilteredTrips] = useState<Trip[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingTrip, setEditingTrip] = useState<Trip | null>(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedFilterStatus, setSelectedFilterStatus] = useState<string | null>(null);

  const statusFilterOptions = [
    { label: 'All Statuses', value: null },
    { label: 'Planning', value: 'Planning' },
    { label: 'Active', value: 'Active' },
    { label: 'Completed', value: 'Completed' },
    { label: 'Cancelled', value: 'Cancelled' }
  ];

  useEffect(() => {
    loadTrips();
  }, []);

  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      if (searchTerm) {
        const filtered = trips.filter(t => 
          t.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
          t.destination.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredTrips(filtered);
      } else {
        setFilteredTrips(trips);
      }
    }, 500);

    return () => clearTimeout(delayDebounceFn);
  }, [searchTerm, trips]);

  const loadTrips = async (status?: string | null) => {
    try {
      setLoading(true);
      const data = await tripService.getAll(status ? { status } : undefined);
      setTrips(data);
      setFilteredTrips(data);
    } catch (err: any) {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: err.message });
    } finally {
      setLoading(false);
    }
  };

  const handleApplyFilter = () => {
    loadTrips(selectedFilterStatus);
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      handleApplyFilter();
    }
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm('Are you sure you want to delete this trip?')) return;
    
    try {
      await tripService.delete(id);
      const updatedTrips = trips.filter(t => t.id !== id);
      setTrips(updatedTrips);
      setFilteredTrips(updatedTrips);
      toast.current?.show({ severity: 'success', summary: 'Deleted', detail: 'Trip successfully deleted!' });
    } catch (err: any) {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: err.message });
    }
  };

  const handleSave = async (data: any) => {
    const actionMessage = editingTrip ? 'Are you sure you want to save these changes?' : 'Are you sure you want to add this trip?';
    if (!window.confirm(actionMessage)) return;

    try {
      if (editingTrip) {
        await tripService.update(editingTrip.id, data);
        toast.current?.show({ severity: 'info', summary: 'Updated', detail: 'Changes have been saved!' });
      } else {
        await tripService.create(data);
        toast.current?.show({ severity: 'success', summary: 'Added', detail: 'Trip successfully added!' });
      }
      setShowForm(false);
      setEditingTrip(null);
      loadTrips(selectedFilterStatus);
    } catch (err: any) {
      toast.current?.show({ severity: 'error', summary: 'Error', detail: err.message });
    }
  };

  const actionTemplate = (rowData: Trip) => (
    <div style={{ display: 'flex', gap: '8px' }}>
      <Button icon="pi pi-pencil" rounded severity="warning" onClick={(e) => { e.stopPropagation(); setEditingTrip(rowData); setShowForm(true); }} />
      <Button icon="pi pi-trash" rounded severity="danger" onClick={(e) => { e.stopPropagation(); handleDelete(rowData.id); }} />
    </div>
  );

  if (loading) return <div style={{ textAlign: 'center', padding: '50px' }}><i className="pi pi-spin pi-spinner" style={{ fontSize: '2rem' }}></i></div>;

  return (
    <div className="card" style={{ padding: '20px', borderRadius: '12px', boxShadow: '0 4px 8px rgba(0,0,0,0.1)' }}>
      <Toast ref={toast} position="center" />

      {!showForm ? (
        <>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px', flexWrap: 'wrap', gap: '15px' }}>
            <h2 style={{ margin: 0 }}>Trips List</h2>
            
            <div style={{ display: 'flex', gap: '15px', flex: 1, margin: '0 20px', alignItems: 'center' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                    <span style={{ fontWeight: 'bold', whiteSpace: 'nowrap' }}>Choose status:</span>
                    <Dropdown 
                        value={selectedFilterStatus} 
                        options={statusFilterOptions} 
                        onChange={(e) => setSelectedFilterStatus(e.value)} 
                        placeholder="Select Status" 
                        style={{ width: '160px' }}
                    />
                </div>

                <span className="p-input-icon-left" style={{ flex: 1, maxWidth: '250px' }}>
                    <i className="pi pi-search" style={{ fontSize: '0.85rem' }} />
                    <InputText 
                        placeholder="Search trip..." 
                        value={searchTerm} 
                        onChange={(e) => setSearchTerm(e.target.value)}
                        onKeyDown={handleKeyDown}
                        style={{ width: '100%', paddingLeft: '1.2rem' }}
                    />
                </span>

                <Button label="Search" onClick={handleApplyFilter} />
            </div>

            <Button label="Add Trip" icon="pi pi-plus" onClick={() => { setEditingTrip(null); setShowForm(true); }} />
          </div>

          <DataTable 
            value={filteredTrips} 
            paginator 
            rows={5} 
            emptyMessage="No trips found."
            selectionMode="single"
            onRowClick={(e) => navigate(`/trip/${e.data.id}`)}
            style={{ cursor: 'pointer' }}
          >
            <Column field="name" header="Name" sortable></Column>
            <Column body={(t) => `${t.destination.name}, ${t.destination.country}`} header="Destination"></Column>
            <Column body={(t) => new Date(t.startDate).toLocaleDateString()} header="Start"></Column>
            <Column body={(t) => new Date(t.endDate).toLocaleDateString()} header="End"></Column>
            <Column field="budget" header="Budget" body={(t) => `${t.budget} €`}></Column>
            <Column body={actionTemplate} header="Actions"></Column>
          </DataTable>
        </>
      ) : (
        <TripForm trip={editingTrip} onSave={handleSave} onCancel={() => { setShowForm(false); setEditingTrip(null); }} />
      )}
    </div>
  );
}