using System;
using System.ComponentModel;

namespace SHHS.FPTB.Base
{
    public class Info : INotifyPropertyChanged
    {
        #region 属性更改事件
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        /// <summary>
        /// 文件名称(全路径名称)
        /// </summary>
        private string _fileName;
        /// <summary>
        /// 文件名称(全路径名称)
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { if (_fileName != value) { _fileName = value; OnPropertyChanged("FileName"); } }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 测试员
        /// </summary>
        private string _operator;
        /// <summary>
        /// 测试员
        /// </summary>
        public string Operator
        {
            get { return _operator; }
            set { if (_operator != value) { _operator = value; OnPropertyChanged("Operator"); } }
        }
        /// <summary>
        /// 制造商
        /// </summary>
        private string _maker;
        /// <summary>
        /// 制造商
        /// </summary>
        public string Maker
        {
            get { return _maker; }
            set { if (_maker != value) { _maker = value; OnPropertyChanged("Maker"); } }
        }

        /// <summary>
        /// 功率计接线
        /// </summary>
        private string _powerPhaseWire;
        /// <summary>
        /// 功率计接线
        /// </summary>
        public string PowerPhaseWire
        {
            get { return _powerPhaseWire; }
            set { if (_powerPhaseWire != value) { _powerPhaseWire = value; OnPropertyChanged("PowerPhaseWire"); } }
        }

        /// <summary>
        /// 型号
        /// </summary>
        private string _model;
        /// <summary>
        /// 型号
        /// </summary>
        public string Model
        {
            get { return _model; }
            set { if (_model != value) { _model = value; OnPropertyChanged("Model"); } }
        }
        /// <summary>
        /// 产品号
        /// </summary>
        private string _sN;
        /// <summary>
        /// 产品号
        /// </summary>
        public string SN
        {
            get { return _sN; }
            set { if (_sN != value) { _sN = value; OnPropertyChanged("SN"); } }
        }
        /// <summary>
        /// 额定电压
        /// </summary>
        private double _ratedVoltage;
        /// <summary>
        /// 额定电压
        /// </summary>
        public double RatedVoltage
        {
            get { return _ratedVoltage; }
            set
            {
                if (_ratedVoltage != value) 
                {
                    //if (value == 0)
                    //    throw new Exception("值不能为零！");
                    _ratedVoltage = value; 
                    OnPropertyChanged("RatedVoltage"); 
                }
            }
        }
        /// <summary>
        /// 额定频率
        /// </summary>
        private double _ratedFrequency;
        /// <summary>
        /// 额定频率
        /// </summary>
        public double RatedFrequency
        {
            get { return _ratedFrequency; }
            set { if (_ratedFrequency != value) { _ratedFrequency = value; OnPropertyChanged("RatedFrequency"); } }
        }
        /// <summary>
        /// 额定功率
        /// </summary>
        private double _ratedPower;
        /// <summary>
        /// 额定功率
        /// </summary>
        public double RatedPower
        {
            get { return _ratedPower; }
            set { if (_ratedPower != value) { _ratedPower = value; OnPropertyChanged("RatedPower"); } }
        }
        /// <summary>
        /// 额定冷量
        /// </summary>
        private double _ratedCold;
        /// <summary>
        /// 额定冷量
        /// </summary>
        public double RatedCold
        {
            get { return _ratedCold; }
            set { if (_ratedCold != value) { _ratedCold = value; OnPropertyChanged("RatedCold"); } }
        }
        /// <summary>
        /// 额定热量
        /// </summary>
        private double _ratedHot;
        /// <summary>
        /// 额定热量
        /// </summary>
        public double RatedHot
        {
            get { return _ratedHot; }
            set { if (_ratedHot != value) { _ratedHot = value; OnPropertyChanged("RatedHot"); } }
        }
        /// <summary>
        /// 电加热功率
        /// </summary>
        private double _eleHotPower;
        /// <summary>
        /// 电加热功率
        /// </summary>
        public double EleHotPower
        {
            get { return _eleHotPower; }
            set { if (_eleHotPower != value) { _eleHotPower = value; OnPropertyChanged("EleHotPower"); } }
        }

