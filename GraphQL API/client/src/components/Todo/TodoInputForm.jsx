import React from 'react';
import {Button, Form, Row} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {addTodo} from "../../redux/todoSlice";
import {useFormik} from "formik";

export const TodoInputForm = ({categories}) => {
    let dispatch = useDispatch()
    const formik = useFormik({
        initialValues:{
            id: null,
            title: "",
            deadline: null,
            categoryId: 0,
            isDone: false
        },
        onSubmit: todo => {
            if (formik.isValid) dispatch(addTodo({todo}))
        },
        validate: values => {
            let errors = {}

            if(!values.title) errors.title = "Todo title is required"
            console.log(errors)

            return errors
        }
    })

    return (
        <Row className={"centered"}>
            <div className={"col-md-10"}>
                <h1>Input your todo, please:</h1>
                <Form onSubmit={formik.handleSubmit}>
                    {formik.errors.title ?
                        <span className={"text-danger field-validation-error"}>{formik.errors.title}</span> : ""}

                    <Form.Group className={"input-group"}>
                        <Form.Control name={"title"}
                                      placeholder={"Enter you todo"}
                                      onChange={formik.handleChange}
                        />
                        <Form.Select name={"categoryId"}
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
                            type={"date"}
                            onChange={formik.handleChange}
                        />
                        <Button type="submit"  variant={"outline-secondary"}>Submit</Button>
                    </Form.Group>
                </Form>
            </div>
        </Row>
    );
}
