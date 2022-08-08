import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import SignIn from "@/components/Users/SignIn.vue";
import { useUserStore } from "@/stores/user.data";
import Register from "@/components/Users/Register.vue";



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "HomePage",
      component: HomeView,
    },
    {
      path: "/SignIn",
      component: SignIn,
      name: "LogInPage",
      beforeEnter(to, from, next) {
        const store = useUserStore();
        if (store.roles.length == 0) {
          next();
        } else {
          console.log(store.roles)
          console.log("User already logged in");
          next({ name: "HomePage" });
        }
      },
      },
    {
      path: "/Register",
      component: Register,
      name: "Register",
      beforeEnter(to, from, next) {
        const store = useUserStore();
        if (store.roles.length == 0) {
          next();
        } else {
          console.log("User already logged in");
          next({ name: "HomePage" });
        }
      },
    },

  ],
});

export default router;
