import React, { useState } from "react";
import { Link } from "react-router-dom";
import { Menu, X, PlusCircle, Building } from "lucide-react";
import { useSelector } from "react-redux";
import { cn } from "../../../lib/utils";
import DarkModeToggle from "./DarckModeToggle";
import LocationDropdown from "./LocationDropdown";
import FavoritesButton from "./FavoritesButton";
import UserMenu from "./UserMenu";
import CreatePropertyModal from "../../properties/CreatePropertyModal";
import type { RootState } from "../../../store";
import type { Location } from "../../../types";
import SearchControls from "./SearchControls";

interface TopBarProps {
  locations: Location[];
  selectedLocation: string | null;
  onLocationChange: (location: string | null) => void;
}

const TopBar: React.FC<TopBarProps> = ({ locations, selectedLocation, onLocationChange }) => {
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const [isCreateModalOpen, setCreateModalOpen] = useState(false);
  const { token } = useSelector((state: RootState) => state.auth);

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  return (
    <header className="bg-luxury-darker text-white shadow-2xl relative z-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <div className="flex-shrink-0">
            <Link to="/" className="flex items-center space-x-2 group">
              <Building className="h-8 w-8 text-luxury-gold-500 group-hover:text-luxury-gold-400" />
              <span className="text-xl font-luxury font-bold tracking-tight transition-colors duration-200 group-hover:text-luxury-gold-400">
                Million
              </span>
            </Link>
          </div>

          {/* Search Controls */}
          <div className="hidden md:flex flex-1 justify-center px-4">
            <SearchControls />
          </div>

          {/* Desktop Navigation Menu */}
          <nav className="hidden md:flex items-center space-x-4">
            <DarkModeToggle />
            <div className="w-px h-5 bg-luxury-600"></div>
            <LocationDropdown 
              locations={locations}
              selectedLocation={selectedLocation}
              onLocationChange={onLocationChange}
            />
            <FavoritesButton />

            {/* Botón Crear Propiedad */}
            {token && (
              <button 
                onClick={() => setCreateModalOpen(true)}
                className="text-sm font-medium transition-colors duration-200 px-3 py-2 rounded-md text-luxury-300 hover:text-white flex items-center space-x-2"
              >
                <PlusCircle className="w-4 h-4" />
                <span>Crear</span>
              </button>
            )}

            <UserMenu />
          </nav>

          {/* Mobile menu button */}
          <div className="md:hidden">
            <button
              type="button"
              onClick={toggleMobileMenu}
              className={cn(
                "inline-flex items-center justify-center p-2 rounded-md transition-all duration-200",
                "text-luxury-300 hover:text-white hover:bg-luxury-800",
                "focus:outline-none focus:ring-2 focus:ring-inset focus:ring-luxury-gold-500"
              )}
              aria-expanded={isMobileMenuOpen}
            >
              <span className="sr-only">
                {isMobileMenuOpen ? "Cerrar menú" : "Abrir menú principal"}
              </span>
              {isMobileMenuOpen ? <X className="h-6 w-6" /> : <Menu className="h-6 w-6" />}
            </button>
          </div>
        </div>
      </div>

      {/* Mobile menu */}
      <div
        className={cn(
          "md:hidden transition-all duration-300 ease-in-out",
          isMobileMenuOpen
            ? "opacity-100 max-h-96 visible"
            : "opacity-0 max-h-0 invisible overflow-hidden"
        )}
      >
        <div className="px-4 pt-4 pb-6 space-y-4 bg-luxury-900 border-t border-luxury-800">
          <div className="flex items-center justify-between py-2">
            <span className="text-sm font-medium text-luxury-300">Tema</span>
            <DarkModeToggle />
          </div>
          <div className="py-2 border-t border-luxury-800">
            <LocationDropdown 
              locations={locations}
              selectedLocation={selectedLocation}
              onLocationChange={onLocationChange}
            />
          </div>
          <div className="py-2 border-t border-luxury-800">
            <FavoritesButton />
          </div>
          {token && (
            <div className="py-2 border-t border-luxury-800">
              <button 
                onClick={() => { setCreateModalOpen(true); toggleMobileMenu(); }}
                className="w-full flex items-center space-x-2 px-3 py-2 text-sm font-medium transition-colors duration-200 rounded-md text-luxury-300 hover:text-white"
              >
                <PlusCircle className="w-4 h-4" />
                <span>Crear Propiedad</span>
              </button>
            </div>
          )}
          <div className="py-2 border-t border-luxury-800">
            <UserMenu />
          </div>
        </div>
      </div>

      {/* Modal de Crear Propiedad */}
      <CreatePropertyModal isOpen={isCreateModalOpen} onClose={() => setCreateModalOpen(false)} />
    </header>
  );
};

export default TopBar;
