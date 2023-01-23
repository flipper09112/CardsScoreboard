using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Models.General;
using CardsScoreboard.UI.ViewHolders.Match;
using CardsScoreboard.ViewModel.Matchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsScoreboard.UI.Adapters.Match
{
    public class AddRoundBottomSheetAdapter : RecyclerView.Adapter
    {
        private List<NewRoundModel> _newRoundItems;

        public AddRoundBottomSheetAdapter(List<NewRoundModel> newRoundItems)
        {
            _newRoundItems = newRoundItems;
        }

        public override int ItemCount => _newRoundItems?.Count ?? 0;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is AddRoundBottomSheetItemViewHolder addRoundBottomSheetItemVH)
                addRoundBottomSheetItemVH.Bind(_newRoundItems[holder.AbsoluteAdapterPosition]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AddRoundBottomSheetItem, parent, false);
            var vh1 = new AddRoundBottomSheetItemViewHolder(view);
            return vh1;
        }
    }
}