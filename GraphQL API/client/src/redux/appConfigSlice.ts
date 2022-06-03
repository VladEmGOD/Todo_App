import {createSlice, PayloadAction} from "@reduxjs/toolkit";

interface AppConfig {
    dataSource: number
}

const initialState:AppConfig = {
    dataSource: 0
}

export const appConfigSlice = createSlice({
    name:'app',
    initialState,
    reducers:{
        setDataSource: (state, action)=>{
            state.dataSource = action.payload
        }
    }
})

export default appConfigSlice.reducer

export const {setDataSource} = appConfigSlice.actions
