<template>
  <div>
    <b-form @submit.prevent="onSubmit">
      <b-form-group
        id="input-group-1"
        label="Product Name:"
        label-for="input-1"
      >
        <b-form-input
          id="input-1"
          v-model="formData.name"
          type="name"
          placeholder="Enter name"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        id="input-group-2"
        label="Product Price:"
        label-for="input-2"
      >
        <b-form-input
          id="input-2"
          v-model="formData.price"
          type="price"
          placeholder="Enter price"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        id="input-group-3"
        label="Product Description:"
        label-for="input-3"
      >
        <b-form-input
          id="input-3"
          v-model="formData.description"
          type="description"
          placeholder="Enter description"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group
        id="input-group-4"
        label="Product Image:"
        label-for="input-4"
      >
        <b-form-input
          id="input-4"
          v-model="formData.image"
          type="image"
          placeholder="Enter image"
          required
        ></b-form-input>
      </b-form-group>
      <div class="alert alert-danger" role="alert" v-if="serverError">
        {{ errorMsg }}
      </div>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form>
  </div>
</template>

<script>
import axios from "axios";
import { useUserStore } from "@/stores/user.data";
export default {
  name: "AddProduct",
  data() {
    return {
      formData: {
        name: "",
        description: "",
        price: "",
        image: "",
      },
      submitted: false,
      serverError: false,
      errorMsg: "",
    };
  },
  methods: {
    onSubmit() {
      const store = useUserStore();
      const config = {
        headers: { Authorization: `Bearer ${store.auth}` },
      };
      console.log(store.userId);

      this.submitted = true;
      this.serverError = false;
      this.errorMsg = "";
      axios
        .post("/products", this.formData, config)
        .then((response) => {
          this.submitted = false;
          this.$router.push({ name: "HomePage" });
        })
        .catch((error) => {
          console.log(error);
          this.submitted = false;
          this.serverError = true;
          this.errorMsg = error.response.data.message;
        });
    },
  },
};
</script>