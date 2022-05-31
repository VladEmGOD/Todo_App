import {configureStore} from "@reduxjs/toolkit";
import todoReducer from "./todoSlice"
import categoryReducer from "./categoriesSlice"
import appSlice from "./appSlice";

export const store = configureStore({
    reducer:{
        todoPage: todoReducer,
        categoryPage: categoryReducer,
        app: appSlice
    }
});
