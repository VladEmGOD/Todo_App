import {ApolloClient, InMemoryCache} from "@apollo/client";

const dataSource = localStorage.getItem('dataSource');

export const client = new ApolloClient({
    uri: "https://localhost:44314/graphql",
    cache: new InMemoryCache({
        addTypename: false
    }),
    credentials: 'include',
    defaultOptions: {
        watchQuery: {
            fetchPolicy: 'no-cache',
            errorPolicy: 'all',
            notifyOnNetworkStatusChange: true,
        },
        query: {
            fetchPolicy: 'no-cache',
            errorPolicy: 'all',
            notifyOnNetworkStatusChange: true,
        },
    },
    headers:{
        "dataSource": dataSource ?? "0"
    }
})
