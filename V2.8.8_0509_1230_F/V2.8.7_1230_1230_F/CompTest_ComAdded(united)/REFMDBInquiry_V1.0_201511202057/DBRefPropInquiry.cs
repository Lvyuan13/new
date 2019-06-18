using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefMDBInquiry
{
    public  class DBRefPropInquiry
    {

        //0.变量声明
        private static string MainFolderPath = "D:\\RefDBMainFolder\\";//数据库综合文件夹路径,eg,"D:\\"

        public enum DBRefPropName
        {
            Temp,
            Pressure,
            Volume,
            VolumeL,
            VolumeV,
            Enthalpy,
            EnthalpyL,
            EnthalpyV
        }//DBRefPropName end

        public enum DBRefName
        {
            R134a,
            R141b,
            R123
        }//DBRefName end

        private static double[,] sphPressureRange = new double[,] { { 0.133, 2.393 } };
        private static double[,] sbcPressureRange = new double[,] { { 0.573, 2.153 } };

        /// <summary>
        /// 通过温度压力查询制冷剂过冷或过热物性
        /// </summary>
        /// <param name="RefName">制冷剂种类</param>
        /// <param name="RequirePropName">待查物性名称</param>
        /// <param name="IsSbc">True按照过冷查;False按照过热查</param>
        /// <param name="TempValue">温度,C</param>
        /// <param name="PressureValue">压力,MPa</param>
        /// <returns></returns>
        public static double TPForSbcOrSphProp
          (
           DBRefName RefName, DBRefPropName RequirePropName,bool IsSubCooling,
           double TempValue,double PressureValue
          )
        {
            //0.准备
            double RequirePropValue = 0;
            string RefDBFolderPath = MainFolderPath  + RefName.ToString() + "\\";
            string SatDBPName =RefName.ToString() + "_sat";

            string SphORSbcDBName="";

            //1.1查饱和表，查出对目标压力附近的两个压力
            double[] SatDBArray = DBRefDBOperation.SrcTabForPar
                (RefDBFolderPath, SatDBPName, "sat",DBRefPropName.Pressure.ToString(),DBRefPropName.Pressure.ToString(), PressureValue);
            //1.2判断饱和压力是否在过冷过热表的范围内
            double PressureUp=SatDBArray[4];
            double PressureDn=SatDBArray[3];
            bool IsBothPressInRange=true;
            int RefNum=(int) RefName;
            if(IsSubCooling)
            {
                SphORSbcDBName=RefName.ToString() + "_sbc";
                bool IsPressUpInRange = DBRefDBOperation.IsAmongTheRange
                    (PressureUp, sbcPressureRange[RefNum, 1], sbcPressureRange[RefNum, 0]);
                bool IsPressDnInRange = DBRefDBOperation.IsAmongTheRange
                    (PressureDn, sbcPressureRange[RefNum, 1], sbcPressureRange[RefNum, 0]);
                IsBothPressInRange = IsPressDnInRange & IsPressUpInRange;
            }
            else
            {
                SphORSbcDBName=RefName.ToString() + "_sph";
                bool IsPressUpInRange = DBRefDBOperation.IsAmongTheRange
                    (PressureUp, sphPressureRange[RefNum, 1], sphPressureRange[RefNum, 0]);
                bool IsPressDnInRange = DBRefDBOperation.IsAmongTheRange
                    (PressureDn, sphPressureRange[RefNum, 1], sphPressureRange[RefNum, 0]);
                IsBothPressInRange = IsPressDnInRange & IsPressUpInRange;
            }

            //2 查物性
            if (IsBothPressInRange)//2.1 如果在过冷或过热表范围内的,查对应的过冷OR过热表
            {
                
                string PressUpTableName = (PressureUp * 1000).ToString("f0");
                string PressDnTableName = (PressureDn * 1000).ToString("f0");
                double[] UpResArray = DBRefDBOperation.SrcTabForPar
                    (RefDBFolderPath, SphORSbcDBName, PressUpTableName, RequirePropName.ToString(), DBRefPropName.Temp.ToString(), TempValue);
                double[] DnResArray = DBRefDBOperation.SrcTabForPar
                    (RefDBFolderPath, SphORSbcDBName, PressDnTableName, RequirePropName.ToString(), DBRefPropName.Temp.ToString(), TempValue);

                RequirePropValue = DBRefDBOperation.LinearInterpol
                    (PressureValue, SatDBArray[4], SatDBArray[3], UpResArray[0], DnResArray[0]);
            }
            else//2.2 否则返回PressureValue下的饱和值
            {
                DBRefPropName satRequirePropName;
                if (IsSubCooling)//过冷返回液相值
                {
                    satRequirePropName = RequirePropName + 1;
                }
                else//过热返回气相值
                {
                    satRequirePropName = RequirePropName + 2;
                }
                RequirePropValue = ForSatProp
                    (RefName, satRequirePropName, DBRefPropName.Pressure, PressureValue);
            }


            return RequirePropValue;
        }//ForSbcProp end


        public static double ForSatProp
        (
         DBRefName RefName, DBRefPropName RequirePropName,
         DBRefPropName KnownPropName, double KnownPropValue
        )
        {
            //0.
            double RequirePropValue = 0;
            string RefDBFolderPath =MainFolderPath+ RefName.ToString()+"\\";
            string SatDBPName =RefName.ToString() + "_sat";

            //1.查饱和物性

            double[] ResDBArray=
                DBRefDBOperation.SrcTabForPar
                (RefDBFolderPath,SatDBPName,"sat",RequirePropName.ToString(),KnownPropName.ToString(),KnownPropValue);
            RequirePropValue = ResDBArray[0];

            return RequirePropValue;
        }//ForSatProp end


    }



}
