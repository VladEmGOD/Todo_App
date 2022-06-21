import {gql} from "@apollo/client";
import {CategoryType, TodoType} from "../redux/types/models";
import {TODO_FRAGMENT} from "./fragments";

// Todos queries
export type FetchTodosType = {
    todos: TodoType[]
}

export type GetTodosVariablesType = {pageNumber: number, pageSizeNumber: number}
export type FetchTodosInputType = {page: number, pageSize: number}
export const GET_TODOS = gql`
      ${TODO_FRAGMENT}
    query GetTodos($pageNumber: Int!, $pageSizeNumber: Int! ){
        todos(page: $pageNumber, count: $pageSizeNumber){
            ...TodoFragment
        }
    }
`

export const GET_TODOS_COUNT = gql`
query GetTodosCount{
  getCount
}
`

// Categories queries
export type FetchCategoriesType = {
    categories: CategoryType[]
}
export const GET_CATEGORIES = gql`
    query getCategories{
        categories{
            id,
            name
        }
    }
`

