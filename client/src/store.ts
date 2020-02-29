import Vue from "vue";
import Vuex, { Module } from "vuex";
import axios from "axios";
import { selectedUserCls, currentUserCls } from "../src/models/user";
import { userModule } from "../src/StoreModules/userStoreModule";
import { chatHistoryModule } from "../src/StoreModules/chatHistoryModule";

//axios.defaults.baseURL = "http://localhost:44376/api/";

export default new Vuex.Store({
  state: {
    apiPrefix: "https://localhost:44376/api/" as string,
    headerOptions: {
      Authorization: ""
    }
  },
  getters: {},
  mutations: {
    SetUserToken(state, iToken: string) {
      state.headerOptions = {
        //headers: {
        Authorization: iToken
        //}
      };
    }
  },
  actions: {},
  modules: {
    userModule: userModule,
    chatHistory: chatHistoryModule
  }
});
