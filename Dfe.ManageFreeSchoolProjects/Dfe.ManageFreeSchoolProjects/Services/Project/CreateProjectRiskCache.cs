using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Risk;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dfe.ManageFreeSchoolProjects.Services.Project
{
    public interface ICreateProjectRiskCache
    {
        public CreateRiskCacheItem Get();
        public void Delete();
        public void Update(CreateRiskCacheItem item);
    }

    public class CreateProjectRiskCache : ICreateProjectRiskCache
    {
        private const string Key = "CreateProjectRisk";
        private readonly ISession _session;

        public CreateProjectRiskCache(ISession session)
        {
            _session = session;
        }

        public CreateRiskCacheItem Get()
        {
            var data = _session.GetString(Key);

            if (data == null)
            {
                return new CreateRiskCacheItem();
            }

            var result = JsonConvert.DeserializeObject<CreateRiskCacheItem>(data);

            if (result == null)
            {
                return new CreateRiskCacheItem();
            }

            return result;
        }

        public void Delete()
        {
            _session.Remove(Key);
        }

        public void Update(CreateRiskCacheItem item)
        {
            var json = JsonConvert.SerializeObject(item);

            _session.SetString(Key, json);
        }
    }

    public class CreateRiskCacheItem
    {
        public RiskEntryModel GovernanceAndSuitability { get; set; }

        public RiskEntryModel Education { get; set; }

        public RiskEntryModel Finance { get; set; }

        public RiskEntryModel Overall { get; set; }

        public string RiskAppraisalFormSharepointLink { get; set; }
    }

    public class RiskEntryModel
    {
        public ProjectRiskRating RiskRating { get; set; }
        public string Summary { get; set; }
    }
}
