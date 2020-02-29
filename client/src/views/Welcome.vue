<template>
  <v-container>
    <v-row>
      <v-card class="mx-auto" width="70%" outlined>
        <v-responsive :aspect-ratio="16/9">
          <template v-if="signUpChosen == true">
            <SignUp />
          </template>
          <template v-else>
            <Login />
          </template>
        </v-responsive>
      </v-card>
    </v-row>

    <v-row>
      <v-card class="mx-auto" width="70%" outlined>
        <span>https://houseofcat.io/tutorials/csharp/identity/createjwt</span>
      </v-card>
    </v-row>

    <v-row>
      <v-card class="mx-auto" width="70%" outlined>
        <v-card-actions class="card-actions">
          <v-btn
            raised
            color="deep-purple accent-4"
            class="white--text"
            @click="ChooseSignUp(true)"
          >Sign up</v-btn>
          <v-btn
            raised
            color="deep-purple accent-4"
            class="white--text"
            @click="ChooseSignUp(false)"
          >Login</v-btn>
        </v-card-actions>
      </v-card>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import SignUp from "../components/SignUp.vue";
import Login from "../components/LogIn.vue";

export default Vue.extend({
  name: "Welcome",

  data: () => ({
    signUpChosen: true
  }),
  methods: {
    ChooseSignUp(iIsSignUp: boolean) {
      this.signUpChosen = iIsSignUp;
    }
  },
  components: {
    SignUp,
    Login
  }
});
</script>


<style scoped>
.card-actions {
  position: absolute;
  bottom: 0;
}
</style>