import React from 'react';
import {Button, Form, Row} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {createTodoAsync} from "../../redux/todoSlice";
import {useFormik} from "formik";
import {TodosCreateInputType} from "../../GraphQl/mutations";
import {useAppSelector} from "../../redux/types/hooks";
import {getCategories} from "../../redux/selectors/categoriesSelector";

type ValidationType = {
    title?: string
}

export const TodoInputForm: React.FC = () => {

    const categories = useAppSelector(getCategories)

    let dispatch = useDispatch()
    const formik = useFormik<TodosCreateInputType>({
        initialValues:{
            title: "",
            deadline: "",
            categoryId: 0,
        },
        onSubmit: (todo:TodosCreateInputType, {resetForm}) => {
            if (formik.isValid) {
                if (todo.deadline !== "") {
                    let date = new Date(todo.deadline as string)
                    todo.deadline = date.toISOString()
                }
                if (todo.deadline === '') todo.deadline = undefined
                dispatch(createTodoAsync(todo))
                resetForm({})
            }
        },
        validate: values => {
            let errors:ValidationType = {}
            if(!values.title) errors.title = "Todo title is required"
            return errors
        }
    })

    return (
        <Row className={"centered"}>
            <div className={"col-md-10"}>
                <h1>Input your todo, please:</h1>
                <Form onSubmit={formik.handleSubmit}>
                    <Form.Group className={"input-group"}>
                        <Form.Control name={"title"}
                                      value={formik.values.title}
                                      placeholder={"Enter you todo"}
                                      onChange={formik.handleChange}
                        />
                        <Form.Select name={"categoryId"}
                                     value={formik.values.categoryId}
                                     className={"form-control"}
                                     onChange={e => formik.setFieldValue(
                                         "categoryId",
                                         parseInt(e.target.value)
                                     )}>
                            <option value={0}>Choose a category</option>
                            {categories.map(c => <option value={c.id}>{c.name}</option>)}
                        </Form.Select>
                        <Form.Control
                            name={"deadline"}
                            value={formik.values.deadline}
                            type={"date"}
                            onChange={formik.handleChange}
                        />
                        <Button type="submit"  variant={"outline-secondary"}>Submit</Button>
                    </Form.Group>
                    {formik.errors.title ?
                        <span className={"text-danger field-validation-error"}>{formik.errors.title}</span> : ""}
                </Form>
            </div>
        </Row>
    );
}
