using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShooter
{
    public class RegionSelector : Form
    {
        private Rectangle selectedRegion;
        private Point startPoint;
        private bool isSelecting = false;
        private bool isDragging = false;
        private bool isResizing = false;
        private Point dragStartPoint;
        private Rectangle originalRegion;
        private Bitmap screenBitmap;
        private float darkenOpacity = 0.7f;
        private SizeGripDirection resizeDirection;

        private enum SizeGripDirection { None, TopLeft, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left }

        public Rectangle SelectedRegion => selectedRegion;

        // Конструктор для новой области
        public RegionSelector()
        {
            InitializeForm();
            CaptureScreen();

            this.Load += (s, e) =>
            {
                this.TopMost = true;
                this.Activate();
            };

            this.Shown += (s, e) =>
            {
                this.TopMost = true;
                this.Activate();
                this.Focus();
            };
        }

        // Конструктор для редактирования существующей области
        public RegionSelector(Rectangle existingRegion)
        {
            InitializeForm();
            CaptureScreen();
            selectedRegion = existingRegion;
            originalRegion = existingRegion;

            this.Load += (s, e) =>
            {
                this.TopMost = true;
                this.Activate();
            };

            this.Shown += (s, e) =>
            {
                this.TopMost = true;
                this.Activate();
                this.Focus();
            };
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Cross;
            this.BackColor = Color.Black;
            this.Opacity = darkenOpacity;

            this.MouseDown += RegionSelector_MouseDown;
            this.MouseMove += RegionSelector_MouseMove;
            this.MouseUp += RegionSelector_MouseUp;
            this.KeyDown += RegionSelector_KeyDown;
            this.Paint += RegionSelector_Paint;
        }

        private void CaptureScreen()
        {
            screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                     Screen.PrimaryScreen.Bounds.Height);

            using (Graphics g = Graphics.FromImage(screenBitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, screenBitmap.Size);
            }
        }

        private const int GripSize = 8;

        private SizeGripDirection GetSizeGripAt(Point point)
        {
            if (selectedRegion == Rectangle.Empty) return SizeGripDirection.None;

            Rectangle region = selectedRegion;

            // Угловые grip'ы
            Rectangle topLeft = new Rectangle(region.X - GripSize / 2, region.Y - GripSize / 2, GripSize, GripSize);
            Rectangle topRight = new Rectangle(region.Right - GripSize / 2, region.Y - GripSize / 2, GripSize, GripSize);
            Rectangle bottomLeft = new Rectangle(region.X - GripSize / 2, region.Bottom - GripSize / 2, GripSize, GripSize);
            Rectangle bottomRight = new Rectangle(region.Right - GripSize / 2, region.Bottom - GripSize / 2, GripSize, GripSize);

            // Боковые grip'ы
            Rectangle top = new Rectangle(region.X + region.Width / 2 - GripSize / 2, region.Y - GripSize / 2, GripSize, GripSize);
            Rectangle bottom = new Rectangle(region.X + region.Width / 2 - GripSize / 2, region.Bottom - GripSize / 2, GripSize, GripSize);
            Rectangle left = new Rectangle(region.X - GripSize / 2, region.Y + region.Height / 2 - GripSize / 2, GripSize, GripSize);
            Rectangle right = new Rectangle(region.Right - GripSize / 2, region.Y + region.Height / 2 - GripSize / 2, GripSize, GripSize);

            if (topLeft.Contains(point)) return SizeGripDirection.TopLeft;
            if (topRight.Contains(point)) return SizeGripDirection.TopRight;
            if (bottomLeft.Contains(point)) return SizeGripDirection.BottomLeft;
            if (bottomRight.Contains(point)) return SizeGripDirection.BottomRight;
            if (top.Contains(point)) return SizeGripDirection.Top;
            if (bottom.Contains(point)) return SizeGripDirection.Bottom;
            if (left.Contains(point)) return SizeGripDirection.Left;
            if (right.Contains(point)) return SizeGripDirection.Right;
            if (region.Contains(point)) return SizeGripDirection.None; // Для перемещения

            return SizeGripDirection.None;
        }

        private Rectangle ResizeRegion(Rectangle original, Point start, Point current, SizeGripDirection direction)
        {
            int deltaX = current.X - start.X;
            int deltaY = current.Y - start.Y;

            int x = original.X;
            int y = original.Y;
            int width = original.Width;
            int height = original.Height;

            switch (direction)
            {
                case SizeGripDirection.TopLeft:
                    x = Math.Max(0, original.X + deltaX);
                    y = Math.Max(0, original.Y + deltaY);
                    width = original.Width - deltaX;
                    height = original.Height - deltaY;
                    break;
                case SizeGripDirection.Top:
                    y = Math.Max(0, original.Y + deltaY);
                    height = original.Height - deltaY;
                    break;
                case SizeGripDirection.TopRight:
                    y = Math.Max(0, original.Y + deltaY);
                    width = original.Width + deltaX;
                    height = original.Height - deltaY;
                    break;
                case SizeGripDirection.Right:
                    width = original.Width + deltaX;
                    break;
                case SizeGripDirection.BottomRight:
                    width = original.Width + deltaX;
                    height = original.Height + deltaY;
                    break;
                case SizeGripDirection.Bottom:
                    height = original.Height + deltaY;
                    break;
                case SizeGripDirection.BottomLeft:
                    x = Math.Max(0, original.X + deltaX);
                    width = original.Width - deltaX;
                    height = original.Height + deltaY;
                    break;
                case SizeGripDirection.Left:
                    x = Math.Max(0, original.X + deltaX);
                    width = original.Width - deltaX;
                    break;
            }

            // Ограничиваем минимальный размер
            width = Math.Max(10, width);
            height = Math.Max(10, height);

            // Ограничиваем в пределах экрана
            x = Math.Max(0, Math.Min(Screen.PrimaryScreen.Bounds.Width - width, x));
            y = Math.Max(0, Math.Min(Screen.PrimaryScreen.Bounds.Height - height, y));

            return new Rectangle(x, y, width, height);
        }

        private void UpdateCursor(Point location)
        {
            var grip = GetSizeGripAt(location);

            switch (grip)
            {
                case SizeGripDirection.TopLeft:
                case SizeGripDirection.BottomRight:
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case SizeGripDirection.TopRight:
                case SizeGripDirection.BottomLeft:
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case SizeGripDirection.Top:
                case SizeGripDirection.Bottom:
                    this.Cursor = Cursors.SizeNS;
                    break;
                case SizeGripDirection.Left:
                case SizeGripDirection.Right:
                    this.Cursor = Cursors.SizeWE;
                    break;
                case SizeGripDirection.None:
                    if (selectedRegion != Rectangle.Empty && selectedRegion.Contains(location))
                        this.Cursor = Cursors.SizeAll;
                    else
                        this.Cursor = Cursors.Cross;
                    break;
            }
        }

        // метод Paint
        private void RegionSelector_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем скриншот экрана
            e.Graphics.DrawImage(screenBitmap, 0, 0);

            // Затемняем весь экран
            using (Brush darkBrush = new SolidBrush(Color.FromArgb((int)(255 * darkenOpacity), Color.Black)))
            {
                e.Graphics.FillRectangle(darkBrush, this.ClientRectangle);
            }

            // Рисуем выделенную область (светлую)
            if (selectedRegion != Rectangle.Empty && selectedRegion.Width > 0 && selectedRegion.Height > 0)
            {
                // Восстанавливаем оригинальное изображение в выделенной области
                e.Graphics.DrawImage(screenBitmap, selectedRegion, selectedRegion, GraphicsUnit.Pixel);

                // Рисуем рамку выделения
                using (Pen borderPen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(borderPen, selectedRegion);
                }

                // Показываем координаты и размер
                string sizeText = $"{selectedRegion.Width} x {selectedRegion.Height}";
                using (Brush textBrush = new SolidBrush(Color.White))
                using (Font font = new Font("Arial", 12))
                {
                    e.Graphics.DrawString(sizeText, font, textBrush,
                                        selectedRegion.X, selectedRegion.Y - 25);
                }
            }
        }

        private void RegionSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var grip = GetSizeGripAt(e.Location);

                if (grip != SizeGripDirection.None && selectedRegion != Rectangle.Empty)
                {
                    // Начало изменения размера
                    isResizing = true;
                    resizeDirection = grip;
                    startPoint = e.Location;
                    originalRegion = selectedRegion;
                }
                else if (selectedRegion != Rectangle.Empty && selectedRegion.Contains(e.Location))
                {
                    // Начало перемещения области
                    isDragging = true;
                    dragStartPoint = e.Location;
                    originalRegion = selectedRegion;
                }
                else
                {
                    // Начало создания новой области
                    startPoint = e.Location;
                    selectedRegion = new Rectangle(e.Location, Size.Empty);
                    isSelecting = true;
                }
                this.Invalidate();
            }
        }

        private void RegionSelector_MouseMove(object sender, MouseEventArgs e)
        {
            // Обновляем курсор
            UpdateCursor(e.Location);

            if (isSelecting)
            {
                // Выделение новой области
                int x = Math.Min(startPoint.X, e.X);
                int y = Math.Min(startPoint.Y, e.Y);
                int width = Math.Abs(startPoint.X - e.X);
                int height = Math.Abs(startPoint.Y - e.Y);

                selectedRegion = new Rectangle(x, y, width, height);
            }
            else if (isDragging)
            {
                // Перемещение области
                int deltaX = e.X - dragStartPoint.X;
                int deltaY = e.Y - dragStartPoint.Y;

                int newX = Math.Max(0, Math.Min(Screen.PrimaryScreen.Bounds.Width - selectedRegion.Width,
                                              originalRegion.X + deltaX));
                int newY = Math.Max(0, Math.Min(Screen.PrimaryScreen.Bounds.Height - selectedRegion.Height,
                                              originalRegion.Y + deltaY));

                selectedRegion = new Rectangle(newX, newY, selectedRegion.Width, selectedRegion.Height);
            }
            else if (isResizing)
            {
                // Изменение размера области
                selectedRegion = ResizeRegion(originalRegion, startPoint, e.Location, resizeDirection);
            }

            if (isSelecting || isDragging || isResizing)
            {
                this.Invalidate();
            }
        }

        private void RegionSelector_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isSelecting = false;
                isDragging = false;
                isResizing = false;

                // Если область слишком маленькая - сбрасываем (только при создании новой)
                if (isSelecting && (selectedRegion.Width < 5 || selectedRegion.Height < 5))
                {
                    selectedRegion = Rectangle.Empty;
                }

                this.Invalidate();
            }
        }

        // обработчик клавиш
        private void RegionSelector_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    // Подтверждение выбора
                    if (selectedRegion != Rectangle.Empty && selectedRegion.Width > 0 && selectedRegion.Height > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    break;

                case Keys.Escape:
                    // Отмена
                    selectedRegion = Rectangle.Empty;
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;

                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    // Изменение выделенной области стрелками
                    if (selectedRegion != Rectangle.Empty)
                    {
                        AdjustSelection(e.KeyCode, e.Control);
                    }
                    break;
            }
        }

        private void AdjustSelection(Keys key, bool isControlPressed)
        {
            int step = isControlPressed ? 10 : 1; // Больший шаг с Ctrl

            switch (key)
            {
                case Keys.Up:
                    selectedRegion.Y = Math.Max(0, selectedRegion.Y - step);
                    break;
                case Keys.Down:
                    selectedRegion.Y = Math.Min(Screen.PrimaryScreen.Bounds.Height - selectedRegion.Height,
                                              selectedRegion.Y + step);
                    break;
                case Keys.Left:
                    selectedRegion.X = Math.Max(0, selectedRegion.X - step);
                    break;
                case Keys.Right:
                    selectedRegion.X = Math.Min(Screen.PrimaryScreen.Bounds.Width - selectedRegion.Width,
                                              selectedRegion.X + step);
                    break;
            }
            this.Invalidate();
        }
    }
}
