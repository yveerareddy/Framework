
namespace WPFSample
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for CalloutToolTip.xaml
    /// </summary>
    public partial class CalloutToolTip
    {
        #region Constants and Fields

        private const double _pointerSize = 7;

        private const double _XMargin = 16;

        private const double _YMargin = 8;

        private readonly PointCollection _points = new PointCollection();

        private Grid _toolTipGrid;

        private Polygon _toolTipShape;

        private TextBlock _toolTipText;

        #endregion

        #region Constructors and Destructors

        public CalloutToolTip()
        {
            this.InitializeComponent();

            Placement = PlacementMode.Bottom;

            // default position, the pointer is at 1/4 of the tooltip width
            PointerPosition = .25;
        }

        #endregion

        #region Public Properties

        public PointCollection Points
        {
            get
            {
                return this._points;
            }
        }

        /// <summary>
        /// A percentage representing the position of the pointer in the tooltip box
        /// </summary>
        /// <value>
        /// The pointer position.
        /// </value>
        public double PointerPosition { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Occurs after the tool tip opened
        /// </summary>
        /// <param name="e">
        /// not used 
        /// </param>
        protected override void OnOpened(RoutedEventArgs e)
        {
            base.OnOpened(e);

            var frameworkElement = this.PlacementTarget as FrameworkElement;
            if (frameworkElement == null)
            {
                return;
            }

            Point target = this.PlacementTarget.PointToScreen(new Point(0, 0));
            Point toolTip = this.PointToScreen(new Point(0, 0));

            bool upsideDown = (toolTip.Y < target.Y);

            //Added 2 statements as System was crashing when there is not tooltip Message
            var actualWidth = _toolTipText != null ? _toolTipText.ActualWidth : 0;
            var actualHeight = _toolTipText != null ? _toolTipText.ActualHeight : 0;

            double height = actualHeight + 2 * _YMargin;
            double width = actualWidth + 2 * _XMargin;

            double distanceToTarget = target.X - toolTip.X;
            double halfTargetWidth = frameworkElement.ActualWidth / 2.0;

            // default distance from left side of the tooltip to the pointer tip
            double distanceToPointer = PointerPosition * width;

            double pointerPos;

            // if the tooltip is pushed back in from the screen left edge
            if (target.X + halfTargetWidth - distanceToPointer < SystemParameters.VirtualScreenLeft)
            {
                this.HorizontalOffset = halfTargetWidth - distanceToPointer;
                pointerPos = target.X - SystemParameters.VirtualScreenLeft + halfTargetWidth;
            }
            // if the tooltip is pushed back in from the screen right edge
            else if (target.X + halfTargetWidth - distanceToPointer + width > SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth)
            {
                this.HorizontalOffset = 0;

                // move the pointer to point to center of the target
                pointerPos = distanceToTarget + halfTargetWidth;
            }
            // if the tooltip is pushed back from the right edge of the 1st monitor (in 2-monitor setup)
            else if (target.X + halfTargetWidth > toolTip.X + distanceToPointer + 1)
            {
                this.HorizontalOffset = 0;

                // move the pointer to point to center of the target
                pointerPos = target.X + halfTargetWidth - toolTip.X;
            }
            // the tooltip is displayed normally
            else
            {
                this.HorizontalOffset = halfTargetWidth - distanceToPointer;
                pointerPos = distanceToPointer;
            }

            // make sure the pointer is within the rectangle
            pointerPos = Math.Min(width - _pointerSize, pointerPos);
            pointerPos = Math.Max(_pointerSize, pointerPos);

            // the points only dictate the shape of the tooltip, not its position.
            this._points.Clear();

            if (upsideDown)
            {
                this._points.Add(new Point(0, 0));
                this._points.Add(new Point(width, 0));
                this._points.Add(new Point(width, height));
                this._points.Add(new Point(pointerPos + _pointerSize, height));
                this._points.Add(new Point(pointerPos, height + _pointerSize));
                this._points.Add(new Point(pointerPos - _pointerSize, height));
                this._points.Add(new Point(0, height));

                if (_toolTipText != null) //Added for System crash when tooltip is empty
                    _toolTipText.Margin = new Thickness(_XMargin, _YMargin, _XMargin, _YMargin + _pointerSize);
            }
            else
            {
                this._points.Add(new Point(0, _pointerSize));
                this._points.Add(new Point(pointerPos - _pointerSize, _pointerSize));
                this._points.Add(new Point(pointerPos, 0));
                this._points.Add(new Point(pointerPos + _pointerSize, _pointerSize));
                this._points.Add(new Point(width, _pointerSize));
                this._points.Add(new Point(width, height + _pointerSize));
                this._points.Add(new Point(0, height + _pointerSize));

                if (_toolTipText != null) //Added for System crash when tooltip is empty
                    _toolTipText.Margin = new Thickness(_XMargin, _YMargin + _pointerSize, _XMargin, _YMargin);
            }

            // redraw the tooltip shape
            this._toolTipShape.InvalidateMeasure();
            this._toolTipShape.InvalidateVisual();
        }

        /// <summary>
        /// The grid_ initialized.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// not used 
        /// </param>
        private void Grid_Initialized(object sender, EventArgs e)
        {
            this._toolTipGrid = sender as Grid;

            if (this.PlacementTarget != null)
                ToolTipService.SetShowDuration(this.PlacementTarget, 300000);  // 5 minutes
        }

        /// <summary>
        /// Set data binding for the polygon
        /// </summary>
        /// <param name="sender">
        /// the tool tip polygon 
        /// </param>
        /// <param name="e">
        /// not used 
        /// </param>
        private void toolTipShape_Initialized(object sender, EventArgs e)
        {
            this._toolTipShape = sender as Polygon;
            if (this._toolTipShape != null)
            {
                var pointsBinding = new Binding("Points") { Source = this };
                this._toolTipShape.SetBinding(Polygon.PointsProperty, pointsBinding);

                var strokeBinding = new Binding("BorderBrush") { Source = this };
                this._toolTipShape.SetBinding(Shape.StrokeProperty, strokeBinding);

                var fillBinding = new Binding("Background") { Source = this };
                this._toolTipShape.SetBinding(Shape.FillProperty, fillBinding);
            }
        }

        /// <summary>
        /// Set the tool tip text
        /// </summary>
        /// <param name="sender">
        /// toolTipText 
        /// </param>
        /// <param name="e">
        /// not used 
        /// </param>
        private void toolTipText_Initialized(object sender, EventArgs e)
        {
            this._toolTipText = sender as TextBlock;
            if (this._toolTipText != null)
            {
                var contentBinding = new Binding("Content") { Source = this };
                this._toolTipText.SetBinding(TextBlock.TextProperty, contentBinding);

                // assume the tooltip is not upside down
                _toolTipText.Margin = new Thickness(_XMargin, _YMargin + _pointerSize, _XMargin, _YMargin);
            }
        }

        #endregion
    }
}