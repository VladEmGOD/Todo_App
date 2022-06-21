import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {BrowserRouter} from "react-router-dom";
import {Provider} from "react-redux";
import {store} from "./redux/store";
import {client} from "./GraphQl/client";
import {ApolloProvider} from "@apollo/client";


const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
    <ApolloProvider client={client}>
        <BrowserRouter>
            <Provider store={store}>
                <App/>
            </Provider>
        </BrowserRouter>
    </ApolloProvider>
);
