using System;
using System.Collections.Generic;
using System.Text;

namespace DSM.EntityModels
{
    public class CheckListJobLOTOTOOperatorEntity
    {
        public class CheckListJobLOTOTOOperatorCustom
        {
            public long checkListJobLototooperatorId { get; set; }
            public long? checkListJobOperatorId { get; set; }
            public long? operatorId { get; set; }
            public string overAllRemark { get; set; }
            public long? lockOutDoneByOperator { get; set; }
            public string lockOutRemark { get; set; }
            public long? tagOutDoneByOperator { get; set; }
            public string tagOutRemark { get; set; }
            public long? tryOutDoneByOperator { get; set; }
            public long? checkListJobLOTOTOId { get; set; }
            public string tryOutRemark { get; set; }
        }
    }
}
