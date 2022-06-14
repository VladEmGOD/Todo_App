import {Table} from "react-bootstrap";
import {Todo} from "./Todo";
import React from "react";
import {useAppSelector} from "../../redux/types/hooks";
import {getSelectedCategoryId, getTodos} from "../../redux/selectors/todoSelectors";
import {getCategories} from "../../redux/selectors/categoriesSelector";



export const Todos: React.FC = () => {
    let todos = useAppSelector(getTodos)
    const categories = useAppSelector(getCategories)
    const selectedCategoryId = useAppSelector(getSelectedCategoryId)

    if (selectedCategoryId) todos = todos.filter(t => t.categoryId === selectedCategoryId)

    if (todos.length === 0) return <h1>There is no todos :(</h1>
    else return (
        <Table>
            <thead>
            <tr>
                <th>Category</th>
                <th>Title</th>
                <th>Deadline</th>
                <th>isDone</th>
                <th></th>
            </tr>
            </thead>
            <tbody>
            {todos.map(t => <Todo key={t.id} todo={t} category={categories.find(c => c.id === t.categoryId)}/>)}
            </tbody>
        </Table>
    )
}