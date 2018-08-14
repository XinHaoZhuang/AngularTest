using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCZM.BLL.WX
{
    public class WX_GetLoginInfo
    {
        DAL.WX.WX_GetLoginInfo dal = new DAL.WX.WX_GetLoginInfo();
        public DataSet GetLoginInfo(string WeiXinAccount) {
            return dal.GetLoginInfo(WeiXinAccount);
        }
        public DataSet getWxAccount_Id(string UserIdList)
        {
            return dal.getWxAccount_Id(UserIdList);
        }
        public DataSet getWxAccount_Dep(string UserDepIdList)
        {
            return dal.getWxAccount_Dep(UserDepIdList);
        }
        public DataSet getWxAccount_Procedure(int AssignmentProcedureId)
        {
            return dal.getWxAccount_Procedure(AssignmentProcedureId);
        }
        public DataSet getWxAccount_Procedure_onlyGroup(int AssignmentProcedureId)
        {
            return dal.getWxAccount_Procedure_onlyGroup(AssignmentProcedureId);
        }
        public DataSet getTimePart(string type) {
            return dal.getTimePart(type);
        }
        public DataSet getMenu() {
            return dal.getMenu();
        }
    }
}
