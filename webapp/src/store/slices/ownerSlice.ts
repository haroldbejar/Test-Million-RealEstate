import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import axiosInstance from "../../api/axiosInstance";
import { endpoints } from "../../endpoints";

// Owner types (moved here as a workaround for module resolution issue)
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
}

interface OwnerState {
  owner: Owner | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: OwnerState = {
  owner: null,
  status: "idle",
  error: null,
};

export const createOwner = createAsyncThunk<Owner, Omit<Owner, "id">>(
  "owner/create",
  async (ownerData: Omit<Owner, "id">, { rejectWithValue }) => {
    try {
      const response = await axiosInstance.post<Owner>(
        endpoints.owners.create,
        ownerData
      );
      return response.data;
    } catch (error: any) {
      if (axios.isAxiosError(error) && error.response) {
        return rejectWithValue(
          error.response.data.message || "Error al crear propietario"
        );
      } else {
        return rejectWithValue(error.message || "Error desconocido");
      }
    }
  }
);

const ownerSlice = createSlice({
  name: "owner",
  initialState,
  reducers: {
    resetOwnerStatus: (state) => {
      state.status = "idle";
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(createOwner.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(createOwner.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.owner = action.payload;
      })
      .addCase(createOwner.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload as string;
        state.owner = null;
      });
  },
});

export const { resetOwnerStatus } = ownerSlice.actions;
export default ownerSlice.reducer;
