import React, {useEffect} from "react";
import {useDispatch} from "react-redux";
import {TodoInputForm} from "../../components/Todo/TodoInputForm";
import {CategorySelector} from "../../common/CategorySelector";
import {Todos} from "../../components/Todo/Todos";
import {fetchTodosAsync} from "../../redux/todoSlice";
import {fetchCategoriesAsync} from "../../redux/categoriesSlice";


export const TodosPage = () => {
    const dispatch = useDispatch()

    useEffect(()=>{
        dispatch(fetchTodosAsync())
    },[dispatch])

    useEffect(()=>{
        dispatch(fetchCategoriesAsync())
    },[dispatch])

    return (
        <>
            <TodoInputForm />
            <hr/>
            <CategorySelector/>
            <hr/>
            <Todos />
        </>
    )
}