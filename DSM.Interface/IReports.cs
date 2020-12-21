using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface IReports
    {
        CommonResponse ViewMultipleCheckListJobForReports(string checkListJobStartTime, string checkListJobEndTime, long userId);
        CommonResponse CheckListForSupervisorApproval(int checkListJobId, int checkListJobGroupId, long userId);
        CommonResponse CheckListForSupervisorApprovalAll(int checkListJobId, long userId);
        CommonResponse CheckForJobApprovalByCheckListJobIdAndCheckListGroupId(int checkListJobId, int checkListJobGroupId, long userId);

        CommonResponse BarChartAllCheckListAverageTimeTakenOverAllMonthly(string fromDate, string toDate, long userId);
        CommonResponseTable LineChartAllCheckListAverageTimeTakenOverAll(string fromDate, string toDate, long userId);
        CommonResponse BarChartAllCheckListAverageTimeTakenOverAllLineWise(string fromDate, string toDate, int lineNumber, long userId);
        CommonResponse BarChartAllCheckListDetailedChangeOverLineWise(string fromDate, string toDate, int lineNumber,string groupIds, long userId);
        CommonResponse PieChartAllActivityTimeTakenByCheckListJobId(string checkListJobName, string groupName, long userId);

        CommonResponse PieChartAllJobsDetails(long userId);
        CommonResponse BarChartAllCheckListAverageTimeTaken(long userId);
        CommonResponse DonutChartAllActivityAverageTimeTaken(int checkListId,long userId);
    
    }
}
