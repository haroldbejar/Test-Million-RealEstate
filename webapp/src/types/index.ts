// Location types
export interface Location {
  id: string;
  name: string;
  count: number;
  state?: string;
  country: string;
}

// User types
export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatar?: string;
  phone?: string;
  createdAt: Date;
}

// Property types
export interface Property {
  id: string;
  title: string;
  amount: number;
  currency: string;
  city: string;
  state: string;
  country: string;
  imageUrl: string[];
  bedrooms: number;
  bathrooms: number;
  squareFootage: number;
  propertyType: "apartment" | "house" | "villa" | "penthouse";
  status: "for-sale" | "for-rent" | "sold";
  isFavorite: boolean;
  description: string;
}

// Owner types
export type OwnerCode = string;
export type Email = string;

export interface Address {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface Owner {
  id?: string;
  ownerCode: OwnerCode;
  fullName: string;
  address: string;
  phone: string;
  email: Email;
  // createdAt: Date;
}
