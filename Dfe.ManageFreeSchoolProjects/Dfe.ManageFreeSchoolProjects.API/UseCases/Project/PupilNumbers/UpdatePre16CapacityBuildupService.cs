using Azure.Core;
using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public class UpdatePre16CapacityBuildupService : IUpdatePupilNumbersSectionService
    {
        public void Execute(Po po, UpdatePupilNumbersRequest request)
        {
            if (request.Pre16CapacityBuildup == null)
            {
                return;
            }

            UpdateNursery(po, request.Pre16CapacityBuildup.Nursery);
            UpdateReception(po, request.Pre16CapacityBuildup.Reception);
            UpdateYear1(po, request.Pre16CapacityBuildup.Year1);
            UpdateYear2(po, request.Pre16CapacityBuildup.Year2);
            UpdateYear3(po, request.Pre16CapacityBuildup.Year3);
            UpdateYear4(po, request.Pre16CapacityBuildup.Year4);
            UpdateYear5(po, request.Pre16CapacityBuildup.Year5);
            UpdateYear6(po, request.Pre16CapacityBuildup.Year6);
            UpdateTotals(po, request);
        }

        private static void UpdateNursery(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA1NurseryCurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB1NurseryFirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC1NurserySecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD1NurseryThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE1NurseryFourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF1NurseryFifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG1NurserySixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH1NurserySeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateReception(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA2ReceptionCurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB2ReceptionFirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC2ReceptionSecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD2ReceptionThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE2ReceptionFourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF2ReceptionFifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG2ReceptionSixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH2ReceptionSeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear1(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA3Year1CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB3Year1FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC3Year1SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD3Year1ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE3Year1FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF3Year1FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG3Year1SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH3Year1SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear2(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA4Year2CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB4Year2FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC4Year2SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD4Year2ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE4Year2FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF4Year2FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG4Year2SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH4Year2SeventhYear = entry.SeventhYear.ToString();
        }
        private static void UpdateYear3(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA5Year3CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB5Year3FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC5Year3SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD5Year3ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE5Year3FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF5Year3FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG5Year3SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH5Year3SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear4(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA6Year4CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB6Year4FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC6Year4SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD6Year4ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE6Year4FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF6Year4FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG6Year4SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH6Year4SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear5(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA7Year5CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB7Year5FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC7Year5SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD7Year5ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE7Year5FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF7Year5FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG7Year5SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH7Year5SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear6(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA8Year6CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB8Year6FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC8Year6SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD8Year6ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE8Year6FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF8Year6FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG8Year6SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH8Year6SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateTotals(Po po, UpdatePupilNumbersRequest request)
        {
            int firstYearTotal = CalculateFirstYearTotal(request);
            int secondYearTotal = CalculateSecondYearTotal(request);
            int thirdYearTotal = CalculateThirdYearTotal(request);
            int fourthYearTotal = CalculateFourthYearTotal(request);
            int fifthYearTotal = CalculateFifthYearTotal(request);
            int sixthYearTotal = CalculateSixthYearTotal(request);

            po.PupilNumbersAndCapacityCellTotalBTotalFirstYear = firstYearTotal.ToString();
            po.PupilNumbersAndCapacityCellTotalCTotalSecondYear = secondYearTotal.ToString();
            po.PupilNumbersAndCapacityCellTotalDTotalThirdYear = thirdYearTotal.ToString();
            po.PupilNumbersAndCapacityCellTotalETotalFourthYear = fourthYearTotal.ToString();
            po.PupilNumbersAndCapacityCellTotalFTotalFifthYear = fifthYearTotal.ToString();
            po.PupilNumbersAndCapacityCellTotalGTotalSixthYear = sixthYearTotal.ToString();
        }

        private static int CalculateFirstYearTotal(UpdatePupilNumbersRequest request)
        {
            return request.Pre16CapacityBuildup.Nursery.FirstYear +
                request.Pre16CapacityBuildup.Reception.FirstYear +
                request.Pre16CapacityBuildup.Year1.FirstYear +
                request.Pre16CapacityBuildup.Year2.FirstYear +
                request.Pre16CapacityBuildup.Year3.FirstYear +
                request.Pre16CapacityBuildup.Year4.FirstYear +
                request.Pre16CapacityBuildup.Year5.FirstYear +
                request.Pre16CapacityBuildup.Year6.FirstYear;
        }

        private static int CalculateSecondYearTotal(UpdatePupilNumbersRequest request)
        {
            return request.Pre16CapacityBuildup.Nursery.SecondYear +
                request.Pre16CapacityBuildup.Reception.SecondYear +
                request.Pre16CapacityBuildup.Year1.SecondYear +
                request.Pre16CapacityBuildup.Year2.SecondYear +
                request.Pre16CapacityBuildup.Year3.SecondYear +
                request.Pre16CapacityBuildup.Year4.SecondYear +
                request.Pre16CapacityBuildup.Year5.SecondYear +
                request.Pre16CapacityBuildup.Year6.SecondYear;
        }

        private static int CalculateThirdYearTotal(UpdatePupilNumbersRequest request)
        {
            return request.Pre16CapacityBuildup.Nursery.ThirdYear +
                request.Pre16CapacityBuildup.Reception.ThirdYear +
                request.Pre16CapacityBuildup.Year1.ThirdYear +
                request.Pre16CapacityBuildup.Year2.ThirdYear +
                request.Pre16CapacityBuildup.Year3.ThirdYear +
                request.Pre16CapacityBuildup.Year4.ThirdYear +
                request.Pre16CapacityBuildup.Year5.ThirdYear +
                request.Pre16CapacityBuildup.Year6.ThirdYear;
        }

        private static int CalculateFourthYearTotal(UpdatePupilNumbersRequest request)
        {
            return request.Pre16CapacityBuildup.Nursery.FourthYear +
                request.Pre16CapacityBuildup.Reception.FourthYear +
                request.Pre16CapacityBuildup.Year1.FourthYear +
                request.Pre16CapacityBuildup.Year2.FourthYear +
                request.Pre16CapacityBuildup.Year3.FourthYear +
                request.Pre16CapacityBuildup.Year4.FourthYear +
                request.Pre16CapacityBuildup.Year5.FourthYear +
                request.Pre16CapacityBuildup.Year6.FourthYear;
        }

        private static int CalculateFifthYearTotal(UpdatePupilNumbersRequest request)
        {
            return request.Pre16CapacityBuildup.Nursery.FifthYear +
                request.Pre16CapacityBuildup.Reception.FifthYear +
                request.Pre16CapacityBuildup.Year1.FifthYear +
                request.Pre16CapacityBuildup.Year2.FifthYear +
                request.Pre16CapacityBuildup.Year3.FifthYear +
                request.Pre16CapacityBuildup.Year4.FifthYear +
                request.Pre16CapacityBuildup.Year5.FifthYear +
                request.Pre16CapacityBuildup.Year6.FifthYear;
        }

        private static int CalculateSixthYearTotal(UpdatePupilNumbersRequest request)
        {
            return request.Pre16CapacityBuildup.Nursery.SixthYear +
                request.Pre16CapacityBuildup.Reception.SixthYear +
                request.Pre16CapacityBuildup.Year1.SixthYear +
                request.Pre16CapacityBuildup.Year2.SixthYear +
                request.Pre16CapacityBuildup.Year3.SixthYear +
                request.Pre16CapacityBuildup.Year4.SixthYear +
                request.Pre16CapacityBuildup.Year5.SixthYear +
                request.Pre16CapacityBuildup.Year6.SixthYear;
        }
    }
}
