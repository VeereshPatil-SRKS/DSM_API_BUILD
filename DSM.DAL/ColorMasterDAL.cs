using DSM.DAL.Resource;
using DSM.DBModels;
using DSM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DSM.EntityModels.ColorMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.DAL
{
    public class ColorMasterDAL : IColorMaster
    {
        DSMContext db = new DSMContext();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ColorMasterDAL));
        public ColorMasterDAL(DSMContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// Add and Edit Document 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditColor(ColorCustom data, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.ColorMaster.Where(m => m.ColorId == data.colorId).FirstOrDefault();
                if (res == null)
                {
                    try
                    {
                        ColorMaster item = new ColorMaster();
                        item.ColorName = data.colorName;
                        item.ColorCode = data.colorCode;
                        item.ColorDescription = data.colorDescription;
                        item.IsActive = true;
                        item.IsDeleted = false;
                        item.CreatedBy = userId;
                        item.CreatedOn = DateTime.Now;
                        db.ColorMaster.Add(item);
                        db.SaveChanges();
                        obj.response = ResourceResponse.AddedSucessfully;
                        obj.isStatus = true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                        obj.response = ResourceResponse.ExceptionMessage;
                        obj.isStatus = false;
                    }
                }
                else
                {
                    try
                    {
                        res.ColorName = data.colorName;
                        res.ColorCode = data.colorCode;
                        res.ColorDescription = data.colorDescription;
                        res.ModifiedBy = userId;
                        res.ModifiedOn = DateTime.Now;
                        db.SaveChanges();
                        obj.response = ResourceResponse.UpdatedSucessfully;
                        obj.isStatus = true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                        obj.response = ResourceResponse.ExceptionMessage;
                        obj.isStatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// AddAndEditColorExcel
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse AddAndEditColorExcel(List<ColorCustom> datas, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                foreach (var data in datas)
                {
                    var res = db.ColorMaster.Where(m => m.ColorId == data.colorId).FirstOrDefault();
                    if (res == null)
                    {
                        try
                        {
                            ColorMaster item = new ColorMaster();
                            item.ColorName = data.colorName;
                            item.ColorCode = data.colorCode;
                            item.ColorDescription = data.colorDescription;
                            item.IsActive = true;
                            item.IsDeleted = false;
                            item.CreatedBy = userId;
                            item.CreatedOn = DateTime.Now;
                            db.ColorMaster.Add(item);
                            db.SaveChanges();
                            obj.response = ResourceResponse.AddedSucessfully;
                            obj.isStatus = true;
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                            obj.response = ResourceResponse.ExceptionMessage;
                            obj.isStatus = false;
                        }
                    }
                    else
                    {
                        try
                        {
                            res.ColorName = data.colorName;
                            res.ColorCode = data.colorCode;
                            res.ColorDescription = data.colorDescription;
                            res.ModifiedBy = userId;
                            res.ModifiedOn = DateTime.Now;
                            db.SaveChanges();
                            obj.response = ResourceResponse.UpdatedSucessfully;
                            obj.isStatus = true;
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                            obj.response = ResourceResponse.ExceptionMessage;
                            obj.isStatus = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Multiple Document 
        /// </summary>
        /// <returns></returns>
        public CommonResponse ViewMultipleColor()
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.ColorMaster
                              where wf.IsDeleted == false
                              select new
                              {
                                  colorId = wf.ColorId,
                                  colorName = wf.ColorName,
                                  colorDescription = wf.ColorDescription,
                                  colorCode = wf.ColorCode,
                                  colorNameCode = wf.ColorName+'-'+wf.ColorCode,
                                  isActive = wf.IsActive
                              }).ToList();
                if (result.Count() != 0)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// View Document  by Id
        /// </summary>
        /// <param name="colorId"></param>
        /// <returns></returns>
        public CommonResponse ViewColorById(int colorId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = (from wf in db.ColorMaster
                              where wf.IsDeleted == false && wf.ColorId == colorId
                              select new
                              {
                                  colorId = wf.ColorId,
                                  colorName = wf.ColorName,
                                  colorDescription = wf.ColorDescription,
                                  colorCode = wf.ColorCode,
                                  isActive = wf.IsActive,
                                  colorNameCode = wf.ColorName + '-' + wf.ColorCode,
                              }).FirstOrDefault();
                if (result != null)
                {
                    obj.response = result;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.NoItemsFound;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Delete Document 
        /// </summary>
        /// <param name="colorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse DeleteColor(int colorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var res = db.ColorMaster.Where(m => m.ColorId == colorId).FirstOrDefault();
                if (res != null)
                {
                    res.IsDeleted = true;
                    res.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    obj.response = ResourceResponse.DeletedSucessfully;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.FailureMessage;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Archive Document 
        /// </summary>
        /// <param name="colorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonResponse ArchiveColor(int colorId, long userId = 0)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.ColorMaster.Where(m => m.ColorId == colorId).FirstOrDefault();
                if (result != null)
                {
                    result.IsActive = false;
                    result.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    obj.response = ResourceResponse.DeletedSucessfully;
                    obj.isStatus = true;
                }
                else
                {
                    obj.response = ResourceResponse.FailureMessage;
                    obj.isStatus = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

        /// <summary>
        /// Check Document 
        /// </summary>
        /// <param name="colorId"></param>
        /// <returns></returns>
        public CommonResponse CheckColor(int colorId)
        {
            CommonResponse obj = new CommonResponse();
            try
            {
                var result = db.ColorMaster.Where(m => m.ColorId == colorId && m.IsDeleted == false).Count();
                if (result > 0)
                {
                    obj.isStatus = true;
                    obj.response = "Are You sure you want to Delete this Record?";
                }
                else
                {
                    obj.response = "This Record is associated with other data and cannot be deleted and can be Archieved";
                }

            }
            catch (Exception ex)
            {
                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
                obj.response = ResourceResponse.ExceptionMessage;
                obj.isStatus = false;
            }
            return obj;
        }

    }
}
