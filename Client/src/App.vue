<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
import Todo from "@/views/Todo.vue";

</script>

<template>
  <div class="grid">
    <Suspense>
      <div class="col-8 col-offset-2">
        <Todo></Todo>
      </div>
    </Suspense>
  </div>
</template>

<style scoped>
</style>

