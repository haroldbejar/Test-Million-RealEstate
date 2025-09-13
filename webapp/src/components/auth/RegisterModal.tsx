import React, { useState, useEffect } from "react";
import { X, Loader2 } from "lucide-react";
import { useDispatch, useSelector } from "react-redux";
import { createOwner, resetOwnerStatus } from "../../store/slices/ownerSlice";
import type { RootState, AppDispatch } from "../../store";
import type { Owner } from "../../types";

interface RegisterModalProps {
  isOpen: boolean;
  onClose: () => void;
}

const RegisterModal: React.FC<RegisterModalProps> = ({ isOpen, onClose }) => {
  const [ownerCode] = useState("");
  const [showSuccessScreen, setShowSuccessScreen] = useState(false);
  const [fullName, setFullName] = useState("");
  const [address, setAddress] = useState("");
  const [phone, setPhone] = useState("");
  const [email, setEmail] = useState("");

  const dispatch = useDispatch<AppDispatch>();
  const { status, error, owner } = useSelector(
    (state: RootState) => state.owner
  );

  // Manejar éxito del registro
  useEffect(() => {
    if (status === "succeeded") {
      setShowSuccessScreen(true);
    }
  }, [status]);

  // Resetear error al abrir/cerrar
  useEffect(() => {
    if (!isOpen) {
      dispatch(resetOwnerStatus());
    }
  }, [isOpen, dispatch]);

  if (!isOpen) return null;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (status === "loading") return;

    const newOwner: Omit<Owner, "id"> = {
      ownerCode,
      fullName,
      address,
      phone,
      email,
    };

    await dispatch(createOwner(newOwner));
  };

  const onCloseSuccessScreen = () => {
    setShowSuccessScreen(false);
    setEmail("");
    setFullName("");
    setAddress("");
    setPhone("");
  };

  const handleCloseSuccess = () => {
    onCloseSuccessScreen();
    onClose();
  };

  return (
    <div
      className="fixed inset-0 bg-black/60 z-50 flex items-center justify-center animate-fade-in-fast p-4 max-h-screen"
      onClick={onClose}
    >
      {/* Modal Content */}
      <div
        className="bg-card text-card-foreground rounded-lg shadow-xl w-full max-w-sm border animate-slide-up-fast overflow-y-auto mx-auto my-8 max-h-[90vh]"
        onClick={(e) => e.stopPropagation()}
      >
        {/* Header */}
        <div className="flex items-center justify-between p-4 border-b">
          <h2 className="text-xl font-semibold">
            {showSuccessScreen ? "Registro Exitoso" : "Registrar Propietario"}
          </h2>
          <button
            onClick={showSuccessScreen ? handleCloseSuccess : onClose}
            className="p-1 rounded-full text-muted-foreground hover:bg-muted transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Body */}
        {showSuccessScreen && owner ? (
          <div className="p-6 text-center space-y-4">
            <p className="text-lg font-medium">
              ¡Bienvenido, {owner.fullName}!
            </p>
            <p className="text-muted-foreground">
              Tu registro ha sido exitoso.
            </p>
            <p className="text-xl font-bold text-primary">
              Tu Código de Propietario es: {owner.ownerCode}
            </p>
            <p className="text-sm text-muted-foreground">
              Guarda este código, lo necesitarás para registrar propiedades.
            </p>
            <button
              onClick={handleCloseSuccess}
              className="w-full bg-primary text-primary-foreground hover:bg-primary/90 font-semibold py-2 px-4 rounded-md transition-colors mt-4"
            >
              Cerrar
            </button>
          </div>
        ) : (
          <form onSubmit={handleSubmit} className="p-6 space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label
                  htmlFor="fullName"
                  className="block text-sm font-medium mb-1"
                >
                  Nombre Completo
                </label>
                <input
                  id="fullName"
                  type="text"
                  value={fullName}
                  onChange={(e) => setFullName(e.target.value)}
                  className="w-full px-3 py-2 bg-input border border-border rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  required
                  disabled={status === "loading"}
                />
              </div>
              <div>
                <label
                  htmlFor="phone"
                  className="block text-sm font-medium mb-1"
                >
                  Teléfono
                </label>
                <input
                  id="phone"
                  type="tel"
                  value={phone}
                  onChange={(e) => setPhone(e.target.value)}
                  className="w-full px-3 py-2 bg-input border border-border rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  required
                  disabled={status === "loading"}
                />
              </div>
              <div>
                <label
                  htmlFor="email"
                  className="block text-sm font-medium mb-1"
                >
                  Email
                </label>
                <input
                  id="email"
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  className="w-full px-3 py-2 bg-input border border-border rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                  required
                  disabled={status === "loading"}
                />
              </div>

              <div>
                <div>
                  <label
                    htmlFor="street"
                    className="block text-sm font-medium mb-1"
                  >
                    Calle
                  </label>
                  <input
                    id="street"
                    type="text"
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                    className="w-full px-3 py-2 bg-input border border-border rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
                    required
                    disabled={status === "loading"}
                  />
                </div>
              </div>
            </div>

            {error && (
              <p className="text-destructive text-sm text-center mt-4">
                {error}
              </p>
            )}

            <button
              type="submit"
              className="w-full bg-primary text-primary-foreground hover:bg-primary/90 font-semibold py-2 px-4 rounded-md transition-colors flex items-center justify-center"
              disabled={status === "loading"}
            >
              {status === "loading" && (
                <Loader2 className="mr-2 h-4 w-4 animate-spin" />
              )}
              Registrar Propietario
            </button>
          </form>
        )}
      </div>
    </div>
  );
};

export default RegisterModal;
