import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import axios from "axios";
import axiosInstance from "../../api/axiosInstance";
import { endpoints } from "../../endpoints";
import type { Property } from "../../types";

// Interfaz para la información de paginación
export interface PaginationInfo {
  totalItems: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

// Interfaz para la respuesta paginada de la API
interface PagedPropertiesResponse {
  items: Property[];
  totalItems: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface SearchParams {
  title?: string;
  bedrooms?: number;
  minPrice?: number;
  maxPrice?: number;
}

// Interfaz para el estado del slice de propiedades
interface PropertyState {
  properties: Property[];
  pagination: PaginationInfo | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

// Estado inicial
const initialState: PropertyState = {
  properties: [],
  pagination: null,
  status: "idle",
  error: null,
};

// Thunk asíncrono para obtener las propiedades
export const fetchProperties = createAsyncThunk(
  "properties/fetchAll",
  async (
    params: { pageNumber: number; pageSize: number },
    { rejectWithValue }
  ) => {
    try {
      const response = await axiosInstance.get<PagedPropertiesResponse>(
        endpoints.properties.getAll,
        {
          params,
        }
      );

      const mappedItems: Property[] = response.data.items.map((item) => ({
        id: item.id,
        title: item.title,
        description: `Hab: ${item.bedrooms},  Baños: ${item.bathrooms},  m²: ${item.squareFootage}`,
        amount: item.amount,
        currency: item.currency,
        city: item.city,
        state: item.state,
        country: item.country,
        imageUrl: [
          `${endpoints.images.show}${
            item.imageUrl ? item.imageUrl : `/${item.imageUrl}`
          }`,
        ],
        bedrooms: item.bedrooms,
        bathrooms: item.bathrooms,
        squareFootage: item.squareFootage,
        propertyType:
          item.propertyType.toLowerCase() as Property["propertyType"],
        status: item.status.toLowerCase() as Property["status"],
        isFavorite: false,
      }));

      return {
        ...response.data,
        items: mappedItems,
      };
    } catch (error: any) {
      if (axios.isAxiosError(error) && error.response) {
        return rejectWithValue(
          error.response.data.message || "Error al obtener las propiedades"
        );
      } else {
        return rejectWithValue(error.message || "Error desconocido");
      }
    }
  }
);

// Thunk asíncrono para buscar propiedades
export const searchProperties = createAsyncThunk(
  "properties/search",
  async (params: SearchParams, { rejectWithValue }) => {
    try {
      const response = await axiosInstance.get<PagedPropertiesResponse>(
        endpoints.properties.search,
        { params }
      );
      const mappedItems: Property[] = response.data.items.map((item) => ({
        id: item.id,
        title: item.title,
        description: `Hab: ${item.bedrooms},  Baños: ${item.bathrooms},  m²: ${item.squareFootage}`,
        amount: item.amount,
        currency: item.currency,
        city: item.city,
        state: item.state,
        country: item.country,
        imageUrl: [
          `${endpoints.images.show}${
            item.imageUrl ? item.imageUrl : `/${item.imageUrl}`
          }`,
        ],
        bedrooms: item.bedrooms,
        bathrooms: item.bathrooms,
        squareFootage: item.squareFootage,
        propertyType:
          item.propertyType.toLowerCase() as Property["propertyType"],
        status: item.status.toLowerCase() as Property["status"],
        isFavorite: false,
      }));

      return {
        ...response.data,
        items: mappedItems,
      };
    } catch (error: any) {
      if (axios.isAxiosError(error) && error.response) {
        return rejectWithValue(
          error.response.data.message || "Error al buscar propiedades"
        );
      } else {
        return rejectWithValue(error.message || "Error desconocido");
      }
    }
  }
);

// Thunk asíncrono para crear una propiedad
export const createProperty = createAsyncThunk(
  "properties/create",
  async (propertyData: FormData, { rejectWithValue }) => {
    try {
      const response = await axiosInstance.post<Property>(
        endpoints.properties.create,
        propertyData
      );
      return response.data;
    } catch (error: any) {
      if (axios.isAxiosError(error) && error.response) {
        return rejectWithValue(
          error.response.data.message || "Error al crear la propiedad"
        );
      } else {
        return rejectWithValue(error.message || "Error desconocido");
      }
    }
  }
);

// Creación del slice de propiedades
const propertySlice = createSlice({
  name: "properties",
  initialState,
  reducers: {
    toggleFavorite: (state, action: PayloadAction<string>) => {
      const property = state.properties.find((p) => p.id === action.payload);
      if (property) {
        property.isFavorite = !property.isFavorite;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchProperties.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(
        fetchProperties.fulfilled,
        (state, action: PayloadAction<PagedPropertiesResponse>) => {
          state.status = "succeeded";
          state.properties = action.payload.items;
          state.pagination = {
            totalItems: action.payload.totalItems,
            pageNumber: action.payload.pageNumber,
            pageSize: action.payload.pageSize,
            totalPages: action.payload.totalPages,
          };
        }
      )
      .addCase(fetchProperties.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload as string;
      })
      .addCase(searchProperties.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(
        searchProperties.fulfilled,
        (state, action: PayloadAction<PagedPropertiesResponse>) => {
          state.status = "succeeded";
          state.properties = action.payload.items;
          state.pagination = {
            totalItems: action.payload.totalItems,
            pageNumber: action.payload.pageNumber,
            pageSize: action.payload.pageSize,
            totalPages: action.payload.totalPages,
          };
        }
      )
      .addCase(searchProperties.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload as string;
      })
      .addCase(createProperty.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(
        createProperty.fulfilled,
        (state, action: PayloadAction<Property>) => {
          state.status = "succeeded";
          state.properties.push(action.payload);
        }
      )
      .addCase(createProperty.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload as string;
      });
  },
});

export const { toggleFavorite } = propertySlice.actions;

export default propertySlice.reducer;
