using KrTrade.Nt.Core.Data;
using NinjaTrader.Gui;

namespace NinjaTrader.NinjaScript.DrawingTools
{
	[CategoryDefaultExpanded(false)]
	public class PriceLine : LabelLine
    {

		#region Public properties		
		
		/// <summary>
		/// The icon to display in the menu item.
		/// </summary>
		public override object Icon { get { return Gui.Tools.Icons.DrawHorizLineTool; } }

		#endregion

		#region State changed event methods

		protected override void OnStateChange()
		{
			// Call to parent.
			base.OnStateChange();

			if (State == State.SetDefaults)
			{
				LabelType = LabelLineType.Price;
				Name = "PriceLineDisplayName"; // Implementar diccionario con cadenas
				//Anchor.DisplayName = KrTrade.Nt.Scripts.Resources.Texts.AnchorYDisplayName;
				//Anchor.IsXPropertiesVisible = false;
				DisplayOnChartsMenus = true;
                //LabelTextMargin = new Margin
                //{
                //    Left = 2,
                //    Top = 2,
                //    Right = 2,
                //    Bottom = 2
                //};
            }
		}

		#endregion


	}
}
