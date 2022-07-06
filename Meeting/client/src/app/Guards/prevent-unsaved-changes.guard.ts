import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CanEdit } from '../Interfaces/can-edit';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard<component extends CanEdit> implements CanDeactivate<unknown> {
  canDeactivate(
    component: component,
  ): boolean {
    if(component.editForm.dirty) {
      
      return confirm("Are you sure you want to continue?\n, any unsaved changed will be lost ")
    }
    return true;
  }
  
  
}
