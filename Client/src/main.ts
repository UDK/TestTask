import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import PrimeVue from 'primevue/config';
import { createPinia } from 'pinia'

import './assets/main.css'
import "primevue/resources/primevue.min.css"
import "primevue/resources/themes/saga-blue/theme.css"
import "primeicons/primeicons.css"
import "/node_modules/primeflex/primeflex.css"

const pinia = createPinia();
const app = createApp(App)

app
    .use(router)
    .use(PrimeVue)
    .use(pinia)

app.mount('#app')
