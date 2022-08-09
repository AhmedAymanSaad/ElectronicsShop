<template>
  <div>
    <form>
      <div class="form-group">
        <label for="exampleInputEmail1">User Name</label>
        <input
          type="text"
          class="form-control"
          id="exampleInputEmail1"
          aria-describedby="emailHelp"
          placeholder="Enter user name"
          v-model="user.email"
        />
      </div>
      <div class="form-group">
        <label for="exampleInputPassword1">Password</label>
        <input
          type="password"
          class="form-control"
          id="exampleInputPassword1"
          placeholder="Password"
          v-model="user.password"
        />
      </div>
      <div v-if="errorMsg" class="alert alert-danger" role="alert">
        Invalid user name or password
      </div>
      <div>
        Don't have an account?
        <router-link :to="{ name: 'Register' }">Register</router-link>
      </div>
      <button type="button" class="btn btn-primary" @click="signInUser">
        Submit
      </button>
    </form>
  </div>
</template>

<script>
import axios from "axios";
import { useUserStore } from "@/stores/user.data";
export default {
  name: "SignIn",
  data() {
    return {
      user: {
        email: "",
        password: "",
      },
      errorMsg: false,
    };
  },
  methods: {
    signInUser() {
      const store = useUserStore();
      axios.post("auth/login", this.user).then(
        //this.$http.post("auth/login", this.user).then(
        (response) => {
          console.log(response);
          this.$router.push({ name: "HomePage" });
          store.userSignIn(
            response.data.authToken,
            response.data.userId,
            response.data.roles,
            io
          );
        },
        (error) => {
          console.log(error);
          this.errorMsg = true;
        }
      );
    },
  },
};
</script>

<style>
</style>