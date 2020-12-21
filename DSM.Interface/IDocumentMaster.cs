using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.DocumentMasterEntity;

namespace DSM.Interface
{
    public interface IDocumentMaster
    {
        CommonResponse AddAndEditDocumentMaster(DocumentMasterCustom data, long userId = 0);
        CommonResponse ViewMultipleDocumentMaster();
        CommonResponse ViewDocumentMasterById(int documentMasterId);
        CommonResponse DeleteDocumentMaster(int documentMasterId, long userId = 0);
        CommonResponse ArchiveDocumentMaster(int documentMasterId, long userId = 0);
        CommonResponse CheckDocumentMaster(int documentMasterId);
    }
}