import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";
export default {
  install(Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl(`${Vue.prototype.$http.defaults.baseURL}/user-hub`)
      .configureLogging(LogLevel.Information)
      .build();

    const userHub = new Vue();

    Vue.prototype.$userHub = userHub;

    connection.on("AddUserEvent", userId => {
      userHub.$emit("user-added-event", userId);
    });

    // connection.on("LogOutUserEvent", userId => {
    //   userHub.$emit("user-loged-out-event", { userId });
    // });

    // if connection closed, reopen it
    let startedPromise = null;
    function start() {
      startedPromise = connection.start().catch(err => {
        return new Promise((resolve, reject) =>
          setTimeout(
            () =>
              start()
                .then(resolve)
                .catch(reject),
            5000
          )
        );
      });
      return startedPromise;
    }

    connection.onclose(() => start());

    start();
  }
};
