import React from 'react';
import {Row} from "react-bootstrap";
import {EditCategoryForm} from "../../components/Category/EditCategoryForm";
import {getCategoryById} from "../../redux/selectors/categoriesSelector";
import {useAppSelector} from "../../redux/types/hooks";
import {useParams} from "react-router-dom";


export function EditCategoryPage() {
    let {id} = useParams<{id: string}>()
    let editedCategory = useAppSelector(getCategoryById(+id))
    if (!editedCategory) return <h1>There is no category for editing with id: {id}!</h1>

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
