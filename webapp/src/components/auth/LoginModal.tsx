import React, { useState, useEffect } from 'react';
import { X, Loader2 } from 'lucide-react';
import { useDispatch, useSelector } from 'react-redux';
import { loginUser } from '../../store/slices/authSlice';
import type { RootState, AppDispatch } from '../../store';

interface LoginModalProps {
  isOpen: boolean;
  onClose: () => void;
}

const LoginModal: React.FC<LoginModalProps> = ({ isOpen, onClose }) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const dispatch = useDispatch<AppDispatch>();
  const { status, error } = useSelector((state: RootState) => state.auth);

  // Cerrar modal al iniciar sesión exitosamente
  useEffect(() => {
    if (status === 'succeeded') {
      onClose();
      setUsername(''); // Limpiar campos
      setPassword('');
    }
  }, [status, onClose]);

  if (!isOpen) return null;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (status === 'loading') return; // Evitar envíos múltiples

    await dispatch(loginUser({ username, password }));
    // El modal se cerrará en el useEffect si el login es exitoso
  };

  return (
    // Overlay
    <div 
      className="fixed inset-0 bg-black/60 z-50 flex items-center justify-center animate-fade-in-fast"
      onClick={onClose}
    >
      {/* Modal Content */}
      <div 
        className="bg-card text-card-foreground rounded-lg shadow-xl w-full max-w-md border animate-slide-up-fast"
        onClick={(e) => e.stopPropagation()} // Evita que el clic en el modal lo cierre
      >
        {/* Header */}
        <div className="flex items-center justify-between p-4 border-b">
          <h2 className="text-xl font-semibold">Ingresar</h2>
          <button 
            onClick={onClose}
            className="p-1 rounded-full text-muted-foreground hover:bg-muted transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Body */}
        <form onSubmit={handleSubmit} className="p-6 space-y-4">
          <div>
            <label htmlFor="username" className="block text-sm font-medium mb-1">Usuario</label>
            <input 
              id="username"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className="w-full px-3 py-2 bg-input border border-border rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
              required
              disabled={status === 'loading'}
            />
          </div>
          <div>
            <label htmlFor="password" className="block text-sm font-medium mb-1">Contraseña</label>
            <input 
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full px-3 py-2 bg-input border border-border rounded-md focus:outline-none focus:ring-2 focus:ring-ring"
              required
              disabled={status === 'loading'}
            />
          </div>

          {error && <p className="text-destructive text-sm text-center">{error}</p>}

          <button 
            type="submit"
            className="w-full bg-primary text-primary-foreground hover:bg-primary/90 font-semibold py-2 px-4 rounded-md transition-colors flex items-center justify-center"
            disabled={status === 'loading'}
          >
            {status === 'loading' && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
            Ingresar
          </button>
        </form>
      </div>
    </div>
  );
};

export default LoginModal;
