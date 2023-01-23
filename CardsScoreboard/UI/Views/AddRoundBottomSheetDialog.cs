using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardsScoreboard.Models.General;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.UI.Adapters.Match;
using CardsScoreboard.ViewModel.Matchs;
using System.Windows.Input;

namespace CardsScoreboard.UI.Views
{
    public class AddRoundBottomSheetDialog : BottomSheetDialogFragment
    {
        private List<NewRoundModel> _newRoundItems;
        private ICommand _addRoundCommand;
        private Button _addRoundBt;
        private RecyclerView _addRoundPlayersDataRv;
        private AddRoundBottomSheetAdapter _adapter;

        public AddRoundBottomSheetDialog(List<NewRoundModel> newRoundItems, ICommand addRoundCommand)
        {
            _newRoundItems = newRoundItems;
            _addRoundCommand = addRoundCommand;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.add_round_bottom_sheet_dialog, container, false);

            _addRoundBt = view.FindViewById<Button>(Resource.Id.addRoundBt);
            _addRoundPlayersDataRv = view.FindViewById<RecyclerView>(Resource.Id.addRoundPlayersDataRv);

            _addRoundPlayersDataRv.SetLayoutManager(new LinearLayoutManager(Context));
            _adapter = new AddRoundBottomSheetAdapter(_newRoundItems);
            _addRoundPlayersDataRv.SetAdapter(_adapter);

            _addRoundBt.Click -= AddRoundBtClick;
            _addRoundBt.Click += AddRoundBtClick;

            SetButtonEnabled(_addRoundCommand.CanExecute(null));

            return view;
        }

        public void SetButtonEnabled(bool enable)
        {
            _addRoundBt.Enabled = enable;
        }

        private void AddRoundBtClick(object sender, EventArgs e)
        {
            _addRoundCommand.Execute(null);
            Dismiss();
        }

        public override void OnStart()
        {
            base.OnStart();
            Dialog dialog = Dialog;
            if (dialog != null)
            {
                dialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
            }
        }

        public void UpdateList()
        {
            if (_addRoundPlayersDataRv.IsComputingLayout)
            {
                return;
            }

            _adapter?.NotifyDataSetChanged();
        }


        public override int Theme => Resource.Style.BottomSheetDialog;
    }
}