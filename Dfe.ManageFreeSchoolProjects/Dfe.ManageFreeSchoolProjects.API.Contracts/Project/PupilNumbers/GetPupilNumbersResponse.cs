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
        public Pre16PublishedAdmissionNumber Pre16PublishedAdmissionNumber { get; set; }
        public Post16PublishedAdmissionNumber Post16PublishedAdmissionNumber { get; set; }
        public Pre16CapacityBuildup Pre16CapacityBuildup { get; set; }
        public Post16CapacityBuildup Post16CapacityBuildup { get; set; }
    }

    public record CapacityWhenFullResponse : CapacityWhenFull
    {
        public int TotalCapacity { get; set; }
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
        public RecruitmentAndViabilityEntry Nursery { get; set; }
        public RecruitmentAndViabilityEntry ReceptionToYear6 { get; set; }
        public RecruitmentAndViabilityEntry Year7ToYear11 { get; set; }
        public RecruitmentAndViabilityEntry Year12ToYear14 { get; set; }
    }

    public record RecruitmentAndViabilityEntry
    {
        public int MinimumViableNumber { get; set; }
        public int ApplicationsReceived { get; set; }
    }

    public record Pre16PublishedAdmissionNumber
    {
        public int Nursery { get; set; }
        public int ReceptionToYear6 { get; set; }
        public int Year7 { get; set; }
        public int Year10 { get; set; }
        public int OtherPre16 { get; set; }
    }

    public record Post16PublishedAdmissionNumber
    {
        public int Year12 { get; set; }
        public int OtherPost16 { get; set; }
        public int Total { get; set; }
    }

    public record Pre16CapacityBuildup
    {
        public object Nursery { get; set; }
        public object Reception { get; set; }
        public object Year1 { get; set; }
        public object Year2 { get; set; }
        public object Year3 { get; set; }
        public object Year4 { get; set; }
        public object Year5 { get; set; }
        public object Year6 { get; set; }
        public object Year7 { get; set; }
        public object Year8 { get; set; }
        public object Year9 { get; set; }
        public object Year10 { get; set; }
        public object Year11 { get; set; }
    }

    public record Post16CapacityBuildup
    {
        public object Year12 { get; set; }
        public object Year13 { get; set; }
        public object Year14 { get; set; }
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
