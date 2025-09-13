import React from 'react';
import { ChevronLeft, ChevronRight } from 'lucide-react';
import { cn } from '../lib/utils';

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

const Pagination: React.FC<PaginationProps> = ({ currentPage, totalPages, onPageChange }) => {
  const handlePrevious = () => {
    if (currentPage > 1) {
      onPageChange(currentPage - 1);
    }
  };

  const handleNext = () => {
    if (currentPage < totalPages) {
      onPageChange(currentPage + 1);
    }
  };

  if (totalPages <= 1) {
    return null; // No renderizar paginación si solo hay una página
  }

  return (
    <div className="flex items-center justify-center space-x-4 mt-12">
      <button
        onClick={handlePrevious}
        disabled={currentPage === 1}
        className={cn(
          'flex items-center justify-center px-4 py-2 rounded-md border bg-card text-card-foreground hover:bg-muted',
          'disabled:opacity-50 disabled:cursor-not-allowed'
        )}
        aria-label="Página anterior"
      >
        <ChevronLeft className="h-4 w-4 mr-2" />
        Anterior
      </button>

      <span className="text-sm font-medium text-muted-foreground">
        Página {currentPage} de {totalPages}
      </span>

      <button
        onClick={handleNext}
        disabled={currentPage === totalPages}
        className={cn(
          'flex items-center justify-center px-4 py-2 rounded-md border bg-card text-card-foreground hover:bg-muted',
          'disabled:opacity-50 disabled:cursor-not-allowed'
        )}
        aria-label="Página siguiente"
      >
        Siguiente
        <ChevronRight className="h-4 w-4 ml-2" />
      </button>
    </div>
  );
};

export default Pagination;
