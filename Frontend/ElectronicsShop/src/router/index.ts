import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import SignIn from "@/components/Users/SignIn.vue";
import { useUserStore } from "@/stores/user.data";
import Register from "@/components/Users/Register.vue";
import HomePage from "@/components/MainPage/HomePage.vue";
import AddProduct from "@/components/Products/AddProduct.vue";
import ProductPage from "@/components/Products/ProductPage.vue";
import ErrorPage from "@/components/MainPage/ErrorPage.vue";
import DeliveryOrders from "@/components/Users/DeliveryOrders.vue";



const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/test",
      name: "Home",
      component: HomeView,
    },
    {
      path: "/",
      name: "HomePage",
      component: HomePage,
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
    {
      path: "/AddProduct",
      name: "AddProduct",
      component: AddProduct,
      beforeEnter(to, from, next) {
        const store = useUserStore();
        if (store.roles.includes("admin")) {
          next();
        } else {
          console.log("Access Denied");
          next({
            name: "Error",
            params: { errorCode: 401, errorMsg: "Access Denied" }
          });
        }
      }
    },
    {
      path: "/ProductPage/:id",
      name: "ProductPage",
      component: ProductPage,
    },
    {
      path: "/Error",
      name: "Error",
      component: ErrorPage,
      props: true,
    },
    {
      path: "/DeliveryOrders",
      name: "DeliveryOrders",
      component: DeliveryOrders,
    }
  ],
});

export default router;
