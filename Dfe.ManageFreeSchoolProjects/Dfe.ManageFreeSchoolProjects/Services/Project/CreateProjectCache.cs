using System.Collections.Generic;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SchoolType = Dfe.ManageFreeSchoolProjects.API.Contracts.Project.SchoolType;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface ICreateProjectCache
    {
        public CreateProjectCacheItem Get();
        public void Delete();
        public void Update(CreateProjectCacheItem project);
    }

    public class CreateProjectCache : ICreateProjectCache
    {
        private const string Key = "CreateProject";
        private readonly ISession _session;

        public CreateProjectCache(ISession session)
        {
            _session = session;
        }

        public CreateProjectCacheItem Get()
        {
            var data = _session.GetString(Key);

            if (data == null)
            {
                return new CreateProjectCacheItem();
            }

            var result = JsonConvert.DeserializeObject<CreateProjectCacheItem>(data);

            if (result == null) 
            {
                return new CreateProjectCacheItem();
            }

            return result;
        }

        public void Delete()
        {
            _session.Remove(Key);
        }

        public void Update(CreateProjectCacheItem project) 
        {
            var json = JsonConvert.SerializeObject(project);

            _session.SetString(Key, json);
        }
    }

    public enum CreateProjectNavigation
    {
        Default,
        BackToCheckYourAnswers
    }

    public record CreateProjectCacheItem
    {
        public CreateProjectNavigation Navigation { get; set; }
        public string ProjectId { get; set; }
        public string SchoolName { get; set; }
        public SchoolPhase SchoolPhase { get; set; }
        public ProjectRegion Region { get; set; }
        public IDictionary<string, string> LocalAuthorities { get; set; }
        public string LocalAuthority { get; set; }
        public string LocalAuthorityCode { get; set; }
        public string EmailToNotify { get; set; }
        public SchoolType SchoolType { get; set; }
        public string TrustName { get; set; }
        public string TRN { get; set; }
        public string ConfirmTrust { get; set; }
        public FaithStatus FaithStatus { get; set; }
        public FaithType FaithType { get; set; }
        public string OtherFaithType { get; set; }
        public ClassType.SixthForm SixthForm { get; set; }
        public ClassType.Nursery Nursery { get; set; }
    }
}
