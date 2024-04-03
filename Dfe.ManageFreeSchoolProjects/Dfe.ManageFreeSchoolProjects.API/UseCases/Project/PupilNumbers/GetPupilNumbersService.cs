using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.API.Extensions;
using Dfe.ManageFreeSchoolProjects.Data;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public interface IGetPupilNumbersService
    {
        public Task<GetPupilNumbersResponse> Execute(string projectId);
    }

    public class GetPupilNumbersService : IGetPupilNumbersService
    {
        private readonly MfspContext _context;

        public GetPupilNumbersService(MfspContext context)
        {
            _context = context;
        }

        public async Task<GetPupilNumbersResponse> Execute(string projectId)
        {
            var kpi = await _context.Kpi.FirstOrDefaultAsync(kpi => kpi.ProjectStatusProjectId == projectId);

            var result = await _context.Po
                .Where(po => po.Rid == kpi.Rid)
                .Select(po => new GetPupilNumbersResponse
                {
                    CapacityWhenFull = new CapacityWhenFullResponse
                    {
                        Nursery = po.PupilNumbersAndCapacityNurseryUnder5s.ToInt(),
                        ReceptionToYear6 = po.PupilNumbersAndCapacityYrY6Capacity.ToInt(),
                        Year7ToYear11 = po.PupilNumbersAndCapacityY7Y11Capacity.ToInt(),
                        Year12ToYear14 = po.PupilNumbersAndCapacityY12Y14Post16Capacity.ToInt(),
                        SpecialistEducationNeeds = po.PupilNumbersAndCapacitySpecialistResourceProvisionSpecial.ToInt(),
                        AlternativeProvision = po.PupilNumbersAndCapacitySpecialistResourceProvisionAp.ToInt(),
                        Total = po.PupilNumbersAndCapacityTotalOfCapacityTotals.ToInt()
                    },
                    Pre16PublishedAdmissionNumber = new Pre16PublishedAdmissionNumberResponse()
                    {
                        ReceptionToYear6 = po.PupilNumbersAndCapacityYrPan.ToInt(),
                        Year7 = po.PupilNumbersAndCapacityY7Pan.ToInt(),
                        Year10 = po.PupilNumbersAndCapacityY10Pan.ToInt(),
                        OtherPre16 = po.PupilNumbersAndCapacityYOtherPanPre16.ToInt(),
                        Total = po.PupilNumbersAndCapacityTotalPanPre16.ToInt()
                    },
                    Post16PublishedAdmissionNumber = new Post16PublishedAdmissionNumberResponse()
                    {
                        Year12 = po.PupilNumbersAndCapacityY12Pan.ToInt(),
                        OtherPost16 = po.PupilNumbersAndCapacityYOtherPanPost16.ToInt(),
                        Total = po.PupilNumbersAndCapacityTotalPanPost16.ToInt()
                    },
                    Pre16CapacityBuildup = new Pre16CapacityBuildup
                    {
                        Reception = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA2ReceptionCurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB2ReceptionFirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC2ReceptionSecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD2ReceptionThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE2ReceptionFourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF2ReceptionFifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG2ReceptionSixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH2ReceptionSeventhYear.ToInt()
                        },
                        Nursery = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA1NurseryCurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB1NurseryFirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC1NurserySecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD1NurseryThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE1NurseryFourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF1NurseryFifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG1NurserySixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH1NurserySeventhYear.ToInt()
                        },
                        Year1 = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA3Year1CurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB3Year1FirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC3Year1SecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD3Year1ThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE3Year1FourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF3Year1FifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG3Year1SixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH3Year1SeventhYear.ToInt()
                        },
                        Year2 = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA4Year2CurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB4Year2FirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC4Year2SecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD4Year2ThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE4Year2FourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF4Year2FifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG4Year2SixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH4Year2SeventhYear.ToInt()
                        },
                        Year3 = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA5Year3CurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB5Year3FirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC5Year3SecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD5Year3ThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE5Year3FourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF5Year3FifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG5Year3SixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH5Year3SeventhYear.ToInt()
                        },
                        Year4 = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA6Year4CurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB6Year4FirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC6Year4SecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD6Year4ThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE6Year4FourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF6Year4FifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG6Year4SixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH6Year4SeventhYear.ToInt()
                        },
                        Year5 = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA7Year5CurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB7Year5FirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC7Year5SecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD7Year5ThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE7Year5FourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF7Year5FifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG7Year5SixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH7Year5SeventhYear.ToInt()
                        },
                        Year6 = new CapacityBuildupEntry()
                        {
                            CurrentCapacity = po.PupilNumbersAndCapacityCellA8Year6CurrentPupilNumbersIfAlreadyOpen.ToInt(),
                            FirstYear = po.PupilNumbersAndCapacityCellB8Year6FirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellC8Year6SecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellD8Year6ThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellE8Year6FourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellF8Year6FifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellG8Year6SixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellH8Year6SeventhYear.ToInt()
                        },
                        Total = new CapacityBuildupEntry()
                        {
                            FirstYear = po.PupilNumbersAndCapacityCellTotalBTotalFirstYear.ToInt(),
                            SecondYear = po.PupilNumbersAndCapacityCellTotalCTotalSecondYear.ToInt(),
                            ThirdYear = po.PupilNumbersAndCapacityCellTotalDTotalThirdYear.ToInt(),
                            FourthYear = po.PupilNumbersAndCapacityCellTotalETotalFourthYear.ToInt(),
                            FifthYear = po.PupilNumbersAndCapacityCellTotalFTotalFifthYear.ToInt(),
                            SixthYear = po.PupilNumbersAndCapacityCellTotalGTotalSixthYear.ToInt(),
                            SeventhYear = po.PupilNumbersAndCapacityCellTotalHTotalSeventhYear.ToInt()
                        }
                    },
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
