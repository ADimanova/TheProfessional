using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Common
{
    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";
        public const string UserRoleName = "User";
        public const int ListPanelCount = 5;
        public static readonly IList<string> EmptyList = new List<string> { "no values specified" };
    }
}
