import React from "react";
import {Categories} from "../../components/Category/Categories";
import {Link} from "react-router-dom";
import {getCategories} from "../../redux/selectors/categoriesSelector";
import {useAppSelector} from "../../redux/types/hooks";

export const CategoriesPage = () => {
    const categories = useAppSelector(getCategories)
    return (
        <>
            <h1>Categories</h1>
            <p><Link to="/categories/create">Create new</Link></p>
            <Categories categories={categories}/>
        </>
    )
}