import {gql} from "@apollo/client";
import {CategoryType} from "../redux/types/models";

// Todo mutations
export type CreateTodoVariablesType = {todo: TodosCreateInputType}
export type TodosCreateInputType = {
    title: string,
    deadline?: string,
    categoryId: number,
}
export const CREATE_TODO = gql`
mutation CreateTodo($todo: TodoCreateInputType! ){
    createTodo(todoCreateInput: $todo){
        id,
        title,
        categoryId,
        deadline,
        isDone
    }
}
`

export type UpdateTodoVariablesType = {updatedTodo: TodoUpdateInputType}
export type TodoUpdateInputType = {
    id: number,
    title: string,
    deadline?: string,
    categoryId?: number,
}
export const UPDATE_TODO = gql`
    mutation UpdateTodo($updatedTodo: TodoUpdateInputType!){
        updateTodo(todoUpdateInput: $updatedTodo){
            id,
            title,
            deadline,
            categoryId,
            isDone
        }
    }
`

export type DeleteTodoVariablesType = {deleteId: number}
export const DELETE_TODO = gql`
mutation DeleteTodo($deleteId: Int!){
    deleteTodo(id: $deleteId){
            id,
            title,
            deadline,
            categoryId,
            isDone
    }
}
`
export type ToggleTodoVariablesType = {toggleId: number}
export const TOGGLE_TODO = gql`
    mutation toggleTodo($toggleId: Int!){
        togleIsDone(id: $toggleId){
            id,
            title,
            deadline,
            categoryId,
            isDone
        }
    }
`
// Category mutations
export type DeleteCategoryVariablesType = {deleteId: number}
export const DELETE_CATEGORY = gql`
mutation DeleteCategory($deleteId: Int!){
  deleteCategory(id: $deleteId){
    id,
    name
  }
}
`

export type UpdateCategoryInputType = {
    id: number,
    name: string,
}
export type UpdateCategoryVariablesType = {updatedCategory: CategoryType}
export const UPDATE_CATEGORY = gql`
mutation UpdateCategory($updatedCategory: UpdateCategoryInputType!){
    updateCategory(updateCategoryInput: $updatedCategory){
        id,
        name
    }
}
`

export type CreateCategoryVariablesType = {category: CategoryCreateInputType}
export type CategoryCreateInputType = {
    name: string
}
export const CREATE_CATEGORY = gql`
mutation CreateCategory($category: CreateCategoryInputType!){
  createCategory(categoryCreateInput: $category){
    id,
    name
  }
}
`

