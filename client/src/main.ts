import Vue from "vue";
import App from "./App.vue";
import axios from "axios";
import store from "./store";
import router from "./router";
import vuetify from "./plugins/vuetify";

import userHub from "./user-hub.js";
import msgHub from "./msg-hub.js";

Vue.config.productionTip = false;

// Setup axios as the Vue default $http library
//axios.defaults.baseURL = "https://localhost:44376"; // same as the Url the server listens to

Vue.prototype.$http = axios.create({
  baseURL: "https://localhost:44376"
});

Vue.use(vuetify);

Vue.use(userHub);
Vue.use(msgHub);

new Vue({
  store,
  router,
  vuetify,
  render: h => h(App)
}).$mount("#app");
