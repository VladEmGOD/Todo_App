import React, {useEffect} from "react";
import {useDispatch} from "react-redux";
import {TodoInputForm} from "../../components/Todo/TodoInputForm";
import {CategorySelector} from "../../common/CategorySelector";
import {Todos} from "../../components/Todo/Todos";
import {fetchTodosAsync} from "../../redux/todoSlice";
import {fetchCategoriesAsync} from "../../redux/categoriesSlice";
import {Paginator} from "../../common/Paginator";
import {useAppSelector} from "../../redux/types/hooks";


export const TodosPage = () => {
    const dispatch = useDispatch()
    const activePage = useAppSelector(state=> state.todoPage.activeTodoPage)
    const selectedPageSize = useAppSelector(state=> state.todoPage.pageSize)

    useEffect(()=>{
        dispatch(fetchTodosAsync({page: activePage, pageSize: selectedPageSize}))
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
            <Paginator/>
        </>
    )
}