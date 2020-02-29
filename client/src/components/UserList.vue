<template>
  <div class="wrapperDiv">
    <v-card height="100%" dark>
      <v-list>
        <v-subheader>Hello {{ getCurrentUser.userName }}</v-subheader>
        <v-list-item-group color="primary">
          <v-list-item v-for="(item, i) in getUserList" :key="i" @click="selectUser(item)">
            <v-list-item-icon>
              <v-icon v-text="item.icon" :color="item.color"></v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title v-text="item.userName"></v-list-item-title>
            </v-list-item-content>
            <v-list-item-content>
              <v-list-item-title v-text="item.id"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-card>

    <button @click="submitCard()">click...</button>

    <span>userName :: {{ userName }}</span>
    <span>userMessage :: {{ userMessage }}</span>
    <span>messages :: {{ messages }}</span>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { mapGetters, mapMutations, Store } from "vuex";
import { selectedUserCls, currentUserCls } from "../models/user";

import store from "../store";

export default Vue.extend({
  name: "UserList",
  data: () => ({
    selectedUserName: "",
    connection: {},
    messages: [],
    userName: "",
    userMessage: ""
  }),
  created: function() {
    this.$userHub.$on("user-added-event", this.userAddedEvent);

    //this.$userHub.$on("user-loged-out-event", this.userAddedEvent);
    //window.addEventListener("beforeunload", this.userLoggedOutEvent);
  },
  beforeDestroy: function() {
    this.$userHub.$off("user-added-event", this.userAddedEvent); //clean SignalR event

    //this.$userHub.$off("user-loged-out-event", this.userLoggedOutEvent);
  },
  methods: {
    submitCard: function() {
      this.userName = "AA";
      this.userMessage = "BB";

      if (this.userName && this.userMessage) {
        this.userName = "";
        this.userMessage = "";
      }
    },
    selectUser(item: selectedUserCls) {
      this.$store.commit("setSelectedUser", item.id);
      //let xxx: selectedUserCls = this.$store.getters["getSelectedUser"];
      //this.selectedUserName = xxx.userName;

      this.$store
        .dispatch("GetMsgHistory", item.id)
        .then(res => {
          // eslint-disable-next-line no-console
        })
        .catch(err => {
          // eslint-disable-next-line no-console
          console.log("error at dispatch signUpUser :", err);
        });
    },
    userAddedEvent(iUserId: number) {
      let userToAdd = new selectedUserCls();
      userToAdd.id = iUserId;

      this.$store.commit("addUser", userToAdd);
    },
    userLoggedOutEvent() {
      alert();
      //this.$store.commit("logOutUser", iUserId);
    }
  },
  mounted: function() {},
  beforeCreate() {
    this.$store
      .dispatch("getUserList")
      .then(res => {})
      .catch(err => {
        // eslint-disable-next-line no-console
        console.log("error at dispatch signUpUser :", err);
      });
  },
  computed: mapGetters(["getUserList", "getSelectedUser", "getCurrentUser"])
});
</script>

<style scoped></style>
