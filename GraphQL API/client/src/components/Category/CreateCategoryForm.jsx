import React from 'react';
import {Button, Form} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {useFormik} from "formik";
import {addCategory} from "../../redux/categoriesSlice";
import {useHistory} from "react-router-dom";

export const CreateCategoryForm = (props) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const formik = useFormik({
        initialValues: {
            id: null,
            name: ""
        },
        onSubmit: newCategory => {
            dispatch(addCategory({category: newCategory}))
            history.push("/categories")
        },
        validate: values => {
            let errors = {}
            if (!values.name) errors.name = "Category name is required"
            return errors
        }
    })

    return (
        <Form onSubmit={formik.handleSubmit}>
            {formik.errors.name ?
                <span className={"text-danger field-validation-error"}>{formik.errors.name}</span> : ""}

            <Form.Group className={"form-group"}>
                <Form.Label>Name</Form.Label>
                <Form.Control onChange={formik.handleChange} name={"name"}/>
            </Form.Group>

            <Form.Group className={"form-group"}>
                <Button type="submit">Submit</Button>
            </Form.Group>
        </Form>
    );
};