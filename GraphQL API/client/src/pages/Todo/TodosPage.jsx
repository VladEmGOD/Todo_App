import React from "react";
import {Todos} from "../../components/Todo/Todos";
import {TodoInputForm} from "../../components/Todo/TodoInputForm";
import {CategorySelector} from "../../common/CategorySelector";
import {useSelector} from "react-redux";
import {getTodos} from "../../redux/selectors/todoSelectors";
import {getCategories, getSelectedCategoryId} from "../../redux/selectors/categoriesSelector";

export const TodosPage = (props) => {
    let todos = useSelector(getTodos)
    const categories = useSelector(getCategories)
    const selectedCategoryId = useSelector(getSelectedCategoryId)

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