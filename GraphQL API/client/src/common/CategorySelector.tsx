import React from 'react';
import {ButtonGroup, Dropdown, DropdownButton,} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {resetSelectedCategoryId, setSelectedCategoryId} from "../redux/todoSlice";
import {useAppDispatch, useAppSelector} from "../redux/types/hooks";
import {CategoryType} from "../redux/types/models";
import {getCategories} from "../redux/selectors/categoriesSelector";

export const CategorySelector: React.FC = () => {
    const dispatch = useAppDispatch()
    const categories = useAppSelector(getCategories)


    return (
        <DropdownButton as={ButtonGroup} menuVariant={"secondary"} title={"Select category"}>
            <Dropdown.Item key={0}
                           onClick={() => dispatch(resetSelectedCategoryId())}>Show All</Dropdown.Item>
            <Dropdown.Divider/>
            {
                categories.map(c => <Dropdown.Item key={c.id}
                                                   onClick={() => dispatch(setSelectedCategoryId(c.id))}>
                    {c.name}
                </Dropdown.Item>)
            }
        </DropdownButton>
    )
};
