import Vue from "vue";
import Vuex, { Module } from "vuex";
import axios from "axios";
import chatMsg from "../models/chatMsg";
import store from "@/store";

class chatMsgStoreState {
  chatMsgList: Array<chatMsg> = [];
  currentMsg: string = "";
}

Vue.use(Vuex, axios);

export const chatHistoryModule: Module<any, any> = {
  state: new chatMsgStoreState(),

  getters: {
    getChatHistory(iState: chatMsgStoreState) {
      return iState.chatMsgList;
      // return iState.chatMsgList.sort(
      //   (a, b) => a.sentDate.getDate() - b.sentDate.getDate()
      // );
    }
  },
  mutations: {
    addMsg(iState: chatMsgStoreState, iMsg: chatMsg) {
      iState.chatMsgList.push(iMsg);
    },
    SetMsgHistory(iState: chatMsgStoreState, iMsgList: Array<chatMsg>) {
      console.log(
        "SetMsgHistory - iMsg is : ",
        iMsgList,
        " * list is : ",
        iState.chatMsgList
      );

      iState.chatMsgList = new Array<chatMsg>();

      iMsgList.forEach(element => {
        let objToPush = new chatMsg();

        objToPush.msg = element.msg;
        objToPush.sentDate = element.sentDate;
        objToPush.toUserId = element.toUserId;
        objToPush.fromUserId = element.fromUserId;

        iState.chatMsgList.push(objToPush);
      });
    },
    AddMsgToMsgList(iState: chatMsgStoreState, iMsg: chatMsg) {
      debugger;

      if (iState.chatMsgList == null) {
        iState.chatMsgList = new Array<chatMsg>();
      }

      iState.chatMsgList.push(iMsg);

      iState.currentMsg = "";
    }
  },
  actions: {
    GetMsgHistory({ commit }, iSelectedUser: number) {
      let axiosConfig = {
        headers: this.state.headerOptions
      };

      let id = iSelectedUser;
      axios
        .get(this.state.apiPrefix + "Msg/GetMsgHistory", {
          params: { id },
          headers: this.state.headerOptions
        })
        .then(msgList => {
          console.log("GetMsgHistory : ", msgList);

          this.commit("SetMsgHistory", msgList.data);
        })
        .catch(err => {});
    },
    SendMsg({ commit }, iMsg: chatMsg) {
      console.log("actions -- option are : ", this.state.headerOptions);

      let axiosConfig = {
        headers: this.state.headerOptions
      };

      axios
        .post(this.state.apiPrefix + "Msg/SendMsg", iMsg, axiosConfig)
        .then(msgList => {
          //this.commit("AddMsgToMsgList", msgList.data);
        })
        .catch(err => {});
    }
  }
};
