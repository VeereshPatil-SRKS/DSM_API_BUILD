using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DSM.DBModels
{
    public partial class DSMContext : DbContext
    {
        public DSMContext()
        {
        }

        public DSMContext(DbContextOptions<DSMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssetMaster> AssetMaster { get; set; }
        public virtual DbSet<CheckListActivityMaster> CheckListActivityMaster { get; set; }
        public virtual DbSet<CheckListAdvanceMaster> CheckListAdvanceMaster { get; set; }
        public virtual DbSet<CheckListCategoryMaster> CheckListCategoryMaster { get; set; }
        public virtual DbSet<CheckListGroupMaster> CheckListGroupMaster { get; set; }
        public virtual DbSet<CheckListJobActivityMaster> CheckListJobActivityMaster { get; set; }
        public virtual DbSet<CheckListJobActivityOperator> CheckListJobActivityOperator { get; set; }
        public virtual DbSet<CheckListJobAdvanceMaster> CheckListJobAdvanceMaster { get; set; }
        public virtual DbSet<CheckListJobAdvanceOperator> CheckListJobAdvanceOperator { get; set; }
        public virtual DbSet<CheckListJobAssignedResourceMaster> CheckListJobAssignedResourceMaster { get; set; }
        public virtual DbSet<CheckListJobLototomaster> CheckListJobLototomaster { get; set; }
        public virtual DbSet<CheckListJobLototooperator> CheckListJobLototooperator { get; set; }
        public virtual DbSet<CheckListJobMaster> CheckListJobMaster { get; set; }
        public virtual DbSet<CheckListJobMasterHistory> CheckListJobMasterHistory { get; set; }
        public virtual DbSet<CheckListJobWrtoperator> CheckListJobWrtoperator { get; set; }
        public virtual DbSet<CheckListLototomaster> CheckListLototomaster { get; set; }
        public virtual DbSet<CheckListMaster> CheckListMaster { get; set; }
        public virtual DbSet<CheckListSubCategoryMaster> CheckListSubCategoryMaster { get; set; }
        public virtual DbSet<CheckListTypeMaster> CheckListTypeMaster { get; set; }
        public virtual DbSet<ColorMaster> ColorMaster { get; set; }
        public virtual DbSet<DepartmentMaster> DepartmentMaster { get; set; }
        public virtual DbSet<DesignationMaster> DesignationMaster { get; set; }
        public virtual DbSet<DocumentMaster> DocumentMaster { get; set; }
        public virtual DbSet<DocumentUplodedMaster> DocumentUplodedMaster { get; set; }
        public virtual DbSet<GradeMaster> GradeMaster { get; set; }
        public virtual DbSet<HelpContentMaster> HelpContentMaster { get; set; }
        public virtual DbSet<LineNumberMaster> LineNumberMaster { get; set; }
        public virtual DbSet<QrCode> QrCode { get; set; }
        public virtual DbSet<ReAssignedcheckListJobResourcesOperator> ReAssignedcheckListJobResourcesOperator { get; set; }
        public virtual DbSet<RejectedJobHistory> RejectedJobHistory { get; set; }
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }
        public virtual DbSet<ShiftMaster> ShiftMaster { get; set; }
        public virtual DbSet<TargetOverall> TargetOverall { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
              
               // optionsBuilder.UseSqlServer("Server=TCP:54.224.28.104,1433;Database=DSM_Local;user id=sa;password=srks4$;");
               
                optionsBuilder.UseSqlServer("Server=TCP:34.236.191.1,1433;Database=DSM;user id=sa;password=srks4$;");



            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AssetMaster>(entity =>
            {
                entity.HasKey(e => e.AssetId);

                entity.ToTable("assetMaster");

                entity.Property(e => e.AssetId).HasColumnName("assetId");

                entity.Property(e => e.AssetDescription)
                    .HasColumnName("assetDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.AssetDocumentUploadedId).HasColumnName("assetDocumentUploadedId");

                entity.Property(e => e.AssetName)
                    .HasColumnName("assetName")
                    .HasMaxLength(250);

                entity.Property(e => e.BarcodeAllocatedNumber)
                    .HasColumnName("barcodeAllocatedNumber")
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LineNumber).HasColumnName("lineNumber");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckListActivityMaster>(entity =>
            {
                entity.HasKey(e => e.ActivityCheckListId);

                entity.ToTable("checkListActivityMaster");

                entity.Property(e => e.ActivityCheckListId).HasColumnName("activityCheckListId");

                entity.Property(e => e.ActivityDescription).HasColumnName("activityDescription");

                entity.Property(e => e.ActivitySubCategoryId).HasColumnName("activitySubCategoryId");

                entity.Property(e => e.AssetId).HasColumnName("assetId");

                entity.Property(e => e.CheckListGroupId).HasColumnName("checkListGroupId");

                entity.Property(e => e.CheckListMasterId).HasColumnName("checkListMasterId");

                entity.Property(e => e.CheckListStepNumber).HasColumnName("checkListStepNumber");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpectedCompletionTime).HasColumnName("expectedCompletionTime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActivityManditory)
                    .HasColumnName("isActivityManditory")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsBarCodeManditory)
                    .HasColumnName("isBarCodeManditory")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPhotoManditory)
                    .HasColumnName("isPhotoManditory")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListAdvanceMaster>(entity =>
            {
                entity.HasKey(e => e.AdvanceCheckListId)
                    .HasName("PK_advanceCheckListMaster");

                entity.ToTable("checkListAdvanceMaster");

                entity.Property(e => e.AdvanceCheckListId).HasColumnName("advanceCheckListId");

                entity.Property(e => e.ActivityBeforeChangeOverDescription).HasColumnName("activityBeforeChangeOverDescription");

                entity.Property(e => e.CheckListGroupId).HasColumnName("checkListGroupId");

                entity.Property(e => e.CheckListMasterId).HasColumnName("checkListMasterId");

                entity.Property(e => e.CheckListStepNumber).HasColumnName("checkListStepNumber");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListCategoryId);

                entity.ToTable("checkListCategoryMaster");

                entity.Property(e => e.CheckListCategoryId).HasColumnName("checkListCategoryId");

                entity.Property(e => e.CheckListCategoryDescription)
                    .HasColumnName("checkListCategoryDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListCategoryName)
                    .HasColumnName("checkListCategoryName")
                    .HasMaxLength(250);

                entity.Property(e => e.CheckListCategoryOwner)
                    .HasColumnName("checkListCategoryOwner")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckListGroupMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListGroupId);

                entity.ToTable("checkListGroupMaster");

                entity.Property(e => e.CheckListGroupId).HasColumnName("checkListGroupId");

                entity.Property(e => e.CheckListGroupDescription)
                    .HasColumnName("checkListGroupDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListGroupName)
                    .HasColumnName("checkListGroupName")
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckListJobActivityMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListJobActivityId);

                entity.ToTable("checkListJobActivityMaster");

                entity.Property(e => e.CheckListJobActivityId).HasColumnName("checkListJobActivityId");

                entity.Property(e => e.ActivityDescription).HasColumnName("activityDescription");

                entity.Property(e => e.ActivitySubCategoryId).HasColumnName("activitySubCategoryId");

                entity.Property(e => e.AssetId).HasColumnName("assetId");

                entity.Property(e => e.CheckListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.CheckListJobMasterId).HasColumnName("checkListJobMasterId");

                entity.Property(e => e.CheckListJobStepNumber).HasColumnName("checkListJobStepNumber");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpectedCompletionTime).HasColumnName("expectedCompletionTime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsActivityManditory)
                    .HasColumnName("isActivityManditory")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsBarCodeManditory)
                    .HasColumnName("isBarCodeManditory")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPhotoManditory)
                    .HasColumnName("isPhotoManditory")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListJobActivityOperator>(entity =>
            {
                entity.ToTable("checkListJobActivityOperator");

                entity.Property(e => e.CheckListJobActivityOperatorId).HasColumnName("checkListJobActivityOperatorId");

                entity.Property(e => e.ActivityEndTime)
                    .HasColumnName("activityEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActivityStartTime)
                    .HasColumnName("activityStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.BarcodeAssetId).HasColumnName("barcodeAssetId");

                entity.Property(e => e.CheckListJobActivityId).HasColumnName("checkListJobActivityId");

                entity.Property(e => e.CheckListJobOperatorId).HasColumnName("checkListJobOperatorId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJobRejected)
                    .HasColumnName("isJobRejected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JobRejectedReason).HasColumnName("jobRejectedReason");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorId).HasColumnName("operatorId");

                entity.Property(e => e.OperatorRemark).HasColumnName("operatorRemark");

                entity.Property(e => e.OperatorScannedBarcodeNumber)
                    .HasColumnName("operatorScannedBarcodeNumber")
                    .HasMaxLength(500);

                entity.Property(e => e.OperatorUpoadedDocumentId).HasColumnName("operatorUpoadedDocumentId");
            });

            modelBuilder.Entity<CheckListJobAdvanceMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListJobAdvanceId)
                    .HasName("PK_advancecheckListJobMaster");

                entity.ToTable("checkListJobAdvanceMaster");

                entity.Property(e => e.CheckListJobAdvanceId).HasColumnName("checkListJobAdvanceId");

                entity.Property(e => e.ActivityBeforeChangeOverDescription).HasColumnName("activityBeforeChangeOverDescription");

                entity.Property(e => e.CheckListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.CheckListJobMasterId).HasColumnName("checkListJobMasterId");

                entity.Property(e => e.CheckListJobStepNumber).HasColumnName("checkListJobStepNumber");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListJobAdvanceOperator>(entity =>
            {
                entity.ToTable("checkListJobAdvanceOperator");

                entity.Property(e => e.CheckListJobAdvanceOperatorId).HasColumnName("checkListJobAdvanceOperatorId");

                entity.Property(e => e.ActivityEndTime)
                    .HasColumnName("activityEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActivityStartTime)
                    .HasColumnName("activityStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckListJobAdvanceId).HasColumnName("checkListJobAdvanceId");

                entity.Property(e => e.CheckListJobOperatorId).HasColumnName("checkListJobOperatorId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJobRejected)
                    .HasColumnName("isJobRejected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JobRejectedReason).HasColumnName("jobRejectedReason");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorId).HasColumnName("operatorId");

                entity.Property(e => e.OperatorRemark).HasColumnName("operatorRemark");
            });

            modelBuilder.Entity<CheckListJobAssignedResourceMaster>(entity =>
            {
                entity.ToTable("checkListJobAssignedResourceMaster");

                entity.Property(e => e.CheckListJobAssignedResourceMasterId).HasColumnName("checkListJobAssignedResourceMasterId");

                entity.Property(e => e.CheckListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.CheckListJobMasterId).HasColumnName("checkListJobMasterId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.PrimaryResource)
                    .HasColumnName("primaryResource")
                    .HasMaxLength(1000);

                entity.Property(e => e.PrimaryResourceToAllFlag).HasColumnName("primaryResourceToAllFlag");

                entity.Property(e => e.SecondaryResource)
                    .HasColumnName("secondaryResource")
                    .HasMaxLength(1000);

                entity.Property(e => e.SecondaryResourceToAllFlag).HasColumnName("secondaryResourceToAllFlag");
            });

            modelBuilder.Entity<CheckListJobLototomaster>(entity =>
            {
                entity.HasKey(e => e.CheckListJobLototoid);

                entity.ToTable("checkListJobLOTOTOMaster");

                entity.Property(e => e.CheckListJobLototoid).HasColumnName("checkListJobLOTOTOId");

                entity.Property(e => e.CheckListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.CheckListJobLockStepNumber).HasColumnName("checkListJobLockStepNumber");

                entity.Property(e => e.CheckListJobMasterId).HasColumnName("checkListJobMasterId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsLockOutRequired)
                    .HasColumnName("isLockOutRequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTagOutRequired)
                    .HasColumnName("isTagOutRequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTryOutRequired)
                    .HasColumnName("isTryOutRequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.PositionDescription).HasColumnName("positionDescription");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListJobLototooperator>(entity =>
            {
                entity.ToTable("checkListJobLOTOTOOperator");

                entity.Property(e => e.CheckListJobLototooperatorId).HasColumnName("checkListJobLOTOTOOperatorId");

                entity.Property(e => e.ActivityEndTime)
                    .HasColumnName("activityEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActivityStartTime)
                    .HasColumnName("activityStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.checkListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.checkListJobId).HasColumnName("checkListJobId");

                entity.Property(e => e.CheckListJobLototoid).HasColumnName("checkListJobLOTOTOId");

                entity.Property(e => e.CheckListJobOperatorId).HasColumnName("checkListJobOperatorId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.isAdminDoneLototo).HasColumnName("isAdminDoneLototo");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJobRejected)
                    .HasColumnName("isJobRejected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JobRejectedReason).HasColumnName("jobRejectedReason");

                entity.Property(e => e.LockOutDoneByOperator).HasColumnName("lockOutDoneByOperator");

                entity.Property(e => e.LockOutRemark).HasColumnName("lockOutRemark");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorId).HasColumnName("operatorId");

                entity.Property(e => e.OverAllRemark).HasColumnName("overAllRemark");

                entity.Property(e => e.TagOutDoneByOperator).HasColumnName("tagOutDoneByOperator");

                entity.Property(e => e.TagOutRemark).HasColumnName("tagOutRemark");

                entity.Property(e => e.TryOutDoneByOperator).HasColumnName("tryOutDoneByOperator");

                entity.Property(e => e.TryOutRemark).HasColumnName("tryOutRemark");
            });

            modelBuilder.Entity<CheckListJobMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListJobId)
                    .HasName("PK_checkListJobMaster1");

                entity.ToTable("checkListJobMaster");

                entity.Property(e => e.CheckListJobId).HasColumnName("checkListJobId");

                entity.Property(e => e.BatchNumber)
                    .HasColumnName("batchNumber")
                    .HasMaxLength(1000);

                entity.Property(e => e.CheckListEndTime)
                    .HasColumnName("checkListEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckListGroup)
                    .IsRequired()
                    .HasColumnName("checkListGroup")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CheckListJobCategoryId).HasColumnName("checkListJobCategoryId");

                entity.Property(e => e.CheckListJobDescription).HasColumnName("checkListJobDescription");

                entity.Property(e => e.CheckListJobLineNumber).HasColumnName("checkListJobLineNumber");

                entity.Property(e => e.CheckListJobName)
                    .HasColumnName("checkListJobName")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListJobSupervisorId).HasColumnName("checkListJobSupervisorId");

                entity.Property(e => e.CheckListJobTypeId).HasColumnName("checkListJobTypeId");

                entity.Property(e => e.CheckListMasterId).HasColumnName("checkListMasterId");

                entity.Property(e => e.CheckListShiftNumber).HasColumnName("checkListShiftNumber");

                entity.Property(e => e.CheckListStartTime)
                    .HasColumnName("checkListStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurrentColor).HasColumnName("currentColor");

                entity.Property(e => e.CurrentGrade).HasColumnName("currentGrade");

                entity.Property(e => e.EstimatedEndTime).HasColumnName("estimatedEndTime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OverAllApproved)
                    .HasColumnName("overAllApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OverAllJobCompleted)
                    .HasColumnName("overAllJobCompleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OverAllRejected)
                    .HasColumnName("overAllRejected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PreviousColor).HasColumnName("previousColor");

                entity.Property(e => e.PreviousGrade).HasColumnName("previousGrade");

                entity.Property(e => e.ProcessOrderNumber)
                    .HasColumnName("processOrderNumber")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListJobMasterHistory>(entity =>
            {
                entity.HasKey(e => e.CheckListHisJobId);

                entity.ToTable("checkListJobMasterHistory");

                entity.Property(e => e.CheckListHisJobId).HasColumnName("checkListHisJobId");

                entity.Property(e => e.BatchNumber)
                    .HasColumnName("batchNumber")
                    .HasMaxLength(1000);

                entity.Property(e => e.CheckListEndTime)
                    .HasColumnName("checkListEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckListGroup)
                    .HasColumnName("checkListGroup")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListJobCategoryId).HasColumnName("checkListJobCategoryId");

                entity.Property(e => e.CheckListJobDescription).HasColumnName("checkListJobDescription");

                entity.Property(e => e.CheckListJobId).HasColumnName("checkListJobId");

                entity.Property(e => e.CheckListJobLineNumber).HasColumnName("checkListJobLineNumber");

                entity.Property(e => e.CheckListJobName)
                    .HasColumnName("checkListJobName")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListJobSupervisorId).HasColumnName("checkListJobSupervisorId");

                entity.Property(e => e.CheckListJobTypeId).HasColumnName("checkListJobTypeId");

                entity.Property(e => e.CheckListMasterId).HasColumnName("checkListMasterId");

                entity.Property(e => e.CheckListShiftNumber).HasColumnName("checkListShiftNumber");

                entity.Property(e => e.CheckListStartTime)
                    .HasColumnName("checkListStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurrentColor).HasColumnName("currentColor");

                entity.Property(e => e.CurrentGrade).HasColumnName("currentGrade");

                entity.Property(e => e.EstimatedEndTime).HasColumnName("estimatedEndTime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OverAllApproved).HasColumnName("overAllApproved");

                entity.Property(e => e.OverAllJobCompleted).HasColumnName("overAllJobCompleted");

                entity.Property(e => e.OverAllRejected).HasColumnName("overAllRejected");

                entity.Property(e => e.PreviousColor).HasColumnName("previousColor");

                entity.Property(e => e.PreviousGrade).HasColumnName("previousGrade");

                entity.Property(e => e.ProcessOrderNumber)
                    .HasColumnName("processOrderNumber")
                    .HasMaxLength(1000);

                entity.Property(e => e.ReassignedBy).HasColumnName("reassignedBy");

                entity.Property(e => e.ReassignedDate)
                    .HasColumnName("reassignedDate")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckListJobWrtoperator>(entity =>
            {
                entity.ToTable("checkListJobWRTOperator");

                entity.Property(e => e.CheckListJobWrtoperatorId).HasColumnName("checkListJobWRTOperatorId");

                entity.Property(e => e.CheckListJobEndTime)
                    .HasColumnName("checkListJobEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CheckListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.CheckListJobIsCompleted).HasColumnName("checkListJobIsCompleted");

                entity.Property(e => e.CheckListJobIsPartialCompleted).HasColumnName("checkListJobIsPartialCompleted");

                entity.Property(e => e.CheckListJobMasterId).HasColumnName("checkListJobMasterId");

                entity.Property(e => e.CheckListJobStartTime)
                    .HasColumnName("checkListJobStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJobClosed)
                    .HasColumnName("isJobClosed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsJobRejected)
                    .HasColumnName("isJobRejected")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JobApprovedTime)
                    .HasColumnName("jobApprovedTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.JobRejectedReason).HasColumnName("jobRejectedReason");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperatorId).HasColumnName("operatorId");
            });

            modelBuilder.Entity<CheckListLototomaster>(entity =>
            {
                entity.HasKey(e => e.LototoCheckListId);

                entity.ToTable("checkListLOTOTOMaster");

                entity.Property(e => e.LototoCheckListId).HasColumnName("lototoCheckListId");

                entity.Property(e => e.CheckListGroupId).HasColumnName("checkListGroupId");

                entity.Property(e => e.CheckListLockStepNumber).HasColumnName("checkListLockStepNumber");

                entity.Property(e => e.CheckListMasterId).HasColumnName("checkListMasterId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsLockOutRequired)
                    .HasColumnName("isLockOutRequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTagOutRequired)
                    .HasColumnName("isTagOutRequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsTryOutRequired)
                    .HasColumnName("isTryOutRequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.PositionDescription).HasColumnName("positionDescription");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<CheckListMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListId);

                entity.ToTable("checkListMaster");

                entity.Property(e => e.CheckListId).HasColumnName("checkListId");

                entity.Property(e => e.CheckListCategoryId).HasColumnName("checkListCategoryId");

                entity.Property(e => e.CheckListDescription).HasColumnName("checkListDescription");

                entity.Property(e => e.CheckListGroup)
                    .HasColumnName("checkListGroup")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListName)
                    .HasColumnName("checkListName")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListOwner)
                    .HasColumnName("checkListOwner")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListTypeId).HasColumnName("checkListTypeId");

                entity.Property(e => e.CheckListVersion)
                    .HasColumnName("checkListVersion")
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.EstimatedEndTime).HasColumnName("estimatedEndTime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckListSubCategoryMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListSubCategoryId);

                entity.ToTable("checkListSubCategoryMaster");

                entity.Property(e => e.CheckListSubCategoryId).HasColumnName("checkListSubCategoryId");

                entity.Property(e => e.CheckListSubCategoryDescription)
                    .HasColumnName("checkListSubCategoryDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListSubCategoryName)
                    .HasColumnName("checkListSubCategoryName")
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<CheckListTypeMaster>(entity =>
            {
                entity.HasKey(e => e.CheckListTypeId)
                    .HasName("PK_checkListType");

                entity.ToTable("checkListTypeMaster");

                entity.Property(e => e.CheckListTypeId).HasColumnName("checkListTypeId");

                entity.Property(e => e.CheckListTypeDescription)
                    .HasColumnName("checkListTypeDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.CheckListTypeName)
                    .HasColumnName("checkListTypeName")
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ColorMaster>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.ToTable("colorMaster");

                entity.Property(e => e.ColorId).HasColumnName("colorId");

                entity.Property(e => e.ColorCode)
                    .HasColumnName("colorCode")
                    .HasMaxLength(250);

                entity.Property(e => e.ColorDescription)
                    .HasColumnName("colorDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.ColorName)
                    .HasColumnName("colorName")
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<DepartmentMaster>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("departmentMaster");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentDescription).HasColumnName("departmentDescription");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("departmentName")
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<DesignationMaster>(entity =>
            {
                entity.HasKey(e => e.DesignationId);

                entity.ToTable("designationMaster");

                entity.Property(e => e.DesignationId).HasColumnName("designationId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.DesignationDescription).HasColumnName("designationDescription");

                entity.Property(e => e.DesignationName)
                    .HasColumnName("designationName")
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<DocumentMaster>(entity =>
            {
                entity.ToTable("documentMaster");

                entity.Property(e => e.DocumentMasterId).HasColumnName("documentMasterId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("isDelete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(250);

                entity.Property(e => e.RecordId)
                    .HasColumnName("recordId")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DocumentUplodedMaster>(entity =>
            {
                entity.HasKey(e => e.DocumentUploaderId);

                entity.ToTable("documentUplodedMaster");

                entity.Property(e => e.DocumentUploaderId).HasColumnName("documentUploaderId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.DocumentMasterId).HasColumnName("documentMasterId");

                entity.Property(e => e.DocumentName)
                    .HasColumnName("documentName")
                    .HasMaxLength(250);

                entity.Property(e => e.DocumentUploadedFor)
                    .HasColumnName("documentUploadedFor")
                    .HasMaxLength(500);

                entity.Property(e => e.FileName)
                    .HasColumnName("fileName")
                    .HasMaxLength(250);

                entity.Property(e => e.FilePath)
                    .HasColumnName("filePath")
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<GradeMaster>(entity =>
            {
                entity.HasKey(e => e.GradeId);

                entity.ToTable("gradeMaster");

                entity.Property(e => e.GradeId).HasColumnName("gradeId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.GradeCode)
                    .HasColumnName("gradeCode")
                    .HasMaxLength(250);

                entity.Property(e => e.GradeDescription)
                    .HasColumnName("gradeDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.GradeName)
                    .HasColumnName("gradeName")
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<HelpContentMaster>(entity =>
            {
                entity.HasKey(e => e.HelpContentId);

                entity.ToTable("helpContentMaster");

                entity.Property(e => e.HelpContentId).HasColumnName("helpContentId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.HelpContentDescription).HasColumnName("helpContentDescription");

                entity.Property(e => e.HelpContentShortName)
                    .HasColumnName("helpContentShortName")
                    .HasMaxLength(500);

                entity.Property(e => e.InstructionLink)
                    .HasColumnName("instructionLink")
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.VisbleToRoleId)
                    .HasColumnName("visbleToRoleId")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<LineNumberMaster>(entity =>
            {
                entity.HasKey(e => e.LineNumberId);

                entity.ToTable("lineNumberMaster");

                entity.Property(e => e.LineNumberId).HasColumnName("lineNumberId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LineNumberDescription)
                    .HasColumnName("lineNumberDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.LineNumberName)
                    .HasColumnName("lineNumberName")
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<QrCode>(entity =>
            {
                entity.ToTable("qrCode");

                entity.Property(e => e.QrcodeId).HasColumnName("qrcodeId");

                entity.Property(e => e.QrCode1).HasColumnName("qrCode");

                entity.Property(e => e.QrCodeText)
                    .HasColumnName("qrCodeText")
                    .HasMaxLength(50);

                entity.Property(e => e.QrCodeText2Pattern)
                    .HasColumnName("qrCodeText2Pattern")
                    .HasMaxLength(50);

                entity.Property(e => e.QrCodeTextPattern)
                    .HasColumnName("qrCodeTextPattern")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReAssignedcheckListJobResourcesOperator>(entity =>
            {
                entity.HasKey(e => e.ReAssignedResourceId)
                    .HasName("PK_checkListJobReAssignedResource");

                entity.Property(e => e.ReAssignedResourceId).HasColumnName("reAssignedResourceId");

                entity.Property(e => e.CheckListJobGroupId).HasColumnName("checkListJobGroupId");

                entity.Property(e => e.CheckListJobMasterId).HasColumnName("checkListJobMasterId");

                entity.Property(e => e.ChecklistJobFirstOperatorIds)
                    .HasColumnName("checklistJobFirstOperatorIds")
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsReassigned).HasColumnName("isReassigned");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.PrimaryResource)
                    .HasColumnName("primaryResource")
                    .HasMaxLength(1000);

                entity.Property(e => e.PrimaryResourceToAllFlag).HasColumnName("primaryResourceToAllFlag");

                entity.Property(e => e.SecondaryResource)
                    .HasColumnName("secondaryResource")
                    .HasMaxLength(1000);

                entity.Property(e => e.SecondaryResourceToAllFlag).HasColumnName("secondaryResourceToAllFlag");
            });

            modelBuilder.Entity<RejectedJobHistory>(entity =>
            {
                entity.HasKey(e => e.RejectedId);

                entity.ToTable("rejectedJobHistory");

                entity.Property(e => e.RejectedId).HasColumnName("rejectedId");

                entity.Property(e => e.CheckListJobId).HasColumnName("checkListJobId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.PreviousJobIdWrtoperator).HasColumnName("previousJobIdWRTOperator");

                entity.Property(e => e.Remark).HasColumnName("remark");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("roleMaster");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleDescription).HasColumnName("roleDescription");

                entity.Property(e => e.RoleName)
                    .HasColumnName("roleName")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ShiftMaster>(entity =>
            {
                entity.HasKey(e => e.ShiftId);

                entity.ToTable("shiftMaster");

                entity.Property(e => e.ShiftId).HasColumnName("shiftId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShiftDescription)
                    .HasColumnName("shiftDescription")
                    .HasMaxLength(500);

                entity.Property(e => e.ShiftEndTimings).HasColumnName("shiftEndTimings");

                entity.Property(e => e.ShiftName)
                    .HasColumnName("shiftName")
                    .HasMaxLength(250);

                entity.Property(e => e.ShiftStartTimings).HasColumnName("shiftStartTimings");
            });

            modelBuilder.Entity<TargetOverall>(entity =>
            {
                entity.HasKey(e => e.TargetId);

                entity.ToTable("targetOverall");

                entity.Property(e => e.TargetId).HasColumnName("targetId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.TargetEndTime)
                    .HasColumnName("targetEndTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TargetStartTime)
                    .HasColumnName("targetStartTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TargetValue)
                    .HasColumnName("targetValue")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TargetYearName)
                    .HasColumnName("targetYearName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("userDetails");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.DesignationId).HasColumnName("designationId");

                entity.Property(e => e.DocumentUploadedId).HasColumnName("documentUploadedId");

                entity.Property(e => e.EmailId)
                    .HasColumnName("emailId")
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeUniqueCode)
                    .HasColumnName("employeeUniqueCode")
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAdminApproved)
                    .HasColumnName("isAdminApproved")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modifiedOn")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(250);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.ReportManagerId).HasColumnName("reportManagerId");

                entity.Property(e => e.ReportingManagerName)
                    .HasColumnName("reportingManagerName")
                    .HasMaxLength(250);

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.UserFirstName)
                    .HasColumnName("userFirstName")
                    .HasMaxLength(250);

                entity.Property(e => e.UserFullName)
                    .HasColumnName("userFullName")
                    .HasMaxLength(250);

                entity.Property(e => e.UserLastName)
                    .HasColumnName("userLastName")
                    .HasMaxLength(250);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(250);
            });
        }
    }
}
