import React from "react";
import {Button} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {removeTodo, setEditTodo, toggleIsDone} from "../../redux/todoSlice";
import {Link, useHistory} from "react-router-dom";

export const Todo = React.memo(function ({todo, category}) {
    const dispatch = useDispatch()
    const history = useHistory()
    return (
        <tr>
            <td>{category ? category.name : "No category"}</td>
            <td>{todo.title}</td>
            <td>{todo.deadline ?? "no deadline"}</td>
            <td>
                <Button variant={todo.isDone ? "success" : "danger"}
                        onClick={() => dispatch(toggleIsDone({id: todo.id}))}>
                    {todo.isDone ? "DONE" : "NOT DONE"}
                </Button>
            </td>
            <td>
                <Link to={"/todo/edit/"}
                   onClick={(event)=>{
                    dispatch(setEditTodo({id: todo.id}))
                    history.push("/todo/edit/" + todo.id)
                }}>Edit</Link> |
                <Link to={"/todo/delete/" }
                   onClick={(event)=>{
                    event.preventDefault()
                    dispatch(removeTodo({id: todo.id}))
                }}>Delete</Link>
            </td>
        </tr>
    )
})