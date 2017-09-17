using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using RxExamples.Annotations;

namespace RxExamples.Pages
{
    /// <summary>
    /// Interaction logic for Flying.xaml
    /// </summary>
    public partial class Flying : UserControl
    {
        private IObservable<Text> ItemsStream
            => DrawingControl.ItemContainerGenerator.Items.Cast<Text>().ToObservable();

        public Flying()
        {
            InitializeComponent();
            DrawingControl.ItemsSource = GenerateItemSource();
            InitializeDrag();
        }

        private void InitializeDrag()
        {
            var mouseMove = Observable.FromEventPattern<MouseEventArgs>(DrawingControl, nameof(DrawingControl.MouseMove));
            var mouseUp = Observable.FromEventPattern<MouseEventArgs>(DrawingControl, nameof(DrawingControl.MouseUp));
            var mouseDown = Observable.FromEventPattern<MouseEventArgs>(DrawingControl, nameof(DrawingControl.MouseDown));

            var drag = mouseMove.SkipUntil(mouseDown).TakeUntil(mouseUp).Repeat();

            #region Flying invoke

            IObservable<Point> dragPoints = drag.Select(
                mouseEvent => mouseEvent.EventArgs.GetPosition(DrawingControl));

            ApplyFlyingDrag(dragPoints);

            #endregion
        }

        #region Flying Impl.       

        private void ApplyFlyingDrag(IObservable<Point> movePoints)
        {
            var loopSchedular = new EventLoopScheduler();

            movePoints
                .SelectMany(
                    point =>
                        ItemsStream.SelectMany(
                            (item, index) => Observable
                                .Return(new {item, Point = new Point(point.X + index, point.Y)})
                                .Delay(TimeSpan.FromMilliseconds(index * 75), loopSchedular))
                )
                .ObserveOnDispatcher()
                .Subscribe(x =>
                {
                    x.item.Left = x.Point.X;
                    x.item.Top = x.Point.Y;
                });
        }

        #endregion

        private List<Text> GenerateItemSource()
        {
            var overallLeftMargin = 0d;
            double MeasureString(string candidate)
            {
                var formattedText = new FormattedText(
                    candidate,
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(
                        DrawingControl.FontFamily,
                        DrawingControl.FontStyle,
                        DrawingControl.FontWeight,
                        DrawingControl.FontStretch),
                    DrawingControl.FontSize + 10,
                    Brushes.Black);

                var marginForCurrentCandidate = overallLeftMargin;
                overallLeftMargin += candidate == " " ? 10 : (formattedText.Width * 1.5);
                return marginForCurrentCandidate;
            }

            return "Rx can make text fly"
                .Select((c, i) => new Text
                {
                    Character = $"{c}",
                    Margin = new Thickness(MeasureString($"{c}"), 0, 0, 0)
                })
                .ToList();
        }
    }

    public class Text : INotifyPropertyChanged
    {
        private double _left = 0d;
        private double _top = 0d;
        public string Character { get; set; }
        public Thickness Margin { get; set; }

        public double Left
        {
            get => _left;
            set
            {
                _left = value;
                OnPropertyChanged();
            }
        }

        public double Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Impl.
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
