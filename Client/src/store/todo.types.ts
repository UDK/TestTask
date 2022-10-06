export interface Todo {
    _id?: string,
    key: string,
    text: string,
    children: Todo[]
}