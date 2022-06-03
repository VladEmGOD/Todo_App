import {RootState} from "../store";

export const getCategories = (state: RootState) => {
    return state.categoryPage.categories
}

export const  getCategoryById = (id: number) => (state : RootState) => {
    return state.categoryPage.categories.find(e => e.id === id)
}

export const getEditCategory = (state: RootState) => {
    return state.categoryPage.editCategory
}