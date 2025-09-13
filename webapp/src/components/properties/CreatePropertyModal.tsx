import React, { useState } from 'react';
import { X, Loader2, Upload } from 'lucide-react';
import { useDispatch, useSelector } from 'react-redux';
import { createProperty } from '../../store/slices/propertySlice';
import type { RootState, AppDispatch } from '../../store';

interface CreatePropertyModalProps {
  isOpen: boolean;
  onClose: () => void;
}

const CreatePropertyModal: React.FC<CreatePropertyModalProps> = ({ isOpen, onClose }) => {
  const dispatch = useDispatch<AppDispatch>();
  const { status, error } = useSelector((state: RootState) => state.properties);

  const [title, setTitle] = useState('');
  const [amount, setAmount] = useState(0);
  const [city, setCity] = useState('');
  const [state, setState] = useState('');
  const [country, setCountry] = useState('');
  const [bedrooms, setBedrooms] = useState(0);
  const [bathrooms, setBathrooms] = useState(0);
  const [squareFootage, setSquareFootage] = useState(0);
  const [yearBuilt, setYearBuilt] = useState(new Date().getFullYear());
  const [propertyType, setPropertyType] = useState('House');
  const [propertyStatus, setPropertyStatus] = useState('ForSale');
  const [codeOwner, setCodeOwner] = useState(0);
  const [imageFile, setImageFile] = useState<File | null>(null);

  if (!isOpen) return null;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append('Title', title);
    formData.append('CodeOwner', String(codeOwner));
    formData.append('Amount', String(amount));
    formData.append('Currency', 'COP'); // Hardcoded for now as per plan
    formData.append('City', city);
    formData.append('State', state);
    formData.append('Country', country);
    formData.append('Bedrooms', String(bedrooms));
    formData.append('Bathrooms', String(bathrooms));
    formData.append('SquareFootage', String(squareFootage));
    formData.append('YearBuilt', String(yearBuilt));
    formData.append('PropertyType', propertyType);
    formData.append('Status', propertyStatus);
    
    if (imageFile) {
      formData.append('ImageFile', imageFile);
    }

    const resultAction = await dispatch(createProperty(formData));

    if (createProperty.fulfilled.match(resultAction)) {
      onClose();
    }
  };

  return (
    <div 
      className="fixed inset-0 bg-black/60 z-50 flex items-center justify-center animate-fade-in-fast p-4"
      onClick={onClose}
    >
      <div 
        className="bg-card text-card-foreground rounded-lg shadow-xl w-full max-w-3xl border animate-slide-up-fast overflow-y-auto max-h-[95vh]"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="flex items-center justify-between p-4 border-b sticky top-0 bg-card z-10">
          <h2 className="text-xl font-semibold">Registrar Nueva Propiedad</h2>
          <button 
            onClick={onClose}
            className="p-1 rounded-full text-muted-foreground hover:bg-muted transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        <form onSubmit={handleSubmit} className="p-6 space-y-6">
          {/* Fila 1: Título y Código Propietario */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label htmlFor="title" className="block text-sm font-medium mb-1">Título de la Propiedad</label>
              <input id="title" type="text" value={title} onChange={(e) => setTitle(e.target.value)} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="codeOwner" className="block text-sm font-medium mb-1">Código del Propietario</label>
              <input id="codeOwner" type="number" value={codeOwner} onChange={(e) => setCodeOwner(Number(e.target.value))} className="w-full input" required />
            </div>
          </div>

          {/* Fila 2: Ubicación */}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div>
              <label htmlFor="country" className="block text-sm font-medium mb-1">País</label>
              <input id="country" type="text" value={country} onChange={(e) => setCountry(e.target.value)} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="state" className="block text-sm font-medium mb-1">Departamento/Estado</label>
              <input id="state" type="text" value={state} onChange={(e) => setState(e.target.value)} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="city" className="block text-sm font-medium mb-1">Ciudad</label>
              <input id="city" type="text" value={city} onChange={(e) => setCity(e.target.value)} className="w-full input" required />
            </div>
          </div>

          {/* Fila 3: Detalles (Habitaciones, Baños, Area, Año) */}
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
             <div>
              <label htmlFor="bedrooms" className="block text-sm font-medium mb-1">Habs.</label>
              <input id="bedrooms" type="number" value={bedrooms} onChange={(e) => setBedrooms(Number(e.target.value))} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="bathrooms" className="block text-sm font-medium mb-1">Baños</label>
              <input id="bathrooms" type="number" value={bathrooms} onChange={(e) => setBathrooms(Number(e.target.value))} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="squareFootage" className="block text-sm font-medium mb-1">Área (m²)</label>
              <input id="squareFootage" type="number" value={squareFootage} onChange={(e) => setSquareFootage(Number(e.target.value))} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="yearBuilt" className="block text-sm font-medium mb-1">Año Constr.</label>
              <input id="yearBuilt" type="number" value={yearBuilt} onChange={(e) => setYearBuilt(Number(e.target.value))} className="w-full input" required />
            </div>
          </div>

          {/* Fila 4: Precio y Tipo/Estado */}
           <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <div>
              <label htmlFor="amount" className="block text-sm font-medium mb-1">Precio (COP)</label>
              <input id="amount" type="number" value={amount} onChange={(e) => setAmount(Number(e.target.value))} className="w-full input" required />
            </div>
            <div>
              <label htmlFor="propertyType" className="block text-sm font-medium mb-1">Tipo de Propiedad</label>
              <select id="propertyType" value={propertyType} onChange={(e) => setPropertyType(e.target.value)} className="w-full select" required>
                <option value="House">Casa</option>
                <option value="Apartment">Apartamento</option>
                <option value="Land">Terreno</option>
                <option value="Commercial">Comercial</option>
              </select>
            </div>
            <div>
              <label htmlFor="propertyStatus" className="block text-sm font-medium mb-1">Estado</label>
              <select id="propertyStatus" value={propertyStatus} onChange={(e) => setPropertyStatus(e.target.value)} className="w-full select" required>
                <option value="ForSale">En Venta</option>
                <option value="Sold">Vendida</option>
                <option value="Rent">En Renta</option>
              </select>
            </div>
          </div>
          
          {/* Fila 5: Carga de Imágenes */}
          <div>
            <label className="block text-sm font-medium mb-2">Imágen de Portada</label>
            <div className="flex items-center justify-center w-full">
              <label htmlFor="dropzone-file" className="flex flex-col items-center justify-center w-full h-40 border-2 border-dashed rounded-lg cursor-pointer bg-muted/50 hover:bg-muted">
                <div className="flex flex-col items-center justify-center pt-5 pb-6">
                  <Upload className="w-8 h-8 mb-3 text-muted-foreground" />
                  <p className="mb-2 text-sm text-muted-foreground">
                    <span className="font-semibold">Click para subir</span> o arrastra y suelta
                  </p>
                  <p className="text-xs text-muted-foreground">PNG, JPG, GIF (MAX. 5MB)</p>
                </div>
                <input id="dropzone-file" type="file" className="hidden" onChange={(e) => setImageFile(e.target.files ? e.target.files[0] : null)} accept="image/*" />
              </label>
            </div> 
            {imageFile && <p className='mt-2 text-sm text-muted-foreground'>Archivo seleccionado: {imageFile.name}</p>}
          </div>

          <style>{`
            .input, .select {
              padding: 8px 12px;
              border-radius: 6px;
              border: 1px solid hsl(var(--border));
              background-color: hsl(var(--input));
              width: 100%;
              transition: box-shadow .2s;
            }
            .input:focus, .select:focus {
              outline: none;
              box-shadow: 0 0 0 2px hsl(var(--ring));
            }
          `}</style>

          <div className="pt-4 border-t">
            <button 
              type="submit"
              className="w-full bg-primary text-primary-foreground hover:bg-primary/90 font-semibold py-2.5 px-4 rounded-md transition-colors flex items-center justify-center disabled:opacity-50"
              disabled={status === 'loading'}
            >
              {status === 'loading' ? <Loader2 className="mr-2 h-5 w-5 animate-spin" /> : 'Crear Propiedad'}
            </button>
          </div>

          {status === 'failed' && <p className='mt-2 text-sm text-red-500 text-center'>{error}</p>}
        </form>
      </div>
    </div>
  );
};

export default CreatePropertyModal;
