import React, { useState } from 'react';
import { Search } from 'lucide-react';
import { useDispatch } from 'react-redux';
import { searchProperties } from '../../../store/slices/propertySlice';
import type { SearchParams } from '../../../store/slices/propertySlice';
import type { AppDispatch } from '../../../store';

const SearchControls: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>();
  const [searchTerm, setSearchTerm] = useState('');
  const [bedrooms, setBedrooms] = useState('');
  const [minPrice, setMinPrice] = useState('');
  const [maxPrice, setMaxPrice] = useState('');

  const handleSearch = () => {
    const params: SearchParams = {};
    if (searchTerm) params.title = searchTerm;
    if (bedrooms) params.bedrooms = parseInt(bedrooms, 10);
    if (minPrice) params.minPrice = parseInt(minPrice, 10);
    if (maxPrice) params.maxPrice = parseInt(maxPrice, 10);
    dispatch(searchProperties(params));
  };

  return (
    <div className="flex items-center space-x-2 rounded-full bg-luxury-700 px-3 py-1 text-sm">
      <input
        type="text"
        placeholder="Search by title..."
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        className="w-32 bg-transparent focus:outline-none"
      />
      <select
        value={bedrooms}
        onChange={(e) => setBedrooms(e.target.value)}
        className="bg-transparent focus:outline-none text-white"
      >
        <option className="text-black" value="">Beds</option>
        <option className="text-black" value="1">1</option>
        <option className="text-black" value="2">2</option>
        <option className="text-black" value="3">3</option>
        <option className="text-black" value="4">4+</option>
      </select>
      <input
        type="number"
        placeholder="Min $"
        value={minPrice}
        onChange={(e) => setMinPrice(e.target.value)}
        className="w-20 bg-transparent focus:outline-none"
      />
      <input
        type="number"
        placeholder="Max $"
        value={maxPrice}
        onChange={(e) => setMaxPrice(e.target.value)}
        className="w-20 bg-transparent focus:outline-none"
      />
      <button onClick={handleSearch} className="rounded-full bg-luxury-gold-500 p-1 text-white">
        <Search size={16} />
      </button>
    </div>
  );
};

export default SearchControls;