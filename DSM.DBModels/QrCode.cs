using System;
using System.Collections.Generic;

namespace DSM.DBModels
{
    public partial class QrCode
    {
        public long QrcodeId { get; set; }
        public string QrCode1 { get; set; }
        public string QrCodeTextPattern { get; set; }
        public string QrCodeText2Pattern { get; set; }
        public string QrCodeText { get; set; }
    }
}
