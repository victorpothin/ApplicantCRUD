using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Repositories.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Validators;
using Hahn.ApplicatonProcess.December2020.Dto.Requests;
using Hahn.ApplicatonProcess.December2020.Dto.Responses;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.String;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger _logger;

        public ApplicantService(IApplicantRepository applicantRepository, ICountryRepository countryRepository, ILogger<ApplicantService> logger)
        {
            _applicantRepository = applicantRepository;
            _countryRepository = countryRepository;
            _logger = logger;
        }

        public Response<Applicant> Validate(Applicant applicant)
        {
            
            ApplicantValidator validator = new ApplicantValidator(_applicantRepository, _countryRepository);
            ValidationResult result = validator.Validate(applicant);
            Response<Applicant> response = new Response<Applicant>()
            {
                Data = applicant,
                Succeeded = result.IsValid,
                Errors = FormatErrors(result.Errors)
            };
            return response;
        }

        private string FormatErrors(IList<ValidationFailure> errors)
        {
            IEnumerable<string> errorsMessages = errors.Select(error => error.ErrorMessage);
            return Join(", ", errorsMessages);
        }

        public Response<Applicant> GetById(int id)
        {
            var applicant = _applicantRepository.GetById(id);
            var response =  new Response<Applicant>()
            {
                Succeeded = (applicant != null),
                Data = applicant
            };
            LogErrors("get by Id", response);
            return response;
        }

        public Response<Applicant> Edit(EditApplicantRequest request)
        {
            Applicant applicant = new Applicant
            {
                Id = request.Id,
                Address = request.Address,
                Age = request.Age,
                Name = request.Name,
                FamilyName = request.FamilyName,
                Hired = request.Hired,
                CountryOfOrigin = request.CountryOfOrigin,
                EMailAddress = request.EMailAddress
            };
            var response = Validate(applicant);
            LogErrors("Update", response);
            if (response.Succeeded)
                _applicantRepository.Update(applicant);
            return response;
        }

        public Response<Applicant> Create(NewApplicantRequest request)
        {
            Applicant applicant = new Applicant
            {
                Address = request.Address,
                Age = request.Age,
                Name = request.Name,
                FamilyName = request.FamilyName,
                Hired = request.Hired,
                CountryOfOrigin = request.CountryOfOrigin,
                EMailAddress = request.EMailAddress
            };
            Response<Applicant> validity = Validate(applicant);
            LogErrors("Create", validity);
            return new Response<Applicant>()
            {
                Succeeded = validity.Succeeded,
                Data = validity.Succeeded ? _applicantRepository.Create(applicant) : null,
                Errors = validity.Errors
            };
        }

        public Response<Applicant> Delete(int id)
        {
            Response<Applicant> response = GetById(id);
            if (!response.Succeeded)
            {
                response.Errors = "Applicant not exist";
                return response;
            }
            _applicantRepository.Delete(response.Data);
            
            LogErrors("Delete", response);
            return response;
        }

        private void LogErrors<T>(string action , Response<T> response)
        {
            if(!response.Succeeded)
                _logger.LogError($"Error on action:{action}, {JsonConvert.SerializeObject(response)}");
        }
    }
}