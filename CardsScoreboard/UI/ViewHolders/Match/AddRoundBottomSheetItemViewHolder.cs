using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.ViewModel.Matchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.ViewHolders.Match
{
    public class AddRoundBottomSheetItemViewHolder : RecyclerView.ViewHolder
    {
        private TextView _playerNameTv;
        private ImageButton _minusBt;
        private ImageButton _plusBt;
        private EditText _editTextNumber;
        private CheckBox _checkBox;
        private NewRoundModel _newRoundModel;

        public AddRoundBottomSheetItemViewHolder(View itemView) : base(itemView)
        {
            _playerNameTv = itemView.FindViewById<TextView>(Resource.Id.playerNameTv);
            _minusBt = itemView.FindViewById<ImageButton>(Resource.Id.minusBt);
            _plusBt = itemView.FindViewById<ImageButton>(Resource.Id.plusBt);
            _editTextNumber = itemView.FindViewById<EditText>(Resource.Id.editTextNumber);
            _checkBox = itemView.FindViewById<CheckBox>(Resource.Id.checkBox);

            _minusBt.Click -= MinusBtClick;
            _minusBt.Click += MinusBtClick;

            _plusBt.Click -= PlusBtClick;
            _plusBt.Click += PlusBtClick;

            _checkBox.CheckedChange -= CheckBoxClick;
            _checkBox.CheckedChange += CheckBoxClick;

            _editTextNumber.TextChanged -= EditTextNumberTextChanged;
            _editTextNumber.TextChanged += EditTextNumberTextChanged;
        }

        private void EditTextNumberTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(e.Text.ToString()))
                _newRoundModel.SelectPoints.Invoke(_newRoundModel, 0);
            else
                _newRoundModel.SelectPoints.Invoke(_newRoundModel, int.Parse(e.Text.ToString()));
        }

        private void CheckBoxClick(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            _newRoundModel.SelectQueenCommand.Invoke(_newRoundModel, e.IsChecked);
        }

        private void PlusBtClick(object sender, EventArgs e)
        {
            _newRoundModel.AddPointCommand.Invoke(_newRoundModel);
        }

        private void MinusBtClick(object sender, EventArgs e)
        {
            _newRoundModel.MinusPointCommand.Invoke(_newRoundModel);
        }

        internal void Bind(ViewModel.Matchs.NewRoundModel newRoundModel)
        {
            _newRoundModel = newRoundModel;

            _playerNameTv.Text = newRoundModel.Player.Name;
            _editTextNumber.Text = newRoundModel.HeartsPoints.ToString();
            _checkBox.Checked = newRoundModel.HasQueen;
        }
    }
}