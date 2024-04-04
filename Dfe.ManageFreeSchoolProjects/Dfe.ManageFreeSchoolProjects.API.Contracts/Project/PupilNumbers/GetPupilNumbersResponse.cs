using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers
{
    public record GetPupilNumbersResponse
    {
        public CapacityWhenFullResponse CapacityWhenFull { get; set; }
        public RecruitmentAndViability RecruitmentAndViability { get; set; }
        public Pre16PublishedAdmissionNumberResponse Pre16PublishedAdmissionNumber { get; set; }
        public Post16PublishedAdmissionNumberResponse Post16PublishedAdmissionNumber { get; set; }
        public Pre16CapacityBuildup Pre16CapacityBuildup { get; set; }
        public Post16CapacityBuildup Post16CapacityBuildup { get; set; }
    }

    public record CapacityWhenFullResponse : CapacityWhenFull
    {
        public int Total { get; set; }
    }

    public record CapacityWhenFull
    {
        public int Nursery { get; set; }
        public int ReceptionToYear6 { get; set; }
        public int Year7ToYear11 { get; set; }
        public int Year12ToYear14 { get; set; }
        public int SpecialistEducationNeeds { get; set; }
        public int AlternativeProvision { get; set; }
    }

    public record RecruitmentAndViability
    {
        public RecruitmentAndViabilityEntry ReceptionToYear6 { get; set; }
        public RecruitmentAndViabilityEntry Year7ToYear11 { get; set; }
        public RecruitmentAndViabilityEntry Year12ToYear14 { get; set; }
        public RecruitmentAndViabilityEntry Total { get; set; }
    }

    public record RecruitmentAndViabilityEntry
    {
        public int MinimumViableNumber { get; set; }
        public int ApplicationsReceived { get; set; }
    }

    public record Pre16PublishedAdmissionNumberResponse : Pre16PublishedAdmissionNumber
    {
        public int Total { get; set; }
    }

    public record Pre16PublishedAdmissionNumber
    {
        public int ReceptionToYear6 { get; set; }
        public int Year7 { get; set; }
        public int Year10 { get; set; }
        public int OtherPre16 { get; set; }
    }

    public record Post16PublishedAdmissionNumberResponse : Post16PublishedAdmissionNumber
    {
        public int Total { get; set; }
    }

    public record Post16PublishedAdmissionNumber
    {
        public int Year12 { get; set; }
        public int OtherPost16 { get; set; }
    }

    public record Pre16CapacityBuildup
    {
        public CapacityBuildupEntry Nursery { get; set; }
        public CapacityBuildupEntry Reception { get; set; }
        public CapacityBuildupEntry Year1 { get; set; }
        public CapacityBuildupEntry Year2 { get; set; }
        public CapacityBuildupEntry Year3 { get; set; }
        public CapacityBuildupEntry Year4 { get; set; }
        public CapacityBuildupEntry Year5 { get; set; }
        public CapacityBuildupEntry Year6 { get; set; }
        public CapacityBuildupEntry Year7 { get; set; }
        public CapacityBuildupEntry Year8 { get; set; }
        public CapacityBuildupEntry Year9 { get; set; }
        public CapacityBuildupEntry Year10 { get; set; }
        public CapacityBuildupEntry Year11 { get; set; }
        public CapacityBuildupEntry Total { get; set; }
    }

    public record Post16CapacityBuildup
    {
        public CapacityBuildupEntry Year12 { get; set; }
        public CapacityBuildupEntry Year13 { get; set; }
        public CapacityBuildupEntry Year14 { get; set; }
        public CapacityBuildupEntry Total { get; set; }
    }

    public record CapacityBuildupEntry
    {
        public int CurrentCapacity { get; set; }
        public int FirstYear { get; set; }
        public int SecondYear { get; set; }
        public int ThirdYear { get; set; }
        public int FourthYear { get; set; }
        public int FifthYear { get; set; }
        public int SixthYear { get; set;}
        public int SeventhYear { get; set; }
    }
}
