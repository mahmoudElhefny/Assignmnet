
export class userToken{
    constructor(private  _Roles:any[],private _token:string,private _tokenExpirationDate:Date=new Date()){}
    get token(){
        if(!this._tokenExpirationDate||this._tokenExpirationDate<new Date){
            return null
        }
        return this._token
    }
    get Roles(){
        if(!this._tokenExpirationDate||this._tokenExpirationDate<new Date){
            return null
        }
        return this._Roles
    }
}
