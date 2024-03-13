export interface ApiResponse<T>{
    data:T
    isSucceded:boolean
    errors:string[]
}