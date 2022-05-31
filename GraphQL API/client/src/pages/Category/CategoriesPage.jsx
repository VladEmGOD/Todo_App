import React from "react";
import {Categories} from "../../components/Category/Categories";
import {useSelector} from "react-redux";
import {Link} from "react-router-dom";
import {getCategories} from "../../redux/selectors/categoriesSelector";

export const CategoriesPage = (props) => {
    const categories = useSelector(getCategories)
    return (
        <>
            <h1>Categories</h1>
            <p><Link to="/categories/create">Create new</Link></p>
            <Categories categories={categories}/>
        </>
    )
}