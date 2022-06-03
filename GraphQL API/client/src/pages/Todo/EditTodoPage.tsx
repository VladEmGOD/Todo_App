import React from 'react';
import {Row} from "react-bootstrap";
import {EditTodoForm} from "../../components/Todo/EditTodoForm";
import {getCategories} from "../../redux/selectors/categoriesSelector";
import {getTodoById} from "../../redux/selectors/todoSelectors";
import {useAppSelector} from "../../redux/types/hooks";
import {useParams} from "react-router-dom";


export const EditTodoPage = () => {
    let {id} = useParams<{id: string}>()
    const editedTodo = useAppSelector(getTodoById(+id))
    const categories = useAppSelector(getCategories)

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
