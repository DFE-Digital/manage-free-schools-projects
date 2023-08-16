using Dfe.ManageFreeSchoolProjects.API.Contracts.Project;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

    public record CreateProjectCacheItem
    {
        public string School { get; set; }
        public ProjectRegion Region { get; set; }
        public ProjectLocalAuthority LocalAuthority { get; set; }
    }
}
