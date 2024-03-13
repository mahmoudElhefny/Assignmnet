const BaseURl:string='https://localhost:5165/api/'
const ImagesURL:string=`${BaseURl}/images`
export const environment = {
  production: true,
  BaseUrl:BaseURl,
  SubCategoriesImagesURL:`${ImagesURL}/subCategoryImages/`,
  ProductsImagesURL:`${ImagesURL}/ProductsImages/`,
  CompaniesImagesURL:`${ImagesURL}/CompaniesImages/`,
  url:"https://localhost:5165/api/DeliverCompany/"
};

