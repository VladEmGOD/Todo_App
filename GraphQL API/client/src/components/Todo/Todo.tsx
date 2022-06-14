import React from "react";
import {Button} from "react-bootstrap";
import {addFetchingTodosId, deleteTodoAsync, toggleIsDoneAsync} from "../../redux/todoSlice";
import {Link} from "react-router-dom";
import {CategoryType, TodoType} from "../../redux/types/models";
import {useAppDispatch, useAppSelector} from "../../redux/types/hooks";

type PropsType = {
    todo: TodoType,
    category?: CategoryType
}

export const Todo: React.FC<PropsType> = React.memo(({todo, category}) => {

    const fetchingId = useAppSelector(state => state.todoPage.fetchingTodosIds.filter(id => id === todo.id))
    const isDisabled = fetchingId.length !== 0
    const dispatch = useAppDispatch()

    let deadlineDate = new Date(todo.deadline as string)

    return (
        <tr>
            <td>{category ? category.name : "No category"}</td>
            <td>{todo.title}</td>
            <td>{todo.deadline ? deadlineDate.toISOString().slice(0, 10) : "no deadline"}</td>
            <td>
                <Button variant={todo.isDone ? "success" : "danger"}
                        disabled={isDisabled}
                        onClick={() => {
                            dispatch(addFetchingTodosId(todo.id))
                            dispatch(toggleIsDoneAsync(todo.id))
                        }}>
                    {todo.isDone ? "DONE" : "NOT DONE"}
                </Button>
            </td>
            <td>
                <Link to={"/todo/edit/" + todo.id}>Edit</Link> |
                <Link to={"/todo/delete/"}
                      onClick={(event) => {
                          event.preventDefault()
                          dispatch(deleteTodoAsync(todo.id))
                      }}>Delete</Link>
            </td>
        </tr>
    )
})