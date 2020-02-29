<template>
  <v-container>
    <v-row>
      <v-col cols="3">
        <UserList />
      </v-col>
      <v-col cols="9">
        <ChatScreen />
      </v-col>
    </v-row>
  </v-container>
  <!-- <HelloWorld /> -->
</template>

<script lang="ts">
import Vue from "vue";
import UserList from "../components/UserList.vue";
import ChatScreen from "../components/ChatScreen.vue";

export default Vue.extend({
  name: "Home",

  components: {
    UserList,
    ChatScreen
    // HelloWorld
  }
});
</script>
