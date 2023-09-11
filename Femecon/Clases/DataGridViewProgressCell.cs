using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Femecon.Clases
{
    public class DataGridViewProgressCell : DataGridViewTextBoxCell
    {
        protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
        DataGridViewElementStates cellState, object value, object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            if (value is int progressValue)
            {
                // Dibuja el fondo de la celda
                g.FillRectangle(Brushes.White, cellBounds);

                // Calcula la posición y el tamaño de la barra de progreso
                int barWidth = cellBounds.Width * progressValue / 100;
                Rectangle progressBarBounds = new Rectangle(cellBounds.X, cellBounds.Y, barWidth, cellBounds.Height);

                // Dibuja la barra de progreso en verde claro
                using (Brush progressBarBrush = new SolidBrush(Color.LightGreen))
                {
                    g.FillRectangle(progressBarBrush, progressBarBounds);
                }

                // Dibuja el texto legible centrado en ambos ejes encima de la barra de progreso
                string text = progressValue.ToString() + "%";
                SizeF textSize = g.MeasureString(text, cellStyle.Font);
                PointF textPosition = new PointF(cellBounds.X + (cellBounds.Width - textSize.Width) / 2, cellBounds.Y + (cellBounds.Height - textSize.Height) / 2);

                using (Brush textBrush = new SolidBrush(Color.Black)) // Color del texto
                {
                    g.DrawString(text, cellStyle.Font, textBrush, textPosition);
                }
            }
        }
    }
}
