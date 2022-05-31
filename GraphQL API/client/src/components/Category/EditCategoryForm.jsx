import React from 'react';
import {Button, Form} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {useHistory} from "react-router-dom";
import {useFormik} from "formik";
import {updateCategory} from "../../redux/categoriesSlice";

export const EditCategoryForm = ({category}) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const formik = useFormik({
        initialValues: {
            id: category.id,
            name: category.name
        },
        onSubmit: editedCategory => {
            if (formik.isValid) {
                dispatch(updateCategory({category: editedCategory}))
                history.push("/categories")
            }
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
                <Form.Control onChange={formik.handleChange} value={formik.values.name} name={"name"}/>
            </Form.Group>

            <Form.Group className={"form-group"}>
                <Button type="submit">Submit</Button>
            </Form.Group>
        </Form>
    );
};