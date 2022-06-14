import React from 'react';
import {Button, Form} from "react-bootstrap";
import {useHistory} from "react-router-dom";
import {useFormik} from "formik";
import {updateCategory, updateCategoryAsync} from "../../redux/categoriesSlice";
import {CategoryType} from "../../redux/types/models";
import {useAppDispatch} from "../../redux/types/hooks";


type PropsType = {
    category: CategoryType
}

type ValidationType = {
    name?: string
}

export const EditCategoryForm: React.FC<PropsType> = ({category}) => {
    const dispatch = useAppDispatch()
    const history = useHistory()
    const formik = useFormik({
        initialValues: {
            id: category.id,
            name: category.name
        },
        onSubmit: (editedCategory: CategoryType) => {
            if (formik.isValid) {
                dispatch((updateCategoryAsync(editedCategory)))
                history.push("/categories")
            }
        },
        validate: values => {
            let errors: ValidationType = {}
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