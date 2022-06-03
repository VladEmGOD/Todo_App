import React from "react";
import {Button} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {removeTodo, setEditTodo, toggleIsDone} from "../../redux/todoSlice";
import {Link, useHistory} from "react-router-dom";
import {CategoryType, TodoType} from "../../redux/types/models";
import {useAppDispatch} from "../../redux/types/hooks";

type PropsType = {
    todo: TodoType,
    category?: CategoryType
}

export const Todo: React.FC<PropsType> = React.memo(({todo, category}) => {
    const dispatch = useAppDispatch()
    const history = useHistory()
    return (
        <tr>
            <td>{category ? category.name : "No category"}</td>
            <td>{todo.title}</td>
            <td>{todo.deadline === "" ? "no deadline" : todo.deadline}</td>
            <td>
                <Button variant={todo.isDone ? "success" : "danger"}
                        onClick={() => dispatch(toggleIsDone(todo.id))}>
                    {todo.isDone ? "DONE" : "NOT DONE"}
                </Button>
            </td>
            <td>
                <Link to={"/todo/edit/" + todo.id}>Edit</Link> |
                <Link to={"/todo/delete/"}
                      onClick={(event) => {
                          event.preventDefault()
                          dispatch(removeTodo(todo.id))
                      }}>Delete</Link>
            </td>
        </tr>
    )
})