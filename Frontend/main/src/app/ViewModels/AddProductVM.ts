

 export interface IAddProduct{
   image: string;
   name: string;
   price: number;
   minimumQuantity:number;
   discountRate:number
   category_Id:number
 }


export interface IProduct {
   id: number;
   image: string;
   imageUrl?:string
  // poductCode: string;
   name: string;
   price: number;
   minimumQuantity:number;
   discountRate:number
   category_Id:number
   category:Icategory
 }
 
 export interface Icategory{
  id:number;
  name:string
}