import {RootState} from "../store";

export const getTodos = (state: RootState) => {
    return state.todoPage.todos
}
export const getEditTodo = (state: RootState) => {
    return state.todoPage.editTodo
}

export const getTodoById = (id: number) => (state: RootState) => {
    return state.todoPage.todos.find(e => e.id === id)
}

export const getSelectedCategoryId = (state: RootState) => {
    return state.todoPage.selectedCategoryId
}