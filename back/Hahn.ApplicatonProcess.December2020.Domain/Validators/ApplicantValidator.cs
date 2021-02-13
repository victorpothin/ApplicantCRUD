using System;
using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Repositories.Interfaces;

namespace Hahn.ApplicatonProcess.December2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly ICountryRepository _countryRepository;

        public ApplicantValidator(IApplicantRepository applicantRepository ,ICountryRepository countryRepository)
        {
            _applicantRepository = applicantRepository;
            _countryRepository = countryRepository;
            RuleFor(applicant => applicant.Id)
                .Must(ApplicantValid).WithMessage("Applicant Id not exists!");

            RuleFor(applicant => applicant.Name)
                .MinimumLength(5);

            RuleFor(applicant => applicant.FamilyName)
                .MinimumLength(5);

            RuleFor(applicant => applicant.Address)
                .MinimumLength(10);

            RuleFor(applicant => applicant.CountryOfOrigin)
                .Must(CountryValid).WithMessage("Country not valid!");

            RuleFor(applicant => applicant.EMailAddress)
                .EmailAddress()
                .Must(VerifyDuplicity).WithMessage("E-Mail already exists!");

            RuleFor(applicant => applicant.Age)
                .ExclusiveBetween(20,60);

            RuleFor(applicant => applicant.Hired)
                .NotNull();
        }

        protected bool CountryValid(string country)
        {
            return _countryRepository.SearchCountryValid(country);
        }

        protected bool VerifyDuplicity(string email)
        {
            Applicant applicant = _applicantRepository.GetByEmail(email);
            return (applicant == null);
        }

        protected bool ApplicantValid(int? id)
        {
            if (id == null)
                return true;
            Applicant applicant = _applicantRepository.GetById(id.Value);
            return applicant != null;
        }
    }
}
