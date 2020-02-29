<template>
  <v-form v-model="valid">
    <span>Login</span>
    <v-container>
      <v-row v-if="codeReceived == false">
        <v-col cols="12" sm="6" md="4">
          <v-text-field
            v-model="loginObj.phone"
            single-line
            label="Phone number"
            required
            :rules="phoneRules"
          />
        </v-col>

        <v-col cols="12" sm="6" md="6">
          <v-btn
            class="mr-4"
            color="success"
            @click="ReceiveCode"
            :disabled="valid == false"
          >Receive Code</v-btn>
        </v-col>
      </v-row>

      <v-row v-if="codeReceived == true">
        <v-col cols="12" sm="6" md="4">
          <v-text-field
            type="number"
            v-model="loginObj.code"
            single-line
            label="Code"
            required
            :rules="codeRules"
          />
        </v-col>

        <v-col cols="12" sm="6" md="6">
          <v-btn
            class="mr-4"
            color="success"
            @click="SendCode"
            :disabled="codeReceived == false || valid == false"
          >Send Code</v-btn>
        </v-col>
      </v-row>

      <v-row></v-row>
    </v-container>
  </v-form>
</template>

<script lang="ts">
import Vue from "vue";
import { mapGetters, mapMutations, Store } from "vuex";
import loginCls from "../models/loginCls";
import store from "../store";
import axios from "axios";

export default Vue.extend({
  name: "login",

  data: () => ({
    loginObj: new loginCls(),
    valid: false,
    codeReceived: false,
    phoneNum: String,

    // asdasd
    phoneRules: [
      (v: any) => !!v || "Phone is required",
      (v: any) =>
        /[0][5][0|1|2|4|3|5|8][0-9]{7}/.test(v) || "Phone must be valid"
    ],
    codeRules: [
      (v: any) => !!v || "Code is required",
      (v: any) => /^[0-9]{4}/.test(v) || "Phone must be valid"
    ]
  }),
  methods: {
    ReceiveCode() {
      this.codeReceived = false;
      var objTosend = new loginCls();
      objTosend.phone = this.loginObj.phone;

      axios
        .post(this.$store.state.apiPrefix + "User/ReceiveCode", objTosend)
        .then(res => {
          this.loginObj.code = res.data;
          this.codeReceived = true;

          return res.data;
        })
        .catch(err => {});

      // this.$store
      //   .dispatch("receiveCode", this.loginObj.phone)
      //   .then(res => {
      //     // eslint-disable-next-line no-console
      //     console.log("login ReceiveCode res ", res);
      //   })
      //   .catch(err => {
      //     // eslint-disable-next-line no-console
      //     console.log("error at dispatch signUpUser :", err);
      //   });
    },
    SendCode() {
      var objTosend = new loginCls();
      objTosend.phone = this.loginObj.phone;
      objTosend.code = this.loginObj.code;

      // const options = {
      //   headers: { "X-Custom-Header": "kasjdbasjkdbasjkbddbasdkasbdjk" }
      // };

      axios
        .post(
          this.$store.state.apiPrefix + "User/Login",
          objTosend,
          this.$store.state.headerOptions
        )
        .then(res => {
          this.$store.commit("setUser", res.data);
          this.$store.commit("SetUserToken", res.data.token);

          this.$router.push("/Home");

          // this.$store
          //   .dispatch("getUserList")
          //   .then(res => {
          //     // eslint-disable-next-line no-console
          //     console.log(" ----> getUserList  res : ", res);
          //   })
          //   .catch(err => {
          //     // eslint-disable-next-line no-console
          //     console.log("error at dispatch signUpUser :", err);
          //   });

          return res.data;
        })
        .catch(err => {});
    }
  }
});
</script>
