using Hahn.ApplicatonProcess.December2020.Domain.Models;

namespace Hahn.ApplicatonProcess.December2020.Domain.Repositories.Interfaces
{
    public interface IApplicantRepository
    {
        Applicant GetById(int id);
        void Update(Applicant applicant);
        Applicant Create(Applicant applicant);
        void Delete(Applicant applicant);
        Applicant GetByEmail(string email);
    }
}