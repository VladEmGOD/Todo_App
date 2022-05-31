import React from 'react';
import {Row} from "react-bootstrap";
import {EditCategoryForm} from "../../components/Category/EditCategoryForm";
import {useSelector} from "react-redux";
import {getEditCategory} from "../../redux/selectors/categoriesSelector";


export function EditCategoryPage(props) {
    const editedCategory = useSelector(getEditCategory)

    if (!editedCategory) return <h1>There is no category for editing!</h1>

    return (
        <>
            <h1>Edit</h1>
            <h4>Category</h4>
            <hr/>
            <Row>
                <div className={"col-md-4"}>
                    <EditCategoryForm category={editedCategory}/>
                </div>
            </Row>
        </>
    );
}
