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

            // Update panel colour
            updateColour(GetColorAt(curr));

        }

        private void updateColour(Color c)
        {
            currColour = c;
            colourPanel.BackColor = c;

            // Get RGB and HEX string
            colorLabel.Text = RGBConverter(c) + "\n" + HexConverter(c);
        }

        private static string HexConverter(Color c)
        {
            return string.Format("#{0}{1}{2}", c.R.ToString("X2"), c.G.ToString("X2"), c.B.ToString("X2"));
        }

        private static string RGBConverter(Color c)
        {
            return string.Format("RGB({0}, {1}, {2})\n", c.R, c.G, c.B);
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
    }
}
