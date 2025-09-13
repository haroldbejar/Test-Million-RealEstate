import React from "react";
import { Heart } from "lucide-react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import { type RootState } from "../../../store";
import { cn } from "../../../lib/utils";

const FavoritesButton: React.FC = () => {
  const favoriteCount = useSelector((state: RootState) => 
    state.properties.properties.filter(p => p.isFavorite).length
  );

  return (
    <Link
      to="/favorites"
      className={cn(
        "relative inline-flex items-center space-x-2 px-3 py-2 text-sm font-medium transition-colors duration-200 group rounded-md",
        "text-luxury-300 hover:text-white"
      )}
    >
      {/* Heart Icon */}
      <div className="relative">
        <Heart
          className={cn(
            "w-5 h-5 transition-colors duration-200"
          )}
        />

        {/* Badge Counter */}
        {favoriteCount > 0 && (
          <div
            className={cn(
              "absolute -top-2 -right-2 min-w-[18px] h-[18px] text-white text-xs font-bold rounded-full flex items-center justify-center",
              "bg-luxury-gold-500 group-hover:bg-luxury-gold-400"
            )}
          >
            {favoriteCount > 99 ? "99+" : favoriteCount}
          </div>
        )}
      </div>

      {/* Text */}
      <span>
        Favoritos
      </span>
    </Link>
  );
};

export default FavoritesButton;

