import { PromptSuccess } from './../../prompts/prompt-success';
import { PromptError } from './../../prompts/prompt-error';
import { ApplicantService } from './../../Services/applicant.service';
import { autoinject } from 'aurelia-framework';
import { Countries } from 'aurelia-ui-framework';
import {
  ValidationControllerFactory,
  ValidationController,
  ValidationRules,
  validateTrigger
} from 'aurelia-validation';
import { BootstrapFormRenderer } from '../renderer/bootstrap-form-renderer';
import { DialogService } from 'aurelia-dialog';
import { Prompt } from './../../prompts/prompt'
import {Router} from 'aurelia-router';



@autoinject
export class ApplicantForm {

  public countries: any;
  public controller: any;
  public name: string;
  public familyName: string;
  public address: string;
  public country: string;
  public eMailAddress: string;
  public age: number;
  public hired: boolean;
  public service: ApplicantService;
  public isValid: boolean;
  public dialogService;
  public router : Router;

  constructor(controllerFactory: ValidationControllerFactory, applicantService: ApplicantService, dialogService: DialogService, router : Router) {
    this.countries = Countries.list.map(country => country.name);
    this.controller = controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.service = applicantService;
    this.dialogService = dialogService;
    this.router = router;

    ValidationRules
      .ensure("name").required().minLength(5)
      .ensure("familyName").required().minLength(5)
      .ensure("address").required().minLength(10)
      .ensure("country").required()
      .ensure("eMailAddress").required().email()
      .ensure("age").required().between(20, 60)
      .on(ApplicantForm);

  }

  submit() {
    this.controller.validate().then(result => {
      if (result.valid) {
        var request = {
          Name: this.name,
          FamilyName: this.familyName,
          Address: this.address,
          CountryOfOrigin: this.country,
          EMailAddress: this.eMailAddress,
          Age: this.age,
          Hired: this.hired
        }
        this.service.SendApplicant(request)
          .then(res => {
            this.router.navigateToRoute('Success');
            this.dialogService.
              open({ viewModel: PromptSuccess, model: "Registration completed successfully!" }).then(openDialogResult => {
                setTimeout(() => {
                  openDialogResult.controller.cancel()
                }, 2000);
                return openDialogResult.closeResult;
              }).then((response) => {
              });
          })
          .catch(res => {
            this.dialogService.
              open({ viewModel: PromptError, model: res }).then(openDialogResult => {
                setTimeout(() => {
                  openDialogResult.controller.cancel()
                }, 3000);
                return openDialogResult.closeResult;
              }).then((response) => {
              });
          })
      }
    });
  }

  clear() {
    this.dialogService.
      open({ viewModel: Prompt }).then(openDialogResult => {
        return openDialogResult.closeResult;
      }).then((response) => {
        if (!response.wasCancelled) {
          this.address = null;
          this.age = null;
          this.eMailAddress = null;
          this.name = null;
          this.familyName = null;
          this.hired = null;
          this.country = null;
          this.controller.reset();
        }
      });
  }
}
