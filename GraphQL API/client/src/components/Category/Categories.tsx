import React from 'react';
import {Category} from "./Category";
import {Table} from "react-bootstrap";
import {CategoryType} from "../../redux/types/models";

type PropsType = {
    categories: CategoryType[]
}

export const Categories: React.FC<PropsType> = ({categories}) => {
    if (categories.length === 0) return <h1>There is no categories :(</h1>

    return (
        <>
            <Table>
                <thead>
                <tr>
                    <th>Name</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {categories.map(c => <Category key={c.id} category={c}/>)}
                </tbody>
            </Table>
        </>
    );
};
