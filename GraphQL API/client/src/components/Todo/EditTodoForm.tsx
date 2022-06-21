import React from 'react';
import {Button, Form} from "react-bootstrap";
import {useFormik} from "formik";
import {updateTodoAsync} from "../../redux/todoSlice";
import {useDispatch} from "react-redux";
import {useHistory, useParams} from "react-router-dom";
import {CategoryType, TodoType} from "../../redux/types/models";
import {TodoUpdateInputType} from "../../GraphQl/mutations";

type PropsType = {
    todo: TodoType,
    categories: CategoryType[]
}

type ValidationType = {
    title?: string
}

export const EditTodoForm: React.FC<PropsType> = ({todo, categories}) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const formik = useFormik<TodoUpdateInputType>({
        initialValues: {
            id: todo.id,
            title: todo.title,
            deadline: todo.deadline?.slice(0, 10),
            categoryId: todo.categoryId,
        },
        onSubmit: (editedTodo: TodoUpdateInputType) => {
            if (formik.isValid) {
                if (editedTodo.deadline !== "" && editedTodo.deadline !== undefined) {
                    let date = new Date(editedTodo.deadline as string)
                    editedTodo.deadline = date.toISOString()
                }
                else editedTodo.deadline = undefined
                dispatch(updateTodoAsync(editedTodo))
                history.push('/')
            }
        },
        validate: values => {
            console.log(values.deadline)
            let errors: ValidationType = {}
            if (!values.title) errors.title = "Todo title is required"
            return errors
        }
    })

    return (
        <Form onSubmit={formik.handleSubmit}>
            <Form.Group className={"form-group"}>
                <Form.Label htmlFor={"categoryId"}>Category</Form.Label>
                <Form.Select id={"categoryId"}
                             className={"form-control"}
                             value={formik.values.categoryId}
                             onChange={e => formik.setFieldValue("categoryId", parseInt(e.target.value))}>
                    <option value={0}>Select a category</option>
                    )
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
                <Form.Label htmlFor={"deadline"}>Deadline</Form.Label>
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
