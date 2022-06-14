import React, {useEffect} from "react";
import {Categories} from "../../components/Category/Categories";
import {Link} from "react-router-dom";
import {getCategories} from "../../redux/selectors/categoriesSelector";
import {useAppDispatch, useAppSelector} from "../../redux/types/hooks";
import {fetchCategoriesAsync} from "../../redux/categoriesSlice";


export const CategoriesPage = () => {
    const categories = useAppSelector(getCategories)
    const dispatch = useAppDispatch()

    useEffect(()=>{
        dispatch(fetchCategoriesAsync())
    },[dispatch])

    return (
        <>
            <h1>Categories</h1>
            <p><Link to="/categories/create">Create new</Link></p>
            <Categories categories={categories}/>
        </>
    )
}