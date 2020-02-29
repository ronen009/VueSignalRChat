<template>
  <div>
    <v-row>
      <v-col cols="12" md="6">
        <ChatHistory />
      </v-col>
      <v-col cols="12" md="6">
        <chat-message />
      </v-col>
    </v-row>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { mapGetters, mapMutations } from "vuex";
import store from "../store";
import ChatMessage from "../components/ChatMessage.vue";
import ChatHistory from "../components/ChatHistory.vue";
import chatMsg from "../models/chatMsg";

export default {
  name: "ChatScreen",
  //
  data: () => ({
    item: 1,
    items: [
      { text: "Real-Time", icon: "mdi-clock" },
      { text: "Audience", icon: "mdi-account" },
      { text: "Conversions", icon: "mdi-flag" }
    ]
  }),
  created: function() {
    this.$msgHub.$on("msg-sent-event", this.msgAddedEvent);
  },
  beforeDestroy: function() {
    this.$msgHub.$off("msg-sent-event", this.msgAddedEvent);
  },
  methods: {
    msgAddedEvent(iFromUserId: number , iToUserId : number, iMsg: string) {
       
      //need to handle groups. only one client should be notified. Temp fix condition used.
     let currentUser = this.$store.getters["getCurrentUser"]; 
        debugger;
     // if (iId == currentUser.id)
     // {
     //   console.log("receiver sender prevented. Temp fix. Groups should be handled.");
     //  return;
     // }
      let msgToAdd = new chatMsg();
      msgToAdd.toUserId = iToUserId; //1019; //current user...
      msgToAdd.fromUserId = iFromUserId;
      msgToAdd.sentDate = new Date(); // should come from server...
      msgToAdd.msg = iMsg;

      this.$store.commit("addMsg", msgToAdd);

      console.log("--- >>> msgAddedEvent : ", iId, iMsg);
    }
  },

  components: {
    ChatMessage,
    ChatHistory
  },
};
</script>

<style scoped></style>
