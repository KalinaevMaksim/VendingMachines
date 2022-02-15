import { createApp } from 'vue'
import App from './App.vue'
import router from './routes/router'
import VuePapaParse from 'vue-papa-parse'

import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-icons/font/bootstrap-icons.css"

const app = createApp(App);
app.use(VuePapaParse).use(router).mount('#app')