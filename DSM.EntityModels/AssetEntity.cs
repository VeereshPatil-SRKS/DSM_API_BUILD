using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class AssetEntity
    {
        public class AssetCustom
        {
            public long assetId { get; set; }
            public string assetName { get; set; }
            public string assetDescription { get; set; }
            public long? assetDocumentUploadedId { get; set; }
            public string barcodeAllocatedNumber { get; set; }
            public long? lineNumber { get; set; }
        }

        public class BarCodePrinter
        {
            public string assetNumber { get; set; }
            public string assetName { get; set; }
            public string lineNumber { get; set; }
        }
    }
}
