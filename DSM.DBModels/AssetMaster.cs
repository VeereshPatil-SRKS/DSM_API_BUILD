using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class AssetMaster
    {
        public long AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public long? AssetDocumentUploadedId { get; set; }
        public string BarcodeAllocatedNumber { get; set; }
        public long? LineNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
