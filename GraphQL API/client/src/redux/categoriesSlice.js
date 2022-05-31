import {createSlice} from "@reduxjs/toolkit";


export const categorySlice = createSlice({
    name:'category',
    initialState:{
        categories:[],
        createCategoryId: 0,
        editCategory: null,
    },
    reducers:{
        addCategory: (state, action) => {
            const {category} = action.payload
            state.createCategoryId += 1
            category.id = state.createCategoryId + 1
            state.categories.push(category)
        },
        removeCategory: (state, action) => {
            const {id} = action.payload
            state.categories = state.categories.filter(c => c.id !== id)
        },
        setEditCategory:(state, action) => {
            const {id} = action.payload
            let category = state.categories.find(e => e.id === id)
            state.editCategory = category
        },
        updateCategory:(state, action) => {
            let {category} = action.payload
            state.categories = state.categories.map(t => {
                if (t.id === category.id) return {...t, ...category}
                return t
            })
        }
    }
})

export default categorySlice.reducer

export const {addCategory, removeCategory, setEditCategory, updateCategory} = categorySlice.actions
