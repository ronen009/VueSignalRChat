/* eslint-disable no-console */
import Vue from "vue";
import Vuex, { Module } from "vuex";
import axios from "axios";
import { selectedUserCls, currentUserCls } from "../models/user";
import loginCls from "../models/loginCls";

class userStoreState {
  userList: Array<selectedUserCls> = [];
  currentUser: currentUserCls | undefined;
  selectedUser: selectedUserCls | undefined;
}

Vue.use(Vuex, axios);

export const userModule: Module<any, any> = {
  state: new userStoreState(),

  getters: {
    getUserList(iState: userStoreState) {
      return iState.userList;
    },
    getCurrentUser(iState: userStoreState) {
      return iState.currentUser;
    },
    getSelectedUser(iState: userStoreState) {
      return iState.selectedUser;
    },
    getCurrentUserToken(iState: userStoreState) {
      if (iState == null || iState.currentUser == null) {
        return "Hello";
      } else {
        console.log("token is : ", iState.currentUser.token);
        return iState.currentUser.token;
      }
    }
  },
  mutations: {
    addUser(iState: userStoreState, iUser: selectedUserCls) {
      console.log("addUser user is  :", iUser);

      let indexToChange = iState.userList.findIndex(x => x.id == iUser.id);

      if (indexToChange > -1) {
        iState.userList[indexToChange].color = "green";
      } else {
        iUser.color = "orange";
        iState.userList.push(iUser);
      }

      console.log("mutations addUser iState.userList : ", iState.userList);
    },
    setUser(iState: userStoreState, iUser: currentUserCls) {
      iState.currentUser = iUser;
    },
    setSelectedUser(iState: userStoreState, iUserId: number) {
      iState.selectedUser = iState.userList.find(x => x.id == iUserId);
    },
    setUserList(iState: userStoreState, iUserList: Array<selectedUserCls>) {
      iState.userList = new Array<selectedUserCls>();

      iUserList.forEach(element => {
        let objToPush = new selectedUserCls();
        objToPush.icon = element.icon;
        objToPush.id = element.id;
        objToPush.phone = element.phone;
        objToPush.userName = element.userName;
        objToPush.color = "pink";

        iState.userList.push(objToPush);
      });
    },
    setSmsCode(iState: userStoreState, iCode: number) {
      iState.currentUser = new currentUserCls();
      iState.currentUser.code = iCode;
    },
    createCurrentUser(iState: userStoreState, iUser: currentUserCls) {
      iState.currentUser = new currentUserCls();

      iState.currentUser.age = iUser.age;
      iState.currentUser.firstName = iUser.firstName;
      iState.currentUser.icon = iUser.icon;
      iState.currentUser.lastName = iUser.lastName;
      iState.currentUser.firstName = iUser.firstName;
      iState.currentUser.phone = iUser.phone;
    },
    logOutUser(iState: userStoreState, iUserId: number) {
      let indexToChange = iState.userList.findIndex(x => x.id == iUserId);
      iState.userList[indexToChange].color = "pink";
    }
  },
  actions: {
    getUserList() {
      axios
        .get(this.state.apiPrefix + "User/GetFullUserList", {
          headers: this.state.headerOptions
        })
        .then(userList => {
          this.commit("setUserList", userList.data);
        })
        .catch(err => {
          console.log("getUserList failed: ", err);
        });
    },
    signUpUser({ commit }, iUser: currentUserCls) {
      axios
        .post(this.state.apiPrefix + "User/CreateUser", iUser)
        .then(createdUser => this.commit("addUser", createdUser.data))
        .catch(err => {
          console.log("getUserList failed: ", err);
        });
    }
  }
};
