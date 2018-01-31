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
                        return Observable.Timer(TimeSpan.FromMilliseconds(20))
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
        private static readonly double _timerInterval = 1000;
        private DispatcherTimer _tFootstep = new DispatcherTimer() {
            Interval = TimeSpan.FromMilliseconds(_timerInterval),
        };
        private void HandleFootstepFadeOut(object sender, EventArgs e) {
            if (_footprints.Count == 0) return;
            double total = _footprintDuration.TotalMilliseconds;

            for (var i=_footprints.Count-1; i>=0; i--) {
                DateTime st = _footprints[i].createtimestamp;
                DateTime et = _footprints[i].modifytimestamp;
                BaseFootprint footprint = _footprints[i].footprint;

                TimeSpan spst = DateTime.Now - st;
                double msst = spst.TotalMilliseconds;
                TimeSpan spet = DateTime.Now - et;
                double mset = spet.TotalMilliseconds;

                double dimsst = total - msst;
                double dimset = total - mset;
                double startopacity = Math.Max(dimsst / total, 0);
                double targetopacity = Math.Max(dimset / total, 0);

                if (dimsst < -(_timerInterval*2) && dimset < -(_timerInterval*2)) {
                    _footprints.RemoveAt(i);
                    footprint.SetAsync(() => (footprint.Parent as Panel).Children.Remove(footprint));
                } else {
                    footprint.SetAsync(() => {
                        footprint.TargetOpacity = targetopacity;
                        footprint.StartOpacity = startopacity;
                    });
                }
            }
        }
        #endregion "Handle Footstep Fade Out"

        #region "Handle XYChanged - Extend or create new footprint"
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
        private class FootPrintUnit {
            public DateTime createtimestamp { get; set; }
            public DateTime modifytimestamp { get; set; }
            public BaseFootprint footprint { get; set; }
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
                    FootPrintUnit lfpu = _footprints.Count == 0 ? null : _footprints.Last();
                    BaseFootprint lfp = lfpu == null ? null : lfpu.footprint;
                    if (lfp == null || !lfp.AngleMatches((double)lastx, (double)lasty, x, y)) {
                        //instance = Activator.CreateInstance(_footprintType) as BaseFootprint;
                        instance = _footprintType.DeepClone();
                        instance.X = (double)lastx;
                        instance.Y = (double)lasty;
                        instance.Size = 3;
                        instance.StartOpacity = 1;
                        instance.TargetOpacity = 1;
                        _footprints.Add(new FootPrintUnit() {
                            createtimestamp = DateTime.Now,
                            modifytimestamp = DateTime.Now,
                            footprint = instance
                        });
                        (this.Parent as Panel).Children.Add(instance);
                    } else {
                        instance = lfp;
                        lfpu.modifytimestamp = DateTime.Now;
                    }
                    instance.TargetX = x;
                    instance.TargetY = y;
                }
            ));
        }
        #endregion "Handle XYChanged - Extend or create new footprint"

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
