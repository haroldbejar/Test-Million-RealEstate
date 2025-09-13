import React, { useState, useRef, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { User, Settings, LogOut, ChevronDown } from "lucide-react";
import { cn } from "../../../lib/utils";
import LoginModal from "../../auth/LoginModal";
import RegisterModal from "../../auth/RegisterModal";
import { logout } from "../../../store/slices/authSlice";
import type { RootState, AppDispatch } from "../../../store";

const UserMenu: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
  const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);
  const dispatch = useDispatch<AppDispatch>();
  const { token, user } = useSelector((state: RootState) => state.auth);

  // Cerrar menú al hacer clic fuera
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        setIsOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const handleLogout = () => {
    dispatch(logout());
    setIsOpen(false);
  };

  // Si el usuario no está autenticado, mostrar botones de registro e ingreso
  if (!token) {
    return (
      <>
        <div className="flex items-center space-x-4">
          {/* Registrarse Button */}
          <button
            className={cn(
              "text-sm font-medium transition-colors duration-200 px-3 py-2 rounded-md",
              "text-luxury-300 hover:text-white"
            )}
            onClick={() => setIsRegisterModalOpen(true)}
          >
            Registrarse
          </button>

          {/* Ingresar Button */}
          <button
            className={cn(
              "bg-primary text-primary-foreground hover:bg-primary/90 font-semibold py-2 px-4 rounded-md transition-colors text-sm"
            )}
            onClick={() => setIsLoginModalOpen(true)}
          >
            Ingresar
          </button>
        </div>
        <LoginModal isOpen={isLoginModalOpen} onClose={() => setIsLoginModalOpen(false)} />
        <RegisterModal isOpen={isRegisterModalOpen} onClose={() => setIsRegisterModalOpen(false)} />
      </>
    );
  }

  // Si el usuario está autenticado, mostrar menú de usuario
  return (
    <div className="relative" ref={menuRef}>
      {/* User Avatar & Dropdown Trigger */}
      <button
        onClick={() => setIsOpen(!isOpen)}
        className={cn(
          "flex items-center space-x-2 px-3 py-2 text-sm font-medium transition-colors duration-200 rounded-lg",
          "text-luxury-300 hover:text-white hover:bg-luxury-800"
        )}
        aria-haspopup="true"
        aria-expanded={isOpen}
      >
        {/* Avatar */}
        <div className="w-8 h-8 bg-luxury-gold-500 rounded-full flex items-center justify-center shadow-lg shadow-luxury-gold-500/25">
          <User className="w-5 h-5 text-white" />
        </div>

        {/* User Name */}
        <span className="hidden md:block">{user?.username}</span>

        {/* Chevron */}
        <ChevronDown
          className={cn(
            "w-4 h-4 transition-transform duration-200",
            isOpen && "rotate-180"
          )}
        />
      </button>

      {/* Dropdown Menu */}
      {isOpen && (
        <div className="absolute top-full right-0 mt-2 w-48 bg-popover text-popover-foreground rounded-lg shadow-xl border border-border z-50 animate-slide-down">
          <div className="py-2">
            {/* Profile Section */}
            <div className="px-4 py-3 border-b border-border">
              <p className="text-sm font-medium">
                {user?.username}
              </p>
              <p className="text-xs text-muted-foreground">
                {user?.email}
              </p>
            </div>

            {/* Menu Items */}
            <div className="py-1">
              <button
                onClick={() => setIsOpen(false)}
                className={cn(
                  "flex items-center w-full px-4 py-2 text-sm transition-colors duration-150",
                  "hover:bg-accent hover:text-accent-foreground"
                )}
              >
                <User className="w-4 h-4 mr-3" />
                Mi Perfil
              </button>

              <button
                onClick={() => setIsOpen(false)}
                className={cn(
                  "flex items-center w-full px-4 py-2 text-sm transition-colors duration-150",
                  "hover:bg-accent hover:text-accent-foreground"
                )}
              >
                <Settings className="w-4 h-4 mr-3" />
                Configuración
              </button>

              {/* Separator */}
              <div className="border-t border-border my-1"></div>

              <button
                onClick={handleLogout}
                className="flex items-center w-full px-4 py-2 text-sm text-destructive hover:bg-destructive/10 transition-colors duration-150"
              >
                <LogOut className="w-4 h-4 mr-3" />
                Cerrar Sesión
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default UserMenu;
