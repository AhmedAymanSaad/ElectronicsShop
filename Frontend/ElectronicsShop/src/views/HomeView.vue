

<template>
  <main>
    <button v-on:click="funcrun">hiiii</button>
    <TheWelcome />
  </main>
</template>

<script>
import axios from "axios";
export default {

  name: "HomeView",
  data() {
    return {
      message: "Hello Vue.js!",
    };
  },
  methods: {
    funcrun() {
      axios.get("products").then(
        (response) => {
          console.log(response);
        },
        (error) => {
          console.log(error);
        }
      );
      console.log("hiiii");
    },
  },
};
</script>




