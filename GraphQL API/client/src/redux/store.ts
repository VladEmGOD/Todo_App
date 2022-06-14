import {AnyAction, configureStore} from "@reduxjs/toolkit";
import todoReducer from "./todoSlice"
import categoryReducer from "./categoriesSlice"
import appConfigReducer from "./appSlice";
import {combineEpics, createEpicMiddleware, Epic} from "redux-observable";
import {todosEpics} from "./epics/todoEpics";
import {categoriesEpics} from "./epics/categoriesEpic";

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export type EpicType = Epic<AnyAction, AnyAction, RootState>;

// @ts-ignore
export const rootEpic = combineEpics(todosEpics, categoriesEpics);

const epicMiddleware = createEpicMiddleware();

export const store = configureStore({
    reducer: {
        todoPage: todoReducer,
        categoryPage: categoryReducer,
        app: appConfigReducer
    },
    middleware: [
        epicMiddleware
    ]
} );

// @ts-ignore
epicMiddleware.run(rootEpic)


