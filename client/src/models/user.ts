export class selectedUserCls {
  public id: number = 0;
  public userName: string = "";
  public phone: string = "";
  public icon: string = "";
  public color: string = "green";

  constructor() {
    this.InitSelectedUser();
  }

  InitSelectedUser() {
    this.phone = "";
    this.userName = "";
    this.icon = "mdi-account";
    this.color = "white";
  }
}

export class currentUserCls extends selectedUserCls {
  public firstName: string = "";
  public lastName: string = "";
  public age: number = 0;
  public code: number = 0;
  public token: string = "";

  constructor() {
    super();
    this.InitCurrentUser();
  }

  InitCurrentUser() {
    this.firstName = "";
    this.lastName = "";
    this.age = 0;
    this.code = 0;
  }
}

// export default class userCls {
//   public static userId: number = 0;

//   public id: number = 0;
//   public userName: string;
//   public firstName: string;
//   public lastName: string;
//   public phone: string;
//   public age: number;
//   public icon: string;
//   public code: number;

//   get UserId(): number {
//     return this.id;
//   }

//   constructor() {
//     this.id = 0;
//     this.firstName = "";
//     this.lastName = "";
//     this.phone = "0545898964";
//     this.userName = "";
//     this.age = 0;
//     this.icon = "mdi-account";
//     this.code = 0;
//   }

//   ClearData() {
//     this.firstName = "";
//     this.lastName = "";
//     this.phone = "0545898964";
//     this.userName = "";
//     this.age = 0;
//     this.icon = "mdi-account";
//     this.code = 0;
//   }
// }
