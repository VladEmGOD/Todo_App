import {createSlice, PayloadAction} from "@reduxjs/toolkit";

interface AppConfig {
    dataSource: string,
    isTodosFetching: boolean,
    isCategoriesFetching: boolean,
    fetchingError: string
}

const initialState: AppConfig = {
    dataSource: localStorage.getItem("dataSource") ?? "0",
    isTodosFetching: true,
    isCategoriesFetching: true,
    fetchingError: ""
}

export const appSlice = createSlice({
    name: 'app',
    initialState,
    reducers: {
        setDataSource: (state, action: PayloadAction<string>) => {
            localStorage.setItem("dataSource", action.payload)
            window.location.reload()
        },
        setIsTodosFetching: (state, action: PayloadAction<boolean>) => {
            state.isTodosFetching = action.payload
        },
        setIsCategoriesFetching: (state, action: PayloadAction<boolean>) => {
            state.isCategoriesFetching = action.payload
        },
        setFetchingError: (state, action: PayloadAction<string>) => {
            console.error(action.payload)
            state.fetchingError = action.payload
        },
    }
})

export default appSlice.reducer

export const {setDataSource, setFetchingError, setIsTodosFetching, setIsCategoriesFetching} = appSlice.actions
