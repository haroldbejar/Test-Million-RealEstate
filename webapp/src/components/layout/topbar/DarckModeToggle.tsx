import React from "react";
import { Sun, Moon } from "lucide-react";
import { cn } from "../../../lib/utils";
import { useTheme } from "../../theme-provider"; // Import useTheme from our new provider

const DarckModeToggle: React.FC = () => {
  const { theme, setTheme } = useTheme();

  // We consider "system" theme as well. For the toggle's state, we need to resolve it.
  const isDark =
    theme === "dark" ||
    (theme === "system" &&
      window.matchMedia("(prefers-color-scheme: dark)").matches);

  const toggleTheme = () => {
    setTheme(isDark ? "light" : "dark");
  };

  return (
    <div className="flex items-center space-x-3">
      {/* Light/Dark indicator text */}
      <span
        className={cn(
          "text-sm font-medium transition-colors duration-200",
          isDark ? "text-luxury-300" : "text-luxury-400"
        )}
      >
        {isDark ? "Dark" : "Light"}
      </span>

      {/* Toggle Switch */}
      <button
        onClick={toggleTheme}
        className={cn(
          "relative inline-flex items-center justify-center w-12 h-6 rounded-full transition-all duration-300 focus:outline-none focus:ring-2 focus:ring-luxury-gold-500 focus:ring-offset-2 focus:ring-offset-background", // Use background for offset
          isDark
            ? "bg-luxury-gold-500 shadow-lg shadow-luxury-gold-500/25"
            : "bg-luxury-600 hover:bg-luxury-500"
        )}
        aria-label={`Cambiar a modo ${isDark ? "claro" : "oscuro"}`}
      >
        <div
          className={cn(
            "absolute left-1 top-1 w-4 h-4 bg-white rounded-full shadow-md transform transition-all duration-300 flex items-center justify-center",
            isDark ? "translate-x-6 shadow-lg" : "translate-x-0"
          )}
        >
          {isDark ? (
            <Moon className="w-2.5 h-2.5 text-luxury-700" />
          ) : (
            <Sun className="w-2.5 h-2.5 text-luxury-gold-500" />
          )}
        </div>
      </button>
    </div>
  );
};

export default DarckModeToggle;
