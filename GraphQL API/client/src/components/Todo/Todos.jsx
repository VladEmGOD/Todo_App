import {Table} from "react-bootstrap";
import {Todo} from "./Todo";
import React from "react";


export const Todos = ({todos, categories}) => {
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