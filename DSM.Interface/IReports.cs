using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.ReportsEntity;

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



        //veeresh code 
        CommonResponse BarChartAllCheckListJObForYear();
        CommonResponseTable BarChartAllCheckListJObForMonth();
        CommonResponse BarChartAllCheckListJObForMonthLine1();
        CommonResponse BarChartAllCheckListJObForMonthLine2();

        CommonResponse ChangeOverTimeReport(COReport data);

    }
}
