import React from 'react';
import {Button, Form} from "react-bootstrap";
import {useFormik} from "formik";
import {updateTodo} from "../../redux/todoSlice";
import {useDispatch} from "react-redux";
import {useHistory} from "react-router-dom";

export const EditTodoForm = ({todo, categories}) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const formik = useFormik({
        initialValues:{
            id: todo.id,
            title: todo.title,
            deadline: todo.deadline,
            categoryId: todo.categoryId,
            isDone: todo.isDone
        },
        onSubmit: editedTodo => {
            if (formik.isValid) {
                dispatch(updateTodo({todo: editedTodo}))
                history.push('/')
            }
        },
        validate: values => {
            let errors = {}
            if(!values.title) errors.title = "Todo title is required"
            return errors
        }
    })

    return (
        <Form onSubmit={formik.handleSubmit}>
            <Form.Group className={"form-group"}>
                <Form.Label htmlFor={"categoryId"}>Category</Form.Label>
                <Form.Select id={"categoryId"} className={"form-control"} onChange={e => formik.setFieldValue(
                    "categoryId",
                    parseInt(e.target.value)
                )}>
                    <option value={0}>Select a category</option>)
                    {
                        categories.map(c =>
                            <option selected={todo.categoryId === c.id} value={c.id}>{c.name}</option>)
                    }
                </Form.Select>
            </Form.Group>
            <Form.Group className={"form-group"}>
                <Form.Label htmlFor={"title"}>Title</Form.Label>
                <Form.Control id={"title"}
                              onChange={formik.handleChange}
                              value={formik.values.title}/>
            </Form.Group>
            <Form.Group className={"form-group"}>
                <Form.Label htmlFor={"Deadline"}>Deadline</Form.Label>
                <Form.Control type={"date"}
                              id={"deadline"}
                              onChange={formik.handleChange}
                              value={formik.values.deadline}/>
            </Form.Group>
            <Form.Group className={"form-group"}>
                <Button type="submit">Submit</Button>
            </Form.Group>
        </Form>
    );
};
