import React from "react";
import {Link} from "react-router-dom";
import {deleteCategoryAsync, removeCategory} from "../../redux/categoriesSlice";
import {CategoryType} from "../../redux/types/models";
import {useAppDispatch} from "../../redux/types/hooks";

type Props = {
    category: CategoryType
}

export const Category: React.FC<Props> = ({category}) => {
    const dispatch = useAppDispatch()
    return (
        <>
            <tr key={category.id}>
                <td>{category.name}</td>
                <td>
                    <Link to={"/category/edit/" + category.id}>Edit</Link> |

                    <Link to={"/category/delete/"}
                          onClick={event => {
                              event.preventDefault()
                              dispatch(deleteCategoryAsync(category.id))
                          }}
                    >Delete</Link>
                </td>
            </tr>
        </>
    )
}