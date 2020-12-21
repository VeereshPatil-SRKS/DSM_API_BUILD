using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class DocumentUplodedMaster
    {
        public int DocumentUploaderId { get; set; }
        public string DocumentName { get; set; }
        public int? DocumentMasterId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string DocumentUploadedFor { get; set; }
    }
}
