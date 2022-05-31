export const getCategories = (state) => {
    return state.categoryPage.categories
}
export const getSelectedCategoryId = (state) => {
    return state.categoryPage.selectedCategoryId
}
export const getEditCategory = (state) => {
    return state.categoryPage.editCategory
}