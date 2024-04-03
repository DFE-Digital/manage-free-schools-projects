using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.PupilNumbers;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;

namespace Dfe.ManageFreeSchoolProjects.API.UseCases.Project.PupilNumbers
{
    public class UpdatePost16CapacityBuildupService : IUpdatePupilNumbersSectionService
    {
        public void Execute(Po po, UpdatePupilNumbersRequest request)
        {
            if (request.Post16CapacityBuildup == null)
            {
                return;
            }

            UpdateYear7(po, request.Post16CapacityBuildup.Year7);
            UpdateYear8(po, request.Post16CapacityBuildup.Year8);
            UpdateYear9(po, request.Post16CapacityBuildup.Year9);
            UpdateYear10(po, request.Post16CapacityBuildup.Year10);
            UpdateYear11(po, request.Post16CapacityBuildup.Year11);
            UpdateYear12(po, request.Post16CapacityBuildup.Year12);
            UpdateYear13(po, request.Post16CapacityBuildup.Year13);
            UpdateYear14(po, request.Post16CapacityBuildup.Year14);
        }

        private static void UpdateYear7(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA9Year7CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB9Year7FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC9Year7SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD9Year7ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE9Year7FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF9Year7FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG9Year7SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH9Year7SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear8(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA10Year8CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB10Year8FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC10Year8SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD10Year8ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE10Year8FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF10Year8FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG10Year8SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH10Year8SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear9(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA11Year9CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB11Year9FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC11Year9SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD11Year9ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE11Year9FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF11Year9FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG11Year9SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH11Year9SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear10(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA12Year10CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB12Year10FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC12Year10SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD12Year10ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE12Year10FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF12Year10FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG12Year10SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH12Year10SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear11(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA13Year11CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB13Year11FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC13Year11SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD13Year11ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE13Year11FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF13Year11FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG13Year11SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH13Year11SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear12(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA14Year12CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB14Year12FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC14Year12SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD14Year12ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE14Year12FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF14Year12FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG14Year12SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH14Year12SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear13(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA15Year13CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB15Year13FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC15Year13SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD15Year13ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE15Year13FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF15Year13FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG15Year13SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH15Year13SeventhYear = entry.SeventhYear.ToString();
        }

        private static void UpdateYear14(Po po, CapacityBuildupEntry entry)
        {
            po.PupilNumbersAndCapacityCellA16Year14CurrentPupilNumbersIfAlreadyOpen = entry.CurrentCapacity.ToString();
            po.PupilNumbersAndCapacityCellB16Year14FirstYear = entry.FirstYear.ToString();
            po.PupilNumbersAndCapacityCellC16Year14SecondYear = entry.SecondYear.ToString();
            po.PupilNumbersAndCapacityCellD16Year14ThirdYear = entry.ThirdYear.ToString();
            po.PupilNumbersAndCapacityCellE16Year14FourthYear = entry.FourthYear.ToString();
            po.PupilNumbersAndCapacityCellF16Year14FifthYear = entry.FifthYear.ToString();
            po.PupilNumbersAndCapacityCellG16Year14SixthYear = entry.SixthYear.ToString();
            po.PupilNumbersAndCapacityCellH16Year14SeventhYear = entry.SeventhYear.ToString();
        }
    }
}
