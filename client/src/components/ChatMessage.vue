<template>
  <div>
    <v-text-field v-model="msg"></v-text-field>
    <v-col>
      <v-btn @click="sendMsg">send</v-btn>
    </v-col>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import chatMsgCls from "../models/chatMsg";
import { selectedUserCls, currentUserCls } from "../models/user";
import { mapGetters } from "vuex";
import store from "../store";
import chatMsg from "../models/chatMsg";

export default Vue.extend({
  name: "ChatMessage",
  data: () => ({
    msg: "",
    currentUser: mapGetters(["getUserList"])
  }),
  computed: {},
  methods: {
    sendMsg() {
      let msgToSend = new chatMsg();

      let selectuser: selectedUserCls = this.$store.getters["getSelectedUser"];

      msgToSend.toUserId = selectuser.id;
      msgToSend.msg = this.msg;

      this.$store
        .dispatch("SendMsg", msgToSend)
        .then(res => {
          // eslint-disable-next-line no-console
        })
        .catch(err => {
          // eslint-disable-next-line no-console
          console.log("error at dispatch signUpUser :", err);
        });
    }
  }
});
</script>

<style scoped></style>
