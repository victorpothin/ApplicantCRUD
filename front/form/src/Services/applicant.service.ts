import { inject } from 'aurelia-dependency-injection';
import {Config, Rest} from 'aurelia-api';


@inject(Config)
export class ApplicantService{

  public api : Rest;
  constructor(config : Config){
    this.api = config.getEndpoint('applicants');
  }


  public SendApplicant(body){
    return this.api.post("/applicants", body);
  } 

}
