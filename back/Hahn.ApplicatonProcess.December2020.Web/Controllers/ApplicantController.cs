using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces;
using Hahn.ApplicatonProcess.December2020.Dto.Requests;
using Hahn.ApplicatonProcess.December2020.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("/applicants")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }
        /// <summary>
        /// Get a applicant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Response<Applicant>> Get([FromQuery] int? id)
        {
            if (id == null)
                return BadRequest("Id is null!");
            return Ok(_applicantService.GetById(id.Value));
        }
        
        /// <summary>
        /// Edit Applicant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Edit([FromBody] EditApplicantRequest request)
        {
            Response<Applicant> response = _applicantService.Edit(request);
            if(response.Succeeded)
                return NoContent();
            return BadRequest(response.Errors);
        }
        
        /// <summary>
        /// Create Applicant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create([FromBody] NewApplicantRequest request)
        {
            Response<Applicant> response = _applicantService.Create(request);
            if(response.Succeeded)
                return Created(this.Url.RouteUrl(response.Data.Id), response.Data.Id);
            return BadRequest(response.Errors);
        }
        
        /// <summary>
        /// Delete Applicant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete([FromQuery] int id)
        {
            Response<Applicant> response = _applicantService.Delete(id);
            if(response.Succeeded)
                return NoContent();
            return BadRequest(response.Errors);
        }
    }
}