import {gql} from "@apollo/client";
import {CategoryType, TodoType} from "../redux/types/models";
import {TODO_FRAGMENT} from "./fragments";

// Todos queries
export type FetchTodosType = {
    todos: TodoType[]
}
export const GET_TODOS = gql`
      ${TODO_FRAGMENT}
      query getTodos{
        todos{
           ...TodoFragment
        }
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

