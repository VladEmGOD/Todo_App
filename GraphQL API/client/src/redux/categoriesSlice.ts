import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {CategoryType} from "./types/models";

interface CategorySlice {
    categories: Array<CategoryType>,
    createCategoryId: number,
    editCategory: CategoryType | null | undefined
}

const initialState: CategorySlice = {
    categories: [],
    createCategoryId: 0,
    editCategory: null,
}

export const categorySlice = createSlice({
    name:'category',
    initialState,
    reducers:{
        addCategory: (state, action: PayloadAction<CategoryType>) => {
            const category = action.payload
            state.createCategoryId += 1
            category.id = state.createCategoryId + 1
            state.categories.push(category)
        },
        removeCategory: (state, action: PayloadAction<number>) => {
            const id = action.payload
            state.categories = state.categories.filter(c => c.id !== id)
        },
        setEditCategory:(state, action: PayloadAction<number>) => {
            const id = action.payload
            state.editCategory = state.categories.find(e => e.id === id)
        },
        updateCategory:(state, action: PayloadAction<CategoryType>) => {
            let category = action.payload
            state.categories = state.categories.map(t => {
                if (t.id === category.id) return {...t, ...category}
                return t
            })
        }
    }
})

export default categorySlice.reducer

export const {addCategory, removeCategory, setEditCategory, updateCategory} = categorySlice.actions
