import {createAction, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {CategoryType} from "./types/models";
import {CategoryCreateInputType, UpdateCategoryInputType} from "../GraphQl/mutations";

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
        setCategories: (state, action: PayloadAction<CategoryType[]>) => {
            state.categories = action.payload
        },
        addCategory: (state, action: PayloadAction<CategoryType>) => {
            const category = action.payload
            state.createCategoryId += state.categories.at(-1)?.id as number
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

export const {addCategory, removeCategory, setEditCategory, setCategories, updateCategory} = categorySlice.actions

export const fetchCategoriesAsync = createAction("category/fetchCategoriesAsync")
export const updateCategoryAsync = createAction<CategoryType>("category/updateTodoAsync")
export const deleteCategoryAsync = createAction<number>("category/deleteCategoryAsync")
export const createCategoryAsync = createAction<CategoryCreateInputType>("category/createCategoryAsync")
