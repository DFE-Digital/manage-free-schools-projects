using Dfe.ManageFreeSchoolProjects.API.Contracts.Project.Tasks;
using Dfe.ManageFreeSchoolProjects.Constants;
using Dfe.ManageFreeSchoolProjects.Logging;
using Dfe.ManageFreeSchoolProjects.Models;
using Dfe.ManageFreeSchoolProjects.Services;
using Dfe.ManageFreeSchoolProjects.Services.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace Dfe.ManageFreeSchoolProjects.Pages.Project.Tasks.PreFundingAgreementCheckpointMeeting
{
    public class EditPreFundingAgreementCheckpointMeetingModel(
        IGetProjectByTaskService getProjectService,
        ErrorService errorService,
        IUpdateProjectByTaskService updateProjectTaskService,
        ILogger<EditPreFundingAgreementCheckpointMeetingModel> logger) : PageModel
    {
        [BindProperty(SupportsGet = true, Name = "projectId")]
        public string ProjectId { get; set; }

        public GetProjectByTaskResponse Project { get; set; }

        [BindProperty(Name = "date-of-the-internal-review-meeting", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date of the internal review meeting")]
        public DateTime? DateOfTheInternalReviewMeeting { get; set; }

        [BindProperty(Name = "date-of-the-formal-meeting", BinderType = typeof(DateInputModelBinder))]
        [DisplayName("Date of the formal meeting")]
        public DateTime? DateOfTheFormalMeeting { get; set; }

        [BindProperty(Name = "why-meeting-not-held")]
        public string WhyMeetingWasNotHeld { get; set; }

        [BindProperty(Name = "commissioned-external-expert")]
        public bool? CommissionedExternalExpert { get; set; }

        [BindProperty(Name = "saved-meeting-note-in-workplaces-folder")]
        public bool? SavedMeetingNoteInWorkplacesFolder { get; set; }

        [BindProperty(Name = "sent-an-email=to-the-trust")]
        public bool? SentAnEmailToTheTrust { get; set; }

        [BindProperty(Name = "type-of-meeting-held")]
        public TypeOfMeetingHeld TypeOfMeetingHeld { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Project = await getProjectService.Execute(ProjectId, TaskName.PreFundingAgreementCheckpointMeeting);

            var pfacmTask = Project.PreFundingAgreementCheckpointMeetingTask;

            TypeOfMeetingHeld = pfacmTask.TypeOfMeetingHeld;

            var dateOfTheMeeting = pfacmTask.DateOfTheMeeting;

            DateOfTheInternalReviewMeeting = TypeOfMeetingHeld == TypeOfMeetingHeld.InternalReviewMeeting
                ? dateOfTheMeeting
                : null;

            DateOfTheFormalMeeting = TypeOfMeetingHeld == TypeOfMeetingHeld.FormalCheckpointMeeting
                ? dateOfTheMeeting
                : null;

            WhyMeetingWasNotHeld = pfacmTask.WhyMeetingWasNotHeld;

            CommissionedExternalExpert =
                pfacmTask.CommissionedExternalExpert;

            SavedMeetingNoteInWorkplacesFolder =
                pfacmTask.SavedMeetingNoteInWorkplacesFolder;

            SentAnEmailToTheTrust =
                pfacmTask.SentAnEmailToTheTrust;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                CheckForErrors("date-of-the-internal-review-meeting", TypeOfMeetingHeld.InternalReviewMeeting, DateOfTheInternalReviewMeeting);
                CheckForErrors("date-of-the-formal-meeting", TypeOfMeetingHeld.FormalCheckpointMeeting, DateOfTheFormalMeeting);

                ClearNotApplicableValues();

                if (!ModelState.IsValid)
                {
                    Project = await getProjectService.Execute(ProjectId, TaskName.PreFundingAgreementCheckpointMeeting);
                    errorService.AddErrors(ModelState.Keys, ModelState);
                    return Page();
                }

                var request = BuildUpdateRequest();

                await updateProjectTaskService.Execute(ProjectId, request);

                return Redirect(string.Format(RouteConstants.ViewPreFundingAgreementCheckpointMeeting, ProjectId));
            }
            catch (Exception e)
            {
                logger.LogErrorMsg(e);
                throw;
            }
        }

        private UpdateProjectByTaskRequest BuildUpdateRequest()
        {
            return new UpdateProjectByTaskRequest
            {
                PreFundingAgreementCheckpointMeetingTask = new PreFundingAgreementCheckpointMeetingTask
                {
                    DateOfTheMeeting = DateOfTheInternalReviewMeeting ?? DateOfTheFormalMeeting,
                    TypeOfMeetingHeld = TypeOfMeetingHeld,
                    WhyMeetingWasNotHeld = WhyMeetingWasNotHeld,
                    CommissionedExternalExpert = CommissionedExternalExpert,
                    SavedMeetingNoteInWorkplacesFolder = SavedMeetingNoteInWorkplacesFolder,
                    SentAnEmailToTheTrust = SentAnEmailToTheTrust
                }
            };
        }

        private void CheckForErrors(string id, TypeOfMeetingHeld typeOfMeetingHeld, DateTime? dateOfMeeting)
        {
            if (ModelState.IsValid && TypeOfMeetingHeld == typeOfMeetingHeld && dateOfMeeting == null)
            {
                errorService.AddErrors(ModelState.Keys, ModelState);
            }

            if (TypeOfMeetingHeld != typeOfMeetingHeld)
            {
                ModelState.Keys.Where(errorKey => errorKey.StartsWith(id))
                    .ToList()
                    .ForEach(errorKey => ModelState.Remove(errorKey));
            }
        }

        private void ClearNotApplicableValues()
        {
            switch (TypeOfMeetingHeld)
            {
                case TypeOfMeetingHeld.FormalCheckpointMeeting:
                    DateOfTheInternalReviewMeeting = null;
                    WhyMeetingWasNotHeld = null;
                    return;
                case TypeOfMeetingHeld.InternalReviewMeeting:
                    DateOfTheFormalMeeting = null;
                    WhyMeetingWasNotHeld = null;
                    return;
                case TypeOfMeetingHeld.NoMeetingHeld:
                    DateOfTheInternalReviewMeeting = null;
                    DateOfTheFormalMeeting = null;
                    return;
            }

            DateOfTheInternalReviewMeeting = null;
            DateOfTheFormalMeeting = null;
            WhyMeetingWasNotHeld = null;
        }
    }
}
