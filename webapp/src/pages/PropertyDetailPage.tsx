import React from 'react';
import { useParams } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { type RootState } from '../store';
import { Bed, Bath, Car, Ruler } from 'lucide-react';

const PropertyDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const property = useSelector((state: RootState) => 
    state.properties.properties.find(p => p.id.toString() === id)
  );

  if (!property) {
    return (
      <div className="min-h-screen py-12 flex items-center justify-center">
        <p className="text-2xl text-muted-foreground">Propiedad no encontrada o cargando...</p>
      </div>
    );
  }

  const locationString = `${property.city}, ${property.state}, ${property.country}`;

  return (
    <div className="min-h-screen py-12">
      <div className="max-w-5xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Galería de Imágenes (simplificada) */}
        <div className="aspect-video w-full bg-muted rounded-lg overflow-hidden mb-8">
          {property.imageUrl && property.imageUrl.length > 0 ? (
            <img src={property.imageUrl[0]} alt={property.title} className="w-full h-full object-cover" />
          ) : (
            <div className="w-full h-full bg-gray-200 flex items-center justify-center">
              <span className="text-gray-500">Sin imagen</span>
            </div>
          )}
        </div>

        {/* Contenido Principal */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          {/* Columna de Información */}
          <div className="md:col-span-2">
            <h1 className="text-4xl font-luxury font-bold text-foreground mb-2">
              {property.title}
            </h1>
            <p className="text-lg text-muted-foreground mb-6">
              {locationString}
            </p>
            <p className="text-foreground leading-relaxed">
              {property.description}
            </p>
          </div>

          {/* Columna de Precio y Características */}
          <div className="md:col-span-1">
            <div className="bg-card border rounded-lg p-6 sticky top-24">
              <span className="text-4xl font-bold luxury-gradient-text">
                {property.currency} ${property.amount.toLocaleString()}
              </span>
              <p className="text-muted-foreground text-sm mb-6">
                {property.status === 'for-rent' ? 'En Renta' : 'En Venta'}
              </p>

              <div className="grid grid-cols-2 gap-4 text-sm my-6">
                <div className="flex items-center"><Bed className="w-4 h-4 mr-2 text-muted-foreground" /> {property.bedrooms} Habs.</div>
                <div className="flex items-center"><Bath className="w-4 h-4 mr-2 text-muted-foreground" /> {property.bathrooms} Baños</div>
                <div className="flex items-center"><Ruler className="w-4 h-4 mr-2 text-muted-foreground" /> {property.squareFootage} m²</div>
                {/* Asumiendo que no hay parking en los datos de la API */}
                <div className="flex items-center"><Car className="w-4 h-4 mr-2 text-muted-foreground" /> 0 Parq.</div>
              </div>

              <button className="w-full bg-primary text-primary-foreground hover:bg-primary/90 px-6 py-3 rounded-md font-semibold transition-transform hover:scale-105">
                Contactar al Agente
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PropertyDetailPage;
