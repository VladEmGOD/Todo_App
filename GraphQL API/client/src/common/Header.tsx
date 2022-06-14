import React from "react";
import {Container, Form, Nav, Navbar} from "react-bootstrap";
import {resetSelectedCategoryId} from "../redux/todoSlice";
import {Link} from "react-router-dom";
import {SourceEnum} from "../redux/types/DataSource";
import {setDataSource} from "../redux/appSlice";
import {useAppDispatch, useAppSelector} from "../redux/types/hooks";

export const Header = () => {
    const dispatch = useAppDispatch()
    const resetSelectedCategory = (e: React.MouseEvent<HTMLElement>) => {
        dispatch(resetSelectedCategoryId())
    }

    const dateSource = useAppSelector(state => state.app.dataSource)

    return (
        <>
            <Navbar expand={"sm"} bg={"while"}
                    className={"navbar-toggleable-sm navbar-light border-bottom box-shadow mb-3"}>
                <Container>
                    <Navbar.Brand onClick={resetSelectedCategory}>TODO_APP React client</Navbar.Brand>
                    <Navbar.Collapse className={"d-sm-inline-flex justify-content-between"}>
                        <Nav className={"flex-grow-1"}>
                            <Link to="/" replace className={"nav-link"} onClick={resetSelectedCategory}>Todos</Link>
                            <Link to="/categories" replace className={"nav-link"}
                                  onClick={resetSelectedCategory}>Categories</Link>
                        </Nav>
                        <Form.Select aria-label={".form-select-lg"}
                                     value={dateSource}
                                     onChange={(e)=>dispatch(setDataSource(e.target.value))}>
                            <option value={SourceEnum.MsSql}>MsSql</option>
                            <option value={SourceEnum.XML}>XML</option>
                        </Form.Select>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    )
}