using Dfe.ManageFreeSchoolProjects.API.Contracts.Common;

namespace Dfe.ManageFreeSchoolProjects.Data.Entities.Existing
{
    public partial class Po
    {
        public bool? PdgGrantLetterLinkSavedToWorkplaces { get; set; }

        public bool? PdgIsWriteOffSetup { get; set; }
        
        public DateTime? PdgInitialGrantLetterDate { get; set; }
        
        public string PdgInitialGrantLetterLink { get; set; }

        public bool? PdgInitialGrantLetterSavedToWorkplaces { get; set; }
        
        public bool? PdgFirstVariationGrantLetterSavedToWorkplaces { get; set; }
        
        public bool? PdgSecondVariationGrantLetterSavedToWorkplaces { get; set; }
        
        public bool? PdgThirdVariationGrantLetterSavedToWorkplaces { get; set; }
        
        public bool? PdgFourthVariationGrantLetterSavedToWorkplaces { get; set; }

        public string UnderwrittenPlacesPrimaryYear1 { get; set; }
        public string UnderwrittenPlacesPrimaryYear2 { get; set; }
        public string UnderwrittenPlacesPrimaryYear3 { get; set; }
        public string UnderwrittenPlacesPrimaryYear4 { get; set; }
        public string UnderwrittenPlacesPrimaryYear5 { get; set; }
        public string UnderwrittenPlacesPrimaryYear6 { get; set; }
        public string UnderwrittenPlacesPrimaryYear7 { get; set; }
        public string UnderwrittenPlacesSecondaryYear1 { get; set; }
        public string UnderwrittenPlacesSecondaryYear2 { get; set; }
        public string UnderwrittenPlacesSecondaryYear3 { get; set; }
        public string UnderwrittenPlacesSecondaryYear4 { get; set; }
        public string UnderwrittenPlacesSecondaryYear5 { get; set; }
        public string UnderwrittenPlacesSixteenToNineteenYear1 { get; set; }
        public string UnderwrittenPlacesSixteenToNineteenYear2 { get; set; }
        public string UnderwrittenPlacesSixteenToNineteenYear3 { get; set; }

        public bool? ConfirmationFromLocalAuthoritySavedInWorkplacesFolder { get; set; }

        public string CommentsAboutUnderwrittenPlaces { get; set; }
    }
}
