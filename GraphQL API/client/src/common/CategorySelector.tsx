import React from 'react';
import {ButtonGroup, Dropdown, DropdownButton,} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {resetSelectedCategoryId, setSelectedCategoryId} from "../redux/todoSlice";
import {useAppDispatch} from "../redux/types/hooks";
import {CategoryType} from "../redux/types/models";

type PropsType = {
    categories: CategoryType[]
}

export const CategorySelector: React.FC<PropsType> = ({categories}) => {
    const dispatch = useAppDispatch()

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
