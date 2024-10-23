using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using Dfe.ManageFreeSchoolProjects.Data.Entities;
using Dfe.ManageFreeSchoolProjects.Data.Entities.Existing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dfe.ManageFreeSchoolProjects.Data.Configuration.Existing
{
	public partial class ConstructDataConfiguration : IEntityTypeConfiguration< ConstructData>
	{
		public void Configure(EntityTypeBuilder<ConstructData> builder)
		{
            builder
                .HasNoKey()
                .ToTable("constructData", "dbo");

            builder.Property(e => e.CapitalProjectRag)
                .IsUnicode(false)
                .HasColumnName("Capital Project RAG");
            builder.Property(e => e.CapitalProjectRagRatingCommentary)
                .IsUnicode(false)
                .HasColumnName("Capital Project RAG Rating Commentary");
            builder.Property(e => e.DateOfHoTSecuredOnTemporaryAccommodationSiteIfRequired)
                .IsUnicode(false)
                .HasColumnName("Date of HoT secured on temporary accommodation site, if required");
            builder.Property(e => e.HoTAgreedForSiteForMainSchoolBuildingActual)
                .IsUnicode(false)
                .HasColumnName("HoT Agreed for site for Main School Building (Actual)");
            builder.Property(e => e.IsThisTheMainPlanningRecord)
                .IsUnicode(false)
                .HasColumnName("Is this the main planning record?");
            builder.Property(e => e.MainSchoolBuildingFirstReadyForOccupationActual)
                .IsUnicode(false)
                .HasColumnName("Main School Building first ready for occupation (Actual)");
            builder.Property(e => e.MainSchoolBuildingFirstReadyForOccupationForecast)
                .IsUnicode(false)
                .HasColumnName("Main School Building first ready for occupation (Forecast)");
            builder.Property(e => e.PlanningDecision)
                .IsUnicode(false)
                .HasColumnName("Planning decision");
            builder.Property(e => e.PlanningRisk)
                .IsUnicode(false)
                .HasColumnName("Planning risk");
            builder.Property(e => e.PlanningSiteId)
                .IsUnicode(false)
                .HasColumnName("Planning Site ID");
            builder.Property(e => e.PostcodeOfSite)
                .IsUnicode(false)
                .HasColumnName("Postcode of site");
            builder.Property(e => e.PracticalCompletionCertificateIssuedDateA)
                .IsUnicode(false)
                .HasColumnName("Practical Completion Certificate issued date (A)");
            builder.Property(e => e.ProjectDirector)
                .IsUnicode(false)
                .HasColumnName("Project Director");
            builder.Property(e => e.ProjectId)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("Project ID");
            builder.Property(e => e.ProjectManager)
                .IsUnicode(false)
                .HasColumnName("Project Manager");
            builder.Property(e => e.RegionalHead)
                .IsUnicode(false)
                .HasColumnName("Regional Head");
            builder.Property(e => e.SiteId)
                .IsUnicode(false)
                .HasColumnName("Site ID");
            builder.Property(e => e.SiteIdentifiedForMainSchoolBuildingActual)
                .IsUnicode(false)
                .HasColumnName("Site identified for main school building (Actual)");
            builder.Property(e => e.SiteStatus)
                .IsUnicode(false)
                .HasColumnName("Site status");
            builder.Property(e => e.TemporaryAccommodationFirstReadyForOccupationActual)
                .IsUnicode(false)
                .HasColumnName("Temporary accommodation first ready for occupation (Actual)");
            builder.Property(e => e.TemporaryAccommodationFirstReadyForOccupationForecast)
                .IsUnicode(false)
                .HasColumnName("Temporary accommodation first ready for occupation (Forecast)");
            builder.Property(e => e.TemporaryRagRating)
                .IsUnicode(false)
                .HasColumnName("Temporary RAG rating");
            builder.Property(e => e.TemporaryRagRatingCommentary)
                .IsUnicode(false)
                .HasColumnName("Temporary RAG Rating Commentary");
            builder.Property(e => e.TypeOfSite)
                .IsUnicode(false)
                .HasColumnName("Type of Site");
            builder.Property(e => e.WillTheProjectOpenInTemporaryAccommodation)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Will the project open in temporary accommodation?");
            builder.Property(e => e.HoTsAgreedForTemporarySiteForecast)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("HoTs agreed for temporary site [Forecast]");
            builder.Property(e => e.ContractorForTemporarySiteAppointedForecast)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Contractor for temporary site appointed [Forecast]");
            builder.Property(e => e.ContractorForTemporarySiteAppointedActual)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Contractor for temporary site appointed [Actual]");
            builder.Property(e => e.DateOfPlanningDecisionForTemporarySiteMainPlanningRecordForecast)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Date of planning decision for temporary site main planning record [Forecast]");
            builder.Property(e => e.DateOfPlanningDecisionForTemporarySiteMainPlanningRecordActual)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Date of planning decision for temporary site main planning record [Actual]");
            builder.Property(e => e.TemporarySitePlanningDecision)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Temporary site planning decision");
            builder.Property(e => e.HoTsAgreedForSiteForMainSchoolBuildingForecast)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("HoTs agreed for site for main school building [Forecast]");
            builder.Property(e => e.ContractorForSiteForMainSchoolBuildingAppointedForecast)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Contractor for site for main school building appointed [Forecast]");
            builder.Property(e => e.ContractorForSiteForMainSchoolBuildingAppointedActual)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Contractor for site for main school building appointed [Actual]");
            builder.Property(e => e.DateOfPlanningDecisionForMainSiteMainPlanningRecordForecast)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Date of planning decision for main site main planning record [Forecast]");
            builder.Property(e => e.DateOfPlanningDecisionForMainSiteMainPlanningRecordActual)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Date of planning decision for main site main planning record [Actual]");
            builder.Property(e => e.TemporarySiteAddress)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Temporary site address");
            builder.Property(e => e.TemporarySitePostcode)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Temporary site postcode");
            builder.Property(e => e.TemporarySitePlanningRisk)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Temporary site planning risk");
            builder.Property(e => e.DateTemporarySitePlanningApprovalGranted)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Date temporary site planning approval granted");
            builder.Property(e => e.MainSiteAddress)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Main site address");
            builder.Property(e => e.DateMainSitePlanningApprovalGranted)
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnName("Date main site planning approval granted");

            AuditConfiguration.Apply(builder);
        }
	}

}
