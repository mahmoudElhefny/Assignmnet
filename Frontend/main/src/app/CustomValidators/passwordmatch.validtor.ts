//cross feild custom validators
import { AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";
export const passwordmatch:ValidatorFn=
  (control:AbstractControl):ValidationErrors|null=>{
    //return (control:AbstractControl):ValidationErrors|null=>{
      let passControl=control.get('password');
      let confirmpassControl=control.get('confirmPassword');
      if(!passControl||!confirmpassControl||!passControl.value||!confirmpassControl.value)
      return null;
      let valErr={'UnmatchedPassword':{'pass':passControl?.value,'confirm':confirmpassControl.value}}
      return (passControl?.value==confirmpassControl?.value)?null:valErr;
    }
  
//}