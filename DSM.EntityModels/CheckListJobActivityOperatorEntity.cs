using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobActivityOperatorEntity
    {
        public class CheckListJobActivityOperatorCustom
        {
            public long? checkListJobActivityOperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public long? checkListJobActivityId { get; set; }
            public string operatorScannedBarcodeNumber { get; set; }
            public long? barcodeAssetId { get; set; }
            public long? operatorUpoadedDocumentId { get; set; }
            public string operatorRemark { get; set; }
        }
    }
}
