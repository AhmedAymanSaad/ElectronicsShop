<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="info" fixed="top">
      <b-navbar-brand href="#">Cinema</b-navbar-brand>
      <b-navbar-nav class="ml-auto text-right">
        <b-nav-item-dropdown text="User" right>
          <b-dropdown-item @click="goToSignIn" v-show="!isSignedIn"
            >Sign In</b-dropdown-item
          >
          <b-dropdown-item @click="logout" v-show="isSignedIn"
            >Log Out</b-dropdown-item
          >
          <b-dropdown-item @click="goToRegister" v-show="!isSignedIn"
            >Register</b-dropdown-item
          >
        </b-nav-item-dropdown>
      </b-navbar-nav>
      <b-navbar-nav class="ml-auto text-right">
        <b-nav-item-dropdown text="Manager" right v-show="isAdmin || isDelivery">
          <b-dropdown-item @click="goToAddProduct" v-show="isAdmin">Add Product</b-dropdown-item>
          <b-dropdown-item @click="goToDeliveryOrders" v-show="isDelivery">Delivery Orders</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-navbar>
  </div>

  Bestsellers:
  <div>
    <b-button
      v-if="false"
      ref="refresh"
      @click="getBestsellers"
      variant="primary"
    ></b-button>
    <keep-alive>
      <span>
        <span v-for="(product, im) in bestsellerList" :key="im" inline>
          <product-cards
            inline
            :image="product.product[0].image"
            :name="product.product[0].name"
            :price="product.product[0].price"
            :index="im"
            :prodId="product.product[0]._id"
            :Description="product.product[0].description"
            @getProduct="getProduct"
          ></product-cards>
        </span>
      </span>
    </keep-alive>
  </div>

  Products:
  <div>
    <b-button
      v-if="false"
      ref="refresh"
      @click="getAllProducts"
      variant="primary"
    ></b-button>
    <keep-alive>
      <span>
        <span v-for="(product, im) in productsList" :key="im" inline>
          <product-cards
            inline
            :image="product.image"
            :name="product.name"
            :price="product.price"
            :index="im"
            :prodId="product._id"
            :Description="product.description"
            @getProduct="getProduct"
          ></product-cards>
        </span>
      </span>
    </keep-alive>
  </div>
</template>

<script>
import axios from "axios";
import { useUserStore } from "@/stores/user.data";
import ProductCards from "@/components/Products/ProductCards.vue";

export default {
  name: "HomePage",
  data() {
    return {
      productsList: [],
      bestsellerList: [],
      createdRun: false,
    };
  },
  methods: {
    goToSignIn() {
      this.$router.push({ name: "LogInPage" });
    },
    logout() {
      const store = useUserStore();
      store.logOutUser();
      this.$router.push({ name: "HomePage" });
    },
    goToRegister() {
      this.$router.push({ name: "Register" });
    },
    goToAddProduct() {
      this.$router.push({ name: "AddProduct" });
    },
    goToDeliveryOrders() {
      this.$router.push({ name: "DeliveryOrders" });
    },
    getProduct(id) {
      this.$router.push({ name: "ProductPage", params: { id: id } });
    },
    getAllProducts() {
      axios.get("products").then(
        (response) => {
          console.log(response.data);
          this.productsList = response.data;
        },
        (error) => {
          console.log(error);
        }
      );
    },
    getBestsellers() {
      axios.get("orders/products/bestsellers").then((response) => {
        console.log(response.data);
        this.bestsellerList = response.data;
      });
    },
  },
  components: {
    ProductCards: ProductCards,
  },
  deactivated() {
    console.log("deac  Called");
  },
  computed: {
    isSignedIn() {
      const store = useUserStore();
      return store.roles.length != 0;
    },
    isUser() {
      const store = useUserStore();
      return store.roles.includes("user");
    },
    isAdmin() {
      const store = useUserStore();
      return store.roles.includes("admin");
    },
    isDelivery() {
      const store = useUserStore();
      return store.roles.includes("delivery");
    },
  },
  created() {
    this.getAllProducts();
    this.getBestsellers();
  },
};
</script>

<style>
.card {
  display: inline-block;
}
</style>