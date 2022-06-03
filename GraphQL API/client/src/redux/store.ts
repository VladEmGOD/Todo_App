import {configureStore} from "@reduxjs/toolkit";
import todoReducer from "./todoSlice"
import categoryReducer from "./categoriesSlice"
import appConfigReducer from "./appConfigSlice";

export const store = configureStore({
    reducer:{
        todoPage: todoReducer,
        categoryPage: categoryReducer,
        app: appConfigReducer
    }
});

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
