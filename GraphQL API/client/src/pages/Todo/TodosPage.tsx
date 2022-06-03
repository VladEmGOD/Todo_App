import React from "react";
import {useSelector} from "react-redux";
import {getSelectedCategoryId, getTodos} from "../../redux/selectors/todoSelectors";
import {getCategories} from "../../redux/selectors/categoriesSelector";
import {TodoInputForm} from "../../components/Todo/TodoInputForm";
import {CategorySelector} from "../../common/CategorySelector";
import {Todos} from "../../components/Todo/Todos";
import {useAppSelector} from "../../redux/types/hooks";

export const TodosPage = () => {
    let todos = useAppSelector(getTodos)
    const categories = useAppSelector(getCategories)
    const selectedCategoryId = useAppSelector(getSelectedCategoryId)

    if (selectedCategoryId) todos = todos.filter(t => t.categoryId === selectedCategoryId)
    return (
        <>
            <TodoInputForm categories={categories}/>
            <hr/>
            <CategorySelector categories={categories}/>
            <hr/>
            <Todos categories={categories} todos={todos}/>
        </>
    )
}