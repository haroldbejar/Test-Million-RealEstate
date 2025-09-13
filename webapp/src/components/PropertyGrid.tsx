import React from "react";
import { useDispatch } from "react-redux";
import { type AppDispatch } from "../store";
import { toggleFavorite } from "../store/slices/propertySlice";
import PropertyCard from "./PropertyCard";
import type { Property } from "../types";

type PropertyGridProps = {
  properties: Property[];
  emptyMessage?: string;
};

const PropertyGrid: React.FC<PropertyGridProps> = ({
  properties,
  emptyMessage = "No se encontraron propiedades.",
}) => {
  const dispatch: AppDispatch = useDispatch();

  const handleToggleFavorite = (id: string) => {
    dispatch(toggleFavorite(id));
  };

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mt-16">
      {properties.length > 0 ? (
        properties.map((property) => (
          <PropertyCard
            key={property.id}
            property={property}
            isFavorite={property.isFavorite}
            onToggleFavorite={() => handleToggleFavorite(property.id)}
          />
        ))
      ) : (
        <p className="col-span-3 text-center text-muted-foreground">
          {emptyMessage}
        </p>
      )}
    </div>
  );
};

export default PropertyGrid;
