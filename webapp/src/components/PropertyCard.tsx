import React from "react";
import { Link } from "react-router-dom";
import { Heart } from "lucide-react";
import { cn } from "../lib/utils";
import type { Property } from "../types";

type PropertyCardProps = {
  property: Property;
  isFavorite: boolean;
  onToggleFavorite: (id: string) => void;
};

const PropertyCard: React.FC<PropertyCardProps> = ({
  property,
  isFavorite,
  onToggleFavorite,
}) => {
  const { city, state, country } = property;
  const locationString = `${city}, ${state}, ${country}`;
  return (
    <div className="bg-card rounded-lg border overflow-hidden group relative transition-all hover:shadow-xl hover:-translate-y-1">
      {/* Contenedor de la Imagen y Botón de Favorito */}
      <div className="aspect-video bg-muted relative">
        <Link
          to={`/properties/${property.id}`}
          className="absolute inset-0 z-0"
        ></Link>
        {property.imageUrl[0] && (
          <img
            src={property.imageUrl[0]}
            alt={property.title}
            className="w-full h-full object-cover"
          />
        )}

        <button
          onClick={() => onToggleFavorite(property.id)}
          className="absolute top-3 right-3 z-10 p-2 rounded-full bg-black/30 hover:bg-black/50 transition-colors duration-200"
          aria-label="Añadir a favoritos"
        >
          <Heart
            className={cn(
              "w-5 h-5 text-white transition-all",
              isFavorite ? "fill-red-500 stroke-red-600" : "fill-transparent"
            )}
          />
        </button>
      </div>

      {/* Contenido de la tarjeta */}
      <div className="p-6 flex flex-col">
        <h3 className="font-luxury font-semibold text-lg text-card-foreground mb-2">
          {property.title}
        </h3>
        <p className="text-muted-foreground text-sm mb-1">{locationString}</p>
        <p className="text-muted-foreground text-xs mb-4 h-[40px] line-clamp-2">
          {property.description}
        </p>
        <div className="flex justify-between items-center mt-auto">
          <span className="text-2xl font-bold luxury-gradient-text">
            {property.currency} ${property.amount.toLocaleString()}
          </span>
          <Link
            to={`/properties/${property.id}`}
            className="bg-luxury-gold-500 text-luxury-darker hover:bg-luxury-gold-400 px-5 py-2 rounded-md text-sm font-semibold z-10 relative transition-all duration-300 ease-in-out hover:scale-105"
          >
            Ver Detalles
          </Link>
        </div>
      </div>
    </div>
  );
};

export default PropertyCard;
