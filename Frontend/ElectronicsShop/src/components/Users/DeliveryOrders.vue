<template>
<div>
    <b-button
      v-if="false"
      ref="refresh"
      @click="getBestsellers"
      variant="primary"
    ></b-button>
<keep-alive>
      <span>
        <span v-for="(order, im) in ordersList" :key="im" inline>
          <order-card
            inline
            :userId="order.userId"
            :userEmail="order.user[0].email"
            :orderId="order._id"
            :prodName="order.product[0].name"
            :price="order.price"
            :quantity="order.quantity"
            :dateOrdered="order.createdAt"
            @deliveryConfirmed="deliveryConfirmed"
            
          ></order-card>
        </span>
      </span>
    </keep-alive>
</div>
</template>

<script>
import axios from "axios";
import { useUserStore } from "@/stores/user.data";
import OrderCard from "@/components/Users/OrderCard.vue";

export default ({
    name: "DeliveryOrders",
    data(){
        return {
            ordersList: [],
        }
    },
    created(){
        this.getPendingOrders();
    },
    methods:{
        getPendingOrders(){
            const store = useUserStore();
            //console.log(store.auth)
            const config = {
                headers: {
                    Authorization: "Bearer " + store.auth,
                },
            };

            axios.get("/orders/delivery/pending",config).then(response => {
                console.log(response.data)
                this.ordersList = response.data;
            }).catch(error => {
                console.log(error);
            }).finally(() => {
                console.log("finally");
            });
        },
        deliveryConfirmed(orderId){
            const store = useUserStore();
            const config = {
                headers: {
                    Authorization: "Bearer " + store.auth,
                },
            };
            axios.patch("/orders/status/" + orderId,{},config).then(response => {
                console.log(response.data)
                this.$router.go();
            }).catch(error => {
                console.log(error);
            }).finally(() => {
                console.log("finally");
            });
        }
    },
    components:{
        OrderCard: OrderCard,
    },
})
</script>
