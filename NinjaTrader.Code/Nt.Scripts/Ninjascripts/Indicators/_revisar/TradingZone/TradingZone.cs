
#region Kr.TradingZone Indicator

#region Using declarations

using NinjaTrader.Core;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.Data;
using NinjaTrader.Gui;
using NinjaTrader.NinjaScript.DrawingTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;

#endregion

/// <summary>
/// namespace that stores any technical indicator 
/// from the ninjatrader ninjascript
/// </summary>
namespace NinjaTrader.NinjaScript.Indicators.KrMasterIndicators
{/*
	/// <summary>
	/// The Trading Zone indicator plots lines that represents the resistance and support levels.
	/// </summary>
	public class KrTradingZone : Indicator
	{

		#region Private classes and enums

		internal class SwingInfoItem
		{
			public double Volume;
			public int StartBarsAgo;
			public int FinalBarsAgo;
			public double MinPrice;
			public double MaxPrice;
			public double TickSize;
			public double LevelTickMargin;
			public double OuterTickMargin;
			public List<int> HighBarsAgo;
			public List<int> LowBarsAgo;
			public List<int> InnerPoints;
			public List<int> OuterHighPoints;
			public List<int> OuterLowPoints;
		}

		public enum LevelSwingsCalculate
		{

			Max,
			Min,
			Average,
		}

		#endregion

		#region Global variables

		// Variable para almacenar la barra actual acceder al modo "each tick"
		private int saveCurrentBar;

		// private int		supportConstant;
		// private int		resistenceConstant;

		#endregion

		#region Swings variables

		// Número total de valores a tener en cuenta para dar por válido el swing point.(2*Strength+1)
		private int constant;

		// Valor actual del swing point
		private double currentSwingHighValue;
		private double currentSwingLowValue;

		// Último valor del swing
		private double lastSwingHighValue;
		private double lastSwingLowValue;

		// Array temporal para almacenar los valores a analizar para considerar si un precio es un swing
		private ArrayList lastHighCache;
		private ArrayList lastLowCache;

		// Series para almacenar las swing series a representar en los diferentes plots
		private Series<double> swingHighSeries;
		private Series<double> swingLowSeries;

		// Series para almacenar los swings points
		private Series<double> swingHighSwings;
		private Series<double> swingLowSwings;

		#endregion

		#region Resistance and Support variables

		// Valor actual de soporte o resistencia
		private double currentResistanceValue;
		private double currentSupportValue;

		// Último valor de resistencia o soporte
		private double lastResistanceValue;
		private double lastSupportValue;

		// Memoria temporal para almacenar los últimos swings para analizar si son soportes o resistencias.
		private ArrayList lastSwingHighCache;
		private ArrayList lastSwingLowCache;

		// Series para almacenar las Resistence y Support series a representar en los diferentes plots
		private Series<double> resistanceSeries;
		private Series<double> supportSeries;

		// Series para almacenar los swing points de los soportes y las resistencias
		private Series<double> resistanceSwings;
		private Series<double> supportSwings;

		// Almacena las instancias de los swing points que forman parte de la linea de resistencia o soporte
		private List<int> swingPointInstances = new List<int>();

		// Primera barra del swing del último soporte o la última resistencia
		private int lastResistanceStartBarsAgo;
		private int lastSupportStartBarsAgo;

		#endregion

		#region Render variables

		#endregion

		#region Data variables

		// Diccionarios para almacenar los puntos de soporte y resistencia
		private List<Dictionary<double, SwingInfoItem>> sortedDicList = new List<Dictionary<double, SwingInfoItem>>();
		private Dictionary<double, SwingInfoItem> cacheDictionary = new Dictionary<double, SwingInfoItem>();

		#endregion

		#region TradingSessionInfo variables

		private DateTime cacheSessionEnd = Globals.InitialDate;
		private DateTime currentDate = Globals.InitialDate;
		private DateTime sessionDateTmp = Globals.InitialDate;
		private SessionIterator sessionIterator;
		private SessionIterator storedSession;
		private List<int> newSessionBarIdx = new List<int>();
		private int startIndexOf;

		#endregion

		#region Private properties

		/// <summary>
		/// Gets the high swing plot values.
		/// </summary>
		private Series<double> ResistancePlot
		{
			get
			{
				Update();
				return Values[0];
			}
		}

		/// <summary>
		/// Gets the low swing plot values.
		/// </summary>
		private Series<double> SwingLowPlot
		{
			get
			{
				Update();
				return Values[1];
			}
		}

		/// <summary>
		/// Gets the session iterator of the bars
		/// <summary>
		private SessionIterator SessionIterator
		{
			get
			{
				if (sessionIterator == null)
					sessionIterator = new SessionIterator(Bars);
				return sessionIterator;
			}
		}

		#endregion

		#region Public Properties

		// SwingStrength. Fuerza de los swings.
		// SwingPeriod. Número de los últimos swings a analizar.
		// TickMargin. El margen en ticks para considerar un precio como soporte o resistencia.
		// ResistenceSwings. Número de swings en el precio para considerarlo resistencia.
		// SupportSwings. Número de swings en el precio para considerarlo soporte.

		/// <summary>
		/// Sets or gets the number of bars to the left to find the resistence an support levels
		/// </summary>
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(Name = "Period", GroupName = "NinjaScriptParameters", Order = 0)]
		public int Period
		{ get; set; }

		/// <summary>
		/// Sets or gets the number of required bars to the left and right of the swing point
		/// </summary>
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(Name = "Swings Strength", GroupName = "NinjaScriptParameters", Order = 1)]
		public int SwingStrength
		{ get; set; }

		/// <summary>
		/// Sets or gets the number of high swings to the to considerate the level than resistence.
		/// </summary>
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(Name = "Number of High Swings", GroupName = "NinjaScriptParameters", Order = 2)]
		public int NumberOfHighSwings
		{ get; set; }

		/// <summary>
		/// Sets or gets the number of high swings to the to considerate the level than resistence.
		/// </summary>
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(Name = "Number of Low Swings", GroupName = "NinjaScriptParameters", Order = 3)]
		public int NumberOfLowSwings
		{ get; set; }

		/// <summary>
		/// Sets or gets the number of ticks to considerate the price than resistence or support.
		/// </summary>
		[Display(Name = "Tick Margin", GroupName = "NinjaScriptParameters", Order = 4)]
		public int TickMargin
		{ get; set; }

		/// <summary>
		/// Sets or gets the type of calculate of the resistence price.
		/// </summary>
		[Display(Name = "Resistence calculate type", GroupName = "NinjaScriptParameters", Order = 5)]
		public LevelSwingsCalculate ResistanceSwingsCalculate
		{ get; set; }

		/// <summary>
		/// Sets or gets the type of calculate of the support price.
		/// </summary>
		[Display(Name = "Support calculate type", GroupName = "NinjaScriptParameters", Order = 6)]
		public LevelSwingsCalculate SupportSwingsCalculate
		{ get; set; }

		/// <summary>
		/// Gets the high swings.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public Series<double> Resistences
		{
			get
			{
				Update();
				return resistanceSeries;
			}
		}

		/// <summary>
		/// Gets the low swings.
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public Series<double> SwingLow
		{
			get
			{
				Update();
				return swingLowSeries;
			}
		}

		#endregion

		#region Stated Changed Method

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description = @"Plots lines that represents the resistance and support levels.";
				Name = "Trading Zone";
				DisplayInDataBox = false;
				PaintPriceMarkers = false;
				IsSuspendedWhileInactive = true;
				IsOverlay = true;

				Period = 5;
				SwingStrength = 5;
				NumberOfHighSwings = 2;
				NumberOfLowSwings = 2;
				TickMargin = 1;
				SupportSwingsCalculate = LevelSwingsCalculate.Max;
				ResistanceSwingsCalculate = LevelSwingsCalculate.Min;

				AddPlot(new Stroke(Brushes.DarkCyan, 2), PlotStyle.Dot, "ResistanceLine");
				AddPlot(new Stroke(Brushes.Goldenrod, 2), PlotStyle.Dot, "SupportLine");
			}

			else if (State == State.ConfigureSessionHoursList)
			{
				// Swing variables

				currentSwingHighValue = 0;
				currentSwingLowValue = 0;
				lastSwingHighValue = 0;
				lastSwingLowValue = 0;
				saveCurrentBar = -1;
				constant = (2 * SwingStrength) + 1;

				// Resistence and support variables

				currentResistanceValue = 0;
				currentSupportValue = 0;
				lastResistanceValue = 0;
				lastSupportValue = 0;
				lastResistanceStartBarsAgo = 0;
				lastSupportStartBarsAgo = 0;

				// Global variables
				ZOrder = -1;
			}

			else if (State == State.DataLoaded)
			{
				// Swings
				lastHighCache = new ArrayList();
				lastLowCache = new ArrayList();
				swingHighSeries = new Series<double>(this);
				swingLowSeries = new Series<double>(this);
				swingHighSwings = new Series<double>(this);
				swingLowSwings = new Series<double>(this);

				// Resistance and supports
				lastSwingLowCache = new ArrayList();
				lastSwingHighCache = new ArrayList();
				resistanceSeries = new Series<double>(this);
				supportSeries = new Series<double>(this);
				resistanceSwings = new Series<double>(this);
				supportSwings = new Series<double>(this);

				// SessionHours
				storedSession = new SessionIterator(Bars);
			}

			else if (State == State.Historical)
			{
				if (Calculate != Calculate.OnEachTick)
					Draw.TextFixed(this, "NinjaScriptInfo", "Se recomienda que el método de cálculo sea OnEachTick.", TextPosition.BottomRight);

				// TODO: Borrar.
				ClearOutputWindow();
			}

			else if (State == State.Realtime)
			{
				ClearOutputWindow();
				PrintSerie(lastSwingHighCache, "SWING HIGH CACHE", "Swing");
				PrintSerie(swingPointInstances, "SWING POINT INSTANCES", "Instance");
			}
		}

		#endregion

		#region Market events methods

		protected override void OnBarUpdate()
		{

			if (Bars.Count <= 0)
				return;

			double high0 = !(Input is PriceSeries || Input is Bars) ? Input[0] : High[0];
			double low0 = !(Input is PriceSeries || Input is Bars) ? Input[0] : Low[0];
			double close0 = !(Input is PriceSeries || Input is Bars) ? Input[0] : Close[0];

			// double			price;
			// long				volume;
			// SwingInfoItem		swingInfoItem;
			DateTime lastBarTimeStamp = GetLastBarSessionDate(Time[0]);

			if (lastBarTimeStamp != currentDate)
			{
				cacheDictionary = new Dictionary<double, SwingInfoItem>();
				sortedDicList.Add(cacheDictionary);
			}

			currentDate = lastBarTimeStamp;

			// If bars supported that remove the last bar....
			// Primero se elimina la barra (CurrentBar-1) por lo que current bar es menor que...
			// ...saveCurrentBar.
			if (BarsArray[0].BarsType.IsRemoveLastBarSupported && CurrentBar < saveCurrentBar)
			{
				// Actualizo los valores a través del plot. 
				// Hay que tener en cuenta que el valor puede variar en un tick
				currentSwingHighValue = ResistancePlot.IsValidDataPoint(0)
										? ResistancePlot[0] : 0;
				currentSwingLowValue = SwingLowPlot.IsValidDataPoint(0)
										? SwingLowPlot[0] : 0;
				currentResistanceValue = ResistancePlot.IsValidDataPoint(0)
										? ResistancePlot[0] : 0;

				// Actualizo los últimos swing points guardados
				lastSwingHighValue = swingHighSeries[0];
				lastSwingLowValue = swingLowSeries[0];

				// Inicializo las swings series y los arrays.
				// Entiendo que primero se elimina la barra y después se añade la nueva barra.
				swingHighSeries[SwingStrength] = 0;
				swingLowSeries[SwingStrength] = 0;
				lastHighCache.Clear();
				lastLowCache.Clear();

				// Actualizo las swings caches. 
				for (int barsBack = Math.Min(CurrentBar, constant) - 1; barsBack >= 0; barsBack--)
				{
					lastHighCache.Add(!(Input is PriceSeries || Input is Bars)
								? Input[barsBack]
								: High[barsBack]);
					lastLowCache.Add(!(Input is PriceSeries || Input is Bars)
								? Input[barsBack]
								: Low[barsBack]);
				}
				saveCurrentBar = CurrentBar;
				return;
			}

			// When the bar is new...in the first tick...the current bar is greatest than saveCurrentBar.
			if (saveCurrentBar != CurrentBar)
			{
				// Inicializo los valores de la barra actual
				swingHighSwings[0] = 0; // initializing important internal
				swingLowSwings[0] = 0;  // initializing important internal

				swingHighSeries[0] = 0; // initializing important internal
				swingLowSeries[0] = 0;  // initializing important internal

				resistanceSwings[0] = 0; // initializing important internal
				supportSwings[0] = 0;  // initializing important internal

				resistanceSeries[0] = 0; // initializing important internal
				supportSeries[0] = 0;  // initializing important internal

				// Actualizo los arrays que sirven de memoria cache.
				// Añado el nuevo valor al final y elimino el valor incial si la caché está llena.
				lastHighCache.Add(high0);
				if (lastHighCache.Count > constant)
					lastHighCache.RemoveAt(0);
				lastLowCache.Add(low0);
				if (lastLowCache.Count > constant)
					lastLowCache.RemoveAt(0);

				// Si la memoria caché del high está llena procedemos a evaluar si es un swing point.
				// En caso de ser un swing point se actualizan los valores de las series y gráficos.
				if (lastHighCache.Count == constant)
				{
					// Es un swing point hasta que se demuestre lo contrario.
					bool isSwingHigh = IsSwingHigh();

					// Añado el high swing a la memoria caché de "resistencias".
					if (isSwingHigh)
					{
						// Update the swing high cache
						lastSwingHighCache.Add(currentSwingHighValue);
						if (lastSwingHighCache.Count > Period)
							lastSwingHighCache.RemoveAt(0);

						// TODO: Comprobar si el swing point coincide con precios del diccionario.
					}

					bool isResistance = isSwingHigh && IsResistance();

					// Si es un swing y una resistencia actualizo el plot.
					if (isResistance)
						for (int i = 0; i <= lastResistanceStartBarsAgo; i++)
							ResistancePlot[i] = currentResistanceValue;

					// Si el precio es mayor al nivel de resistencia o el valor actual es 0... 
					// ... actualizo el "currentSwingHigh" y anulo la representación gráfica
					else if (high0 > currentResistanceValue || currentResistanceValue.ApproxCompare(0.0) == 0)
					{
						currentResistanceValue = 0.0;
						ResistancePlot[0] = close0;
						ResistancePlot.Reset();
					}

					// En otro caso represento el valor actual de la resistencia...ya que no es 0.
					else
						ResistancePlot[0] = currentResistanceValue;

					// Si es un nivel de resistencia actualizo la serie desde el swing point
					if (isResistance)
						for (int i = 0; i <= lastResistanceStartBarsAgo; i++)
							resistanceSeries[i] = lastResistanceValue;

					// En otro caso actualizo únicamente el valor actual de la serie
					else
						resistanceSeries[0] = lastResistanceValue;
				}

				// Si la memoria caché del high está llena procedemos a evaluar si es un swing point.
				// En caso de ser un swing point se actualizan los valores de las series y gráficos.
				if (lastLowCache.Count == constant)
				{
					// Es un swing point hasta que se demuestre lo contrario.
					bool isSwingLow = true;
					double swingLowCandidateValue = (double)lastLowCache[SwingStrength];

					//  Comprobamos los valores a la izquierda del swing.
					for (int i = 0; i < SwingStrength; i++)
						if (((double)lastLowCache[i]).ApproxCompare(swingLowCandidateValue) <= 0)
							isSwingLow = false;

					// Comprobamos los valores a la derecha del swing
					for (int i = SwingStrength + 1; i < lastLowCache.Count; i++)
						if (((double)lastLowCache[i]).ApproxCompare(swingLowCandidateValue) < 0)
							isSwingLow = false;

					// Actualizamos el valor de la swing serie
					swingLowSwings[SwingStrength] = isSwingLow ? swingLowCandidateValue : 0.0;
					if (isSwingLow)
						lastSwingLowValue = swingLowCandidateValue;

					// Si es un swing actualizo el "currentSwingLow" y la gráfica.
					if (isSwingLow)
					{
						currentSwingLowValue = swingLowCandidateValue;
						for (int i = 0; i <= SwingStrength; i++)
							SwingLowPlot[i] = currentSwingLowValue;
					}

					// Si no es un swing point y el valor actual es inferior al swing o es 0... 
					// ... actualizo el "currentSwingLow" y anulo la representación gráfica
					else if (low0 < currentSwingLowValue || currentSwingLowValue.ApproxCompare(0.0) == 0)
					{
						currentSwingLowValue = double.MaxValue;
						SwingLowPlot[0] = close0;
						SwingLowPlot.Reset();
					}

					// En otro caso represento el valor actual del swing...ya que no es 0.
					else
						SwingLowPlot[0] = currentSwingLowValue;

					// Si es un swing actualizo la swing serie desde el swing hasta el el valor actual
					if (isSwingLow)
						for (int i = 0; i <= SwingStrength; i++)
							swingLowSeries[i] = lastSwingLowValue;

					// En otro caso actualizo únicamente el valor actual de la swing serie
					else
						swingLowSeries[0] = lastSwingLowValue;

				}

				// Actualizo la barra actual
				saveCurrentBar = CurrentBar;
			}

			// When calculate for each tick...
			else if (CurrentBar >= constant - 1)
			{

				// Compruebo si la caché está llena y si el valor del tick es superior al último valor guardado
				if (lastHighCache.Count == constant && high0.ApproxCompare((double)lastHighCache[lastHighCache.Count - 1]) > 0)
					lastHighCache[lastHighCache.Count - 1] = high0;

				// Compruebo si la caché está llena y si el valor del tick es inferior al último valor guardado.
				if (lastLowCache.Count == constant && low0.ApproxCompare((double)lastLowCache[lastLowCache.Count - 1]) < 0)
					lastLowCache[lastLowCache.Count - 1] = low0;

				// Si el valor del tick es superior al valor actual del high y es un swing...ya no es un swing...
				if (high0 > currentSwingHighValue && swingHighSwings[SwingStrength] > 0.0 && resistanceSwings[lastResistanceStartBarsAgo] > 0.0)
				{
					// Actualizo valores. El precio no hace que haya swing.
					swingHighSwings[SwingStrength] = 0.0;
					currentSwingHighValue = 0.0;

					resistanceSwings[lastResistanceStartBarsAgo] = 0.0;
					currentResistanceValue = 0.0;

					lastSwingHighCache.RemoveAt(lastSwingHighCache.Count - 1);
					swingPointInstances.RemoveAt(0);

					for (int i = 0; i <= lastResistanceStartBarsAgo; i++)
					{
						ResistancePlot[i] = close0;
						ResistancePlot.Reset(i);
					}

					// TODO: Recalcular la resistencia
				}

				// Si el valor del tick es superior al valor actual del high y es un swing ...
				// ...ya no hay que dibujarlo
				else if (high0 > currentResistanceValue && currentResistanceValue.ApproxCompare(0.0) != 0)
				{
					// Borro el dibujo del último punto
					ResistancePlot[0] = close0;
					ResistancePlot.Reset();
					currentSwingHighValue = 0.0;
				}

				// Si el valor del hight es menor al guardado...continuo dibujando el swing
				else if (high0 <= currentResistanceValue)
					ResistancePlot[0] = currentResistanceValue;

				if (low0 < currentSwingLowValue && swingLowSwings[SwingStrength] > 0.0)
				{
					swingLowSwings[SwingStrength] = 0.0;
					for (int i = 0; i <= SwingStrength; i++)
					{
						SwingLowPlot[i] = close0;
						SwingLowPlot.Reset(i);
						currentSwingLowValue = double.MaxValue;
					}
				}
				else if (low0 < currentSwingLowValue && currentSwingLowValue.ApproxCompare(double.MaxValue) != 0)
				{
					SwingLowPlot.Reset();
					currentSwingLowValue = double.MaxValue;
				}
				else if (low0 >= currentSwingLowValue)
					SwingLowPlot[0] = currentSwingLowValue;
			}

		}

		#endregion

		#region Functions

		/// <summary>
		/// Returns the number of bars ago a swing low occurred. Returns a value of -1 if a swing low is not found within the look back period.
		/// </summary>
		/// <param name="barsAgo"></param>
		/// <param name="instance"></param>
		/// <param name="lookBackPeriod"></param>
		/// <returns></returns>
		public int SwingLowBar(int barsAgo, int instance, int lookBackPeriod)
		{
			if (instance < 1)
				throw new Exception(string.Format(NinjaTrader.Custom.Resource.SwingSwingLowBarInstanceGreaterEqual, GetType().Name, instance));
			if (barsAgo < 0)
				throw new Exception(string.Format(NinjaTrader.Custom.Resource.SwingSwingLowBarBarsAgoGreaterEqual, GetType().Name, barsAgo));
			if (barsAgo >= Count)
				throw new Exception(string.Format(NinjaTrader.Custom.Resource.SwingSwingLowBarBarsAgoOutOfRange, GetType().Name, (Count - 1), barsAgo));

			Update();

			for (int idx = CurrentBar - barsAgo - SwingStrength; idx >= CurrentBar - barsAgo - SwingStrength - lookBackPeriod; idx--)
			{
				if (idx < 0)
					return -1;
				if (idx >= swingLowSwings.Count)
					continue;

				if (swingLowSwings.GetValueAt(idx).Equals(0.0))
					continue;

				if (instance == 1) // 1-based, < to be save
					return CurrentBar - idx;

				instance--;
			}

			return -1;
		}

		/// <summary>
		/// Returns the number of bars ago a swing high occurred. Returns a value of -1 if a swing high is not found within the look back period.
		/// </summary>
		/// <param name="barsAgo"></param>
		/// <param name="instance"></param>
		/// <param name="lookBackPeriod"></param>
		/// <returns></returns>
		public int SwingHighBar(int barsAgo, int instance, int lookBackPeriod)
		{
			if (instance < 1)
				throw new Exception(string.Format(NinjaTrader.Custom.Resource.SwingSwingHighBarInstanceGreaterEqual, GetType().Name, instance));
			if (barsAgo < 0)
				throw new Exception(string.Format(NinjaTrader.Custom.Resource.SwingSwingHighBarBarsAgoGreaterEqual, GetType().Name, barsAgo));
			if (barsAgo >= Count)
				throw new Exception(string.Format(NinjaTrader.Custom.Resource.SwingSwingHighBarBarsAgoOutOfRange, GetType().Name, (Count - 1), barsAgo));

			Update();

			for (int idx = CurrentBar - barsAgo - SwingStrength; idx >= CurrentBar - barsAgo - SwingStrength - lookBackPeriod; idx--)
			{
				if (idx < 0)
					return -1;
				if (idx >= swingHighSwings.Count)
					continue;

				if (swingHighSwings.GetValueAt(idx).Equals(0.0))
					continue;

				if (instance <= 1) // 1-based, < to be save
					return CurrentBar - idx;

				instance--;
			}

			return -1;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Gets the date of the last bar of the session.
		/// </summary>
		/// <param name="time">The current time.</param>
		/// <returns></returns>
		private DateTime GetLastBarSessionDate(DateTime time)
		{
			if (time <= cacheSessionEnd)
				return sessionDateTmp;

			if (!Bars.BarsType.IsIntraday)
				return sessionDateTmp;

			storedSession.GetNextSession(time, true);

			cacheSessionEnd = storedSession.ActualSessionEnd;
			sessionDateTmp = TimeZoneInfo.ConvertTime(cacheSessionEnd.AddSeconds(-1), Globals.GeneralOptions.TimeZoneInfo, Bars.TradingHours.TimeZoneInfo);

			if (newSessionBarIdx.Count == 0 || newSessionBarIdx.Count > 0 && CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
				newSessionBarIdx.Add(CurrentBar);

			return sessionDateTmp;
		}

		/// <summary>
		/// Calculate if the prices stores in <see cref="lastHighCache"/> are a high swing.
		/// </summary>
		/// <returns>True if the last prices are a swing point, otherwise false.</returns>
		private bool IsSwingHigh()
		{
			// Es un swing point hasta que se demuestre lo contrario.
			bool isSwingHigh = true;
			double swingHighCandidateValue = (double)lastHighCache[SwingStrength];

			//  Comprobamos los valores a la izquierda del swing.
			for (int i = 0; i < SwingStrength; i++)
				if (((double)lastHighCache[i]).ApproxCompare(swingHighCandidateValue) >= 0)
					isSwingHigh = false;

			// Comprobamos los valores a la derecha del swing
			for (int i = SwingStrength + 1; i < lastHighCache.Count; i++)
				if (((double)lastHighCache[i]).ApproxCompare(swingHighCandidateValue) > 0)
					isSwingHigh = false;

			swingHighSwings[SwingStrength] = isSwingHigh ? swingHighCandidateValue : 0.0;
			lastSwingHighValue = currentSwingHighValue = swingHighCandidateValue;

			return isSwingHigh;
		}

		/// <summary>
		/// Calculate if the swings stores in <see cref="lastSwingHighCache"/> are a resistance zone.
		/// </summary>
		/// <returns>True if the last swings are a resistance zone, otherwise false.</returns>
		private bool IsResistance()
		{
			List<int> instancesCache = new List<int>();
			bool isResistance;
			double tickMargin;
			bool insideMargin;
			double newCandidateValue;
			swingPointInstances.Clear();

			for (int i = lastSwingHighCache.Count - 1; i >= NumberOfHighSwings; i--)
			{
				newCandidateValue = (double)lastSwingHighCache[i];
				for (int j = lastSwingHighCache.Count - 1; j >= NumberOfHighSwings - 1; j--)
				{
					tickMargin = Math.Abs(newCandidateValue - (double)lastSwingHighCache[j]) / TickSize;
					insideMargin = tickMargin.ApproxCompare(TickMargin) <= 0;
					if (j == lastSwingHighCache.Count - 1 && !insideMargin)
						break;
					else if (insideMargin)
						instancesCache.Add(lastSwingHighCache.Count - j);
				}
				if (instancesCache.Count >= NumberOfHighSwings && instancesCache.Count > swingPointInstances.Count)
					swingPointInstances = new List<int>(instancesCache);

				instancesCache.Clear();
			}

			isResistance = swingPointInstances.Count >= NumberOfHighSwings;

			lastResistanceStartBarsAgo = isResistance ? SwingHighBar(0, swingPointInstances[swingPointInstances.Count - 1], CurrentBar - SwingStrength) : -1;

			if (isResistance)
				lastResistanceValue = currentResistanceValue = ResistanceValueCalculate();
			//else
			//	resistanceSwings[lastResistanceStartBarsAgo] = 0.0;

			return isResistance;
		}

		/// <summary>
		/// Returns the resistance price value based on calculate method.
		/// </summary>
		/// <returns></returns>
		private double ResistanceValueCalculate()
		{

			double min = double.MaxValue;
			double max = double.MinValue;
			double resistanceValue;

			for (int i = 0; i < swingPointInstances.Count; i++)
			{
				min = Math.Min(min, High[SwingHighBar(0, swingPointInstances[i], CurrentBar - SwingStrength)]);
				max = Math.Max(max, High[SwingHighBar(0, swingPointInstances[i], CurrentBar - SwingStrength)]);
			}
			switch (ResistanceSwingsCalculate)
			{
				case LevelSwingsCalculate.Max:
					resistanceValue = max;
					break;
				default:
					resistanceValue = min;
					break;
			}

			// TODO: Remove.
			//Print(string.Format("Last Resistance Bar = {0}", CurrentBar - lastResistanceStartBarsAgo + 1));
			//Print(string.Format("Resistance Value    = {0}", lastResistanceValue));

			lastResistanceValue = currentResistanceValue = resistanceSwings[lastResistanceStartBarsAgo] = resistanceValue;

			return resistanceValue;
		}

		private void PrintSerie(IEnumerable array, string title, string elementName)
		{
			if (array == null)
				Print("Array is null");

			string text = title + " = [";
			int cont = 1;
			foreach (object item in array)
			{
				text = text + elementName + " " + cont + " = " + item.ToString() + " ";
				cont++;
			}

			text = text + "]";
			Print(text);
		}

		#endregion

	}
	*/
}

#endregion
