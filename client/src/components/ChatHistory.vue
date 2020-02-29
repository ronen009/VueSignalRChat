<template>
  <div id="app" style="border:2px solid;">
   
     <span>current user is : {{getCurrentUser}}</span>
    
    <v-app id="inspire">
      <div v-for="item in getChatHistory" :key="item">
        <v-card
          v-bind:class="[item.fromUserId == getCurrentUser.id ? 'selfChatItem' : 'otherChatitem']"
          max-width="80%"
          outlined
          raised="true"
        >
          <v-list-item three-line>
            <v-list-item-content>
              <v-list-item-title class="headline mb-1">{{ item.msg }}</v-list-item-title>
              <v-list-item-action-text class="overline mb-3">from : {{ item.fromUserId }}</v-list-item-action-text>
              <v-list-item-subtitle>{{item.sentDate}}</v-list-item-subtitle>
              <v-list-item-subtitle>{{item}}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-card>
      </div>
    </v-app>
  </div>
</template>

<script lang="ts">
import { mapGetters } from "vuex";
import chatMsg from "../models/chatMsg";

export default {
  data: () => ({
    item: Number
  }),
  computed: mapGetters(["getChatHistory", "getCurrentUser"])
};
</script>

<style scoped>
.divContainer {
  background: lightskyblue;
  height: 100%;
}

.selfChatItem {
  background-color: papayawhip;
  float: right;
  margin: 5px;
}

.otherChatitem {
  background-color: powderblue;
  float: left;
  margin: 5px;
}
</style>
