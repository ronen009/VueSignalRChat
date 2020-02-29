import Vue from "vue";
import Router from "vue-router";
import Welcome from "./views/Welcome.vue";
import Home from "./views/Home.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "Welcome",
      component: Welcome
    },
    {
      path: "/about",
      name: "about",
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () =>
        import(/* webpackChunkName: "about" */ "./views/About.vue")
    },
    {
      path: "/Home",
      name: "Home",
      component: Home
    }
  ]
});
