import {useAppDispatch, useAppSelector} from "../redux/types/hooks";
import {Pagination} from "react-bootstrap";
import {fetchTodosAsync} from "../redux/todoSlice";

export const Paginator = () => {
    let count = useAppSelector(state => state.todoPage.todosCount)
    let currentPageSize = useAppSelector(state => state.todoPage.pageSize)
    let active = useAppSelector(state => state.todoPage.activeTodoPage)
    const dispatch = useAppDispatch()


    let items = []
    for (let i = 0; i < count / currentPageSize; i++)
    {
        items.push(
            <Pagination.Item key={i}
                             active={i === active}
                             onClick={e => {
                                 dispatch(fetchTodosAsync({page: i, pageSize: currentPageSize}))
                             }}
                             activeLabel={""}>
                {i+1}
            </Pagination.Item>
        )
    }

    return <Pagination>
        {items}
    </Pagination>
}