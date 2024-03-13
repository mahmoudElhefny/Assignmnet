import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IAddProduct, Icategory } from 'src/app/ViewModels/AddProductVM';
import { ProductService } from 'src/app/services/ProductService/product.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})

export class AddProductComponent implements OnInit{
  Product:IAddProduct|null={image:null!,category_Id:0,price:0,name:'',minimumQuantity:0,discountRate:0}
  progress: number=0;
  message: string="";
  private selectedFile: File | null = null;
  private selectedFileName: string | null = null;
  AddPrdFrm:FormGroup;
  categories:Icategory[]
  errorMessages:any[];
  //fbv:any
  // ecommerce card
 // productcards: IAddProduct
constructor(private prdserv:ProductService,private router:Router,
  private FB:FormBuilder) {
   this.AddPrdFrm=FB.group({
        name:['',Validators.required],       
        price:['',Validators.required],
        image:[null,Validators.required],
        minimumQuantity:['',Validators.required], 
        discountRate:['',Validators.required],     
        category_Id:['',Validators.required],
       })
}

  ngOnInit(): void {
 //this.fbv=this.AddPrdFrm.controls
    this.categories=[
      {id:1,name:'cat1'},
      {id:2,name:'cat2'},
      {id:3,name:'cat3'},
      {id:4,name:'cat4'},
    ]
  }
  get fbv(){
    return this.AddPrdFrm.controls;
  }
AddProducts(){ 
  if (this.AddPrdFrm.valid) {
    
    const productData: IAddProduct = this.AddPrdFrm.value;
    const formData = new FormData();
    formData.append('name', productData.name);
    formData.append('price', productData.price.toString());
    formData.append('discountRate', productData.discountRate.toString());
    formData.append('minimumQuantity', productData.minimumQuantity.toString());
    formData.append('category_Id', productData.category_Id.toString());
    formData.append('image', productData.image!);
    
    this.prdserv.AddProduct(formData).subscribe(
      response=> { 
        if(response.isSucceded){ 
          this.Toast.fire({
           // toast:true,
            icon: "success",
            title: "Product addedd successfully"
          }).then(() => {
            this.router.navigateByUrl('/dashboard');
          });
         
         // this.AddPrdFrm.reset();
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
        title:"Something went wront plz try again"
       })
      }
    );
  }

}
onFileChange(event: any): void {
  const file = event.target.files[0];
  this.AddPrdFrm.patchValue({
    image: file
  });
  this.AddPrdFrm.get('image')!.updateValueAndValidity();
}
 Toast = Swal.mixin({
  toast: true,
  position: "top",
  showConfirmButton: false,
  timer: 1000,
  timerProgressBar: true,
  didOpen: (toast) => {
    toast.onmouseenter = Swal.stopTimer;
    toast.onmouseleave = Swal.resumeTimer;
  }
});
}
