using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dfe.ManageFreeSchoolProjects.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                EXEC('CREATE PROCEDURE [dbo].[sp_DataHistory]
                AS
                --Historic data code
                -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                /*
                Historic data for the following fields - all from the KPI table
                project status
                School Type
                school phase
                application wave
                Wave
                Phase
                Realistic Year of Opening
                [Project Status.Provisional opening date agreed with trust]
                */

                --this UD gets the required fields and casts as varchar. Nulls are set to string NULLS as there was an issue with the unpivot or
                --or grouping (cant remember where), and setting to string resolved this. The nulls are changed back to normal nulls later.
                ;WITH KPI_UD AS
                    (
                    SELECT 
                        ISNULL(cast([RID] as varchar(255)),''NULL'') AS [RID]
                        ,ISNULL(cast([p_rid] as varchar(255)),''NULL'') AS [p_rid]
		                ,ISNULL(cast([Project Status.Current free school name] as varchar(255)),''NULL'') AS [Project Status.Current free school name]
		                ,ISNULL(cast([School Details.School type (mainstream, AP etc)] as varchar(255)),''NULL'') AS [School Details.School type (mainstream, AP etc)]
		                ,ISNULL(cast([School Details.School phase (primary, secondary)] as varchar(255)),''NULL'') AS [School Details.School phase (primary, secondary)]
		                ,ISNULL(cast([Project Status.Free school application wave] as varchar(255)),''NULL'') AS [Project Status.Free school application wave]
                        ,ISNULL(cast([Project Status.Project status] as varchar(255)),''NULL'') AS [Project Status.Project status]
                        ,ISNULL(cast([Project Status.Realistic year of opening] as varchar(255)),''NULL'') AS [Project Status.Realistic year of opening]
                        ,ISNULL(cast([Project Status.Provisional opening date agreed with trust] as varchar(255)),''NULL'') AS [Project Status.Provisional opening date agreed with trust]
                        ,ISNULL(cast([Key Contacts.FSG lead contact] as varchar(255)),''NULL'') AS [Key Contacts.FSG lead contact]
                        ,ISNULL(cast([Key Contacts.FSG team leader] as varchar(255)),''NULL'') AS [Key Contacts.FSG team leader]
                        ,ISNULL(cast([Key Contacts.FSG Grade 6] as varchar(255)),''NULL'') AS [Key Contacts.FSG Grade 6]
                        ,ISNULL(cast([Trust ID] as varchar(255)),''NULL'') AS [Trust ID]
                        ,ISNULL(cast([Trust name] as varchar(255)),''NULL'') AS [Trust name]
                        ,ISNULL(cast([Trust type] as varchar(255)),''NULL'') AS [Trust type]		
                        ,ISNULL(cast([PeriodEnd] as varchar(255)),''NULL'') AS [PeriodEnd]
                        ,ISNULL(cast([PeriodStart] as varchar(255)),''NULL'') AS [PeriodStart]
		                ,ISNULL(cast([UpdatedByUserId] as varchar(255)),''NULL'') AS [UpdatedByUserId]
                    FROM
                         dbo.KPI for system_time all
	                where [Project Status.Project ID] is not null
	                ),

                --unpivot the UD table to get the field name in rows rather than columns. We do this to get the table in the same format as the
                --previous setup when we used the KIM DS_Data tables. We set a row number for each projectID, field and date created. This is
                --to allow us to identify when a field value has change for a project and the date it changed.
                Unpivot_KPI_UD as
                (
                    select     
                        [RID]
                        ,[p_rid]
    	                ,Field
    	                ,Field_Value
    	                ,[PeriodStart]
		                ,[UpdatedByUserId]
                        ,ROW_NUMBER() over (partition by [RID], Field order by [RID], Field, [PeriodStart]) as n
                    FROM
                        KPI_UD
                    UNPIVOT
                        (
    	                Field_Value
    	                    FOR
    	                Field
    	                    IN
    	                    (
                            [Project Status.Project status]
			                ,[Project Status.Current free school name]
			                ,[School Details.School type (mainstream, AP etc)]
			                ,[School Details.School phase (primary, secondary)]
			                ,[Project Status.Free school application wave]
                            ,[Project Status.Realistic year of opening]
                            ,[Project Status.Provisional opening date agreed with trust]
                            ,[Key Contacts.FSG lead contact]
                            ,[Key Contacts.FSG team leader]
                            ,[Key Contacts.FSG Grade 6]
                            ,[Trust ID]
                            ,[Trust name]
                            ,[Trust type]
    		                )
    		                ) AS UNPVT
                ),

                --this table is where we identify the hisoric changes. We compare the values for each field in each project for each sequential start date.
                --We have to do this as the History table just provides all the data for a project when there is any change - it doesnt identify the specific 
                --change. If there is no change in the value for a field we ignore it, but if there is a change we keep it. We also keep the very first
                --value for each field when the project was created.
                Historic_KPI AS
                (
                --this is the initial data entered when the project was first created.
                select
                    [RID]
	                ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN Field_Value = ''NULL'' then NULL ELSE Field_Value END AS [Value]
	                ,PeriodStart as [CreatedDate]
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    Unpivot_KPI_UD
                where
                    n = 1

                UNION ALL

                --get all the changes since project created
                select
                    [RID]
                    ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN NewValue = ''NULL'' then NULL ELSE NewValue END AS [Value]
	                ,CreatedDate
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    (
	                --compare the values for the data field, if different we marked as changed.
                    select *,
                        case when StartValue = NewValue or NewValue is null then ''N'' else ''Y'' end as ValueUpdatesIndicator
                    from
	                    (
	                    --join the table to compare field values between start dates, done by joining on the row number n.
                        select
		                    T1.*, T2.NewValue, T2.CreatedDate
		                from 
                            (select [RID],[p_rid]
			                        , Field, Field_Value as ''StartValue'', [PeriodStart], [UpdatedByUserId], n from Unpivot_KPI_UD) T1
                            left join 
                            (select [RID],[p_rid]
			                        ,Field, Field_Value as ''NewValue'', [PeriodStart] as ''CreatedDate'', [UpdatedByUserId], n from Unpivot_KPI_UD) T2
                            on T1.[RID] = T2.[RID] and T1.Field = T2.Field and T1.n = T2.n - 1
                        )T3
                    )T4
                where ValueUpdatesIndicator = ''Y''
                ),

                ------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                /*
                Historic data for the following fields from the Milestones table
                PDapp Actual date of completion
                FA Actual date of completion

                --methodology follows the same comments as the KPI history section
                */

                Milestones_UD AS
                    (
                    SELECT 
                         ISNULL(cast([RID] as varchar(255)),''NULL'') AS [RID]
                        ,ISNULL(cast([p_rid] as varchar(255)),''NULL'') AS [p_rid]
		                ,ISNULL(cast([FSG Pre Opening Milestones.PDapp Forecast date] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.PDapp Forecast date]
		                ,ISNULL(cast([FSG Pre Opening Milestones.PDapp Actual date of completion] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.PDapp Actual date of completion]
		                ,ISNULL(cast([FSG Pre Opening Milestones.FA Forecast date] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.FA Forecast date]
		                ,ISNULL(cast([FSG Pre Opening Milestones.FA Actual date of completion] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.FA Actual date of completion]	
		                ,ISNULL(cast([FSG Pre Opening Milestones.ROM Actual date of completion] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.ROM Actual date of completion]	
		                ,ISNULL(cast([FSG Pre Opening Milestones.FPA Actual date of completion] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.FPA Actual date of completion]	
		                ,ISNULL(cast([FSG Pre Opening Milestones.GIAS Actual date of completion] as varchar(255)),''NULL'') AS [FSG Pre Opening Milestones.GIAS Actual date of completion]	
                        ,ISNULL(cast([PeriodEnd] as varchar(255)),''NULL'') AS [PeriodEnd]
                        ,ISNULL(cast([PeriodStart] as varchar(255)),''NULL'') AS [PeriodStart]
		                ,ISNULL(cast([UpdatedByUserId] as varchar(255)),''NULL'') AS [UpdatedByUserId]
                    FROM
                        [dbo].[Milestones] for system_time all
	                ),


                Unpivot_Milestones_UD as
                (
                    select     
                        [RID]
                        ,[p_rid]
    	                ,Field
    	                ,Field_Value
    	                ,[PeriodStart]
		                ,[UpdatedByUserId]
                        ,ROW_NUMBER() over (partition by [RID], Field order by [RID], Field, [PeriodStart]) as n
                    FROM
                        Milestones_UD
                    UNPIVOT
                        (
    	                Field_Value
    	                    FOR
    	                Field
    	                    IN
    	                    (
			                [FSG Pre Opening Milestones.PDapp Forecast date]
			                ,[FSG Pre Opening Milestones.PDapp Actual date of completion]
			                ,[FSG Pre Opening Milestones.FA Forecast date]
                            ,[FSG Pre Opening Milestones.FA Actual date of completion]
			                ,[FSG Pre Opening Milestones.ROM Actual date of completion]
			                ,[FSG Pre Opening Milestones.FPA Actual date of completion]	
			                ,[FSG Pre Opening Milestones.GIAS Actual date of completion]
    		                )
    		                ) AS UNPVT
                ),


                Historic_Milestones AS
                (
                --this is the initial data entered when the project was first created.
                select
                    [RID]
                    ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN Field_Value = ''NULL'' then NULL ELSE Field_Value END AS [Value]
	                ,PeriodStart as [CreatedDate]
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    Unpivot_Milestones_UD
                where
                    n = 1

                UNION ALL

                --get all the changes since project created
                select
                    [RID]
	                ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN NewValue = ''NULL'' then NULL ELSE NewValue END AS [Value]
	                ,CreatedDate
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    (
                    select
	                    *
                        ,case when StartValue = NewValue or NewValue is null then ''N'' else ''Y'' end as ValueUpdatesIndicator
                    from
	                    (
                        select
		                    T1.*, T2.NewValue, T2.CreatedDate
		                from 
                            (select [RID],[p_rid], Field, Field_Value as ''StartValue'', [PeriodStart], [UpdatedByUserId], n from Unpivot_Milestones_UD) T1
                            left join 
                            (select [RID],[p_rid], Field, Field_Value as ''NewValue'', [PeriodStart] as ''CreatedDate'', [UpdatedByUserId], n from Unpivot_Milestones_UD) T2
                            on T1.RID = T2.RID and T1.Field = T2.Field and T1.n = T2.n - 1
                        )T3
                    )T4
                where ValueUpdatesIndicator = ''Y''
                ),

                -----------------------------------------------------------------------------------------------------------------------------------------------------------
                /*
                Historic data for the following fields from the PO table
                [Pupil numbers and capacity.% accepted applications vs viability YR-Y6]
                [Pupil numbers and capacity.% accepted applications vs viability Y7-Y11]
                [Pupil numbers and capacity.% accepted applications vs viability Y12-Y14]

                --methodology follows the same comments as the KPI history section
                */

                PO_UD AS
                    (
                    SELECT 
                         ISNULL(cast([RID] as varchar(255)),''NULL'') AS [RID]
                        ,ISNULL(cast([p_rid] as varchar(255)),''NULL'') AS [p_rid]
                        ,ISNULL(cast([Pupil numbers and capacity.YR PAN] as varchar(255)),''NULL'') AS [Pupil numbers and capacity.YR PAN]
		                ,ISNULL(cast([Pupil numbers and capacity.Y7 PAN] as varchar(255)),''NULL'') AS [Pupil numbers and capacity.Y7 PAN]
		                ,ISNULL(cast([Pupil numbers and capacity.Y12 PAN] as varchar(255)),''NULL'') AS [Pupil numbers and capacity.Y12 PAN]
		                ,ISNULL(cast([Pupil numbers and capacity.% accepted applications vs viability YR-Y6] as varchar(255)),''NULL'') AS [Pupil numbers and capacity.% accepted applications vs viability YR-Y6]
		                ,ISNULL(cast([Pupil numbers and capacity.% accepted applications vs viability Y7-Y11] as varchar(255)),''NULL'') AS [Pupil numbers and capacity.% accepted applications vs viability Y7-Y11]		
                        ,ISNULL(cast([Pupil numbers and capacity.% accepted applications vs viability Y12-Y14] as varchar(255)),''NULL'') AS [Pupil numbers and capacity.% accepted applications vs viability Y12-Y14]		
                        ,ISNULL(cast([PeriodEnd] as varchar(255)),''NULL'') AS [PeriodEnd]
                        ,ISNULL(cast([PeriodStart] as varchar(255)),''NULL'') AS [PeriodStart]
		                ,ISNULL(cast([UpdatedByUserId] as varchar(255)),''NULL'') AS [UpdatedByUserId]
                    FROM
                        [dbo].[PO] for system_time all
	                ),


                Unpivot_PO_UD as
                (
                    select     
                        [RID]
                        ,[p_rid]
    	                ,Field
    	                ,Field_Value
    	                ,[PeriodStart]
		                ,[UpdatedByUserId]
                        ,ROW_NUMBER() over (partition by [RID], Field order by [RID], Field, [PeriodStart]) as n
                    FROM
                        PO_UD
                    UNPIVOT
                        (
    	                Field_Value
    	                    FOR
    	                Field
    	                    IN
    	                    (
			                [Pupil numbers and capacity.YR PAN]
			                ,[Pupil numbers and capacity.Y7 PAN]
			                ,[Pupil numbers and capacity.Y12 PAN]
                            ,[Pupil numbers and capacity.% accepted applications vs viability YR-Y6]
                            ,[Pupil numbers and capacity.% accepted applications vs viability Y7-Y11]
			                ,[Pupil numbers and capacity.% accepted applications vs viability Y12-Y14]
    		                )
    		                ) AS UNPVT
                ),


                Historic_PO AS
                (
                --this is the initial data entered when the project was first created.
                select
                    [RID]
                    ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN Field_Value = ''NULL'' then NULL ELSE Field_Value END AS [Value]
	                ,PeriodStart as [CreatedDate]
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    Unpivot_PO_UD
                where
                    n = 1

                UNION ALL

                --get all the changes since project created
                select [RID]
                    ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN NewValue = ''NULL'' then NULL ELSE NewValue END AS [Value]
	                ,CreatedDate
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    (
                    select
	                    *
                        ,case when StartValue = NewValue or NewValue is null then ''N'' else ''Y'' end as ValueUpdatesIndicator
                    from
	                 (
                        select
		                    T1.*, T2.NewValue, T2.CreatedDate
		                from 
                            (select [RID],[p_rid], Field, Field_Value as ''StartValue'', [PeriodStart], [UpdatedByUserId], n from Unpivot_PO_UD) T1
                            left join 
                            (select [RID],[p_rid], Field, Field_Value as ''NewValue'', [PeriodStart] as ''CreatedDate'', [UpdatedByUserId], n from Unpivot_PO_UD) T2
                            on T1.RID = T2.RID and T1.Field = T2.Field and T1.n = T2.n - 1
                        )T3
                    )T4
                where ValueUpdatesIndicator = ''Y''
                ),

                -----------------------------------------------------------------------------------------------------------------------------------------
                /*
                Historic data for the following fields - all from the RAG table
                Overall RAG rating

                --methodology follows the same comments as the KPI history section
                */
                RAG_UD AS
                    (
                    SELECT 
                         ISNULL(cast([RID] as varchar(255)),''NULL'') AS [RID]
                        ,ISNULL(cast([p_rid] as varchar(255)),''NULL'') AS [p_rid]
		                ,ISNULL(cast([Rag Ratings.Overall RAG rating] as varchar(255)),''NULL'') AS [Rag Ratings.Overall RAG rating]
                        ,ISNULL(cast([PeriodEnd] as varchar(255)),''NULL'') AS [PeriodEnd]
                        ,ISNULL(cast([PeriodStart] as varchar(255)),''NULL'') AS [PeriodStart]
		                ,ISNULL(cast([UpdatedByUserId] as varchar(255)),''NULL'') AS [UpdatedByUserId]
                    FROM
                        [dbo].[RAG] for system_time all
	                ),


                Unpivot_RAG_UD as
                (
                    select     
                        [RID]
                        ,[p_rid]
    	                ,Field
    	                ,Field_Value
    	                ,[PeriodStart]
		                ,[UpdatedByUserId]
                        ,ROW_NUMBER() over (partition by [RID], Field order by [RID], Field, [PeriodStart]) as n
                    FROM
                        RAG_UD
                    UNPIVOT
                        (
    	                Field_Value
    	                    FOR
    	                Field
    	                    IN
    	                    (
                            [Rag Ratings.Overall RAG rating]
    		                )
    		                ) AS UNPVT
                ),


                Historic_RAG AS
                (
                --this is the initial data entered when the project was first created.
                select
                    [RID]
                    ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN Field_Value = ''NULL'' then NULL ELSE Field_Value END AS [Value]
	                ,PeriodStart as [CreatedDate]
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    Unpivot_RAG_UD
                where
                    n = 1

                UNION ALL

                --get all the changes since project created
                select [RID]
                    ,CASE WHEN [p_rid] = ''NULL'' then NULL ELSE [p_rid] END AS [p_rid]
                    ,Field
	                ,CASE WHEN NewValue = ''NULL'' then NULL ELSE NewValue END AS [Value]
	                ,CreatedDate
	                ,CASE WHEN [UpdatedByUserId] = ''NULL'' then NULL ELSE [UpdatedByUserId] END AS [UpdatedByUserId]
                from
                    (
                    select *,
                        case when StartValue = NewValue or NewValue is null then ''N'' else ''Y'' end as ValueUpdatesIndicator
                    from
	                 (
                        select
		                    T1.*, T2.NewValue, T2.CreatedDate
		                from 
                            (select [RID],[p_rid], Field, Field_Value as ''StartValue'', [PeriodStart], [UpdatedByUserId], n from Unpivot_RAG_UD) T1
                            left join 
                            (select [RID],[p_rid], Field, Field_Value as ''NewValue'', [PeriodStart] as ''CreatedDate'', [UpdatedByUserId], n from Unpivot_RAG_UD) T2
                            on T1.RID = T2.RID and T1.Field = T2.Field and T1.n = T2.n - 1
                        )T3
                    )T4
                where ValueUpdatesIndicator = ''Y''
                ),

                -----------------------------------------------------------------------------------------------------------------------------------------------------------
                --put all the historic data into one table and remove the suffixes from the field names

                Final_Historic_Data AS
                (
                select 
                    HistoricData.p_rid
                    ,ID_Wave_Data.*
                    ,HistoricData.Field
                    ,HistoricData.[Value]
                    ,HistoricData.CreatedDate
                    ,HistoricData.[UpdatedByUserId]
                from
                    (
                    select * from Historic_Milestones
    
                    UNION
    
                    select * from Historic_KPI
    
                    UNION
    
                    select * from Historic_PO
    
                    UNION
    
                    select * from Historic_RAG
                    )HistoricData
                    left join
                    (
                    select distinct RID, [Project Status.Project ID], [Project Status.Free school application wave], [Project Status.Project status] as ''CurrentStatus'' from [dbo].[KPI]
                    where [Project Status.Project ID] is not null
                    UNION
                    select distinct RID, [Project Status.Project ID], [Project Status.Free school application wave], [Project Status.Project status] as ''CurrentStatus'' from [dbo].[KPIHistory]
                    where [Project Status.Project ID] is not null and RID not in (select RID from [dbo].[KPI])
                    )ID_Wave_Data
                    on HistoricData.RID = ID_Wave_Data.RID
                )

                select 
                    Final_Historic_Data.*
	                ,Email
                from
                    Final_Historic_Data
                    left join
                    (select [Id], [Email] from [mfsp].[User])Users
                    on Final_Historic_Data.UpdatedByUserId = Users.Id
                ');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE [dbo].[sp_DataHistory] 
            ");
        }
    }
}
