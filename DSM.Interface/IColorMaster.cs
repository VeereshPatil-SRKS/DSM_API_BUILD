using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.ColorMasterEntity;
using static DSM.EntityModels.CommonEntity;

namespace DSM.Interface
{
    public interface IColorMaster
    {
        CommonResponse AddAndEditColor(ColorCustom data, long userId = 0);
        CommonResponse AddAndEditColorExcel(List<ColorCustom> datas, long userId = 0);
        CommonResponse ViewMultipleColor();
        CommonResponse ViewColorById(int colorId);
        CommonResponse DeleteColor(int colorId, long userId = 0);
        CommonResponse ArchiveColor(int colorId, long userId = 0);
        CommonResponse CheckColor(int colorId);
    }
}
