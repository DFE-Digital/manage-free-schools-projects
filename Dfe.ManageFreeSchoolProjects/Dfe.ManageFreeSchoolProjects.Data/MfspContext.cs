using System;
using System.Collections.Generic;
using Dfe.ManageFreeSchoolProjects.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dfe.ManageFreeSchoolProjects.Data;

public partial class MfspContext : DbContext
{
    public MfspContext()
    {
    }

    public MfspContext(DbContextOptions<MfspContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kpi> Kpis { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMfspSqlServer("Server=localhost;Database=mfsp;Integrated Security=true;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kpi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("KPI");

            entity.Property(e => e.AprilIndicator)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("April Indicator");
            entity.Property(e => e.BasicNeedAdditionalEvidenceOfNeed)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Additional evidence of need");
            entity.Property(e => e.BasicNeedAdditionalEvidenceOfNeedSecondary)
                .HasMaxLength(999)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Additional evidence of need - secondary");
            entity.Property(e => e.BasicNeedAdditionalEvidenceOfNeedSecondaryAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Additional evidence of need - secondary (assessment)");
            entity.Property(e => e.BasicNeedKp02PlanningAreaCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Basic Need.KP02_Planning area code");
            entity.Property(e => e.BasicNeedKp04PlanningAreaCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Basic Need.KP04_Planning area code");
            entity.Property(e => e.BasicNeedPercentageShortfallInLocalAreaAllYearGroupsInScapYear4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Percentage shortfall in local area (all year groups in SCAP year +4)");
            entity.Property(e => e.BasicNeedPercentageShortfallInLocalAreaAllYearGroupsInScapYear4Secondary)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Percentage shortfall in local area (all year groups in SCAP year +4) - secondary");
            entity.Property(e => e.BasicNeedPercentageShortfallInLocalAreaYearOfOpeningAllYearGroups)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Percentage shortfall in local area — year of opening (all year groups)");
            entity.Property(e => e.BasicNeedPercentageShortfallInLocalAreaYearOfOpeningAllYearGroupsSecondary)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Percentage shortfall in local area - year of opening (all year groups) - secondary");
            entity.Property(e => e.BasicNeedPlanningAreaCodeSecondary)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Planning area code - secondary");
            entity.Property(e => e.BasicNeedPlanningAreaCodeSecondaryAssessment)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Planning area code - secondary (assessment)");
            entity.Property(e => e.BasicNeedSchoolInLocalAreaWithAShortfallOfPlacesInLocalArea)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in local area with a shortfall of places in local area");
            entity.Property(e => e.BasicNeedSchoolInLocalAreaWithAShortfallOfPlacesInLocalAreaSecondary)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in local area with a shortfall of places in local area - secondary");
            entity.Property(e => e.BasicNeedSchoolInLocalAreaWithAShortfallOfPlacesSecondaryAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in local area with a shortfall of places - secondary (assessment)");
            entity.Property(e => e.BasicNeedSchoolInPlanningAreaOfBasicNeed)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in planning area of basic need");
            entity.Property(e => e.BasicNeedSchoolInPlanningAreaOfBasicNeedAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in planning area of basic need (assessment)");
            entity.Property(e => e.BasicNeedSchoolInPlanningAreaOfBasicNeedSecondary)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in planning area of basic need - secondary");
            entity.Property(e => e.BasicNeedSchoolInPlanningAreaOfBasicNeedSecondaryAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.School in planning area of basic need - secondary (assessment)");
            entity.Property(e => e.BasicNeedShortfallOfPlacesInLocalAreaAllYearGroupsInScapYear4)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Shortfall of places in local area (all year groups in SCAP year +4)");
            entity.Property(e => e.BasicNeedShortfallOfPlacesInLocalAreaAllYearGroupsInScapYear4Secondary)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Shortfall of places in local area (all year groups in SCAP year +4) - secondary");
            entity.Property(e => e.BasicNeedShortfallOfPlacesInLocalAreaYearOfOpeningAllYearGroups)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Shortfall of places in local area — year of opening (all year groups)");
            entity.Property(e => e.BasicNeedShortfallOfPlacesInLocalAreaYearOfOpeningAllYearGroupsSecondary)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Shortfall of places in local area - year of opening (all year groups) - secondary");
            entity.Property(e => e.BasicNeedYearOfProjectedNeedSecondaryAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Year of projected need - secondary (assessment)");
            entity.Property(e => e.BasicNeedYearOfScapSurvey)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Year of SCAP survey");
            entity.Property(e => e.BasicNeedYearOfScapSurveySecondary)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Basic Need.Year of SCAP survey - secondary");
            entity.Property(e => e.CommunicationsArchivedLinesToTake)
                .IsUnicode(false)
                .HasColumnName("Communications.Archived lines to take");
            entity.Property(e => e.CommunicationsCurrentLinesToTake)
                .IsUnicode(false)
                .HasColumnName("Communications.Current lines to take");
            entity.Property(e => e.CommunicationsMediaPenPortrait)
                .IsUnicode(false)
                .HasColumnName("Communications.Media pen portrait");
            entity.Property(e => e.ContingencyPlanningBackUpField)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Back-up Field");
            entity.Property(e => e.ContingencyPlanningCanCurrentCohortRemainInSchool)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Can current cohort remain in school?");
            entity.Property(e => e.ContingencyPlanningCanSchoolTakeOnAnotherCohort)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Can school take on another cohort?");
            entity.Property(e => e.ContingencyPlanningCanTempsBeExtended)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Can temps be extended?");
            entity.Property(e => e.ContingencyPlanningEssentialThatItIsDeliveredForSeptemberOrCurrentScheduledDateInTheRealisticYearOfOpening)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Essential that it is delivered for September (or current scheduled date) in the Realistic Year of Opening?");
            entity.Property(e => e.ContingencyPlanningExtraInformation)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Extra information");
            entity.Property(e => e.ContingencyPlanningFscDeliverabilityAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.FSC Deliverability Assessment");
            entity.Property(e => e.ContingencyPlanningFscDeliverabilityComment)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.FSC Deliverability Comment");
            entity.Property(e => e.ContingencyPlanningHowLongCanTempsBeExtended)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.How long can temps be extended?");
            entity.Property(e => e.ContingencyPlanningHowManyStudentsWillNeedAlternativeArrangements)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.How many students will need alternative arrangements?");
            entity.Property(e => e.ContingencyPlanningIfOtherWhy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.If 'Other', why?");
            entity.Property(e => e.ContingencyPlanningIfOtherWhyForRAndAExplainAnythingBeingExploredOrNextSteps)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.If 'Other', why? (for R and A, explain anything being explored or next steps)");
            entity.Property(e => e.ContingencyPlanningIfYesWhy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.If ‘Yes’, why?");
            entity.Property(e => e.ContingencyPlanningProjectedLengthOfDelayToProject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Projected length of delay to project");
            entity.Property(e => e.ContingencyPlanningRddRationale)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.RDD Rationale");
            entity.Property(e => e.ContingencyPlanningRddSiteContingencyRag)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.RDD Site Contingency RAG");
            entity.Property(e => e.ContingencyPlanningSiteShutdown)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Contingency Planning.Site shutdown");
            entity.Property(e => e.FsType)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("FS_Type");
            entity.Property(e => e.FsType1)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("FS_Type_1");
            entity.Property(e => e.KeyContactsAddressOfLeadProposer)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Address of lead proposer");
            entity.Property(e => e.KeyContactsAllocatedLawFirm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Allocated law firm");
            entity.Property(e => e.KeyContactsAssessmentTeamLeader)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Assessment team leader");
            entity.Property(e => e.KeyContactsChairOfGovernorsEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Chair of Governors email");
            entity.Property(e => e.KeyContactsChairOfGovernorsMat)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Chair of governors MAT");
            entity.Property(e => e.KeyContactsChairOfGovernorsMatEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Chair of governors MAT email");
            entity.Property(e => e.KeyContactsChairOfGovernorsMatPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Chair of governors MAT phone");
            entity.Property(e => e.KeyContactsChairOfGovernorsName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Chair of Governors name");
            entity.Property(e => e.KeyContactsChairOfGovernorsPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Chair of Governors phone");
            entity.Property(e => e.KeyContactsCommercialManager)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Commercial Manager");
            entity.Property(e => e.KeyContactsEaOnceSchoolIsOpen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.EA once school is open");
            entity.Property(e => e.KeyContactsEducationAdviserAssessment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Education adviser (assessment)");
            entity.Property(e => e.KeyContactsEducationAdviserPreOpening)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Education adviser (pre-opening)");
            entity.Property(e => e.KeyContactsEsfaAcademiesSeniorAdviser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA academies senior adviser");
            entity.Property(e => e.KeyContactsEsfaCapitalHeadOfRegion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA Capital Head of Region");
            entity.Property(e => e.KeyContactsEsfaCapitalProjectDirector)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA Capital project director");
            entity.Property(e => e.KeyContactsEsfaCapitalProjectManager)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA Capital project manager");
            entity.Property(e => e.KeyContactsEsfaCapitalProjectManagerFirm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA Capital project manager firm");
            entity.Property(e => e.KeyContactsEsfaLinkOfficer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA Link Officer");
            entity.Property(e => e.KeyContactsEsfaPropertyLead)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA property lead");
            entity.Property(e => e.KeyContactsEsfaRegionalPropertyLead)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA regional property lead");
            entity.Property(e => e.KeyContactsEsfaTechnicalAdviser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ESFA technical adviser");
            entity.Property(e => e.KeyContactsFrameworkPlanningConsultant)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Framework planning consultant");
            entity.Property(e => e.KeyContactsFrameworkPlanningFirm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Framework planning firm");
            entity.Property(e => e.KeyContactsFrameworkPlanningFirmOther)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Framework planning firm – other");
            entity.Property(e => e.KeyContactsFsgAssessmentLead)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.FSG assessment lead");
            entity.Property(e => e.KeyContactsFsgGrade6)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.FSG Grade 6");
            entity.Property(e => e.KeyContactsFsgLeadContact)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.FSG lead contact");
            entity.Property(e => e.KeyContactsFsgTeamLeader)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.FSG team leader");
            entity.Property(e => e.KeyContactsIctAdviser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.ICT adviser");
            entity.Property(e => e.KeyContactsInterviewChair)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Interview chair");
            entity.Property(e => e.KeyContactsLeadProposerEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Lead proposer email ");
            entity.Property(e => e.KeyContactsLeadProposerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Lead proposer name");
            entity.Property(e => e.KeyContactsLeadProposerPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Lead proposer phone");
            entity.Property(e => e.KeyContactsLegalFirm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Legal firm");
            entity.Property(e => e.KeyContactsLegalManager)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Legal manager");
            entity.Property(e => e.KeyContactsLocalAuthorityContactPresumptionProject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Local authority contact (presumption project)");
            entity.Property(e => e.KeyContactsLocatEdAcquisitionManager)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.LocatED acquisition manager");
            entity.Property(e => e.KeyContactsNamedContactOnceSchoolIsOpen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Named contact once school is open");
            entity.Property(e => e.KeyContactsPlanningAdviser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Planning Adviser");
            entity.Property(e => e.KeyContactsPostCodeForMapping)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.PostCode for Mapping");
            entity.Property(e => e.KeyContactsPostcode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Postcode");
            entity.Property(e => e.KeyContactsPrincipalDesignateEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Principal Designate email");
            entity.Property(e => e.KeyContactsPrincipalDesignateName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Principal Designate Name");
            entity.Property(e => e.KeyContactsPrincipalDesignatePhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Principal Designate phone");
            entity.Property(e => e.KeyContactsPropertyAdviserAllocated)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Property adviser allocated");
            entity.Property(e => e.KeyContactsPropertyDocumentRepositoryLink)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Property document repository link");
            entity.Property(e => e.KeyContactsPropertyFirmDealing)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Property firm dealing");
            entity.Property(e => e.KeyContactsSchoolAddress)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.School address");
            entity.Property(e => e.KeyContactsSeniorExecutiveLeaderEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Senior Executive Leader email");
            entity.Property(e => e.KeyContactsSeniorExecutiveLeaderName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Senior Executive Leader name");
            entity.Property(e => e.KeyContactsSeniorExecutiveLeaderPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Senior Executive Leader phone");
            entity.Property(e => e.KeyContactsStrategicDesignAdviser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Strategic design adviser");
            entity.Property(e => e.KeyContactsTechnicalAdvisoryFirm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Technical advisory firm");
            entity.Property(e => e.KeyContactsTrustSIctLeadContact)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Trust's ICT lead contact");
            entity.Property(e => e.KeyContactsTrustSIctLeadContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Trust's ICT lead contact email");
            entity.Property(e => e.KeyContactsTrustSIctLeadContactPhone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Key Contacts.Trust's ICT lead contact phone");
            entity.Property(e => e.LeadSponsorId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("Lead sponsor ID");
            entity.Property(e => e.LeadSponsorName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Lead sponsor name");
            entity.Property(e => e.LocalAuthority)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Local authority");
            entity.Property(e => e.MatUnitProjects)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("MAT Unit Projects");
            entity.Property(e => e.PRid)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("p_rid");
            entity.Property(e => e.ProjectStatusActualDateOfOpeningInPermanentAccommodation)
                .HasColumnType("date")
                .HasColumnName("Project Status.Actual date of opening in permanent accommodation");
            entity.Property(e => e.ProjectStatusActualDateOfOpeningInTemporaryAccommodation)
                .HasColumnType("date")
                .HasColumnName("Project Status.Actual date of opening in temporary accommodation");
            entity.Property(e => e.ProjectStatusActualOpeningDate)
                .HasColumnType("date")
                .HasColumnName("Project Status.Actual opening date");
            entity.Property(e => e.ProjectStatusCommentaryForCancellation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Commentary for cancellation");
            entity.Property(e => e.ProjectStatusCommentaryForFirstDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Commentary for first deferral");
            entity.Property(e => e.ProjectStatusCommentaryForSecondDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Commentary for second deferral");
            entity.Property(e => e.ProjectStatusCommentaryForThirdDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Commentary for third deferral");
            entity.Property(e => e.ProjectStatusCommentaryForWithdrawal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Commentary for withdrawal");
            entity.Property(e => e.ProjectStatusCurrentFreeSchoolName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Current free school name");
            entity.Property(e => e.ProjectStatusDateCancelled)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date cancelled");
            entity.Property(e => e.ProjectStatusDateClosed)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date closed");
            entity.Property(e => e.ProjectStatusDateOfApplicationIfOutsideWave)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date of application if outside wave");
            entity.Property(e => e.ProjectStatusDateOfEntryIntoPreOpening)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date of entry into pre-opening");
            entity.Property(e => e.ProjectStatusDateOfFirstDeferral)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date of first deferral");
            entity.Property(e => e.ProjectStatusDateOfSecondDeferral)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date of second deferral");
            entity.Property(e => e.ProjectStatusDateOfThirdDeferral)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date of third deferral");
            entity.Property(e => e.ProjectStatusDateWithdrawn)
                .HasColumnType("date")
                .HasColumnName("Project Status.Date withdrawn");
            entity.Property(e => e.ProjectStatusFreeSchoolApplicationWave)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Free school application wave");
            entity.Property(e => e.ProjectStatusFreeSchoolPenPortrait)
                .IsUnicode(false)
                .HasColumnName("Project Status.Free school pen portrait");
            entity.Property(e => e.ProjectStatusFreeSchoolsApplicationNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Project Status.Free schools application number");
            entity.Property(e => e.ProjectStatusHasProjectBeenCancelled)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Has project been cancelled?");
            entity.Property(e => e.ProjectStatusHasProjectBeenDeferredForASecondTime)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Has project been deferred for a second time?");
            entity.Property(e => e.ProjectStatusHasProjectBeenDeferredForAThirdTime)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Has project been deferred for a third time?");
            entity.Property(e => e.ProjectStatusHasProjectBeenWithdrawn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Has project been withdrawn?");
            entity.Property(e => e.ProjectStatusHasTheFreeSchoolChangedItsName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Has the free school changed its name?");
            entity.Property(e => e.ProjectStatusHasTheProjectBeenDeferred)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Has the project been deferred?");
            entity.Property(e => e.ProjectStatusKp05ReasonForSiteDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.KP05_Reason for site deferral");
            entity.Property(e => e.ProjectStatusKp06ReasonForSiteDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.KP06_Reason for site deferral");
            entity.Property(e => e.ProjectStatusNewOpeningDateFollowingFirstDeferral)
                .HasColumnType("date")
                .HasColumnName("Project Status.New opening date following first deferral");
            entity.Property(e => e.ProjectStatusNewOpeningDateFollowingSecondDeferral)
                .HasColumnType("date")
                .HasColumnName("Project Status.New opening date following second deferral");
            entity.Property(e => e.ProjectStatusNewOpeningDateFollowingThirdDeferral)
                .HasColumnType("date")
                .HasColumnName("Project Status.New opening date following third deferral");
            entity.Property(e => e.ProjectStatusPlannedMoveDateToPermanentSite)
                .HasColumnType("date")
                .HasColumnName("Project Status.Planned move date to permanent site");
            entity.Property(e => e.ProjectStatusPreviousFreeSchoolName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Previous free school name");
            entity.Property(e => e.ProjectStatusPrimaryReasonForCancellation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Primary reason for cancellation");
            entity.Property(e => e.ProjectStatusPrimaryReasonForFirstDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Primary reason for first deferral");
            entity.Property(e => e.ProjectStatusPrimaryReasonForSecondDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Primary reason for second deferral");
            entity.Property(e => e.ProjectStatusPrimaryReasonForThirdDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Primary reason for third deferral");
            entity.Property(e => e.ProjectStatusPrimaryReasonForWithdrawal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Primary reason for withdrawal");
            entity.Property(e => e.ProjectStatusProjectId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Project Status.Project ID");
            entity.Property(e => e.ProjectStatusProjectStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Project status");
            entity.Property(e => e.ProjectStatusProvisionalOpeningDateAgreedWithTrust)
                .HasColumnType("date")
                .HasColumnName("Project Status.Provisional opening date agreed with trust");
            entity.Property(e => e.ProjectStatusRealisticYearOfOpening)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Realistic year of opening");
            entity.Property(e => e.ProjectStatusReasonForSiteCancellation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Reason for site cancellation");
            entity.Property(e => e.ProjectStatusReasonForSiteDeferral)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Reason for site deferral");
            entity.Property(e => e.ProjectStatusReasonForSiteWithdrawal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Reason for site withdrawal");
            entity.Property(e => e.ProjectStatusRebrokeredUrn)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Rebrokered URN");
            entity.Property(e => e.ProjectStatusTrustsPreferredYearOfOpening)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.Trusts preferred year of opening");
            entity.Property(e => e.ProjectStatusUrnWhenGivenOne)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Project Status.URN (when given one)");
            entity.Property(e => e.RatProvisionalOpeningDateAgreedWithTrust)
                .HasColumnType("date")
                .HasColumnName("RAT Provisional opening date agreed with trust");
            entity.Property(e => e.Rid)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("RID");
            entity.Property(e => e.RyooWd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RYOO_WD");
            entity.Property(e => e.SchoolDetailsAeaCatagory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.AEA Catagory");
            entity.Property(e => e.SchoolDetailsAgeRange)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Age range");
            entity.Property(e => e.SchoolDetailsConstituency)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Constituency");
            entity.Property(e => e.SchoolDetailsConstituencyMp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Constituency MP");
            entity.Property(e => e.SchoolDetailsDeprivationDecline)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Deprivation decline");
            entity.Property(e => e.SchoolDetailsDetailsOfResidentialBoardingProvision)
                .IsUnicode(false)
                .HasColumnName("School Details.Details of residential/boarding provision");
            entity.Property(e => e.SchoolDetailsDistinguishingFeatures)
                .IsUnicode(false)
                .HasColumnName("School Details.Distinguishing features");
            entity.Property(e => e.SchoolDetailsDistrict)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.District");
            entity.Property(e => e.SchoolDetailsEfaTerritory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.EFA Territory");
            entity.Property(e => e.SchoolDetailsEmployerPartners)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Employer Partners");
            entity.Property(e => e.SchoolDetailsEmployerSponsorsUtcsSsOnly)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Employer sponsors - UTCs/SS only");
            entity.Property(e => e.SchoolDetailsFaithStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Faith status");
            entity.Property(e => e.SchoolDetailsFaithType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Faith type");
            entity.Property(e => e.SchoolDetailsGender)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Gender");
            entity.Property(e => e.SchoolDetailsGeographicalRegion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Geographical Region");
            entity.Property(e => e.SchoolDetailsIndependentConverter)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Independent converter");
            entity.Property(e => e.SchoolDetailsLaestabWhenGivenOne)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.LAESTAB (when given one)");
            entity.Property(e => e.SchoolDetailsLeadSponsorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("School Details.Lead sponsor ID");
            entity.Property(e => e.SchoolDetailsLeadSponsorName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Lead sponsor name");
            entity.Property(e => e.SchoolDetailsLocalAuthority)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Local authority");
            entity.Property(e => e.SchoolDetailsLocalAuthorityControl)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Local authority control");
            entity.Property(e => e.SchoolDetailsNeetInLa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.% NEET in LA");
            entity.Property(e => e.SchoolDetailsNumberOfFormsOfEntry)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Number of forms of entry");
            entity.Property(e => e.SchoolDetailsNursery)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Nursery");
            entity.Property(e => e.SchoolDetailsOtherPartners)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Other Partners");
            entity.Property(e => e.SchoolDetailsPleaseSpecifyOtherFaithType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Please specify other faith type");
            entity.Property(e => e.SchoolDetailsPleaseSpecifyOtherTypeOfProposerGroup)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Please specify other type of proposer group");
            entity.Property(e => e.SchoolDetailsPoliticalParty)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Political party");
            entity.Property(e => e.SchoolDetailsResidentialOrBoardingProvision)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Residential or boarding provision");
            entity.Property(e => e.SchoolDetailsRscRegion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.RSC region");
            entity.Property(e => e.SchoolDetailsSchoolPhasePrimarySecondary)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.School phase (primary, secondary)");
            entity.Property(e => e.SchoolDetailsSchoolTypeMainstreamApEtc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.School type (mainstream, AP etc)");
            entity.Property(e => e.SchoolDetailsSixthForm)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Sixth form");
            entity.Property(e => e.SchoolDetailsSixthFormType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Sixth form type");
            entity.Property(e => e.SchoolDetailsSizeOfGoverningBody)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("School Details.Size of Governing Body");
            entity.Property(e => e.SchoolDetailsSpecialism)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Specialism");
            entity.Property(e => e.SchoolDetailsSpecialistResourceProvision)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Specialist Resource Provision");
            entity.Property(e => e.SchoolDetailsSponsorType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Sponsor type");
            entity.Property(e => e.SchoolDetailsStartOfTermDate)
                .HasColumnType("date")
                .HasColumnName("School Details.Start of term date");
            entity.Property(e => e.SchoolDetailsTrustId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("School Details.Trust ID");
            entity.Property(e => e.SchoolDetailsTrustName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Trust name");
            entity.Property(e => e.SchoolDetailsTrustType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Trust type");
            entity.Property(e => e.SchoolDetailsTypeOfProposerGroup)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.Type of proposer group");
            entity.Property(e => e.SchoolDetailsUniversitySponsor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("School Details.University Sponsor");
            entity.Property(e => e.SponsorUnitProjects)
                .IsRequired()
                .HasMaxLength(39)
                .IsUnicode(false)
                .HasColumnName("Sponsor Unit Projects");
            entity.Property(e => e.TrustId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("Trust ID");
            entity.Property(e => e.TrustName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Trust name");
            entity.Property(e => e.TrustType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Trust type");
            entity.Property(e => e.UpperStatus)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Upper Status");
            entity.Property(e => e.Wave)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", "mfsp");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).HasMaxLength(80);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
