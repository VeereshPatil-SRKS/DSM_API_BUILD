using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.AssetEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface IAsset
    {
        CommonResponseWithIds AddAndEditAsset(AssetCustom data, long userId = 0);
        CommonResponse ViewMultipleAsset();
        CommonResponse ViewAssetById(int assetId);
        CommonResponse DeleteAsset(int assetId, long userId = 0);
        CommonResponse ArchiveAsset(int assetId, long userId = 0);
        CommonResponse CheckAsset(int assetId);
        CommonResponse GetAssetBarCodeNumber();
        CommonResponse CheckManualBarCodeNumber(string barCode);
        CommonResponse DownloadBarCodeForAssetByAssetId(int assetId);
        CommonResponse DownloadBarCodeForAssetByAssetAll();
    }
}
