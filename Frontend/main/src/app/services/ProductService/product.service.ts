import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, retry, throwError } from 'rxjs';
import {IAddProduct,IProduct} from'../../ViewModels/AddProductVM';
import { environment } from 'src/environments/environment.prod';
import { ApiResponse } from 'src/app/ViewModels/Response';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  //httpOpion:any;
  private baseUrl = 'https://localhost:7269/api/Product';
  constructor(private httpCLient:HttpClient) {
    // this.httpOpion={
    //   headers:new HttpHeaders({'Content-Type': 'multipart/form-data' })
    //  }
    // let token=localStorage.getItem('token')
    // this.httpOpion={
    //   headers:new HttpHeaders({
    //     'Content-Type':'application/json',
    //     Authorization: 'bearer'+token
    //   })
    // }
   }

   get(id:number):Observable<ApiResponse<IProduct>>{
    return this.httpCLient.get<ApiResponse<IProduct>>(`${this.baseUrl}/GetProductById?id=${id}`).pipe(
     // retry(2),
      catchError(this.handleError)
    )
   }
   getAll():Observable<ApiResponse<IProduct[]>>{
    return this.httpCLient.get<ApiResponse<IProduct[]>>(`${this.baseUrl}/GetAllProducts`).pipe(
     // retry(3),
      catchError(this.handleError)
     )
   }
 
AddProduct(prd: FormData):Observable<ApiResponse<IAddProduct>>{
  
  return this.httpCLient.post<ApiResponse<IAddProduct>>(`${this.baseUrl}/AddProduct`,prd).pipe(
    //retry(3),
    catchError(this.handleError)
   )
}
EditProduct(prdId:number,prd: FormData):Observable<ApiResponse<IAddProduct>>{
  
  return this.httpCLient.post<ApiResponse<IAddProduct>>(`${this.baseUrl}/UpdateProduct?id=${prdId}`,prd).pipe(
   // retry(3),
    catchError(this.handleError)
   )
}
delete(prdId:number):Observable<ApiResponse<boolean>>{
return this.httpCLient.delete<ApiResponse<boolean>>(`${this.baseUrl}/Delete?id=${prdId}`).pipe(
 // retry(2),
  catchError(this.handleError)
)
}

private handleError(error:HttpErrorResponse){
  if(error.status===0){
    console.error('an error occurred :',error.error);
  }
  else{
    console.error(`backend error ${error.status}, body was:`,error.error)
  }
  return throwError(
    ()=>new Error('error occured,please try again')
  )
}
}
