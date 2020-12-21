using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class DocumentUploaderEntity
    {
        public class DocumentUplodedMasterCustom
        {
            public int documentUploaderId { get; set; }
            public int? documentMasterId { get; set; }
            public string documentMasterName { get; set; }
            public string DocumentUploadedFor { get; set; }
            public IFormFile Image { get; set; }
            public string base64Image { get; set; }
        }

        public class DocumentUplodedMasterCustomBase64
        {
            public int documentUploaderId { get; set; }
            public int? documentMasterId { get; set; }
            public string documentMasterName { get; set; }
            public string DocumentUploadedFor { get; set; }
            public string base64Image { get; set; }
            public string uploadedFileName { get; set; }
        }
    }
}
