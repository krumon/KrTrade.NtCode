using System;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public class SwingLevel : BaseElement
    {

		#region Private members

		private SwingType swingLevelType;
		private SwingLevelCalculateMode calculateMode;
		private List<SwingPoint> levelSwings = new List<SwingPoint>();
		private double value;
		private double topValue;
		private double bottomValue;

		//private double calculateValue;
		//private double marginValue;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the swing level type.
		/// </summary>
		public SwingType SwingLevelType { get { return swingLevelType; } }

		/// <summary>
		/// Gets or sets the level calculate mode.
		/// </summary>
		public SwingLevelCalculateMode CalculateMode
		{
			get { return calculateMode; }
			set
			{
				// Make sure value changued
				if (calculateMode == value)
					return;
				// Update value
				calculateMode = value;
				// Changes all proterties than changes when calculate mode changed.
				RecalculateValues();
				// Call to listeners
				OnCalculateModeChangued(calculateMode);
			}
		}

		/// <summary>
		/// Gets the level swings collection.
		/// </summary>
		public List<SwingPoint> LevelSwings { get { return levelSwings; } }

		/// <summary>
		/// Gets the level calculate value.
		/// </summary>
		public double Value { get { return value; } }

		/// <summary>
		/// Gets the level calculate value.
		/// </summary>
		public double TopValue { get { return topValue; } }

		/// <summary>
		/// Gets the level calculate value.
		/// </summary>
		public double BottomValue { get { return bottomValue; } }

		/// <summary>
		/// Gets the maximum value of the level.
		/// </summary>
		public double UpperValue { get; set; }

		/// <summary>
		/// Gets the minimum value of the level.
		/// </summary>
		public double LowerValue { get; set; }


		public int Strength { get; set; }
		public int StartIdx { get; set; }
		public int EndIdx { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default Constructor
		/// </summary>
		public SwingLevel(double value, double calculateValue, double marginValue) //, List<SwingPoint> levelSwings)
		{
			this.value = value;
			//this.calculateValue = calculateValue;
			//this.marginValue = marginValue;
			//this.levelSwings = levelSwings;
		}

		#endregion

		#region Public methods

		public void AddSwingPoint(SwingPoint swingPoint, bool recalculate = false)
		{
			if (levelSwings == null)
				levelSwings = new List<SwingPoint>();

			levelSwings.Add(swingPoint);

			if (recalculate)
				RecalculateValues();

			OnLevelSwingsChanged();
		}

		public void AddSwingPoints(IEnumerable<SwingPoint> swingPoints, bool recalculate = true)
		{
			if (levelSwings == null)
				levelSwings = new List<SwingPoint>();

			ClearSwingPoints();

			levelSwings.AddRange(swingPoints);

			if (recalculate)
				RecalculateValues();

			OnLevelSwingsChanged();
		}

		public void ClearSwingPoints()
		{
			if (levelSwings != null && levelSwings.Count > 0)
			{
				levelSwings.Clear();
				InitializeValues();
				OnLevelSwingsChanged();
			}
		}

		public void DisposeSwingPoints()
		{
			if (levelSwings != null && levelSwings.Count > 0)
				levelSwings.Clear();

			levelSwings = null;

			InitializeValues();

			OnLevelSwingsChanged();
		}

		#endregion

		#region Handler methods

		protected virtual void OnCalculateModeChangued(SwingLevelCalculateMode newCalculateMode)
		{
		}

		protected virtual void OnLevelSwingsChanged()
		{
		}

		#endregion

		#region Private methods

		private void InitializeValues()
		{
			Strength = StartIdx = EndIdx = -1;
			UpperValue = LowerValue = 0.0;
		}

		private void RecalculateValues()
		{
			if (levelSwings == null || levelSwings.Count < 1)
				return;
			double upper = double.MinValue;
			double lower = double.MaxValue;
			for (int i = 0; i < levelSwings.Count; i++)
			{
				lower = Math.Min(lower, levelSwings[i].Value);
				upper = Math.Max(upper, levelSwings[i].Value);
			}
		}

		#endregion

	}
}
