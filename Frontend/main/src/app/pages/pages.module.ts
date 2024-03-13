import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PagesRoutes } from './pages.routing.module';
import { MaterialModule } from '../material.module';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgApexchartsModule } from 'ng-apexcharts';
// icons
import { TablerIconsModule } from 'angular-tabler-icons';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { AppDashboardComponent } from './dashboard/dashboard.component';
import { AddProductComponent } from './dashboard/AddProduct/add-product/add-product.component';
import { AdminProductsComponent } from './dashboard/admin-products/admin-products.component';
import { EditProductComponent } from './dashboard/edit-product/edit-product.component';

@NgModule({
  declarations: [AppDashboardComponent,AddProductComponent,AdminProductsComponent,EditProductComponent],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    NgApexchartsModule,
    RouterModule.forChild(PagesRoutes),
    TablerIconsModule.pick(TablerIcons),
    ReactiveFormsModule
  ],
  exports: [TablerIconsModule],
})
export class PagesModule {}
