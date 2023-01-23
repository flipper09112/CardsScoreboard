using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using CardsScoreboard.Helpers;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.ListFormatter;
using static Android.Icu.Text.Transliterator;

namespace CardsScoreboard.UI.LayoutManagers
{
    public class PickerLayoutManager : LinearLayoutManager
    {
        private RecyclerView recyclerView;
        private IOnItemPickerLayoutManagerListener callback;

        public PickerLayoutManager(Context context, IOnItemPickerLayoutManagerListener callback) : base(context)
        {
            Orientation = OrientationHelper.Horizontal;
            this.callback = callback;
        }

        public override void ScrollToPosition(int position)
        {
            var move = ScreenHelper.DpToPx(CrossCurrentActivity.Current.Activity, 100) * (position + 1) - ScreenHelper.DpToPx(CrossCurrentActivity.Current.Activity, 100) / 2 - ScreenHelper.DpToPx(CrossCurrentActivity.Current.Activity, 40);
            ScrollToPositionWithOffset(0, -move);
        }

        public override void OnAttachedToWindow(RecyclerView view)
        {
            base.OnAttachedToWindow(view);
            recyclerView = view;

            // snapping
            new LinearSnapHelper().AttachToRecyclerView(recyclerView);
        }

        public override void OnLayoutChildren(RecyclerView.Recycler recycler, RecyclerView.State state)
        {
            base.OnLayoutChildren(recycler, state);
            ScaleDownView();
        }

        public override int ScrollHorizontallyBy(int dx, RecyclerView.Recycler recycler, RecyclerView.State state)
        {
            if (Orientation == OrientationHelper.Horizontal)
            {
                int scrolled = base.ScrollHorizontallyBy(dx, recycler, state);
                ScaleDownView();
                return scrolled;
            }
            else
            {
                return 0;
            }
        }

        private void ScaleDownView()
        {
            if (Width == 0) return;

            float mid = Width / 2.0f;
            for (int i = 0; i < ChildCount; i++)
            {
                View child = GetChildAt(i);
                float childMid = (GetDecoratedLeft(child) + GetDecoratedRight(child)) / 2.0f;
                float distanceFromCenter = Java.Lang.Math.Abs(mid - childMid);

                // The scaling formula
                float scale = 1 - (float)System.Math.Sqrt(distanceFromCenter / Width) * 0.66f;

                // Set scale to view
                child.ScaleX = scale;
                child.ScaleY = scale;
            }
        }

        public override void OnScrollStateChanged(int state)
        {
            base.OnScrollStateChanged(state);

            // When scroll stops we notify on the selected item
            if (state == RecyclerView.ScrollStateIdle)
            {
                // Find the closest child to the recyclerView center --> this is the selected item.
                int recyclerViewCenterX = GetRecyclerViewCenterX();
                int minDistance = recyclerView.Width;
                int position = -1;
                for (int i = 0; i < recyclerView.ChildCount; i++)
                {
                    View child = recyclerView.GetChildAt(i);
                    int childCenterX = GetDecoratedLeft(child) + (GetDecoratedRight(child) - GetDecoratedLeft(child)) / 2;
                    int newDistance = Java.Lang.Math.Abs(childCenterX - recyclerViewCenterX);
                    if (newDistance < minDistance)
                    {
                        minDistance = newDistance;
                        position = recyclerView.GetChildLayoutPosition(child);
                    }
                }

                // Notify on item selection
                callback?.OnItemSelected(position);
            }
        }

        private int GetRecyclerViewCenterX()
        {
            return (recyclerView.Right - recyclerView.Left) / 2 + recyclerView.Left;
        }

        public interface IOnItemPickerLayoutManagerListener
        {
            void OnItemSelected(int layoutPosition);
        }
    }
}