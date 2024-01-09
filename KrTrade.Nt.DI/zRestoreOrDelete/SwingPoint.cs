using KrTrade.Nt.Core.Bars;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.DI.Data
{
    public class SwingPoint //: BaseElement
    {

        #region Private members

        private readonly SwingType swingType;
        private readonly List<Bar> swingBars = new List<Bar>();

        #endregion

        #region Public properties

        public double Value { get; set; }
        public int Strength { get; set; }

        public SwingType Type => swingType;
        public List<Bar> SwingBars => swingBars;

        #endregion

        #region Constructors

        public SwingPoint(SwingType swingType, List<Bar> swingBars)
        {
            this.swingBars = swingBars;
            this.swingType = swingType;
            Strength = (swingBars.Count - 1) / 2;
            Value = Type == SwingType.High ? swingBars[Strength].High : swingBars[Strength].Low;
        }

        public SwingPoint(List<Bar> swingBars)
        {
            this.swingBars = swingBars;
            Strength = (swingBars.Count - 1) / 2;
            this.swingType = GetSwingType();
            Value = Type == SwingType.High ? swingBars[Strength].High : swingBars[Strength].Low;
        }

        #endregion

        #region Public methods

        public override string ToString()
        {
            return string.Format("Swing Point {0}= {1}",Type.ToString(),Value.ToString("N2"));
        }

        public string ToSwingBarsArray()
        {
            if (swingBars == null || swingBars.Count < 1)
                throw new Exception("Swing bars is not initialize.");

            string text = "[ ";

            for (int i = 0; i < swingBars.Count; i++)
                text += Type == SwingType.High ? swingBars[i].High.ToString() + " - " : swingBars[i].Low.ToString() + " - ";
            text += "]";

            return string.Format("Swing Point {0}= {1}", Type.ToString(), text);
        }

        #endregion

        #region Private methods

        private SwingType GetSwingType()
        {
            if (swingBars == null || swingBars.Count < 1)
                throw new Exception("Swing bars is not initialize.");

            double candidateValue = swingBars[Strength].High;
            bool isSwing = true;

            //for (int i = 0; i < Strength; i++)
            //    if (swingBars[i].High.ApproxCompare(candidateValue) > 0)
            //        isSwing = false;
            //for (int i = Strength + 1; i < swingBars.Count; i++)
            //    if (swingBars[i].High.ApproxCompare(candidateValue) > 0)
            //        isSwing = false;
            if (isSwing)
                return SwingType.High;

            candidateValue = swingBars[Strength].Low;
            isSwing = true;

            //for (int i = 0; i < Strength; i++)
            //    if (swingBars[i].Low.ApproxCompare(candidateValue) < 0)
            //        isSwing = false;
            //for (int i = Strength + 1; i < swingBars.Count; i++)
            //    if (swingBars[i].Low.ApproxCompare(candidateValue) < 0)
            //        isSwing = false;
            if (isSwing)
                return SwingType.Low;

            throw new Exception("Swing bars are not swing.");
        }

        #endregion

    }
}
