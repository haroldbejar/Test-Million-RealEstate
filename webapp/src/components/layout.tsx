import React from "react";
import TopBar from "./layout/topbar/TopBar";
import type { Location } from "../types";

interface LayoutProps {
  children: React.ReactNode;
  locations: Location[];
  selectedLocation: string | null;
  onLocationChange: (location: string | null) => void;
}

const Layout: React.FC<LayoutProps> = ({ children, locations, selectedLocation, onLocationChange }) => {
  return (
    <div className="min-h-screen bg-white dark:bg-luxury-darker">
      {/* Header */}
      <TopBar 
        locations={locations}
        selectedLocation={selectedLocation}
        onLocationChange={onLocationChange}
      />

      {/* Main Content */}
      <main className="flex-1">{children}</main>

      {/* Footer */}
      <footer className="bg-luxury-darker text-white mt-auto">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            {/* Logo & Description */}
            <div className="md:col-span-2">
              <h3 className="text-lg font-luxury font-bold mb-4">
                MILLION<span className="luxury-gradient-text">TEST</span>
                REALESTATE
              </h3>
              <p className="text-luxury-300 text-sm leading-relaxed mb-6">
                Tu socio confiable en bienes raíces de lujo. Descubre las
                propiedades más exclusivas con la mejor experiencia de compra y
                venta.
              </p>
              <div className="flex space-x-4">
                {/* Social Links */}
                <a
                  href="#"
                  className="text-luxury-400 hover:text-luxury-gold-400 transition-colors duration-200"
                >
                  <span className="sr-only">Facebook</span>
                  <svg
                    className="h-6 w-6"
                    fill="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      fillRule="evenodd"
                      d="M22 12c0-5.523-4.477-10-10-10S2 6.477 2 12c0 4.991 3.657 9.128 8.438 9.878v-6.987h-2.54V12h2.54V9.797c0-2.506 1.492-3.89 3.777-3.89 1.094 0 2.238.195 2.238.195v2.46h-1.26c-1.243 0-1.63.771-1.63 1.562V12h2.773l-.443 2.89h-2.33v6.988C18.343 21.128 22 16.991 22 12z"
                      clipRule="evenodd"
                    />
                  </svg>
                </a>
                <a
                  href="#"
                  className="text-luxury-400 hover:text-luxury-gold-400 transition-colors duration-200"
                >
                  <span className="sr-only">Instagram</span>
                  <svg
                    className="h-6 w-6"
                    fill="currentColor"
                    viewBox="0 0 24 24"
                  >
                    <path
                      fillRule="evenodd"
                      d="M12.017 0C8.396 0 8.025.015 6.624.072 5.22.13 4.297.333 3.45.63c-.875.368-1.683.882-2.378 1.578C.377 2.904-.134 3.713-.502 4.588c-.297.846-.5 1.77-.558 3.174C-.075 8.975-.09 9.346-.09 12.017c0 3.624.014 3.994.072 5.395.058 1.405.26 2.328.558 3.174.368.875.882 1.683 1.578 2.378.695.695 1.504 1.21 2.378 1.578.85.297 1.77.5 3.174.558 1.4.058 1.771.072 5.395.072 3.624 0 3.994-.015 5.395-.072 1.405-.058 2.328-.26 3.174-.558.875-.368 1.683-.882 2.378-1.578.695-.695 1.21-1.504 1.578-2.378.297-.85.5-1.77.558-3.174.058-1.4.072-1.771.072-5.395 0-3.624-.015-3.994-.072-5.395-.058-1.405-.26-2.328-.558-3.174-.368-.875-.882-1.683-1.578-2.378C19.778.377 18.969-.134 18.094-.502c-.846-.297-1.77-.5-3.174-.558C13.52.015 13.149 0 9.525 0h2.492zm-.57 5.945A6.072 6.072 0 1018.89 12.017 6.072 6.072 0 0011.447 5.945zm0 10.017a3.945 3.945 0 110-7.89 3.945 3.945 0 010 7.89zM18.908 4.156a1.42 1.42 0 11-2.84 0 1.42 1.42 0 012.84 0z"
                      clipRule="evenodd"
                    />
                  </svg>
                </a>
              </div>
            </div>

            {/* Quick Links */}
            <div>
              <h4 className="text-sm font-semibold text-luxury-200 uppercase tracking-wider mb-4">
                Enlaces Rápidos
              </h4>
              <ul className="space-y-2">
                <li>
                  <a
                    href="#"
                    className="text-luxury-400 hover:text-luxury-gold-400 transition-colors duration-200"
                  >
                    Propiedades
                  </a>
                </li>
                <li>
                  <a
                    href="#"
                    className="text-luxury-400 hover:text-luxury-gold-400 transition-colors duration-200"
                  >
                    Servicios
                  </a>
                </li>
                <li>
                  <a
                    href="#"
                    className="text-luxury-400 hover:text-luxury-gold-400 transition-colors duration-200"
                  >
                    Sobre Nosotros
                  </a>
                </li>
                <li>
                  <a
                    href="#"
                    className="text-luxury-400 hover:text-luxury-gold-400 transition-colors duration-200"
                  >
                    Contacto
                  </a>
                </li>
              </ul>
            </div>

            {/* Contact */}
            <div>
              <h4 className="text-sm font-semibold text-luxury-200 uppercase tracking-wider mb-4">
                Contacto
              </h4>
              <ul className="space-y-2 text-luxury-400 text-sm">
                <li>+57 (1) 234-5678</li>
                <li>info@milliontestrealestate.com</li>
                <li>Bogotá, Colombia</li>
              </ul>
            </div>
          </div>

          {/* Bottom Bar */}
          <div className="mt-8 pt-8 border-t border-luxury-800">
            <div className="flex flex-col md:flex-row justify-between items-center">
              <p className="text-luxury-400 text-sm">
                © 2025 MillionTest RealEstate. Todos los derechos reservados.
              </p>
              <div className="flex space-x-6 mt-4 md:mt-0">
                <a
                  href="#"
                  className="text-luxury-400 hover:text-luxury-gold-400 text-sm transition-colors duration-200"
                >
                  Política de Privacidad
                </a>
                <a
                  href="#"
                  className="text-luxury-400 hover:text-luxury-gold-400 text-sm transition-colors duration-200"
                >
                  Términos de Servicio
                </a>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
};

export default Layout;
