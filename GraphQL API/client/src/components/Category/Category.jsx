import React from "react";
import {Link, useHistory} from "react-router-dom";
import {useDispatch} from "react-redux";
import {removeCategory, setEditCategory} from "../../redux/categoriesSlice";

export const Category = ({category}) => {
    const dispatch = useDispatch()
    return (
        <>
            <tr key={category.id}>
                <td>{category.name}</td>
                <td>
                    <Link to={"/categories/edit/"}
                          onClick={event => {
                              dispatch(setEditCategory({id: category.id}))
                          }}>Edit</Link> |

                    <Link to={"/categories/delete/"}
                          onClick={event => {
                              event.preventDefault()
                              dispatch(removeCategory({id: category.id}))
                          }}
                    >Delete</Link>
                </td>
            </tr>
        </>
    )
}