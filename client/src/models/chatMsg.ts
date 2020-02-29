export default class chatMsg {
  public toUserId: number | undefined;
  public fromUserId: number | undefined;
  public sentDate: Date = new Date();
  public msg: string = "";
}
