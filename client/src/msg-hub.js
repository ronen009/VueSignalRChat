import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";
export default {
  install(Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl(`${Vue.prototype.$http.defaults.baseURL}/msg-hub`)
      .configureLogging(LogLevel.Information)
      .build();

    const msgHub = new Vue();

    Vue.prototype.$msgHub = msgHub;

    connection.on("SentMsgEvent", (fromId, toId, msg) => {
      msgHub.$emit("msg-sent-event", fromId, toId, msg);
    });

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