        /// <summary>
        /// 制冷剂
        /// </summary>
        private string _ref = "R134A";
        /// <summary>
        /// 制冷剂
        /// </summary>
        public string Refrigerant
        {
            get { return _ref; }
            set { if (_ref != value) { _ref = value; OnPropertyChanged("Refrigerant"); } }
        }
        /// <summary>
        /// 冷凝压力
        /// </summary>
        private double _condPress;
        /// <summary>
        /// 冷凝压力
        /// </summary>
        public double CondPress
        {
            get { return _condPress; }
            set { if (_condPress != value) { _condPress = value; OnPropertyChanged("CondPress"); } }
        }
        /// <summary>
        /// 蒸发压力
        /// </summary>
        private double _evapPress;
        /// <summary>
        /// 蒸发压力
        /// </summary>
        public double EvapPress
        {
            get { return _evapPress; }
            set { if (_evapPress != value) { _evapPress = value; OnPropertyChanged("EvapPress"); } }
        }
        /// <summary>
        /// 冷凝器表面积
        /// </summary>
        private double _condArea;
        /// <summary>
        /// 冷凝器表面积
        /// </summary>
        public double CondArea
        {
            get { return _condArea; }
            set { if (_condArea != value) { _condArea = value; OnPropertyChanged("CondArea"); } }
        }
        /// <summary>
        /// 蒸发器表面积
        /// </summary>
        private double _evapArea;
        /// <summary>
        /// 蒸发器表面积
        /// </summary>
        public double EvapArea
        {
            get { return _evapArea; }
            set { if (_evapArea != value) { _evapArea = value; OnPropertyChanged("EvapArea"); } }
        }
        /// <summary>
        /// 冷凝器漏热系数
        /// </summary>
        private double _condLeakage;
        /// <summary>
        /// 冷凝器漏热系数
        /// </summary>
        public double CondLeakage
        {
            get { return _condLeakage; }
            set { if (_condLeakage != value) { _condLeakage = value; OnPropertyChanged("CondLeakage"); } }
        }
        /// <summary>
        /// 蒸发器漏热系数
        /// </summary>
        private double _evapLeakage;
        /// <summary>
        /// 蒸发器漏热系数
        /// </summary>
        public double EvapLeakage
        {
            get { return _evapLeakage; }
            set { if (_evapLeakage != value) { _evapLeakage = value; OnPropertyChanged("EvapLeakage"); } }
        }
        
        /// <summary>
        /// 蒸发器污垢系数 m^2*℃/kw
        /// </summary>
        private double _evapDirtyCoef;
        /// <summary>
        /// 蒸发器污垢系数
        /// </summary>
        public double EvapDirtyCoef
        {
            get { return _evapDirtyCoef; }
            set { if (_evapDirtyCoef != value) { _evapDirtyCoef = value; OnPropertyChanged("EvapDirtyCoef"); } }
        }

        /// <summary>
        /// 蒸发器污垢面积 m^2
        /// </summary>
        private double _evapDirtyArea;
        /// <summary>
        /// 蒸发器污垢面积
        /// </summary>
        public double EvapDirtyArea
        {
            get { return _evapDirtyArea; }
            set { if (_evapDirtyArea != value) { _evapDirtyArea = value; OnPropertyChanged("EvapDirtyArea"); } }
        }

        /// <summary>
        /// 冷凝器污垢系数 m^2*℃/kw
        /// </summary>
        private double _condDirtyCoef;
        /// <summary>
        /// 冷凝器污垢系数 m^2*℃/kw
        /// </summary>
        public double CondDirtyCoef
        {
            get { return _condDirtyCoef; }
            set { if (_condDirtyCoef != value) { _condDirtyCoef = value; OnPropertyChanged("CondDirtyCoef"); } }
        }

        /// <summary>
        /// 冷凝器污垢面积 m^2
        /// </summary>
        private double _condDirtyArea;
        /// <summary>
        /// 冷凝器污垢面积
        /// </summary>
        public double CondDirtyArea
        {
            get { return _condDirtyArea; }
            set { if (_condDirtyArea != value) { _condDirtyArea = value; OnPropertyChanged("CondDirtyArea"); } }
        }

    }
}
