export type TodoType = {
    id: number,
    title: string,
    deadline?: string,
    isDone : boolean,
    categoryId?: number
}

export type CategoryType = {
    id: number,
    name: string
}