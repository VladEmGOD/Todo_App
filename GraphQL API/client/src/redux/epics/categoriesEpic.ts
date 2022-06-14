import {combineEpics, Epic, ofType} from "redux-observable";
import {RootState} from "../store";
import {catchError, endWith, from, map, mergeMap, of} from "rxjs";
import {client} from "../../GraphQl/client";
import {FetchCategoriesType, GET_CATEGORIES} from "../../GraphQl/queries";
import {setFetchingError, setIsCategoriesFetching} from "../appSlice";
import {
    addCategory, createCategoryAsync,
    deleteCategoryAsync,
    fetchCategoriesAsync,
    removeCategory,
    setCategories, updateCategory,
    updateCategoryAsync
} from "../categoriesSlice";
import {CategoryType} from "../types/models";
import {
    CREATE_CATEGORY,
    CreateCategoryVariablesType,
    DELETE_CATEGORY,
    DeleteCategoryVariablesType,
    UPDATE_CATEGORY, UpdateCategoryVariablesType,

} from "../../GraphQl/mutations";

export const fetchCategoriesEpic: Epic<typeof fetchCategoriesAsync, any, RootState> = action$ =>
    action$.pipe(
        ofType("category/fetchCategoriesAsync"),
        mergeMap(action => from(client.query<FetchCategoriesType, {}>({
            query: GET_CATEGORIES
        })).pipe(
            map(res => setCategories(res.data.categories)),
            catchError(error => of(setFetchingError(error))),
            endWith(setIsCategoriesFetching(false))
        ))
    );

export const deleteCategoryEpic: Epic<ReturnType<typeof deleteCategoryAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("category/deleteCategoryAsync"),
        mergeMap(action => from(client.mutate<{ deleteCategory: CategoryType }, DeleteCategoryVariablesType>({
            mutation: DELETE_CATEGORY,
            variables: {deleteId: action.payload}
        })).pipe(
            map(response => {
                console.log(response)
                return removeCategory(response.data?.deleteCategory.id as number)
            }),
            catchError(error => of(setFetchingError(error)))
        ))
    );

export const updateCategoryEpic: Epic<ReturnType<typeof updateCategoryAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("category/updateTodoAsync"),
        mergeMap(action => from(client.mutate<{ updateCategory: CategoryType }, UpdateCategoryVariablesType>({
            mutation: UPDATE_CATEGORY,
            variables: {updatedCategory: action.payload}
        })).pipe(
            map(response => updateCategory(response.data?.updateCategory as CategoryType)),
            catchError(error => of(setFetchingError(error))),
        ))
    );

export const createCategoryEpic: Epic<ReturnType<typeof createCategoryAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("category/createCategoryAsync"),
        mergeMap(action => from(client.mutate<{ createCategory: CategoryType }, CreateCategoryVariablesType>({
            mutation: CREATE_CATEGORY,
            variables: {category: action.payload}
        })).pipe(
            map(response => addCategory(response.data?.createCategory as CategoryType)),
            catchError(error => of(setFetchingError(error))),
        ))
    );


// @ts-ignore
export const categoriesEpics = combineEpics(fetchCategoriesEpic, createCategoryEpic,
    updateCategoryEpic, deleteCategoryEpic)