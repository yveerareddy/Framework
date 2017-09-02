
namespace WPFSample
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    ///    Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : Button
    {
        #region Static Fields

        /// <summary>
        ///    The enable scale transform animation property.
        /// </summary>
        public static readonly DependencyProperty EnableScaleTransformAnimationProperty =
           DependencyProperty.Register(
              "EnableScaleTransformAnimation",
              typeof(bool),
              typeof(ImageButton),
              new FrameworkPropertyMetadata(false));

        /// <summary>
        /// The foreground hover property.
        /// </summary>
        public static readonly DependencyProperty ForegroundHoverProperty = DependencyProperty.Register(
           "ForegroundHover",
           typeof(Brush),
           typeof(ImageButton),
           new FrameworkPropertyMetadata(null));

        /// <summary>
        ///    The foreground pressed property.
        /// </summary>
        public static readonly DependencyProperty ForegroundPressedProperty =
           DependencyProperty.Register(
              "ForegroundPressed",
              typeof(Brush),
              typeof(ImageButton),
              new FrameworkPropertyMetadata(null));

        /// <summary>
        ///    The foreground disabled property.
        /// </summary>
        public static readonly DependencyProperty ForegroundDisabledProperty =
           DependencyProperty.Register(
              "ForegroundDisabled",
              typeof(Brush),
              typeof(ImageButton),
              new FrameworkPropertyMetadata(null));

        /// <summary>
        ///    The image style property.
        /// </summary>
        public static readonly DependencyProperty ImageStyleProperty = DependencyProperty.Register(
           "ImageStyle",
           typeof(Style),
           typeof(ImageButton),
           new FrameworkPropertyMetadata(null, OnImageStyleChanged));

        /// <summary>
        ///    The foreground hover property.
        /// </summary>
        public static readonly DependencyProperty PathFillProperty = DependencyProperty.Register(
           "PathFill",
           typeof(Brush),
           typeof(ImageButton),
           new FrameworkPropertyMetadata(null));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///    Initializes a new instance of the <see cref="ImageButton" /> class.
        /// </summary>
        public ImageButton()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///    Gets or sets a value indicating whether enable scale transform animation.
        /// </summary>
        /// <value>
        ///    The enable scale transform animation.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        public bool EnableScaleTransformAnimation
        {
            get
            {
                return (bool)this.GetValue(EnableScaleTransformAnimationProperty);
            }

            set
            {
                this.SetValue(EnableScaleTransformAnimationProperty, value);
            }
        }

        /// <summary>
        ///    Gets or sets the foreground hover.
        /// </summary>
        /// <value>
        ///    The foreground hover.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        public Brush ForegroundHover
        {
            get
            {
                return (Brush)this.GetValue(ForegroundHoverProperty);
            }

            set
            {
                this.SetValue(ForegroundHoverProperty, value);
            }
        }

        /// <summary>
        ///    Gets or sets the foreground pressed.
        /// </summary>
        /// <value>
        ///    The foreground pressed.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        public Brush ForegroundPressed
        {
            get
            {
                return (Brush)this.GetValue(ForegroundPressedProperty);
            }

            set
            {
                this.SetValue(ForegroundPressedProperty, value);
            }
        }

        /// <summary>
        ///    Gets or sets the foreground disabled.
        /// </summary>
        /// <value>
        ///    The foreground disabled.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        public Brush ForegroundDisabled
        {
            get
            {
                return (Brush)this.GetValue(ForegroundDisabledProperty);
            }

            set
            {
                this.SetValue(ForegroundDisabledProperty, value);
            }
        }

        /// <summary>
        ///    Gets or sets the image style.
        /// </summary>
        /// <value>
        ///    The image style.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        public Style ImageStyle
        {
            get
            {
                return (Style)this.GetValue(ImageStyleProperty);
            }

            set
            {
                this.SetValue(ImageStyleProperty, value);
            }
        }

        /// <summary>
        ///    Gets or sets the foreground hover.
        /// </summary>
        /// <value>
        ///    The foreground hover.
        /// </value>
        [Browsable(true)]
        [Category("Behavior")]
        public Brush PathFill
        {
            get
            {
                return (Brush)this.GetValue(PathFillProperty);
            }

            set
            {
                this.SetValue(PathFillProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on image style changed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        internal void OnImageStyleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            var style = e.NewValue as Style;
            if (style != null)
            {
                if (style.TargetType == typeof(Path))
                {
                    var path = new Path { Style = style, Height = double.NaN, Width = double.NaN, Stretch = Stretch.Uniform };
                    var fillBinding = new Binding("PathFill") { Source = this, Mode = BindingMode.OneWay };
                    path.SetBinding(Shape.FillProperty, fillBinding);
                    this.Content = path;
                }
                else if (style.TargetType == typeof(Image))
                {
                    var image = new Image
                    {
                        Style = style,
                        Height = double.NaN,
                        Width = double.NaN,
                        Stretch = Stretch.Uniform
                    };
                    this.Content = image;
                }
            }
        }

        /// <summary>
        /// The on image style changed.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void OnImageStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ImageButton).OnImageStyleChanged(e);
        }

        #endregion
    }
}