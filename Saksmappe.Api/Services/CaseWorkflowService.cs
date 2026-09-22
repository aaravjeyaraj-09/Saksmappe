using Saksmappe.Api.Enums;

namespace Saksmappe.Api.Services;

public class CaseWorkflowService
{
    public bool CanTransition(CaseStatus currentStatus, CaseStatus newStatus)
    {
        return currentStatus switch
        {
            CaseStatus.Received => 
            newStatus == CaseStatus.DocumentCheck,
            
            CaseStatus.DocumentCheck =>
            newStatus == CaseStatus.UnderReview,

            CaseStatus.UnderReview =>
            newStatus == CaseStatus.WaitingForInformation ||
            newStatus == CaseStatus.DecisionPending,

            CaseStatus.WaitingForInformation =>
            newStatus == CaseStatus.UnderReview,

            CaseStatus.DecisionPending =>
            newStatus == CaseStatus.Approved ||
            newStatus == CaseStatus.Rejected,

            CaseStatus.Approved =>
            newStatus == CaseStatus.Closed,

            CaseStatus.Rejected =>
            newStatus == CaseStatus.Closed,

            CaseStatus.Closed => false,
            _ => false,

        };
    }
}

            