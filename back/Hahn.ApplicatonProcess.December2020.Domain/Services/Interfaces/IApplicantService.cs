using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Dto.Requests;
using Hahn.ApplicatonProcess.December2020.Dto.Responses;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces
{
    public interface IApplicantService
    {
        Response<Applicant> GetById(int id);
        Response<Applicant> Edit(EditApplicantRequest request);
        Response<Applicant> Create(NewApplicantRequest request);
        Response<Applicant> Delete(int id);
    }
}