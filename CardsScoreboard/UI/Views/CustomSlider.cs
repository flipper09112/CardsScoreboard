using Android.App;
using Android.Content;
using Android.Media.TV;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Views
{
    public class CustomSlider : LinearLayout
    {
        public EventHandler ValueChange { get; set; }

        private Slider _slider;
        private TextView _firstLabel;
        private TextView _secondLabel;
        private TextView _thirdLabel;
        private TextView _fourthLabel;
        private TextView _fifthLabel;
        private TextView _sixthLabel;
        private TextView _seventhLabel;
        private TextView _eigthLabel;
        private TextView _ninethLabel;
        private TextView _tenthLabel;
        private List<TextView> _labelsList;

        public CustomSlider(Context context) : base(context)
        {
        }

        public CustomSlider(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CustomSlider(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public CustomSlider(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected CustomSlider(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();

            var inflater = LayoutInflater.From(Context);
            var view = inflater.Inflate(Resource.Layout.CustomSliderView, this, true);

            _slider = view.FindViewById<Slider>(Resource.Id.sliderMat);
            _firstLabel = view.FindViewById<TextView>(Resource.Id.firstLabel);
            _secondLabel = view.FindViewById<TextView>(Resource.Id.secondLabel);
            _thirdLabel = view.FindViewById<TextView>(Resource.Id.thirdLabel);
            _fourthLabel = view.FindViewById<TextView>(Resource.Id.fourthLabel);
            _fifthLabel = view.FindViewById<TextView>(Resource.Id.fifthLabel);
            _sixthLabel = view.FindViewById<TextView>(Resource.Id.sixthLabel);
            _seventhLabel = view.FindViewById<TextView>(Resource.Id.seventhLabel);
            _eigthLabel = view.FindViewById<TextView>(Resource.Id.eigthLabel);
            _ninethLabel = view.FindViewById<TextView>(Resource.Id.ninethLabel);
            _tenthLabel = view.FindViewById<TextView>(Resource.Id.tenthLabel);

            _labelsList = new List<TextView>()
            {
                _firstLabel,
                _secondLabel,
                _thirdLabel,
                _fourthLabel,
                _fifthLabel,
                _sixthLabel,
                _seventhLabel,
                _eigthLabel,
                _ninethLabel,
                _tenthLabel,
            };

            Initialize();
        }

        private void Initialize()
        {
            SetSliderConfig();
            SetDefault();
        }

        private void SetDefault()
        {
            _firstLabel.Visibility = ViewStates.Visible;
            _secondLabel.Visibility = ViewStates.Invisible;
            _thirdLabel.Visibility = ViewStates.Invisible;
            _fourthLabel.Visibility = ViewStates.Invisible;
            _fifthLabel.Visibility = ViewStates.Invisible;
            _sixthLabel.Visibility = ViewStates.Invisible;
            _seventhLabel.Visibility = ViewStates.Invisible;
            _eigthLabel.Visibility = ViewStates.Invisible;
            _ninethLabel.Visibility = ViewStates.Invisible;
            _tenthLabel.Visibility = ViewStates.Visible;
        }

        private void SetSliderConfig()
        {
            _slider.Value = 1;
            _slider.ValueFrom = 1;
            _slider.ValueTo = 10;
            _slider.StepSize = 1;

            _slider.Touch -= Slider_Touch;
            _slider.Touch += Slider_Touch;
        }

        private void Slider_Touch(object sender, View.TouchEventArgs e)
        {
            switch (e.Event.Action)
            {
                //case MotionEventActions.Move:
                case MotionEventActions.Up:
                    OnValueChange(_slider, _slider.Value, true);  //(Slider slider, float value, boolean fromUser)
                    break;
            }
            e.Handled = false;
        }

        //This footprint matches native library callback
        private void OnValueChange(Java.Lang.Object sender, float value, bool fromUser)
        {
            //Do what you want to do as if you are listening to ValueChanged event (or at least very near to actual event)
            SetLabels((int)value);
            ValueChange.Invoke((int)value, null);
        }

        private void SetLabels(int value)
        {
            for(int i = 1; i < _labelsList.Count - 1; i++)
            {
                _labelsList[i].Visibility = value - 1 == i ? ViewStates.Visible : ViewStates.Invisible;
            }
        }
    }
}