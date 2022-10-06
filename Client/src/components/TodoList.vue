<template>
  <div class="grid">
    <div class="col-12">
      <Card>
        <template #content>
          <TreeTable :value="nodes" selectionMode="checkbox" v-model:selectionKeys="selectionKeys"
            :rows="10">
            <Column header="Name" :expander="true">
              <template #body="slotProps">
                <span v-if="slotProps.node.text && slotProps.node.key != editKey" :class="isCheckNode(slotProps.node.key)">
                  {{slotProps.node.text}}
                </span>
                <span v-else>
                  <InputText v-model="itemText" />
                </span>
              </template>
            </Column>
            <Column>
              <template #body="slotProps">
                <template v-if="slotProps.node.text && slotProps.node.key != editKey">
                  <Button type="button" icon="pi pi-plus" @click="addNewChildItem(slotProps.node)"
                    style="margin-right: .5em"></Button>
                </template>
                <template v-else>
                  <Button type="button" icon="pi pi-check" class="p-button-success" style="margin-right: .5em"
                    @click="acceptNewItem(slotProps.node)"></Button>
                </template>
                <Button type="button" icon="pi pi-pencil" class="p-button-warning" style="margin-right: .5em"
                  @click="editKey = slotProps.node.key; itemText = slotProps.node.text"></Button>
                <Button type="button" icon="pi pi-trash" class="p-button-danger" style="margin-right: .5em"
                  @click="removeItem(slotProps.node.key)"></Button>
              </template>
            </Column>
          </TreeTable>
        </template>
      </Card>
    </div>
    <div class="col-12 grid">
      <InputText v-model="newItemRootText" style="margin: 6px;" class="col-9" />
      <Button type="button" icon="pi pi-plus" style="margin: 6px;" @click="acceptNewItem()" class="col-1" />
    </div>
  </div>
  <Toast position="bottom-left" />
</template>

<script setup lang="ts">
import Card from 'primevue/card';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import TreeTable from 'primevue/treetable';
import Column from 'primevue/column';
import type { Todo } from '@/store/todo.types';
import { useTodoStore } from '@/store/TodoStore'
import { ref } from 'vue';
import { useToast } from "primevue/usetoast";

const nodes = ref([] as Todo[]);

const itemText = ref("");
const newItemKey = ref();
const newItemRootText = ref("");
const loading = ref(true);
const selectionKeys = ref();
const editKey = ref("");
const toast = useToast();
const store = useTodoStore();
await store.load({ offset: 0, limit: 999 });
loading.value = false;
nodes.value = store.getTodos;

const getElementNode = (key: string) => {
  const keysArray = key.split('-');
  let node = nodes.value[+keysArray[0]];

  keysArray.slice(1).forEach(q => {
    node = node.children[+q];
  });
  return node;
};
const addNewChildItem = (data: Todo) => {
  const nodeCurrent = getElementNode(data.key);  
  const newKey =
    nodeCurrent.children.length !== 0
      ? nodeCurrent?.children[nodeCurrent?.children.length - 1].key.replace(/.$/, (+nodeCurrent?.children[nodeCurrent?.children.length - 1].key.slice(-1) + 1).toString()) :
      nodeCurrent.key + "-0";
  
  newItemKey.value = newKey
  nodeCurrent?.children?.push({
    key: newKey,
    text: "",
    children: []
  });
  nodes.value = nodes.value.map(q => {
    if (q.key === nodeCurrent?.key) return nodeCurrent;
    return q;
  });
  itemText.value = "";
  editKey.value = "";

}
const acceptNewItem = async (data?: Todo) => {
  if(!itemText.value && !newItemRootText.value) {
    toast.add({severity: 'error', summary: 'Empty text'})
    return;}
  if (!!data?.key && nodes.value.find(q => q.key == data.key[0])) {
    const rootElement = nodes.value.find(q => q.key == data.key[0]);
    if (!rootElement) return;
    data.text = itemText.value;
    nodes.value.map(q => {
      if (q.key === data.key) return data
      return q;
    })
    await store.update(rootElement);
  } else {
    nodes.value.push({
      key: nodes.value.length.toString(),
      text: newItemRootText.value,
      children: []
    });
    await store.create(newItemRootText.value);
  }
  editKey.value = "";
}
const removeItem = async (key: string) => {
  const keysArray = key.split('-');
  if (keysArray.length == 1) {
    const rootElement = nodes.value[+keysArray[0]]._id;
    nodes.value = nodes.value.filter(q => q._id != rootElement)
    
    if (rootElement)
      store.delete(rootElement)
  }
  let node = nodes.value[+keysArray[0]];
  keysArray.slice(1).forEach((i, ind, array) => {
    if (ind === array.length - 1) {      
      node.children = node.children.splice(+i, +i + 1);
    }
    else
      node = node.children[+i];
  })  
  store.update(nodes.value[+keysArray[0]]);
}
const isCheckNode = (key: string) => {
  if (selectionKeys.value) return selectionKeys.value[key]?.checked ? "line-through" : "";
}
</script>

<style scoped>

</style>
