using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HuaWenCapture.Forms {
    public partial class ScreenshotForm : Form {
        private Bitmap? _fullscreenImage;
        private Bitmap? _screenshotImage;

        private Rectangle _captureRegion;
        private Point _startPoint;
        private Point _endPoint;

        private const int SELECTION_RECTANGLE_THICKNESS = 1;
        private const float SELECTION_RECTANGLE_DASH_WIDTH = 10F;

        private bool _isSelecting = false;
        private bool _isRightMouseButtonDown = false;

        public ScreenshotForm() {
            InitializeComponent();
            CaptureScreen();

            this.Load += OnFormLoad;
            this.MouseDown += OnMouseDown;
            this.MouseUp += OnMouseUp;
            this.MouseMove += OnMouseMove;
        }

        private void CaptureScreen() {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            _fullscreenImage = new Bitmap(bounds.Width, bounds.Height);
            using Graphics g = Graphics.FromImage(_fullscreenImage);
            g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
        }

        private void CaptureRegion() {
            if (_fullscreenImage == null) return;
            if (_captureRegion.Width < 1 || _captureRegion.Height < 1) return;

            Bitmap crop = new Bitmap(_captureRegion.Width, _captureRegion.Height);
            using Graphics g = Graphics.FromImage(crop);
            g.DrawImage(_fullscreenImage, new Rectangle(0, 0, crop.Width, crop.Height), _captureRegion, GraphicsUnit.Pixel);

            _screenshotImage = crop;
            DialogResult = DialogResult.OK;
        }

        public Bitmap? GetScreenshotBitmap() => _screenshotImage;

        public byte[]? GetScreenshotMemoryStream() {
            if (_screenshotImage == null) return null;
            MemoryStream stream = new();
            _screenshotImage.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }

        private void OnMouseDown(object? sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _startPoint = e.Location;
                _isSelecting = true;
            } else if (e.Button == MouseButtons.Right) {
                if (_isSelecting) {
                    _isSelecting = false;
                    _captureRegion = new Rectangle();
                    Invalidate();
                } else {
                    _isRightMouseButtonDown = true;
                }
            }
        }

        private void OnMouseMove(object? sender, MouseEventArgs e) {
            if (!_isSelecting) return;
            _endPoint = e.Location;

            int x = Math.Min(_startPoint.X, _endPoint.X);
            int y = Math.Min(_startPoint.Y, _endPoint.Y);
            int width = Math.Abs(_startPoint.X - _endPoint.X);
            int height = Math.Abs(_startPoint.Y - _endPoint.Y);
            _captureRegion = new Rectangle(x, y, width, height);

            Invalidate();
        }

        private void OnMouseUp(object? sender, MouseEventArgs e) {
            if (_isRightMouseButtonDown && e.Button == MouseButtons.Right) {
                _isRightMouseButtonDown = false;
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (!_isSelecting) return;
            _isSelecting = false;
            CaptureRegion();
            this.Close();
        }

        private void OnFormLoad(object? sender, EventArgs e) {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            this.Location = bounds.Location;
            this.Bounds = bounds;
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (_fullscreenImage == null) return;

            // full screenshot
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            e.Graphics.DrawImage(_fullscreenImage, 0, 0);

            // dark overlay
            using (SolidBrush brush = new(Color.FromArgb(120, 0, 0, 0))) {
                Region region = new(bounds);
                region.Exclude(_captureRegion);
                e.Graphics.FillRegion(brush, region);
            }

            // selection rectangle
            using Pen pen = new(Color.White, SELECTION_RECTANGLE_THICKNESS);
            pen.DashStyle = DashStyle.Dash;
            pen.DashPattern = [SELECTION_RECTANGLE_DASH_WIDTH, SELECTION_RECTANGLE_DASH_WIDTH];
            e.Graphics.DrawRectangle(pen, _captureRegion);
        }
    }
}
