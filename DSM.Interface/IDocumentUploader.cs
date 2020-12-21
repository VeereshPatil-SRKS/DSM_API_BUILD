using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DocumentUploaderEntity;

namespace DSM.Interface
{
    public interface IDocumentUploader
    {
        CommonResponseWithIdsDoc AddAndEditDocumentUploader(DocumentUplodedMasterCustom documentDetails,long userId = 0);
        CommonResponseWithIdsDoc AddAndEditDocumentUploaderBase64(DocumentUplodedMasterCustomBase64 documentDetails,long userId = 0);
        CommonResponse ViewDocumentUploadedById(long documentUploaderId,long userId = 0);
    }
}
