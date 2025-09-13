import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { type RootState, type AppDispatch } from "./store";
import { fetchProperties } from "./store/slices/propertySlice";
import Layout from "./components/layout";
import PropertyGrid from "./components/PropertyGrid";
import Pagination from "./components/Pagination";
import PropertyDetailPage from "./pages/PropertyDetailPage";
import type { Property, Location } from "./types";

const getDropdownLocations = (properties: Property[]): Location[] => {
  if (!properties) return [];
  const cities = [...new Set(properties.map((p) => p.city))];
  const locations: Location[] = cities.map((city) => ({
    id: city.toLowerCase(),
    name: city,
    count: properties.filter((p) => p.city === city).length,
    country: "Colombia",
  }));

  return [
    {
      id: "all",
      name: "Todas las ubicaciones",
      count: properties.length,
      country: "Colombia",
    },
    ...locations,
  ];
};

interface PageProps {
  locationFilter: string | null;
}

const HomePage: React.FC<PageProps> = ({ locationFilter }) => {
  const { properties, status } = useSelector((state: RootState) => state.properties);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 6;

  const filteredProperties = locationFilter
    ? properties.filter((p) => p.city === locationFilter)
    : properties;

  const totalPages = Math.ceil(filteredProperties.length / pageSize);
  const paginatedProperties = filteredProperties.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  if (status === "loading") {
    return <p className="text-center py-12">Cargando propiedades...</p>;
  }

  return (
    <div className="min-h-screen py-12">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center">
          <h1 className="text-4xl font-luxury font-bold text-foreground mb-8">
            Bienvenido a{" "}
            <span className="luxury-gradient-text">MillionTest</span> RealEstate
          </h1>
          <p className="text-xl text-muted-foreground mb-12">
            Descubre las propiedades más exclusivas de Colombia
          </p>
          <PropertyGrid properties={paginatedProperties} />
          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            onPageChange={setCurrentPage}
          />
        </div>
      </div>
    </div>
  );
};

const PropertiesPage: React.FC = () => (
  <div className="min-h-screen py-12">
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <h1 className="text-3xl font-luxury font-bold text-foreground mb-8">
        Propiedades
      </h1>
      <p className="text-muted-foreground">
        Lista de propiedades disponibles...
      </p>
    </div>
  </div>
);

const FavoritesPage: React.FC<PageProps> = ({ locationFilter }) => {
  const { properties } = useSelector((state: RootState) => state.properties);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 6;

  const favoriteProperties = properties.filter((p) => p.isFavorite);

  const filteredFavorites = locationFilter
    ? favoriteProperties.filter((p) => p.city === locationFilter)
    : favoriteProperties;

  const totalPages = Math.ceil(filteredFavorites.length / pageSize);
  const paginatedFavorites = filteredFavorites.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className="min-h-screen py-12">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <h1 className="text-3xl font-luxury font-bold text-foreground mb-8">
          Mis Favoritos
        </h1>
        <PropertyGrid 
          properties={paginatedFavorites} 
          emptyMessage="No tienes propiedades favoritas."
        />
        <Pagination
          currentPage={currentPage}
          totalPages={totalPages}
          onPageChange={setCurrentPage}
        />
      </div>
    </div>
  );
};

const App: React.FC = () => {
  const dispatch: AppDispatch = useDispatch();
  const { properties, status } = useSelector((state: RootState) => state.properties);
  const [locationFilter, setLocationFilter] = useState<string | null>(null);

  useEffect(() => {
    if (status === 'idle') {
      dispatch(fetchProperties({ pageNumber: 1, pageSize: 100 })); // Fetch more to have enough data
    }
  }, [status, dispatch]);

  const dropdownLocations = getDropdownLocations(properties);

  return (
    <Router>
      <Layout
        locations={dropdownLocations}
        selectedLocation={locationFilter}
        onLocationChange={setLocationFilter}
      >
        <Routes>
          <Route
            path="/"
            element={<HomePage locationFilter={locationFilter} />}
          />
          <Route path="/properties" element={<PropertiesPage />} />
          <Route path="/properties/:id" element={<PropertyDetailPage />} />
          <Route
            path="/favorites"
            element={<FavoritesPage locationFilter={locationFilter} />}
          />
        </Routes>
      </Layout>
    </Router>
  );
};

export default App;


