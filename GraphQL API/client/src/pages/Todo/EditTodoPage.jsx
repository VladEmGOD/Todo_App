import React from 'react';
import {Row} from "react-bootstrap";
import {EditTodoForm} from "../../components/Todo/EditTodoForm";
import {useSelector} from "react-redux";
import {getCategories} from "../../redux/selectors/categoriesSelector";
import {getEditTodo} from "../../redux/selectors/todoSelectors";


export const EditTodoPage = () => {
    const editedTodo = useSelector(getEditTodo)
    const categories = useSelector(getCategories)

    if (!editedTodo) return <h1>There is no todo for editing!</h1>

    return (
        <>
            <h1>Edit</h1>
            <h4>Todo</h4>
            <hr/>
            <Row>
                <div className={"col-md-4"}>
                    <EditTodoForm todo={editedTodo} categories={categories}/>
                </div>
            </Row>
        </>
    );
};
