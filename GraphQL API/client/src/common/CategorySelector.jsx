import React from 'react';
import {ButtonGroup, Dropdown, DropdownButton,} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {resetSelectedCategoryId, setSelectedCategoryId} from "../redux/todoSlice";

export const CategorySelector = ({categories}) => {
    const dispatch = useDispatch()

    return (
        <DropdownButton as={ButtonGroup} menuVariant={"secondary"} title={"Select category"}>
            <Dropdown.Item key={0}
                           onClick={() => dispatch(resetSelectedCategoryId())}>Show All</Dropdown.Item>
            <Dropdown.Divider/>
            {
                categories.map(c => <Dropdown.Item key={c.id}
                                                   onClick={() => dispatch(setSelectedCategoryId({id: c.id}))}>
                    {c.name}
                </Dropdown.Item>)
            }
        </DropdownButton>
    )
};
