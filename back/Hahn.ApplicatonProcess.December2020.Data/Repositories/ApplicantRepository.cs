using System;
using System.Linq;
using Hahn.ApplicatonProcess.December2020.Data.Contexts;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
	    private readonly ApplicantContext _applicantContext;

	    public ApplicantRepository(ApplicantContext applicantContext)
	    {
		    _applicantContext = applicantContext;
	    }

      public Applicant GetById(int id)
      {
	      return _applicantContext.Applicants.FirstOrDefault(applicant => applicant.Id == id);
      }

      public Applicant GetByEmail(string email)
      {
	      return _applicantContext.Applicants.FirstOrDefault(applicant => applicant.EMailAddress == email);
      }

      public void Update(Applicant applicant)
      {
	      _applicantContext.Update(applicant);
	      _applicantContext.SaveChanges();
      }

      public Applicant Create(Applicant applicant)
      {
	      EntityEntry<Applicant> result = _applicantContext.Add(applicant);
	      _applicantContext.SaveChanges();
	      return result.Entity;
      }

      public void Delete(Applicant applicant)
      {
	      _applicantContext.Remove(applicant);
	      _applicantContext.SaveChanges();
      }
    }
}