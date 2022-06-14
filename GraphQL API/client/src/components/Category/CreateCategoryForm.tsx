import React from 'react';
import {Button, Form} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {useFormik} from "formik";
import {addCategory, createCategoryAsync} from "../../redux/categoriesSlice";
import {useHistory} from "react-router-dom";
import {CategoryType} from "../../redux/types/models";
import {useAppDispatch} from "../../redux/types/hooks";
import {CategoryCreateInputType} from "../../GraphQl/mutations";

type ValidationType = {
    name?: string
}

export const CreateCategoryForm = () => {
    const dispatch = useAppDispatch()
    const history = useHistory()
    const formik = useFormik({
        initialValues: {
            name: ""
        },
        onSubmit: (newCategory: CategoryCreateInputType, {resetForm}) => {
            dispatch(createCategoryAsync(newCategory))
            resetForm({})
            history.push("/categories")
        },
        validate: values => {
            let errors: ValidationType = {}
            if (!values.name) errors.name = "Category name is required"
            return errors
        }
    })

    return (
        <Form onSubmit={formik.handleSubmit}>
            <Form.Group className={"form-group"}>
                <Form.Label>Name</Form.Label>
                <Form.Control onChange={formik.handleChange}
                              name={"name"}
                              value={formik.values.name}
                />
                {formik.errors.name ?
                    <span className={"text-danger field-validation-error"}>{formik.errors.name}</span> : ""}
            </Form.Group>

            <Form.Group className={"form-group"}>
                <Button type="submit">Submit</Button>
            </Form.Group>
        </Form>
    );
};