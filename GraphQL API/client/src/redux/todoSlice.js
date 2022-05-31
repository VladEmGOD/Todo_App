import {createSlice} from "@reduxjs/toolkit";


export const todoSlice = createSlice({
    name:'todo',
    initialState:{
        todos:[],
        todosXML:[],
        editTodo: null,
        createTodoId: 0,
        selectedCategoryId: null
    },
    reducers:{
        addTodo: (state, action) => {
            const {todo} = action.payload
            state.createTodoId += 1
            todo.id = state.createTodoId + 1
            state.todos.push(todo)
        },
        toggleIsDone:(state, action) => {
            const {id} = action.payload
            let todo = state.todos.find(e => e.id === id)
            todo.isDone = !todo.isDone
        },
        removeTodo: (state, action) => {
            const {id} = action.payload
            state.todos = state.todos.filter(todo => todo.id !== id)
        },
        setEditTodo:(state, action) => {
            const {id} = action.payload
            state.editTodo = state.todos.find(e => e.id === id)
        },
        removeEditTodo:(state, action) => {
            state.editedTodo = null
        },
        updateTodo:(state, action) => {
            let {todo} = action.payload
            state.todos = state.todos.map(t => {
                if (t.id === todo.id) return {...t, ...todo}
                return t
            })
        },
        setSelectedCategoryId:(state, action) => {
            const {id} = action.payload
            state.selectedCategoryId = id
        },
        resetSelectedCategoryId:(state, action) => {
            state.selectedCategoryId = null
        },
    }
})

export default todoSlice.reducer

export const {addTodo, removeTodo,
    toggleIsDone, setEditTodo,
    updateTodo, setSelectedCategoryId,
    resetSelectedCategoryId} = todoSlice.actions
