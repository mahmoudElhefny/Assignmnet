import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IAddProduct, IProduct, Icategory } from 'src/app/ViewModels/AddProductVM';
import { ProductService } from 'src/app/services/ProductService/product.service';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit{
  //Product:IAddProduct={id:0,image:null!,category_Id:0,price:0,name:'',ProductCode:'',minimumQuantity:0,discountRate:0}
  progress: number=0;
  srcImage:string
  message: string="";
  private selectedFile: File | null = null;
  private selectedFileName: string | null = null;
  prdId:number
  errorMessages:any[];
  EditPrdFrm:FormGroup;
  categories:Icategory[]
  products:IProduct={id: 0,
  name: '',
  price: 0,
  category_Id:0,
  discountRate: 0,
  minimumQuantity: 0,
  image: '',
  category: { id: 0, name: '' }
  };
  imageValue=false

  productcard: IAddProduct={
    name: '', price: 0,
    minimumQuantity: 0, discountRate: 0, image: '', category_Id: 0,
  }
constructor(private prdserv:ProductService,private router:Router,
  private FB:FormBuilder,private route: ActivatedRoute) {
    this.EditPrdFrm=FB.group({
      name:['',Validators.required],       
      price:['',Validators.required],
      image:[null,Validators.required],
      minimumQuantity:['',Validators.required], 
      discountRate:['',Validators.required],     
      category_Id:['',Validators.required],
     })  
}
  ngOnInit(): void {
    this.categories=[
      {id:2,name:'Cat1'},
      {id:3,name:'Cat2'},
      {id:4,name:'Cat3'},
      {id:5,name:'Cat4'},
    ]
     this.prdId=+this.route.snapshot.paramMap.get('id')!;
    if(this.prdId!=null)
    {
      this.prdserv.get(this.prdId).subscribe(response=>{
        if(response.isSucceded){      
          this.products=response.data
          this.srcImage=`https://localhost:44389/${this.products.image}`
          this.imageValue=true
          this.EditPrdFrm.patchValue({
            name: this.products.name,
            price: this.products.price,
            discountRate: this.products.discountRate,
            minimumQuantity: this.products.minimumQuantity,
            category_Id: this.products.category_Id,
            image: this.products.image
          });
          console.log(this.products)
        }
        else{    
          this.srcImage=""     
          Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "Something went wrong!",           
          });
        }
      })
    }
  }
  // get fbv(){
  //   return this.EditPrdFrm.controls;
  // }
  EditProduct(){ 
    console.log(this.EditPrdFrm.valid)
    if (this.EditPrdFrm.valid) {
      debugger
       const productData:any =this.EditPrdFrm.value
      console.log(this.EditPrdFrm.value)
      const formData = new FormData();     
      formData.append('name', productData.name);
      formData.append('price', productData.price.toString());
      formData.append('discountRate', productData.discountRate.toString());
      formData.append('minimumQuantity', productData.minimumQuantity.toString());
      formData.append('category_Id', productData.category_Id.toString());
      if(typeof(productData.image) === 'string'){
        formData.append('imageUrl', productData.image);
        
      }
      else{
        formData.append('image', productData.image);
      }
      
      
      this.prdserv.EditProduct(this.prdId,formData).subscribe(
        response=> { 
          if(response.isSucceded){ 
            this.Toast.fire({

              icon: "success",
              title: "Product addedd successfully"
            }).then(() => {
              this.router.navigateByUrl('/dashboard');
            });
          }
          else{
            response.errors.forEach(element => {
              this.errorMessages.push(element);
            });
            this.errorMessages = response.errors;
          }
        },
       
        (error: any) => {
         this.Toast.fire({
          icon:"error",
          title:"Somethin went wront plz try again"
         })
        }
      );
    }
  }
  onFileChange(event: any): void {
    const file = event.target.files[0];
    this.EditPrdFrm.patchValue({
      image: file
    });
  }
  Toast = Swal.mixin({
    toast: true,
    position: "top",
    showConfirmButton: false,
    timer: 1500,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.onmouseenter = Swal.stopTimer;
      toast.onmouseleave = Swal.resumeTimer;
    }
  });
}
