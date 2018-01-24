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
        public BaseObject() {
            base.OnXChangedEvent += BaseObject_OnXChangedEvent;
            base.OnYChangedEvent += BaseObject_OnYChangedEvent;

            //Observable.CombineLatest(_sjXChanged, _sjYChanged)
            //    .Select<IList<double>, IObservable<IList<double>>>((o) => {
            //        return Observable.Timer(TimeSpan.FromMilliseconds(1))
            //            .Select<long, IList<double>>((t) => o);
            //    })
            //    .Switch()
            //    .Subscribe(X => HandleXYChanged(X[0], X[1]));

            Observable.CombineLatest(_sjXChanged, _sjYChanged)
                .Select((o) => {
                    return Observable.Timer(TimeSpan.FromMilliseconds(1))
                        .Select((t) => o);
                })
                .Switch()
                .Subscribe(X => HandleXYChanged(X[0], X[1]));
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
                    var instance = Activator.CreateInstance(_footprintType) as BaseFootprint;
                    instance.X = (double)lastx;
                    instance.Y = (double)lasty;
                    instance.TargetX = x;
                    instance.TargetY = y;
                    instance.Size = 3;
                    instance.StartOpacity = 1;
                    instance.TargetOpacity = 1;

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
