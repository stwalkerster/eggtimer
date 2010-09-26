using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class AutoFontLabel : Label
    {
        private const int WIDTH_RATIO = 23;
        private const int HEIGHT_RATIO = 3;

        public AutoFontLabel()
        {
            this.AutoEllipsis = true;
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            updateFontSize();
            base.OnPaddingChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            updateFontSize();
            base.OnResize(e);
        }

        private void updateFontSize()
        {
            int textHeight = this.ClientRectangle.Height
                - (this.Padding.Top + this.Padding.Bottom);
            int textWidth = this.ClientRectangle.Width
                - (this.Padding.Left + this.Padding.Right);


            int newTextHeight = 0;

            // which is smaller, height *23/4, or width.
            if ((textHeight * WIDTH_RATIO / HEIGHT_RATIO) < textWidth)
            // height * 23/4 is smaller, text height = height
            {
                newTextHeight = textHeight;
            }
            else
            // width is smaller, text height = width *4/23
            {
                newTextHeight = textWidth * HEIGHT_RATIO / WIDTH_RATIO;
            }


            if (newTextHeight > 0)
            {
                this.Font = new Font(this.Font.FontFamily,
                    newTextHeight, GraphicsUnit.Pixel);
            }
        }
    }
}
