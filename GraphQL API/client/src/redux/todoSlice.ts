import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {TodoType} from "./types/models";

interface TodoSlice {
    todos: Array<TodoType>,
    editTodo: TodoType | null | undefined,
    createTodoId: number,
    selectedCategoryId: number | null
}

const initialState: TodoSlice = {
    todos: [],
    editTodo: null,
    createTodoId: 0,
    selectedCategoryId: null
}

export const todoSlice = createSlice({
    name: 'todo',
    initialState,
    reducers: {
        addTodo: (state, action: PayloadAction<TodoType>) => {
            let todo = action.payload
            state.createTodoId += 1
            todo.id = state.createTodoId + 1
            state.todos.push(todo)
        },
        toggleIsDone: (state, action: PayloadAction<number>) => {
            const id = action.payload
            let todo = state.todos.find(e => e.id === id)
            if (todo) {
                todo.isDone = !todo.isDone
            }
        },
        removeTodo: (state, action: PayloadAction<number>) => {
            state.todos = state.todos.filter(todo => todo.id !== action.payload)
        },
        setEditTodo: (state, action: PayloadAction<number>) => {
            state.editTodo = state.todos.find(e => e.id === action.payload)
        },
        removeEditTodo: (state, action) => {
            state.editTodo = null
        },
        updateTodo: (state, action: PayloadAction<TodoType>) => {
            let todo = action.payload
            state.todos = state.todos.map(t => {
                if (t.id === todo.id) return {...t, ...todo}
                return t
            })
        },
        setSelectedCategoryId: (state, action: PayloadAction<number>) => {
            state.selectedCategoryId = action.payload
        },
        resetSelectedCategoryId: (state) => {
            state.selectedCategoryId = null
        },
    }
})

export default todoSlice.reducer

export const {
    addTodo, removeTodo,
    toggleIsDone, setEditTodo,
    updateTodo, setSelectedCategoryId,
    resetSelectedCategoryId
} = todoSlice.actions
