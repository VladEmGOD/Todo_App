import {createSlice} from "@reduxjs/toolkit";


export const appSlice = createSlice({
    name:'app',
    initialState:{
        dataSource: 0
    },
    reducers:{
        setDataSource: (state, action)=>{
            state.dataSource = action.payload.dataSource
        }
    }
})

export default appSlice.reducer

export const {setDataSource} = appSlice.actions
