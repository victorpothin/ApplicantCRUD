import { PLATFORM } from 'aurelia-pal';
import {Router, RouterConfiguration} from 'aurelia-router';


export class App {
  router: Router;

  constructor(){
  }


  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Applicants';
    config.map([
      {
        route: '',
        name: 'Register',
        moduleId: PLATFORM.moduleName('components/form/applicant-form.component'),
        title: 'Register Applicant'
      },
      {
        route: 'success',
        name: 'Success',
        moduleId: PLATFORM.moduleName('components/success/success-view.component'),
        title: 'Register Applicant Success'
      },
    ]);
    this.router = router;
  }
}
