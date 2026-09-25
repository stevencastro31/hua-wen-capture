using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HuwWenCapture.Forms {
    public partial class ScreenshotForm : Form {
        private Bitmap? _screenshot;
        public Bitmap? screenCapture;

        private Rectangle _region;
        private Point _start;
        private Point _end;

        private const int SELECTION_RECTANGLE_THICKNESS = 1;
        private const float SELECTION_RECTANGLE_DASH_WIDTH = 10F;

        private bool _isSelecting = false;
        private bool _isRightDown = false;

        public ScreenshotForm() {
            InitializeComponent();
            CaptureScreen();

            this.Load += ScreenshotForm_Load;
            this.MouseDown += ScreenshotForm_MouseDown;
            this.MouseUp += ScreenshotForm_MouseUp;
            this.MouseMove += ScreenshotForm_MouseMove;
        }

        private void ScreenshotForm_MouseDown(object? sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _start = e.Location;
                _isSelecting = true;
            }
            else if (e.Button == MouseButtons.Right) {
                if (_isSelecting) {
                    _isSelecting = false;
                    _region = new Rectangle();
                    Invalidate();
                } else {
                    _isRightDown = true;
                }
            }
        }

        private void ScreenshotForm_MouseMove(object? sender, MouseEventArgs e) {
            if (!_isSelecting) return;
            _end = e.Location;

            int x = Math.Min(_start.X, _end.X);
            int y = Math.Min(_start.Y, _end.Y);
            int width = Math.Abs(_start.X - _end.X);
            int height = Math.Abs(_start.Y - _end.Y);
            _region = new Rectangle(x, y, width, height);

            Invalidate();
        }

        private void ScreenshotForm_MouseUp(object? sender, MouseEventArgs e) {
            if (_isRightDown && e.Button == MouseButtons.Right) {
                _isRightDown = false;
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
                
            if (!_isSelecting) return;
            _isSelecting = false;
            CaptureRegion();
            this.Close();
        }

        private void ScreenshotForm_Load(object? sender, EventArgs e) {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            this.Location = bounds.Location;
            this.Bounds = bounds;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (_screenshot == null) return;

            // full screenshot
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            e.Graphics.DrawImage(_screenshot, 0, 0);

            // dark overlay
            using (SolidBrush brush = new(Color.FromArgb(120, 0, 0, 0))) {
                Region region = new(bounds);
                region.Exclude(_region);
                e.Graphics.FillRegion(brush, region);
            }

            // selection rectangle
            using Pen pen = new(Color.White, SELECTION_RECTANGLE_THICKNESS);
            pen.DashStyle = DashStyle.Dash;
            pen.DashPattern = [SELECTION_RECTANGLE_DASH_WIDTH, SELECTION_RECTANGLE_DASH_WIDTH];
            e.Graphics.DrawRectangle(pen, _region);
        }

        private void CaptureScreen() {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            _screenshot = new Bitmap(bounds.Width, bounds.Height);
            using Graphics g = Graphics.FromImage(_screenshot);
            g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
        }

        private void CaptureRegion() {
            if (_screenshot == null) return;
            if (_region.Width < 1 || _region.Height < 1) return;

            Bitmap crop = new Bitmap(_region.Width, _region.Height);
            using Graphics g = Graphics.FromImage(crop);
            g.DrawImage(_screenshot, new Rectangle(0, 0, crop.Width, crop.Height), _region, GraphicsUnit.Pixel);

            screenCapture = UpscaleImage(crop);
            DialogResult = DialogResult.OK;
        }

        // scale the image up, for OCR
        private Bitmap UpscaleImage(Bitmap bmp) {
            using Bitmap scaled = new Bitmap(bmp.Width * 3, bmp.Height * 3);
            scaled.SetResolution(300, 300); // 300, 300 DPI
            using Graphics g2 = Graphics.FromImage(scaled);
            g2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g2.DrawImage(bmp, 0, 0, scaled.Width, scaled.Height);
            return scaled.Clone(new Rectangle(0, 0, scaled.Width, scaled.Height), PixelFormat.Format24bppRgb); // remove alpha channel
        }
    }
}
