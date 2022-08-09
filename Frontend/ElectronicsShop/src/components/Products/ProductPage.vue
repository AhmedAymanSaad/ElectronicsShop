<template>
  <div>
    <span>
      <b-card
        :title="product.name"
        :img-src="product.image"
        img-alt="Image"
        img-top
        tag="article"
        class="mb-2 text-center"
      >
        <b-card-text class="text-center">
          <div>Description {{ product.description }}</div>
          <div>
            Price
            {{ product.price }}
          </div>
          <div>
            <b-form-group
              id="input-group-1"
              label="Quantity:"
              label-for="input-1"
            >
              <b-form-input
                id="input-1"
                v-model="quantity"
                type="number"
                placeholder="Enter quantity"
                required
              ></b-form-input>
            </b-form-group>

            <b-button @click="makeOrder" variant="primary" class="text-right">
              Order
            </b-button>
          </div>
        </b-card-text>
      </b-card>
    </span>
  </div>
</template>

<script>
import axios from "axios";
import { useUserStore } from "@/stores/user.data";
export default {
  name: "ProductPage",
  data() {
    return {
      product: null,
      createdRun: false,
      quantity: 1,
    };
  },
  props: {},
  methods: {
    getProduct() {
      axios.get("products/" + this.$route.params.id).then((response) => {
        console.log(response.data);
        this.product = response.data;
      });
    },
    makeOrder() {
      const store = useUserStore();
      const body = {
        userId: store.userId,
        productId: this.product._id,
        quantity: this.quantity,
      };
      const config = {
        headers: { Authorization: `Bearer ${store.auth}` },
      };
      console.log(this.quantity);
      axios
        .post("orders", body,config)
        .then((response) => {
          console.log(response.data);
          this.createdRun = true;
            this.$router.push({ name: "HomePage" });
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  created() {
    this.getProduct();
  },
  computed: {
    isUserLoggedIn() {
      const store = useUserStore();
      return store.isUserLoggedIn;
    },
  },
};
</script>