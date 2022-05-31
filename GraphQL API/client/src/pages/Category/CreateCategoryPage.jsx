import React from 'react';
import {Row} from "react-bootstrap";
import {CreateCategoryForm} from "../../components/Category/CreateCategoryForm";

export function CreateCategoryPage(props) {
    return (
        <>
            <h1>Create</h1>
            <h4>Category</h4>
            <hr/>
            <Row>
                <div className={"col-md-4"}>
                    <CreateCategoryForm/>
                </div>
            </Row>
        </>
    );
}
