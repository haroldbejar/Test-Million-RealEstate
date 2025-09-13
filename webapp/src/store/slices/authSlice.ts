import {
  createSlice,
  createAsyncThunk,
  type PayloadAction,
} from "@reduxjs/toolkit";
import axios from "axios";
import { endpoints } from "../../endpoints";
import axiosInstance from "../../api/axiosInstance";

// Definimos el tipo para el estado de autenticación
interface AuthState {
  user: { username: string; email: string } | null;
  token: string | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

// Estado inicial
const initialState: AuthState = {
  user: null,
  token: localStorage.getItem("authToken") || null,
  status: "idle",
  error: null,
};

// Thunk asíncrono para el login
export const loginUser = createAsyncThunk<
  { username: string; email: string; token: string },
  { username: string; password: string }
>("auth/login", async (credentials, { rejectWithValue }) => {
  try {
    const response = await axiosInstance.post(
      endpoints.auth.login,
      credentials
    );

    localStorage.setItem("authToken", response.data.token);
    return response.data;
  } catch (error: any) {
    if (axios.isAxiosError(error) && error.response) {
      return rejectWithValue(
        error.response.data.message || "Error de autenticación"
      );
    } else {
      return rejectWithValue(error.message || "Error desconocido");
    }
  }
});

//slice de autenticación
const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.token = null;
      state.status = "idle";
      state.error = null;
      localStorage.removeItem("authToken");
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(
        loginUser.fulfilled,
        (
          state,
          action: PayloadAction<{
            username: string;
            email: string;
            token: string;
          }>
        ) => {
          state.status = "succeeded";
          state.user = {
            username: action.payload.username,
            email: action.payload.email,
          };
          state.token = action.payload.token;
        }
      )
      .addCase(loginUser.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload as string;
        state.user = null;
        state.token = null;
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
