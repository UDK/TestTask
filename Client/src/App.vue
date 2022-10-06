<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import Todo from "@/views/Todo.vue";

</script>

<template>
  <Suspense>
    <Todo></Todo>
  </Suspense>
</template>

<style scoped>
</style>

