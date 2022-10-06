import { defineStore } from 'pinia'
import type { Todo } from "./todo.types";

export const useTodoStore = defineStore({
    id: 'Todo',
    state: () => ({
        todos: [] as Todo[]
    }),
    getters: {
        getTodos: (state) => {
            return state.todos.map((i, ind) => {
                i.key = ind.toString();
                i.children = Id(i.children, ind.toString());
                return i;
            })
        }
    },
    actions: {
        async load(data: { offset: number, limit: number }) {
            const response = await fetch("http://localhost:8008/api/Task/all", {
                method: 'POST',
                body: JSON.stringify({ offset: data.offset, limit: data.limit }),
                headers: {
                    'Content-Type': 'application/json'
                },
            });
            this.todos = await response.json();
        },
        async create(data: string) {
            const response = await fetch("http://localhost:8008/api/Task", {
                method: 'POST',
                body: JSON.stringify({ text: data }),
                headers: {
                    'Content-Type': 'application/json'
                },
            });
        },
        async update(data: Todo) {
            console.log(data);

            console.log(convertToUpdateTodo(data));

            const response = await fetch("http://localhost:8008/api/Task", {
                method: 'PUT',
                body: JSON.stringify(convertToUpdateTodo(data)),
                headers: {
                    'Content-Type': 'application/json'
                },
            });
        },
        async delete(id: string) {
            const response = await fetch("http://localhost:8008/api/Task/" + id, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
            });
            this.todos = this.todos.filter(q => q._id != id);
        }
    }
})

function Id(items: Todo[], indexStart: string) {
    items.forEach((i, ind) => {
        i.key = `${indexStart}-${ind}`;
        if (i.children.length > 0) i.children = Id(i.children, i.key);
    });
    return items;
}

function convertToUpdateTodo(item: Todo) {
    let children: any;
    if (item.children.length > 0)
        children = item.children.map(q => convertToUpdateTodo(q));
    return { _id: item._id ?? "children", text: item.text, children: children ?? [] };
}