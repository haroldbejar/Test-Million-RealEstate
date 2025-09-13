import React, { useState, useRef, useEffect } from "react";
import { ChevronDown, MapPin, Search } from "lucide-react";
import { cn } from "../../../lib/utils";
import type { Location } from "../../../types";

interface LocationDropdownProps {
  locations: Location[];
  selectedLocation: string | null;
  onLocationChange: (location: string | null) => void;
}

const LocationDropdown: React.FC<LocationDropdownProps> = ({ locations, selectedLocation, onLocationChange }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const dropdownRef = useRef<HTMLDivElement>(null);

  const filteredLocations = locations.filter((location) =>
    location.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const handleLocationSelect = (location: Location) => {
    const newLocation = location.id === 'all' ? null : location.name;
    onLocationChange(newLocation);
    setIsOpen(false);
    setSearchTerm("");
  };

  const displayText = selectedLocation || "Ubicaciones";

  return (
    <div className="relative" ref={dropdownRef}>
      {/* Dropdown Trigger */}
      <button
        onClick={() => setIsOpen(!isOpen)}
        className={cn(
          "flex items-center space-x-2 px-3 py-2 text-sm font-medium transition-colors duration-200 rounded-md",
          "text-luxury-300 hover:text-white"
        )}
        aria-haspopup="true"
        aria-expanded={isOpen}
      >
        <MapPin className="w-4 h-4" />
        <span className="max-w-32 truncate">{displayText}</span>
        <ChevronDown
          className={cn(
            "w-4 h-4 transition-transform duration-200",
            isOpen && "rotate-180"
          )}
        />
      </button>

      {/* Dropdown Menu */}
      {isOpen && (
        <div className="absolute top-full left-0 mt-2 w-72 bg-popover text-popover-foreground rounded-lg shadow-xl border z-50 animate-slide-down">
          {/* Search Input */}
          <div className="p-3 border-b">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-muted-foreground" />
              <input
                type="text"
                placeholder="Buscar ubicación..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="bg-transparent w-full pl-10 pr-4 py-2 border border-input rounded-md focus:ring-1 focus:ring-ring"
              />
            </div>
          </div>

          {/* Locations List */}
          <div className="max-h-64 overflow-y-auto py-2">
            {filteredLocations.length > 0 ? (
              filteredLocations.map((location) => (
                <button
                  key={location.id}
                  onClick={() => handleLocationSelect(location)}
                  className="w-full text-left px-4 py-3 hover:bg-accent hover:text-accent-foreground transition-colors duration-150 flex items-center justify-between group"
                >
                  <span className="text-sm font-medium">
                    {location.name}
                  </span>
                  <span className="text-xs text-muted-foreground bg-muted px-2 py-1 rounded-full">
                    {location.count}
                  </span>
                </button>
              ))
            ) : (
              <div className="px-4 py-6 text-center">
                <p className="text-sm text-muted-foreground">
                  No se encontraron ubicaciones
                </p>
              </div>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default LocationDropdown;
