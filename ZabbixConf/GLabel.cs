using System;
using System.Drawing;
using System.Drawing.Drawing2D;
 
namespace System.Windows.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public class GLabel : Label
	{
		// declare two color for linear gradient
		private Color cLeft;
		private Color cRight;
 
		/// <summary>
		/// property of begin color in linear gradient
		/// </summary>
		public Color BeginColor
		{
			get
			{
				return cLeft;
			}
			set
			{
				cLeft = value;
			}
		}
		/// <summary>
		/// property of end color in linear gradient
		/// </summary>
		public Color EndColor
		{
			get
			{
				return cRight;
			}
			set
			{
				cRight = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public GLabel()
		{
			// Default get system color 
			cLeft = SystemColors.ActiveCaption;
			cRight = SystemColors.Control;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			// declare linear gradient brush for fill background of label
			LinearGradientBrush GBrush = new LinearGradientBrush(
				new Point(0, 0),
				new Point(this.Width, 0), cLeft, cRight);
			Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
			// Fill with gradient 
			e.Graphics.FillRectangle(GBrush, rect);
 
			// draw text on label
			SolidBrush drawBrush = new SolidBrush(this.ForeColor);
			StringFormat sf = new StringFormat();
			// align with center
			sf.Alignment = StringAlignment.Near;
			// set rectangle bound text
			RectangleF rectF = new
				RectangleF(0, this.Height / 2 - Font.Height / 2, this.Width, this.Height);
			// output string
			e.Graphics.DrawString(this.Text, this.Font, drawBrush, rectF, sf);
		}
	}
} 
