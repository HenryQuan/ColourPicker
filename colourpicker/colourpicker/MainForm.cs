using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColourPicker
{
    public partial class MainForm : Form
    {
        Point lastLocaion;
        Color currColour;

        public MainForm()
        {
            InitializeComponent();
            lastLocaion = Cursor.Position;
            resetTip();

            // Set a timer to update colour every 10ms
            Timer t = new Timer();
            t.Interval = 10;
            t.Enabled = true;
            t.Tick += T_Tick;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            Point curr = Cursor.Position;
            Console.WriteLine(curr);
            // Same location don't update anything
            if (lastLocaion.X == curr.X && lastLocaion.Y == curr.Y) return;
            lastLocaion = curr;

            // Update panel colour
            updateColour(GetColorAt(curr));
            resetTip();
        }

        private void resetTip()
        {
            tipLabel.Text = "Ctrl + C to copy current colour\nIt only works if this window is being focused";
        }

        /// <summary>
        /// Update colour, panel and label
        /// </summary>
        /// <param name="c">Color</param>
        private void updateColour(Color c)
        {
            // Update currColour and colourPanel back colour
            currColour = c;
            colourPanel.BackColor = c;

            // Get RGB and HEX string
            colorLabel.Text = getFormattedColour(c);
        }

        private string getFormattedColour(Color c)
        {
            return RGBConverter(c) + "\n" + HexConverter(c);
        }

        /// <summary>
        /// Convert COlor to HEX format, from https://stackoverflow.com/questions/2395438/convert-system-drawing-color-to-rgb-and-hex-value
        /// </summary>
        /// <param name="c">Colour</param>
        /// <returns>A string like #123456</returns>
        private string HexConverter(Color c)
        {
            return string.Format("#{0}{1}{2}", c.R.ToString("X2"), c.G.ToString("X2"), c.B.ToString("X2"));
        }

        /// <summary>
        /// Convert Color to RGB format
        /// </summary>
        /// <param name="c">Color</param>
        /// <returns>A string like RGB(0, 0, 0)</returns>
        private string RGBConverter(Color c)
        {
            return string.Format("RGB({0}, {1}, {2})", c.R, c.G, c.B);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        /// <summary>
        /// Get pixel colour at x, y, from https://stackoverflow.com/questions/1483928/how-to-read-the-color-of-a-screen-pixel#1483963
        /// </summary>
        /// <param name="p">Cursor position</param>
        /// <returns>Color</returns>
        public static Color GetColorAt(Point p)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, p.X, p.Y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(getFormattedColour(currColour));
                tipLabel.Text = "Copied :)";
            }
        }
    }
}
