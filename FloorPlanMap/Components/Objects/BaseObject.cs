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
using System.Timers;
using System.Windows.Threading;

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

                _tFootstep.Start();
            };

            base.Unloaded += (object sender, RoutedEventArgs e) => {
                base.OnXChangedEvent -= BaseObject_OnXChangedEvent;
                base.OnYChangedEvent -= BaseObject_OnYChangedEvent;

                _subscription.Dispose();

                CleanupFootprints();

                _tFootstep.Stop();
            };

            _tFootstep.Tick += HandleFootstepFadeOut;
        }

        #region "Handle Footstep Fade Out"
        private DispatcherTimer _tFootstep = new DispatcherTimer() {
            Interval = TimeSpan.FromMilliseconds(1000),
        };
        private void HandleFootstepFadeOut(object sender, EventArgs e) {
            if (_footprints.Count == 0) return;
            double total = _footprintDuration.TotalMilliseconds;
            double opacity = 1;
            for (var i=_footprints.Count-1; i>=0; i--) {
                DateTime time = _footprints[i].timestamp;
                BaseFootprint footprint = _footprints[i].footprint;

                TimeSpan span = DateTime.Now - time;
                double ms = span.TotalMilliseconds;

                /// do target fading
                if (ms > total && opacity == 1) {
                    opacity = Math.Max(1 - (ms - total) / total, 0);
                }

                double targetOpacity = opacity;
                opacity = Math.Max(((total - ms) / total), 0);
                double startOpacity = opacity;

                footprint.SetAsync(() => {
                    footprint.TargetOpacity = targetOpacity;
                    footprint.StartOpacity = startOpacity;
                });
            }
            // Remove Faded Steps
            do {
                FootPrintUnit unit = _footprints.First();
                BaseFootprint footprint = unit.footprint;

                bool shouldBreak = false;

                footprint.SetSync(() => {
                    if (footprint.TargetOpacity > 0.05 || footprint.StartOpacity > 0.05) {
                        shouldBreak = true; return;
                    }
                    (footprint.Parent as Panel).Children.Remove(footprint);
                });
                if (shouldBreak) break;

                _footprints.RemoveAt(0);

            } while (_footprints.Count > 0);
        }
        #endregion "Handle Footstep Fade Out"

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
        private struct FootPrintUnit {
            public DateTime timestamp;
            public BaseFootprint footprint;
        }
        private List<FootPrintUnit> _footprints = new List<FootPrintUnit>();
        private void CleanupFootprints() {
            foreach (FootPrintUnit footprintunit in _footprints) {
                (footprintunit.footprint.Parent as Panel).Children.Remove(footprintunit.footprint);
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
            if (_footprintType == null) return;
            (this.Parent as Panel).Dispatcher.BeginInvoke(new Action(
                () => {
                    BaseFootprint instance;
                    BaseFootprint lfp = _footprints.Count == 0 ? null : _footprints.Last().footprint;
                    if (lfp == null || !lfp.AngleMatches((double)lastx, (double)lasty, x, y)) {
                        //instance = Activator.CreateInstance(_footprintType) as BaseFootprint;
                        instance = _footprintType.DeepClone();
                        instance.X = (double)lastx;
                        instance.Y = (double)lasty;
                        instance.Size = 3;
                        instance.StartOpacity = 1;
                        instance.TargetOpacity = 1;
                        _footprints.Add(new FootPrintUnit() {
                            timestamp = DateTime.Now,
                            footprint = instance
                        });
                        (this.Parent as Panel).Children.Add(instance);
                    } else {
                        instance = lfp;
                    }
                    instance.TargetX = x;
                    instance.TargetY = y;
                }
            ));
        }
        #endregion "Handle XYChanged"

        #region "Normal Properties"

        #region "FootprintDuration"
        private TimeSpan _footprintDuration = TimeSpan.FromMilliseconds(10000);
        public TimeSpan FootprintDuration {
            get { return _footprintDuration; }
            set { _footprintDuration = value; }
        }
        #endregion "FootprintDuration"

        #region "FootprintType"
        //private Type _footprintType = null;
        //public Type FootprintType {
        //    get { return _footprintType; }
        //    set {
        //        if (value != null && !typeof(BaseFootprint).IsAssignableFrom(value)) {
        //            throw new ArgumentException("FootprintType type error. Must inherit BaseFootprint.");
        //        }
        //        _footprintType = value;
        //    }
        //}
        private BaseFootprint _footprintType = null;
        public BaseFootprint FootprintType {
            get { return _footprintType; }
            set { _footprintType = value; }
        }
        #endregion "FootprintType"

        #endregion "Normal Properties"
    }
}
