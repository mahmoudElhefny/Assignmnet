import { Component, ViewEncapsulation, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import {
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexLegend,
  ApexStroke,
  ApexTooltip,
  ApexAxisChartSeries,
  ApexXAxis,
  ApexYAxis,
  ApexGrid,
  ApexPlotOptions,
  ApexFill,
  ApexMarkers,
  ApexResponsive,
} from 'ng-apexcharts';
import { IProduct } from 'src/app/ViewModels/AddProductVM';
import { ProductService } from 'src/app/services/ProductService/product.service';

interface month {
  value: string;
  viewValue: string;
}

// export interface salesOverviewChart {
//   series: ApexAxisChartSeries;
//   chart: ApexChart;
//   dataLabels: ApexDataLabels;
//   plotOptions: ApexPlotOptions;
//   yaxis: ApexYAxis;
//   xaxis: ApexXAxis;
//   fill: ApexFill;
//   tooltip: ApexTooltip;
//   stroke: ApexStroke;
//   legend: ApexLegend;
//   grid: ApexGrid;
//   marker: ApexMarkers;
// }


export interface productsData {
  id: number;
  imagePath: string;
  uname: string;
  position: string;
  productName: string;
  budget: number;
  priority: string;
}

// ecommerce card


const ELEMENT_DATA: productsData[] = [
 
];

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class AppDashboardComponent {
  @ViewChild('chart') chart: ChartComponent = Object.create(null);

 // public salesOverviewChart!: Partial<salesOverviewChart> | any;
 
  displayedColumns: string[] = ['assigned', 'name', 'priority', 'budget'];
  dataSource = ELEMENT_DATA;

  months: month[] = [
    { value: 'mar', viewValue: 'March 2023' },
    { value: 'apr', viewValue: 'April 2023' },
    { value: 'june', viewValue: 'June 2023' },
  ];
  AddPrdFrm:FormGroup;
  // ecommerce card
  productcards: IProduct[] =[]
  
 // private catgServ:CategoryService,
  constructor(private prdserv:ProductService,private router:Router,
    private FB:FormBuilder,) {   
       this.prdserv.getAll().subscribe(products=>{this.productcards=products.data
        console.log(products.data)
        console.log('fff',this.productcards)
      })
     
       
  }
  
  getImageUrl(imagePath:string){
    
    return `https://localhost:7269${imagePath}`;
  }

  AddNewProduct(){
    this.router.navigateByUrl('/addproduct')
  }
}
