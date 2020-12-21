using System;
using System.Collections.Generic;
using System.Text;
using static DSM.EntityModels.CommonEntity;
using static DSM.EntityModels.HelpContentEntity;

namespace DSM.Interface
{
    public interface IHelpContent
    {
        CommonResponse AddAndEditHelpContent(HelpContentCustom data, long userId = 0);
        CommonResponse ViewMultipleHelpContent();
        CommonResponse ViewMultipleHelpContentInHelpScreen(long userId);
        CommonResponse ViewHelpContentById(int helpContentId);
        CommonResponse DeleteHelpContent(int helpContentId, long userId = 0);
        CommonResponse ArchiveHelpContent(int helpContentId, long userId = 0);
        CommonResponse CheckHelpContent(int helpContentId);
    }
}
