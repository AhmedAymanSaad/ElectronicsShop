

<template>
  <header></header>
   <div class="toast fixed-bottom" style="margin: 10pt">
    <div class="toast-header">
      Delivery Notification
    </div>
    <div class="toast-body">
      Your order has been delivered!
    </div>
  </div>

  <RouterView />
  
</template>
<script >
import { useUserStore } from "@/stores/user.data";
export default {
  name: "App",
  methods: {
    
  },
  mounted() {
    var socket = io.connect("ws://localhost:3000");
    socket.on("notification", function (data) {
      console.log(data);
    });
    socket.on("connect", function () {
      console.log("connected");
    });
    socket.on("join", function (data) {
      console.log(data);
      const store = useUserStore();
      if (store.userId == null) {
        console.log("no user id");
        return;
      }
      socket.emit("join", store.userId);
      console.log("joined");
    });
    socket.on("orderdelivery", function (data) {
      console.log(data);
      $(document).ready(function(){
  $('.toast').toast('show');
});
      //document.getElementById("toastbutton").click();
    });
    
  },
};
/*
var socket = io.connect("ws://localhost:3000");
socket.on("notification", function (data) {
  console.log(data);
});
socket.on("connect", function () {
  console.log("connected");
});
socket.on("join", function (data) {
  console.log(data);
  const store = useUserStore();
  if (store.userId == null) {
    console.log("no user id");
    return;
  }
  socket.emit("join", store.userId);
  console.log("joined");
});
socket.on("orderdelivery", function (data) {
  console.log(data);});
  document.getElementById("toastbutton").click();
  */
</script>



