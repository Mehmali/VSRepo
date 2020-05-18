using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounts.Models
{
    //added
    public class MyModels
    {
    }
    public class Units
    {
        public decimal PID { get; set; }
        public string LoomNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Unit { get; set; }
        public string ShiftCode { get; set; }
        public DateTime ShiftDate { get; set; }
        public string RPM { get; set; }
        public string Status { get; set; }
        public string CurrentStatus { get; set; }
    }
    public class ShedMaster
    {
        public string ShedID { get; set; }
        public List<ShedDetail> detail = new List<ShedDetail>();
    }
    public class ShedDetail
    {
        public string LineDescription { get; set; }
        public string NoOfLooms { get; set; }

    }

    public class UnitList
    {
        public int UnitID { get; set; }
        public string UnitDescription { get; set; }
        public string UnitName { get; set; }
        public string UnitLocation { get; set; }
        public Boolean Archieved { get; set; }
    }

    public class ShedList
    {
        public int ShedID { get; set; }
        public string ShedDescription { get; set; }
        public string ShedStyle { get; set; }
        public string ShedNumber { get; set; }
        public string ShedName { get; set; }
        public string ShedUnit { get; set; }
        public int UnitID { get; set; }

    }

    public class ShedLinesandLooms
    {
        public int ShedNumber { get; set; }
        public int Slid { get; set; }
        public int Nooflooms { get; set; }
        public int ShedLines { get; set; }
        public List<ShedWiseLines> ShedLineDetails { get; set; }
    }

    public class Dashboard
    {
        public int MacID { get; set; }
        public int ShedID { get; set; }
        public string ShedName { get; set; }
        public int MacLine { get; set; }
        public string Value { get; set; }
        public string ErrorTime { get; set; }
        public string ErrorClass { get; set; }
    }

    public class StopHistory
    {
        public string ErrorCode { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
    public class DashboardStops
    {

        public string AR { get; set; }
        public string DOF { get; set; }
        public string KN { get; set; }
        public string ELEMEC { get; set; }
        public string OK { get; set; }
        public string OS { get; set; }
        public string PO { get; set; }
        public string TST { get; set; }
        public string WF { get; set; }
        public string WP { get; set; }
        public string BST { get; set; }

        public decimal ARP { get; set; }
        public decimal DOFP { get; set; }
        public decimal KNP { get; set; }
        public decimal ELEMECP { get; set; }
        public decimal OKP { get; set; }
        public decimal OSP { get; set; }
        public decimal POP { get; set; }
        public decimal TSTP { get; set; }
        public decimal WFP { get; set; }
        public decimal WPP { get; set; }
        public decimal BSTP { get; set; }

        public string ART { get; set; }
        public string DOFT { get; set; }
        public string KNT { get; set; }
        public string ELEMECT { get; set; }
        public string OKT { get; set; }
        public string OST { get; set; }
        public string POT { get; set; }
        public string TSTT { get; set; }
        public string WFT { get; set; }
        public string WPT { get; set; }
        public string BSTT { get; set; }

        public string RunningMac { get; set; }
        public string ShiftStartPic { get; set; }
        public string ShiftCurrentPic { get; set; }
        public string ShiftTotalPic { get; set; }
        public string RPM { get; set; }
        public string MaxRPM { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftDuration { get; set; }
        public string MacStatus { get; set; }
        public string RunningQuality { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public List<StopHistory> lstStopHistory = new List<StopHistory>();
    }

    public class ShedLine
    {
        public int SLID { get; set; }
        public string LineDescription { get; set; }
    }
    public class MacPosition
    {
        public int ID { get; set; }
        public int MacPositionID { get; set; }
    }

    public class SizingMasterPro
    {
        public string WONO { get; set; }
        public string EntryDate { get; set; }
        public DateTime sysTimeStamp { get; set; }
        public string QualityCode { get; set; }
        public string WarpCount { get; set; }
        public decimal TotalEnds { get; set; }
        public string ReddCount { get; set; }
        public string Shade { get; set; }
        public string Construction { get; set; }

        public List<SizingDetailPro> detail { get; set; }
    }

    public class SizingDetailPro
    {
        public string WONO { get; set; }
        public byte Srno { get; set; }
        public string BeamNo { get; set; }
        public decimal TotalEnds { get; set; }
        public decimal BeamLength { get; set; }
        public bool Archived { get; set; }
        public string Qualitycode { get; set; }
        public string BeamPosition { get; set; }
        public string IsAssigned { get; set; }
        public byte MSrno { get; set; }
    }

    public class SizingStockIndex
    {
        public string WONO { get; set; }
        public string EntryDate { get; set; }
        public string QualityCode { get; set; }
        public int BeamCount { get; set; }
        public decimal TotalLength { get; set; }

    }

    public class SizingStockDetailForUpdate
    {
        public string WONO { get; set; }
        public string BeamNo { get; set; }
        public int Srno { get; set; }
    }


    public class BeamAssigmentList
    {
        public DateTime PlanDate { get; set; }
        public int MacID { get; set; }
        public int Sno { get; set; }
        public string WONO { get; set; }
        public string SizedBeam { get; set; }
        public string QualityCode { get; set; }
        public decimal PicPerInch { get; set; }
        public string ShiftCode { get; set; }
        public byte Panel { get; set; }
        public Boolean Archived { get; set; }
        public byte Insertion { get; set; }
        public string Construction { get; set; }
        public string RWONO { get; set; }
        public string RSizedBeam { get; set; }
    }
    //public class QualityMaster
    //{
    //    public string QualityCode { get; set; }
    //    public string WarpCount { get; set; }
    //    public string WeftCount { get; set; }
    //    public string PicksPerInch { get; set; }
    //    public string EndsPerInch { get; set; }
    //    public string Width { get; set; }
    //    public string Weave { get; set; }
    //    public string Twill { get; set; }
    //    public string TotalEnds { get; set; }
    //    public int reedCount { get; set; }
    //    public string Construction { get; set; }
    //    public int Insertions { get; set; }

    //}

    public class QualityMaster
    {
        public string QualityCode { get; set; }
        public string QualityName { get; set; }
        public string StdEfficiency { get; set; }
        public string PlanEfficiency { get; set; }
        public string WarpCount { get; set; }
        public string WPLY { get; set; }
        public string WYarnType { get; set; }
        public string WCountSys { get; set; }
        public string WeftCount { get; set; }
        public string WFPLY { get; set; }
        public string WFYarnType { get; set; }
        public string WFCountSys { get; set; }
        public string PicksPerInch { get; set; }
        public string EndsPerInch { get; set; }
        public string Panel { get; set; }
        public int Insertions { get; set; }
        public Int64 ProductionPicks { get; set; }
        public string Weave { get; set; }
        public string Contraction { get; set; }
        public string WarpCountAuto { get; set; }
        public string WeftCountAuto { get; set; }
        public string Width { get; set; }
        public string Width1 { get; set; }
        public string Width2 { get; set; }
        public string Width3 { get; set; }
        public string Construction { get; set; }
        public string Color { get; set; }
        public string Twill { get; set; }
        public string TotalEnds { get; set; }
        public int reedCount { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int RecNo { get; set; }
    }

    public class ArticleMaster
    {
        public string ArticleCode { get; set; }
        public string Description { get; set; }
        public List<ArticleDetail> detail = new List<ArticleDetail>();
    }
    public class ArticleDetail
    {
        public string DDescription { get; set; }
        public string DType { get; set; }

    }
    public class DataLog
    {
        public string UID { get; set; }
        public string MacID { get; set; }
        public string RefCode { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public decimal ReFval { get; set; }
        public decimal RPM { get; set; }
        public float ENumeric1 { get; set; }
        public float Enumeric2 { get; set; }
        public string EANumeric1 { get; set; }
        public string EANumeric2 { get; set; }
        public string ShiftCode { get; set; }
        public string SizedBeam { get; set; }
    }

    public class ReportsCriteria
    {
        public string ReportType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ExportType { get; set; }

    }

    //Model Class for the yarn types list index
    public class tblYarnTypes
    {
        public int YTypeCode { get; set; }
        public string YTypeShort { get; set; }
        public string YTypeDesc { get; set; }
    }

    public class LineWiseMacInfo
    {
        public int MacID { get; set; }
        public string MacDesc { get; set; }
        public int MacPosition { get; set; }
        public int MyShedLine { get; set; }
    }
    public class ShedWiseLines
    {
        public int ShedLine { get; set; }
        public List<LineWiseMacInfo> LineMachines { get; set; }
    }

    public class tblMacGroups
    {
        public int MacGroupID { get; set; }
        public string MacShortDesc { get; set; }
        public string MacGroupDesc { get; set; }
    }

    public class MacMaster
    {
        public int MacId { get; set; }
        public string MacAddress { get; set; }
        public int UnitID { get; set; }
        public int ShedID { get; set; }
        public string MacDesc { get; set; }
        public string MacDetails { get; set; }
        public int MacWidth { get; set; }
        public Boolean Archived { get; set; }
        public int NoOfPanels { get; set; }
        public int MacGroupID { get; set; }
        public int MacSpeed { get; set; }
        public int ShedLineID { get; set; }
        public int MacPosition { get; set; }
        public bool isAdd { get; set; }
    }


    public class DashBoardData
    {
        public int MacID { get; set; }
        public decimal PicCounted { get; set; }
        public DateTime ShiftStartTime { get; set; }
        public DateTime UptoTime { get; set; }
        public decimal AvgRpm { get; set; }
        public int WefC { get; set; }
        public decimal WefT { get; set; }
        public int WarC { get; set; }
        public decimal WarT { get; set; }
        public int OthC { get; set; }
        public decimal OthT { get; set; }
        public int MecC { get; set; }
        public decimal MecT { get; set; }
        public int EleC { get; set; }
        public decimal EleT { get; set; }
        public int ArtC { get; set; }
        public decimal ArtT { get; set; }
        public int KntC { get; set; }
        public decimal KntT { get; set; }
        public int GatC { get; set; }
        public decimal GatT { get; set; }
        public int DofC { get; set; }
        public decimal DofT { get; set; }
        public int WefShC { get; set; }
        public decimal WefShT { get; set; }
        public int BeamShC { get; set; }
        public decimal BeamShT { get; set; }
        public int MacPosition { get; set; }
        public string macdesc { get; set; }
        public int ShedLineID { get; set; }
        public int ShedID { get; set; }
        public int UnitID { get; set; }
        public decimal ShortMinues { get; set; }
        public decimal LongStopsMinues { get; set; }
        public string EffData { get; set; }

        public string QualityCode { get; set; }
        public string ShiftCode { get; set; }
        public int ShiftHours { get; set; }
        public int TotalLooms { get; set; }
    }

    public class DashBoardGraphs
    {
        public string RefCode { get; set; }
        public string CodeId { get; set; }
        public int RefCount { get; set; }
    }
    public class ClassLoadLatestErr
    {
        public int MacID { get; set; }
        public int UnitID { get; set; }
        public int ShedID { get; set; }
        public int ShedLineID { get; set; }
        public int MacPosition { get; set; }
        public string LastErr { get; set; }
        public DateTime TimeIn { get; set; }


    }

    public class DashBoardGraphSliceDet
    {

        public int MacId { get; set; }
        public string Qualitycode { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double Tminutes { get; set; }

    }

    public class classUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public string EmailAddress { get; set; }
        public string NewPassword { get; set; }
    }

    public class UserPermissions
    {
        public int ID { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string MenuDesc { get; set; }
        public int IsAllowed { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
    }

    public class tblMacLine
    {
        public int MacID { get; set; }
        public int ShedID { get; set; }
        public string ShedLineID { get; set; }

        public int MacPosition { get; set; }
    }
}