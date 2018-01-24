using FloorPlanMap.Components.Footprints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FloorPlanMap.Components.Objects {
    public class BaseObject : BaseComponent {
        private IDisposable _subscription;
        public BaseObject() {

            base.Loaded += (object sender, RoutedEventArgs e) => {
                base.OnXChangedEvent += BaseObject_OnXChangedEvent;
                base.OnYChangedEvent += BaseObject_OnYChangedEvent;

                _subscription = Observable.CombineLatest(_sjXChanged, _sjYChanged)
                    .Select((o) => {
                        return Observable.Timer(TimeSpan.FromMilliseconds(1))
                            .Select((t) => o);
                    })
                    .Switch()
                    .Subscribe(X => HandleXYChanged(X[0], X[1]));
            };

            base.Unloaded += (object sender, RoutedEventArgs e) => {
                base.OnXChangedEvent -= BaseObject_OnXChangedEvent;
                base.OnYChangedEvent -= BaseObject_OnYChangedEvent;

                _subscription.Dispose();

                CleanupFootprints();
            };

        }

        #region "Handle XYChanged"
        private Subject<double> _sjXChanged = new Subject<double>();
        private Subject<double> _sjYChanged = new Subject<double>();

        private void BaseObject_OnXChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            _sjXChanged.OnNext((double)e.NewValue);
        }

        private void BaseObject_OnYChangedEvent(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            _sjYChanged.OnNext((double)e.NewValue);
        }

        private double? _lastx = null;
        private double? _lasty = null;
        private List<BaseFootprint> _footprints = new List<BaseFootprint>();
        private void CleanupFootprints() {
            foreach (BaseFootprint footprint in _footprints) {
                (footprint.Parent as Panel).Dispatcher.BeginInvoke(new Action(
                        () => (footprint.Parent as Panel).Children.Remove(footprint)
                    ));
            }
            _footprints.Clear();
        }
        private void HandleXYChanged(double x, double y) {
            double? lastx = _lastx;
            double? lasty = _lasty;
            _lastx = x;
            _lasty = y;
            // Ready. Got all tracks of BaseObject
            if (lastx == null || lasty == null) return;
            //Console.WriteLine("LastX: {0}, LastY: {1}, X: {2}, Y: {3}", lastx, lasty, x, y);
            if (_footprintType == null) return;
            (this.Parent as Panel).Dispatcher.BeginInvoke(new Action(
                () => {
                    BaseFootprint instance = Activator.CreateInstance(_footprintType) as BaseFootprint;
                    instance.X = (double)lastx;
                    instance.Y = (double)lasty;
                    instance.Size = 3;
                    instance.StartOpacity = 1;
                    instance.TargetOpacity = 1;
                    instance.TargetX = x;
                    instance.TargetY = y;
                    _footprints.Add(instance);
                    (this.Parent as Panel).Children.Add(instance);
                }
            ));
        }
        #endregion "Handle XYChanged"

        #region "Normal Properties"

        #region "FootprintType"
        private Type _footprintType = null;
        public Type FootprintType {
            get { return _footprintType; }
            set {
                if (value != null && !typeof(BaseFootprint).IsAssignableFrom(value)) {
                    throw new ArgumentException("FootprintType type error. Must inherit BaseFootprint.");
                }
                _footprintType = value;
            }
        }
        #endregion "FootprintType"

        #endregion "Normal Properties"
    }
}
