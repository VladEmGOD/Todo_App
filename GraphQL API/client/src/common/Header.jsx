import React from "react";
import {Container, Form, Nav, Navbar} from "react-bootstrap";
import {useDispatch} from "react-redux";
import {resetSelectedCategoryId} from "../redux/todoSlice";
import {Link} from "react-router-dom";
import {SourceEnum} from "../redux/DataSourceEnum";
import {setDataSource} from "../redux/appSlice";

export const Header = (props) => {
    const dispatch = useDispatch()
    const resetSelectedCategory = (e) => {
        dispatch(resetSelectedCategoryId())
    }
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
                                     onChange={(e)=>dispatch(setDataSource({dataSource: +e.target.value}))}>
                            <option value={SourceEnum.MsSql}>MsSql</option>
                            <option value={SourceEnum.XML}>XML</option>
                        </Form.Select>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    )
}