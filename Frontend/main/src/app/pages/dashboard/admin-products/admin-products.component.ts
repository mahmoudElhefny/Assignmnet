import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/app/ViewModels/AddProductVM';
import { ProductService } from 'src/app/services/ProductService/product.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-products',
  templateUrl: './admin-products.component.html',
  styleUrls: ['./admin-products.component.scss']
})
export class AdminProductsComponent implements OnInit{
  products: IProduct[] = [];

  constructor(private productService: ProductService,private router:Router) {


  }
  ngOnInit(): void {
    this.getProducts()
  }
  getProducts() {
    this.productService.getAll().subscribe((Response:any) => {
      this.products = Response.data;
    });
  }
  editProduct(prdId:number) {
   this.router.navigate(['/edit-product',prdId])
  }
  AddProduct(){
    this.router.navigateByUrl('/AddProduct')
  }

  deleteProduct(prdId: number) {
    console.log('id='+prdId)
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.productService.delete(prdId).subscribe(response=>{
          if(!response.isSucceded){
            swalWithBootstrapButtons.fire({         
              title: "Something went wront!",
              text: "please try again.",
              icon: "error"
            });
          }
          else{
            this.getProducts();
            swalWithBootstrapButtons.fire({
              title: "Deleted!",
              text: "Your file has been deleted.",
              icon: "success"
              
            });
            
          }
        })
        
       
      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          text: "Your imaginary file is safe :)",
          icon: "error"
        });
      }
    });
  }

}
