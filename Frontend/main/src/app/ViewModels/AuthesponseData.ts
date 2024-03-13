export interface AuthesponseData{
    token:string;
    expiration:Date;
    roles:string[];
    message:string
    isAuthenticated:boolean
    username:string
    refreshToken:string
    RefreshTokenExpiration:Date
  }

  export interface errorMessag{
    errorMessag:string
  }
