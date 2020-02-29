
<template>
  <v-form v-model="valid">
    <span>Sign up</span>

    <v-container>
      <v-row>
        <v-col cols="12" sm="6" md="4">
          <v-text-field
            v-model="userSign.userName"
            :rules="userNameRules"
            :counter="50"
            label="User name"
            required
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="6" md="4">
          <v-text-field
            v-model="userSign.firstName"
            :rules="nameRules"
            :counter="10"
            label="First name"
            required
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="6" md="4">
          <v-text-field
            v-model="userSign.lastName"
            :rules="nameRules"
            :counter="10"
            label="Last name"
            required
          ></v-text-field>
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" sm="6" md="4">
          <v-text-field
            v-model="userSign.phone"
            single-line
            label="Phone"
            required
            :rules="phoneRules"
          />
        </v-col>

        <v-col cols="12" sm="6" md="4">
          <v-text-field
            v-model="userSign.age"
            single-line
            type="number"
            label="Age"
            required
            :rules="ageRules"
          />
        </v-col>
        <v-col cols="12" sm="6" md="4">
          <v-text-field v-model="userSign.icon" single-line label="Icon" required />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="12" sm="6" md="6">
          <v-btn class="mr-4" @click="submit">submit</v-btn>
        </v-col>
        <v-col cols="12" sm="6" md="6">
          <v-btn @click="clear">clear</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-form>
</template>

<script lang="ts">
import Vue from "vue";
import { mapGetters, mapMutations, Store } from "vuex";
import { selectedUserCls, currentUserCls } from "../models/user";
import store from "../store";

export default Vue.extend({
  name: "SignUp",

  data: () => ({
    userSign: new currentUserCls(),

    valid: false,

    userNameRules: [
      (v: any) => !!v || "User name is required",
      (v: any) => v.length <= 50 || "User name must be less than 50 characters"
    ],
    nameRules: [
      (v: any) => !!v || "Name is required",
      (v: any) => v.length <= 50 || "Name must be less than 50 characters",
      (v: any) => /^([^0-9]*)$/.test(v) || "Name cannot include numbers"
    ],
    phoneRules: [
      (v: any) => !!v || "Phone is required",
      (v: any) =>
        /[0][5][0|1|2|4|3|5|8][0-9]{7}/.test(v) || "Phone must be valid"
    ],
    ageRules: [
      (v: any) => !!v || "Age is required",
      (v: any) => v > 18 || "Age must above 18"
    ]
  }),
  methods: {
    submit() {
      // eslint-disable-next-line no-console
      console.log("this.userSign is: ", this.userSign);

      //this.$store.commit("addUser", this.userSign);
      this.$store
        .dispatch("signUpUser", this.userSign)
        .then(res => {
          this.$store.commit("setUser", res.data);
          this.$store.commit("SetUserToken", res.data.token);

          this.$router.push("/Home");
        })
        .catch(err => {
          // eslint-disable-next-line no-console
          console.log("error at dispatch signUpUser :", err);
        });
    },
    clear() {
      this.valid = false;
      this.userSign.InitCurrentUser();
    }
  },
  beforeCreate() {
    //Console.log("beforeCreate");
  },
  beforeDestroy() {
    //Console.log("beforeDestroy");
  }
});
</script>
