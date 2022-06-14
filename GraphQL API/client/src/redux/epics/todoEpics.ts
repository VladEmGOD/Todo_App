import {RootState} from "../store";
import {catchError, endWith, finalize, from, map, mapTo, merge, mergeMap, of, startWith} from "rxjs";
import todoSlice, {
    addTodo,
    addTodos, createTodoAsync,
    deleteTodoAsync, removeFetchingTodosId,
    removeTodo,
    toggleIsDone,
    toggleIsDoneAsync,
    updateTodo,
    updateTodoAsync
} from "../todoSlice";
import {ofType, combineEpics, Epic} from "redux-observable";
import {client} from "../../GraphQl/client";
import {FetchTodosType, GET_TODOS} from "../../GraphQl/queries";
import {fetchTodosAsync} from "../todoSlice";
import {setFetchingError} from "../appSlice";
import {TodoType} from "../types/models";
import {
    CREATE_TODO,
    CreateTodoVariablesType,
    DELETE_TODO,
    DeleteTodoVariablesType,
    TOGGLE_TODO,
    ToggleTodoVariablesType,
    UPDATE_TODO, UpdateTodoVariablesType
} from "../../GraphQl/mutations";

export const fetchTodosEpic: Epic<typeof fetchTodosAsync, any, RootState> = action$ =>{
    return action$.pipe(
        ofType("todo/fetchTodosAsync"),
        mergeMap(action => from(client.query<FetchTodosType, {}>({
            query: GET_TODOS,
        })).pipe(
            map(res => addTodos(res.data.todos)),
            catchError(error => of(setFetchingError(error))),
        ))
)};


export const toggleIsDoneForTodoEpic: Epic<ReturnType<typeof toggleIsDoneAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("todo/toggleIsDoneAsync"),
        mergeMap(action => from(client.mutate<{togleIsDone: TodoType}, ToggleTodoVariablesType>({
            mutation: TOGGLE_TODO,
            variables: {toggleId: action.payload}
        })).pipe(
            mergeMap(response => merge(
                of(toggleIsDone(response.data?.togleIsDone.id as number)),
                of(removeFetchingTodosId(response.data?.togleIsDone.id as number)),
                )),
            catchError(error => of(setFetchingError(error))),
        ))
    );

export const deleteTodoEpic: Epic<ReturnType<typeof deleteTodoAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("todo/deleteTodoAsync"),
        mergeMap(action => from(client.mutate<{deleteTodo: TodoType}, DeleteTodoVariablesType>({
            mutation: DELETE_TODO,
            variables: {deleteId: action.payload}
        })).pipe(
            map(response => removeTodo(response.data?.deleteTodo.id as number)),
            catchError(error => of(setFetchingError(error)))
        ))
    );

export const updateTodoEpic: Epic<ReturnType<typeof updateTodoAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("todo/updateTodoAsync"),
        mergeMap(action => from(client.mutate<{updateTodo: TodoType}, UpdateTodoVariablesType>({
            mutation: UPDATE_TODO,
            variables: {updatedTodo: action.payload}
        })).pipe(
            map(response => updateTodo(response.data?.updateTodo as TodoType)),
            catchError(error => of(setFetchingError(error))),
        ))
    );

export const createTodoEpic: Epic<ReturnType<typeof createTodoAsync>, any, RootState> = action$ =>
    action$.pipe(
        ofType("todo/createTodoAsync"),
        mergeMap(action => from(client.mutate<{createTodo: TodoType}, CreateTodoVariablesType>({
            mutation: CREATE_TODO,
            variables: {todo: action.payload}
        })).pipe(
            map(response => addTodo(response.data?.createTodo as TodoType)),
            catchError(error => of(setFetchingError(error))),
        ))
    );

// @ts-ignore
export const todosEpics = combineEpics(fetchTodosEpic, toggleIsDoneForTodoEpic, deleteTodoEpic,
    updateTodoEpic, createTodoEpic)