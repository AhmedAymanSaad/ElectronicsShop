<template>
  <div>
    <b-form @submit.prevent="onSubmit">
      <b-form-group
        id="input-group-3"
        label="Email address:"
        label-for="input-3"
      >
        <b-form-input
          id="input-3"
          v-model="formData.email"
          type="email"
          placeholder="Enter email"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-5" label="Password:" label-for="input-5">
        <b-form-input
          id="input-5"
          v-model="formData.password"
          type="password"
          placeholder="Enter password"
          :state="validation"
          required
        >
        </b-form-input>
      </b-form-group>
      <div class="alert alert-danger" role="alert" v-if="serverError">
        {{ errorMsg }}
      </div>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form>
    Already Registered?
    <router-link :to="{ name: 'LogInPage' }">Sign In</router-link>
  </div>
</template>

<script>
import { useUserStore } from "@/stores/user.data";
import axios from "axios";
export default {
  name: "Register",
  data() {
    return {
      formData: {
        email: "",
        password: "",
      },
      confirmPass: "",
      submitted: false,
      options: [
        { text: "User Account", value: "Customer" },
        { text: "Manager Account", value: "Manager" },
      ],
      serverError: false,
      errorMsg: "",
    };
  },
  methods: {
    onSubmit() {
      const store = useUserStore();
      this.submitted = true;
      if (!(this.validation && this.validateConfirmPass)) {
        console.log("validation failed, request aborted");
        return;
      }
      axios.post("auth/register", this.formData).then(
        (response) => {
          console.log(response);
          store.userSignIn(
            response.data.authToken,
            response.data.userId,
            response.data.roles,
            io
          );

          console.log(store.userId);
          console.log(store.authToken);
          console.log(store.roles);
          this.$router.push({ name: "HomePage" });
        },
        (error) => {
          console.log(error);
          this.serverError = true;
          this.errorMsg = error.response.data.message;
        }
      );
      /*
      this.$http.post("customers/", this.formData).then(
        (response) => {
          console.log(response);
          console.log("user created");
          this.$router.push({ name: "HomePage" });
          this.$store.commit("setAuth", response.headers.get("x-auth-token"));
          console.log(response.headers.get("x-auth-token"));
          this.$store.commit("setUserType", "Customer");
        },
        (error) => {
          console.log(error);
          this.serverError = true;
          this.errorMsg = error.body;
        }
      );*/
    },
  },
  computed: {
    validation() {
      return true;
      if (!this.submitted) {
        return null;
      }
      return (
        this.validatePasswordLength &&
        this.validatePasswordNumber &&
        this.validatePasswordLetter
      );
    },
    validatePasswordLength() {
      if (!this.submitted) {
        return true;
      }
      return this.formData.password.length > 8;
    },
    validatePasswordNumber() {
      if (!this.submitted) {
        return true;
      }
      return /\d/.test(this.formData.password);
    },
    validatePasswordLetter() {
      if (!this.submitted) {
        return true;
      }
      return /[a-zA-Z]/.test(this.formData.password);
    },
    validateConfirmPass() {
      return true;
      if (!this.submitted || !this.validation) {
        return null;
      }
      return this.formData.password == this.confirmPass;
    },
  },
};
</script>

<style>
</style>