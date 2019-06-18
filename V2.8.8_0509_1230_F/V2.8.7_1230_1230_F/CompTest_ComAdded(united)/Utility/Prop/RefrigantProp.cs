using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsharpRefprop;

namespace Utility
{
    public  class RefrigantProp
    {
        public CommRefProp R22 = new CommRefProp(CommRefProp.CommRefType.R22);
        public CommRefProp R134a = new CommRefProp(CommRefProp.CommRefType.R134a);
        //public CommRefProp R141b = new CommRefProp(CommRefProp.CommRefType.R141b);
        public CommRefProp r141b = new CommRefProp(CommRefProp.CommRefType.R141b);
        /// <summary>
        /// 20151020当前正在使用的；有R22以及R134a两种
        /// </summary>
        public CommRefProp RefInUsing;
    }
}
