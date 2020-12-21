using DSM.DBModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using static DSM.EntityModels.CommonEntity;
using System.Threading.Tasks;
using MailTracker = DSM.EntityModels.CommonEntity.MailTracker;
using System.Xml;
using static DSM.EntityModels.AssetEntity;
using DSM.DAL.Helpers;
using Microsoft.Extensions.Options;

namespace DSM.DAL.App_Start
{
    public class CommonFunction
    {
        DSMContext db = new DSMContext();
        public readonly IHostingEnvironment _hostingEnvironment;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CheckListActivityMasterDAL));

        /// <summary>
        /// Generate a random number between two numbers   
        /// </summary>
        /// <returns></returns>
        public string RandomNumber()
        {
            int min= 1, max = 999999999;
            Random random = new Random();
            string barCode = random.Next(min, max).ToString("D9");

            var dbCheck = (from wf in db.AssetMaster
                          where wf.IsDeleted == false && wf.BarcodeAllocatedNumber == barCode
                          select wf).ToList();
            

            if (dbCheck.Count > 0)
            {
               RandomNumber();
            }
            return barCode;
        }

        /// <summary>
        /// Read XML Data and Modify
        /// </summary>
        /// <param name="data"></param>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        public CommonResponse ReadXML(BarCodePrinter data, AppSettings appSettings)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                string path = Path.Combine(appSettings.BarCodeTemplate);
                XmlTextReader reader = new XmlTextReader(path);
                XmlDocument doc = new XmlDocument();
                int counter = 0;
                doc.Load(reader);
                string fileName = data.assetName + ".ezpx";
                string newPath = Path.Combine(appSettings.ImageUrlSave, fileName);
                var retrivalPath = Path.Combine(appSettings.ImageUrl, fileName);
                CommonFunction common = new CommonFunction();
                foreach (XmlNode node in doc.DocumentElement.SelectNodes("Label/qlabel/GraphicShape/Data"))
                {
                    string text = node.InnerText; //or loop through its children as well
                    switch (counter)
                    {
                        //case 0:
                        //    node.InnerText =  data.assetName;
                        //    break;
                        case 0:
                            node.InnerText = data.assetNumber;
                            break;
                        default:
                            break;
                    }
                    counter++;
                }

               
                if (File.Exists(Path.Combine(newPath)))
                {
                    File.Delete(Path.Combine(newPath));
                }
                try
                {
                    doc.Save(newPath);
                }
                catch (Exception ex)
                {
                }
                reader.Close();
                obj.isStatus = true;
                obj.response = retrivalPath;

            }
            catch (Exception ex)
            {
                obj.isStatus = false;
                obj.response = Resource.ResourceResponse.ExceptionMessage;
            }


            return obj;
        }

        /// <summary>
        /// DownloadAllQrCode
        /// </summary>
        /// <param name="isAll"></param>
        /// <param name="data"></param>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        public CommonResponse DownloadAllQrCode(bool isAll,BarCodePrinter data, AppSettings appSettings)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                string fileName = "";
                var check = db.QrCode.FirstOrDefault();
                string allData = "";
                if (isAll == true)
                {
                    fileName = "AllQRCode.prn";
                   
                    var assetLibary = db.AssetMaster.Where(m => m.IsDeleted == false).ToList();
                   
                    string masterQrcode = check.QrCode1;
                    foreach (var asset in assetLibary)
                    {
                        string qrCode = masterQrcode;
                        qrCode = qrCode.Replace("$$$$$",asset.BarcodeAllocatedNumber);
                        qrCode = qrCode.Replace("$$$$", asset.AssetName + " " + db.LineNumberMaster.Where(m=>m.LineNumberId==asset.LineNumber).Select(m=>m.LineNumberName).FirstOrDefault());
                        qrCode = qrCode.Replace("$$$", appSettings.CompanyName);
                        allData = allData + qrCode + "\n\n";
                    }
                }
                else
                {
                    fileName = data.assetName + ".prn";
                    string masterQrcode = check.QrCode1;
                    string qrCode = masterQrcode;
                    qrCode = qrCode.Replace("$$$$$", data.assetNumber);
                    qrCode = qrCode.Replace("$$$$", data.assetName + " " + data.lineNumber);
                    qrCode = qrCode.Replace("$$$", appSettings.CompanyName);
                    allData = allData + qrCode + "\n\n";

                }

                string newPath = Path.Combine(appSettings.ImageUrlSave, fileName);
                var retrivalPath = Path.Combine(appSettings.ImageUrl, fileName);
                CommonFunction common = new CommonFunction();
                
                try
                {
                    if (File.Exists(Path.Combine(newPath)))
                    {
                        File.Delete(Path.Combine(newPath));
                    }
                    using (StreamWriter writetext = new StreamWriter(newPath))
                    {
                        writetext.WriteLine(allData);
                    }
                }
                catch (Exception ex)
                {
                }

                obj.isStatus = true;
                obj.response = retrivalPath;

            }
            catch (Exception ex)
            {
                obj.isStatus = false;
                obj.response = Resource.ResourceResponse.ExceptionMessage;
            }


            return obj;
        }

        /// <summary>
        /// Dynamic Slip Number
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public string DynamicVersionNumber(string checkListName,int count = 0)
        {
            //Syntax to genrate : checkListName-dd/MM/YY-VN
            string versionNumber = "";
            try
            {

                DateTime todayDate = DateTime.Now;
                string fromDate = DateTime.Now.ToShortDateString() + " 00:00:00";
                string toDate = DateTime.Now.ToShortDateString() + " 23:59:59";

                DateTime fDate = Convert.ToDateTime(fromDate);
                DateTime tDate = Convert.ToDateTime(toDate);
                if (count == 0)
                {
                    count = db.CheckListMaster.Where(m => m.CheckListName == checkListName && m.IsDeleted == false).ToList().Count();
                    count = 1;
                }
                else
                {
                    count++;
                }

                string tdyDate = todayDate.ToString("dd/MM/yy");
                tdyDate = tdyDate.Replace('-', '/');

                versionNumber = checkListName + "-" + tdyDate + "-V" + (count).ToString("D1");

                var dbCheck = db.CheckListMaster.Where(m => m.CheckListVersion == versionNumber && m.IsDeleted == false).ToList();

                if (dbCheck.Count == 0)
                {
                    return versionNumber;
                }
                else
                {
                    versionNumber = DynamicVersionNumber(checkListName,count);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
            }

            return versionNumber;
        }

        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public dynamic GetUserDetails(long userId)
        {
            var userDetails = db.UserDetails.Where(m => m.UserId == userId).FirstOrDefault();
            return userDetails;
        }

        /// <summary>
        /// File To Base 64
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string FileToBase64(string path)
        {
            string base64ImageRepresentation = "";
            try
            {
                base64ImageRepresentation = Convert.ToBase64String(File.ReadAllBytes(path));
            }
            catch (Exception ex)
            {
            }
            return base64ImageRepresentation;
        }

        /// <summary>
        /// Get Date Difference
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public string GetDateDifference(DateTime? startDateTime, DateTime? endDateTime)
        {
            string response = "";
            try
            {
                if (startDateTime != null && endDateTime != null)
                {
                    DateTime st = Convert.ToDateTime(startDateTime);
                    DateTime et = Convert.ToDateTime(endDateTime);
                    var seconds = (st - et).TotalSeconds;
                    TimeSpan time = TimeSpan.FromSeconds(seconds);

                    response = time.ToString(@"hh\:mm\:ss");
                    try
                    {
                        string[] arry = response.Split(':');
                        response = arry[0] + " Hrs " + arry[1] + " Mins " + arry[2] + " Secs";
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }

        /// <summary>
        /// GetDateDifferenceInMins
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public decimal GetDateDifferenceInMins(DateTime? startDateTime, DateTime? endDateTime)
        {
            decimal response = 0;
            try
            {
                if (startDateTime != null && endDateTime != null)
                {
                    DateTime st = Convert.ToDateTime(startDateTime);
                    DateTime et = Convert.ToDateTime(endDateTime);
                    var mins = (et - st).TotalMinutes;
                    //TimeSpan time = TimeSpan.FromSeconds(seconds);

                    response = Math.Round(Convert.ToDecimal(mins),2);
                    if (response < 0)
                    {
                        response = response * (-1);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }

        /// <summary>
        /// GetJobStatus
        /// </summary>
        /// <param name="IsJobClosed"></param>
        /// <param name="IsAdminApproved"></param>
        /// <param name="CheckListJobIsCompleted"></param>
        /// <param name="IsJobRejected"></param>
        /// <param name="isNotYetStarted"></param>
        /// <returns></returns>
        public string GetJobStatus(bool? IsJobClosed,bool? IsAdminApproved,bool? CheckListJobIsCompleted, bool? IsJobRejected,bool isNotYetStarted)
        {
            string jobStatus = "In Process";
            try
            {
                if (IsJobClosed == true)
                {
                    if (IsAdminApproved == true)
                    {
                        //green static
                        //jobStatus = "Job Closed And Approved By Admin";
                        jobStatus = "Job Approved";
                    }
                    else
                    {
                        //jobStatus = "Job Closed And Not Approved By Admin";
                        //jobStatus = "Job Closed And Not Approved By Admin";
                    }
                }
                else if (CheckListJobIsCompleted == true && IsJobClosed == false && IsJobRejected != true)
                {
                    if (IsAdminApproved == true)
                    {
                        //green blink
                        //jobStatus = "Job Completed By Operator And Approved By Admin";
                        jobStatus = "Waiting For Job Closure";
                    }
                    else
                    {
                        // yellow blink
                        //jobStatus = "Job Completed By Operator And Not Approved By Admin";
                        jobStatus = "Need Job Approval";
                    }

                }
                else if (CheckListJobIsCompleted == false && IsJobRejected != true)
                {
                    //blue blink
                    //jobStatus = "Job Not Yet Completed By Operator";
                    jobStatus = "Job Ongoing";
                }
                else if (IsJobRejected == true)
                {
                    //red static
                    jobStatus = "Job Rejected";
                }

                if (isNotYetStarted != true)
                {
                    //red static
                    jobStatus = "Not Yet Started";
                }
            }
            catch (Exception ex)
            {

            }
            return jobStatus;
        }

        /// <summary>
        /// GetJobCreatedOrNotStatus
        /// </summary>
        /// <param name="checkListJobMasterId"></param>
        /// <returns></returns>
        public bool GetJobCreatedOrNotStatus(long checkListJobMasterId)
        {
            bool jobStatus = true;
            try
            {
                var job = db.CheckListJobMaster.Where(m => m.CheckListJobId == checkListJobMasterId).FirstOrDefault();

                List<int> gppids = job.CheckListGroup.Split(',').Select(int.Parse).ToList();
                foreach (var grp in gppids)
                {
                    int checkListJobGroupId = grp;

                    var checkListJobActivityMaster = db.CheckListJobActivityMaster.Where(wf=> wf.IsDeleted == false && wf.CheckListJobMasterId == checkListJobMasterId && wf.CheckListJobGroupId == checkListJobGroupId).ToList();
                    var checkListJobAdvanceMaster = db.CheckListJobAdvanceMaster.Where(wf=> wf.IsDeleted == false && wf.CheckListJobMasterId == checkListJobMasterId && wf.CheckListJobGroupId == checkListJobGroupId).ToList();

                    if (checkListJobActivityMaster.Count == 0 || checkListJobAdvanceMaster.Count == 0)
                    {
                        jobStatus = false;
                        break;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                jobStatus = false;
            }
            return jobStatus;
        }

        /// <summary>
        /// GetCheckListCreatedOrNotStatus
        /// </summary>
        /// <param name="checkListMasterId"></param>
        /// <returns></returns>
        public bool GetCheckListCreatedOrNotStatus(long checkListMasterId)
        {
            bool jobStatus = true;
            try
            {
                var job = db.CheckListMaster.Where(m => m.CheckListId == checkListMasterId).FirstOrDefault();

                List<int> gppids = job.CheckListGroup.Split(',').Select(int.Parse).ToList();
                foreach (var grp in gppids)
                {
                    int checkListJobGroupId = grp;

                    var checkListActivityMaster = db.CheckListActivityMaster.Where(wf => wf.IsDeleted == false && wf.CheckListMasterId == checkListMasterId && wf.CheckListGroupId == checkListJobGroupId).ToList();
                    var checkListAdvanceMaster = db.CheckListAdvanceMaster.Where(wf => wf.IsDeleted == false && wf.CheckListMasterId == checkListMasterId && wf.CheckListGroupId == checkListJobGroupId).ToList();

                    if (checkListActivityMaster.Count == 0 || checkListAdvanceMaster.Count == 0)
                    {
                        jobStatus = false;
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                jobStatus = false;
            }
            return jobStatus;
        }

        /// <summary>
        /// Get Type Details
        /// </summary>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        public long GetTypeDetails(AppSettings appSettings)
        {
            long typeId = 0;
            var checkListTypeMaster = db.CheckListTypeMaster.Where(m => m.CheckListTypeName == appSettings.Type).FirstOrDefault();
            if (checkListTypeMaster != null)
            {
                typeId = checkListTypeMaster.CheckListTypeId;
            }
            return typeId;
        }

        /// <summary>
        /// ChangeCheckListAndJobStepNumberOfPreviousItem
        /// </summary>
        /// <param name="checkListMasterId"></param>
        /// <param name="checkListGroupId"></param>
        /// <param name="activityType"></param>
        /// <param name="major"></param>
        /// <returns></returns>
        public int ChangeCheckListAndJobStepNumberOfPreviousItem(long? checkListMasterId,long? checkListGroupId,string activityType,string major)
        {
            int stepNumber = 0;
            try
            {
                if (major == "CheckList")
                {
                    switch (activityType)
                    {
                        case "Activity":
                            var checkListActivityMaster = db.CheckListActivityMaster.Where(m => m.CheckListMasterId == checkListMasterId && m.CheckListGroupId == checkListGroupId && m.IsDeleted == false).OrderBy(m => m.CreatedOn).ToList();
                            int i = 1;
                            foreach (var item in checkListActivityMaster)
                            {
                                item.CheckListStepNumber = i;
                                db.SaveChanges();
                                i++;
                            }
                            stepNumber = checkListActivityMaster.Count;
                            break;
                        case "Advance":
                            var checkListAdvanceMaster = db.CheckListAdvanceMaster.Where(m => m.CheckListMasterId == checkListMasterId && m.CheckListGroupId == checkListGroupId && m.IsDeleted == false).OrderBy(m => m.CreatedOn).ToList();
                            int j = 1;
                            foreach (var item in checkListAdvanceMaster)
                            {
                                item.CheckListStepNumber = j;
                                db.SaveChanges();
                                j++;
                            }
                            stepNumber = checkListAdvanceMaster.Count;
                            break;
                        case "LOTOTO":
                            var checkListLototomaster = db.CheckListLototomaster.Where(m => m.CheckListMasterId == checkListMasterId && m.CheckListGroupId == checkListGroupId && m.IsDeleted == false).OrderBy(m => m.CreatedOn).ToList();
                            int k = 1;
                            foreach (var item in checkListLototomaster)
                            {
                                item.CheckListLockStepNumber = k;
                                db.SaveChanges();
                                k++;
                            }
                            stepNumber = checkListLototomaster.Count;
                            break;
                        default: break;
                    }
                }
                else
                {
                    switch (activityType)
                    {
                        case "Activity":
                            var checkListActivityMaster = db.CheckListJobActivityMaster.Where(m => m.CheckListJobMasterId == checkListMasterId && m.CheckListJobGroupId == checkListGroupId && m.IsDeleted == false).OrderBy(m => m.CreatedOn).ToList();
                            int i = 1;
                            foreach (var item in checkListActivityMaster)
                            {
                                item.CheckListJobStepNumber = i;
                                db.SaveChanges();
                                i++;
                            }
                            stepNumber = checkListActivityMaster.Count;
                            break;
                        case "Advance":
                            var checkListAdvanceMaster = db.CheckListJobAdvanceMaster.Where(m => m.CheckListJobMasterId == checkListMasterId && m.CheckListJobGroupId == checkListGroupId && m.IsDeleted == false).OrderBy(m => m.CreatedOn).ToList();
                            int j = 1;
                            foreach (var item in checkListAdvanceMaster)
                            {
                                item.CheckListJobStepNumber = j;
                                db.SaveChanges();
                                j++;
                            }
                            stepNumber = checkListAdvanceMaster.Count;
                            break;
                        case "LOTOTO":
                            var checkListLototomaster = db.CheckListJobLototomaster.Where(m => m.CheckListJobMasterId == checkListMasterId && m.CheckListJobGroupId == checkListGroupId && m.IsDeleted == false).OrderBy(m => m.CreatedOn).ToList();
                            int k = 1;
                            foreach (var item in checkListLototomaster)
                            {
                                item.CheckListJobLockStepNumber = k;
                                db.SaveChanges();
                                k++;
                            }
                            stepNumber = checkListLototomaster.Count;
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            { }

            return stepNumber;
        }

    }

  
}