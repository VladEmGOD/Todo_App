import React from "react";
import {Route} from "react-router-dom";
import {CategoriesPage} from "./pages/Category/CategoriesPage";
import {TodosPage} from "./pages/Todo/TodosPage";
import {Header} from "./common/Header";
import {Container} from "react-bootstrap";
import {EditTodoPage} from "./pages/Todo/EditTodoPage";
import {EditCategoryPage} from "./pages/Category/EditCategoryPage";
import {CreateCategoryPage} from "./pages/Category/CreateCategoryPage";

function App() {
    return <>
        <Header/>
        <Container>
            <div className="App pb-3">
                <Route path="/" exact render={() => <TodosPage/>}/>
                <Route path="/categories" exact render={() => <CategoriesPage/>}/>
                <Route path="/categories/create" exact render={() => <CreateCategoryPage/>}/>
                <Route path="/todo/edit/:id" exact render={() => <EditTodoPage/>}/>
                <Route path="/category/edit/:id" exact render={() => <EditCategoryPage/>}/>
            </div>
        </Container>
    </>
}

export default App;
