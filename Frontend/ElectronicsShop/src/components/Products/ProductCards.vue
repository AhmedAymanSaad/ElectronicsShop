<template>
  <span>
    <b-card
      :title="name"
      :img-src="image"
      img-alt="Image"
      img-top
      style="width: 100%; display: inline-block; margin: 5px"
      class="mb-2 text-center"
      
    >
      <b-card-text>
        <div>Description {{Description}}</div>
        <div>Price {{price}}</div>
      </b-card-text>

      <b-button @click="getProduct" variant="primary" class="text-right" >Order</b-button>
    </b-card>
  </span>
</template>

<script>
export default {
  name: "ProductCards",
  props: ["image", "name", "price","index","prodId","Description"],
  methods:{
      getProduct(id){
        console.log(this.prodId);
          return this.$emit('getProduct', this.$props.prodId);
      }
  },
};
</script>

<style>
.card {
  display: inline-block;
}</style>