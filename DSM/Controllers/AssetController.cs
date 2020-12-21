using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DSM.DAL.Helpers;
using DSM.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static DSM.EntityModels.AssetEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Controllers
{
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAsset asset;

        public AssetController(IOptions<AppSettings> appSettings, IAsset _asset)
        {
            _appSettings = appSettings.Value;
            asset = _asset;
        }

        /// <summary>
        /// Add and Edit Document Master
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Asset/AddAndEditAsset")]
        public async Task<IActionResult> AddAndEditAsset(AssetCustom data)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponseWithIds response = new CommonResponseWithIds();
            response = asset.AddAndEditAsset(data, userId);

            return Ok(response);
        }

        /// <summary>
        /// View Multiple Document Master
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/ViewMultipleAsset")]
        public async Task<IActionResult> ViewMultipleAsset()
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = asset.ViewMultipleAsset();

            return Ok(response);
        }

        /// <summary>
        /// View Document Master By Id
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/ViewAssetById")]
        public async Task<IActionResult> ViewAssetById(int assetId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = asset.ViewAssetById(assetId);

            return Ok(response);
        }

        /// <summary>
        /// Delete Document Master
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/DeleteAsset")]
        public async Task<IActionResult> DeleteAsset(int assetId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = new CommonResponse();
            response = asset.DeleteAsset(assetId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Archive Document Master
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/ArchiveAsset")]
        public async Task<IActionResult> ArchiveAsset(int assetId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = new CommonResponse();
            response = asset.ArchiveAsset(assetId, userId);

            return Ok(response);
        }

        /// <summary>
        /// Get Asset Bar Code Number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/GetAssetBarCodeNumber")]
        public async Task<IActionResult> GetAssetBarCodeNumber()
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = new CommonResponse();
            response = asset.GetAssetBarCodeNumber();

            return Ok(response);
        }

        /// <summary>
        /// Check Manual Bar Code Number
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/CheckManualBarCodeNumber")]
        public async Task<IActionResult> CheckManualBarCodeNumber(string barCode)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = new CommonResponse();
            response = asset.CheckManualBarCodeNumber(barCode);

            return Ok(response);
        }

        /// <summary>
        /// Download Bar Code For Asset By Asset Id
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/DownloadBarCodeForAssetByAssetId")]
        public async Task<IActionResult> DownloadBarCodeForAssetByAssetId(int assetId)
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = new CommonResponse();
            response = asset.DownloadBarCodeForAssetByAssetId(assetId);

            return Ok(response);
        }

        /// <summary>
        /// Download Bar Code For Asset By Asset All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Asset/DownloadBarCodeForAssetByAssetAll")]
        public async Task<IActionResult> DownloadBarCodeForAssetByAssetAll()
        {
            #region Authorization code
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string id = "";
            string role = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                id = identity.Claims.Where(m => m.Type == ClaimTypes.Sid).Select(m => m.Value).FirstOrDefault();
                role = identity.Claims.Where(m => m.Type == ClaimTypes.Role).Select(m => m.Value).FirstOrDefault();
            }
            long userId = Convert.ToInt32(id);
            #endregion
            //calling AssetDAL busines layer
            CommonResponse response = new CommonResponse();
            response = asset.DownloadBarCodeForAssetByAssetAll();

            return Ok(response);
        }

    }
}