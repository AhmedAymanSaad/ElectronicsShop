<template>
  <div>
    <span>
      <b-card
        :title="name"
        style="width: 100%; display: inline-block; margin: 5px"
        class="mb-2 text-center"
      >
        <b-card-text>
          <div>userEmail: {{ userEmail }}</div>
          <div>prodName: {{ prodName }}</div>
          <div>Price: {{ price }}</div>
          <div>Quantity: {{ quantity }}</div>
          <div>DateOrdered: {{ dateOrdered }}</div>
          <div>OrderId: {{ orderId }}</div>
          <div>UserId: {{ userId }}</div>
        </b-card-text>

        <b-button
          @click="deliveryConfirmed"
          variant="primary"
          class="text-right"
          >Delivered</b-button
        >
      </b-card>
    </span>
  </div>
</template>

<script>
export default {
  name: "OrderCard",
  props: [
    "userId",
    "userEmail",
    "orderId",
    "prodName",
    "price",
    "quantity",
    "dateOrdered",
  ],
  data() {
    return {};
  },
  created() {},
  methods: {
    deliveryConfirmed(orderId) {
      return this.$emit("deliveryConfirmed", this.$props.orderId);
    },
  },
  components: {},
};
</script>

<style>
.card {
  display: inline-block;
}
</style>