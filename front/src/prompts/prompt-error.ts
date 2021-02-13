import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class PromptError {

  public controller : DialogController;
  public message;
   constructor(controller : DialogController) {
      this.controller = controller;
      controller.settings.centerHorizontalOnly = true;
   }
   
   activate(message) {
      this.message = message;
   }
}
