/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;

using Java.Interop;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    /// <summary>
    /// https://github.com/xamarin/monodroid-samples/blob/master/ViewPagerIndicator/ViewPagerIndicator/Resources/layout/simple_circles.xml
    /// </summary>
    /// <remarks>
    /// TODO: this needs cleaned up in a bad way
    /// </remarks>
    public sealed class CircleViewPagerIndicator : View, IViewPagerIndicator
    {
		private const int Horizontal = 0;
		private const int Vertical = 1;

		private float _radius;

		private readonly Paint _paintPageFill;
		private readonly Paint _paintStroke;
		private readonly Paint _paintFill;

		private ViewPager _viewPager;
		private ViewPager.IOnPageChangeListener _listener;

		private int mCurrentPage;
		private int mSnapPage;
		private int mCurrentOffset;
		private int mScrollState;
		private int mPageSize;
		private int mOrientation;
		private bool mCentered;
		private bool mSnap;
		private const int INVALID_POINTER = -1;
		private int mTouchSlop;
		private float mLastMotionX = -1;
		private int mActivePointerId = INVALID_POINTER;
		private bool mIsDragging;
		
		public CircleViewPagerIndicator (Context context) : this(context, null)
		{
		}
		
		public CircleViewPagerIndicator (Context context, IAttributeSet attrs) : this (context, attrs, Resource.Attribute.vpiCirclePageIndicatorStyle)
		{
		}

		public CircleViewPagerIndicator (Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			//Load defaults from resources
			var res = Resources;
			int defaultPageColor = res.GetColor (Resource.Color.default_circle_indicator_page_color);
			int defaultFillColor = res.GetColor (Resource.Color.default_circle_indicator_fill_color);
			int defaultOrientation = res.GetInteger (Resource.Integer.default_circle_indicator_orientation);
			int defaultStrokeColor = res.GetColor (Resource.Color.default_circle_indicator_stroke_color);
			float defaultStrokeWidth = res.GetDimension (Resource.Dimension.default_circle_indicator_stroke_width);
			float defaultRadius = res.GetDimension (Resource.Dimension.default_circle_indicator_radius);
			bool defaultCentered = res.GetBoolean (Resource.Boolean.default_circle_indicator_centered);
			bool defaultSnap = res.GetBoolean (Resource.Boolean.default_circle_indicator_snap);
			
			//Retrieve styles attributes
			var a = context.ObtainStyledAttributes (attrs, Resource.Styleable.CirclePageIndicator, defStyle, Resource.Style.Widget_CirclePageIndicator);
		
			mCentered = a.GetBoolean (Resource.Styleable.CirclePageIndicator_centered, defaultCentered);
			mOrientation = a.GetInt (Resource.Styleable.CirclePageIndicator_android_orientation, defaultOrientation);
			_paintPageFill = new Paint (PaintFlags.AntiAlias);
			_paintPageFill.SetStyle (Paint.Style.Fill);
			_paintPageFill.Color = a.GetColor (Resource.Styleable.CirclePageIndicator_pageColor, defaultPageColor);
			_paintStroke = new Paint (PaintFlags.AntiAlias);
			_paintStroke.SetStyle (Paint.Style.Stroke);
			_paintStroke.Color = a.GetColor (Resource.Styleable.CirclePageIndicator_strokeColor, defaultStrokeColor);
			_paintStroke.StrokeWidth = a.GetDimension (Resource.Styleable.CirclePageIndicator_strokeWidth, defaultStrokeWidth);
			_paintFill = new Paint (PaintFlags.AntiAlias);
			_paintFill.SetStyle (Paint.Style.Fill);
			_paintFill.Color = a.GetColor (Resource.Styleable.CirclePageIndicator_fillColor, defaultFillColor);
			_radius = a.GetDimension (Resource.Styleable.CirclePageIndicator_radius, defaultRadius);
			mSnap = a.GetBoolean (Resource.Styleable.CirclePageIndicator_snap, defaultSnap);
			
			a.Recycle ();
			
			var configuration = ViewConfiguration.Get (context);
			mTouchSlop = ViewConfigurationCompat.GetScaledPagingTouchSlop (configuration);
			
		}

		public void SetCentered (bool centered)
		{
			mCentered = centered;
			Invalidate ();
		}
	
		public bool IsCentered ()
		{
			return mCentered;
		}
	
		public void SetPageColor (Color pageColor)
		{
			_paintPageFill.Color = pageColor;
			Invalidate ();
		}
	
		public int GetPageColor ()
		{
			return _paintPageFill.Color;
		}
	
		public void SetFillColor (Color fillColor)
		{
			_paintFill.Color = fillColor;
			Invalidate ();
		}
	
		public int GetFillColor ()
		{
			return _paintFill.Color;
		}
		
		public void SetOrientation (int orientation)
		{
			switch (orientation)
            {
			case Horizontal:
			case Vertical:
				mOrientation = orientation;
				UpdatePageSize ();
				RequestLayout ();
				break;
			default:
				throw new Java.Lang.IllegalArgumentException("Orientation must be either Horizontal or Vertical.");
			}
		}
	
		public int GetOrientation ()
		{
			return mOrientation;
		}
	
		public void SetStrokeColor (Color strokeColor)
		{
			_paintStroke.Color = strokeColor;
			Invalidate ();
		}
	
		public int GetStrokeColor ()
		{
			return _paintStroke.Color;
		}
	
		public void SetStrokeWidth (float strokeWidth)
		{
			_paintStroke.StrokeWidth = strokeWidth;
			Invalidate ();
		}
	
		public float GetStrokeWidth ()
		{
			return _paintStroke.StrokeWidth;
		}
	
		public void SetRadius (float radius)
		{
			_radius = radius;
			Invalidate ();
		}
	
		public float GetRadius ()
		{
			return _radius;
		}

		public void SetSnap (bool snap)
		{
			mSnap = snap;
			Invalidate ();
		}
	
		public bool IsSnap ()
		{
			return mSnap;
		}
		
		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			
			if (_viewPager == null) {
				return;
			}
			int count = _viewPager.Adapter.Count;
			if (count == 0) {
				return;
			}
	
			if (mCurrentPage >= count) {
				SetCurrentItem (count - 1);
				return;
			}
	
			int longSize;
			int longPaddingBefore;
			int longPaddingAfter;
			int shortPaddingBefore;
			if (mOrientation == Horizontal) {
				longSize = Width;
				longPaddingBefore = PaddingLeft;
				longPaddingAfter = PaddingRight;
				shortPaddingBefore = PaddingTop;
			} else {
				longSize = Height;
				longPaddingBefore = PaddingTop;
				longPaddingAfter = PaddingBottom;
				shortPaddingBefore = PaddingLeft;
			}
	
			float threeRadius = _radius * 3;
			float shortOffset = shortPaddingBefore + _radius;
			float longOffset = longPaddingBefore + _radius;
			if (mCentered) {
				longOffset += ((longSize - longPaddingBefore - longPaddingAfter) / 2.0f) - ((count * threeRadius) / 2.0f);
			}
			
			float dX;
			float dY;
	
			float pageFillRadius = _radius;
			if (_paintStroke.StrokeWidth > 0) {
				pageFillRadius -= _paintStroke.StrokeWidth / 2.0f;
			}
	
			//Draw stroked circles
			for (int iLoop = 0; iLoop < count; iLoop++) {
				float drawLong = longOffset + (iLoop * threeRadius);
				if (mOrientation == Horizontal) {
					dX = drawLong;
					dY = shortOffset;
				} else {
					dX = shortOffset;
					dY = drawLong;
				}
				// Only paint fill if not completely transparent
				if (_paintPageFill.Alpha > 0) {
					canvas.DrawCircle (dX, dY, pageFillRadius, _paintPageFill);
				}
	
				// Only paint stroke if a stroke width was non-zero
				if (pageFillRadius != _radius) {
					canvas.DrawCircle (dX, dY, _radius, _paintStroke);
				}
			}
	
			//Draw the filled circle according to the current scroll
			float cx = (mSnap ? mSnapPage : mCurrentPage) * threeRadius;
			if (!mSnap && (mPageSize != 0)) {
				cx += (mCurrentOffset * 1.0f / mPageSize) * threeRadius;
			}
			if (mOrientation == Horizontal) {
				dX = longOffset + cx;
				dY = shortOffset;
			} else {
				dX = shortOffset;
				dY = longOffset + cx;
			}
			canvas.DrawCircle (dX, dY, _radius, _paintFill);
		}
		
		public override bool OnTouchEvent (MotionEvent ev)
        {
			if (base.OnTouchEvent (ev)) {
				return true;
			}

			if ((_viewPager == null) || (_viewPager.Adapter.Count == 0)) {
				return false;
			}
	
			var action = ev.Action;
	
			switch ((int)action & MotionEventCompat.ActionMask)
            {
			case (int) MotionEventActions.Down:
				mActivePointerId = MotionEventCompat.GetPointerId (ev, 0);
				mLastMotionX = ev.GetX ();
				break;
			case (int)MotionEventActions.Move:
                {
					int activePointerIndex = MotionEventCompat.FindPointerIndex (ev, mActivePointerId);
					float x = MotionEventCompat.GetX (ev, activePointerIndex);
					float deltaX = x - mLastMotionX;
	
					if (!mIsDragging) {
						if (Java.Lang.Math.Abs (deltaX) > mTouchSlop) {
							mIsDragging = true;
						}
					}
	
					if (mIsDragging) {
						if (!_viewPager.IsFakeDragging) {
							_viewPager.BeginFakeDrag ();
						}
	
						mLastMotionX = x;
	
						_viewPager.FakeDragBy (deltaX);
					}
	
					break;
				}
			case (int)MotionEventActions.Cancel:
			case (int)MotionEventActions.Up:
				if (!mIsDragging) {
					int count = _viewPager.Adapter.Count;
					int width = Width;
					float halfWidth = width / 2f;
					float sixthWidth = width / 6f;
	
					if ((mCurrentPage > 0) && (ev.GetX () < halfWidth - sixthWidth)) {
						_viewPager.CurrentItem = mCurrentPage - 1;
						return true;
					} else if ((mCurrentPage < count - 1) && (ev.GetX () > halfWidth + sixthWidth)) {
						_viewPager.CurrentItem = mCurrentPage + 1;
						return true;
					}
				}
	
				mIsDragging = false;
				mActivePointerId = INVALID_POINTER;

				if (_viewPager.IsFakeDragging) {
					_viewPager.EndFakeDrag ();
                }
				break;
			case MotionEventCompat.ActionPointerDown: {
					int index = MotionEventCompat.GetActionIndex (ev);
					float x = MotionEventCompat.GetX (ev, index);
					mLastMotionX = x;
					mActivePointerId = MotionEventCompat.GetPointerId (ev, index);
					break;
				}
			case MotionEventCompat.ActionPointerUp:
				int pointerIndex = MotionEventCompat.GetActionIndex (ev);
				int pointerId = MotionEventCompat.GetPointerId (ev, pointerIndex);
				if (pointerId == mActivePointerId) {
					int newPointerIndex = pointerIndex == 0 ? 1 : 0;
					mActivePointerId = MotionEventCompat.GetPointerId (ev, newPointerIndex);
				}
				mLastMotionX = MotionEventCompat.GetX (ev, MotionEventCompat.FindPointerIndex (ev, mActivePointerId));
				break;
			}
	
			return true;
		}

        public override bool PerformClick()
        {
            return base.PerformClick();
        }

        public void SetViewPager (ViewPager view)
		{
			if (view.Adapter == null) {
				throw new Java.Lang.IllegalStateException ("ViewPager does not have adapter instance.");
			}
			_viewPager = view;
			_viewPager.AddOnPageChangeListener(this);
			UpdatePageSize ();
			Invalidate ();
		}
		
		private void UpdatePageSize ()
		{
			if (_viewPager != null) {
				mPageSize = (mOrientation == Horizontal) ? _viewPager.Width : _viewPager.Height;
			}
		}
		
		public void SetViewPager (ViewPager view, int initialPosition)
		{
			SetViewPager (view);
			SetCurrentItem (initialPosition);
		}
		
		public void SetCurrentItem (int item)
		{
			if (_viewPager == null) {
				throw new Java.Lang.IllegalStateException ("ViewPager has not been bound.");
			}
			_viewPager.CurrentItem = item;
			mCurrentPage = item;
			Invalidate ();
		}
		
		public void NotifyDataSetChanged ()
		{
			Invalidate ();
		}
		
		public void OnPageScrollStateChanged (int state)
		{
			mScrollState = state;

		    _listener?.OnPageScrollStateChanged (state);
		}
		
		public void OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
		{
			mCurrentPage = position;
			mCurrentOffset = positionOffsetPixels;
			UpdatePageSize ();
			Invalidate ();

		    _listener?.OnPageScrolled (position, positionOffset, positionOffsetPixels);
		}
		
		public void OnPageSelected (int position)
		{
			if (mSnap || mScrollState == ViewPager.ScrollStateIdle) {
				mCurrentPage = position;
				mSnapPage = position;
				Invalidate ();
			}

		    _listener?.OnPageSelected (position);
		}
		
		public void SetOnPageChangeListener (ViewPager.IOnPageChangeListener listener)
		{
			_listener = listener;
		}
		
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			if (mOrientation == Horizontal) {
				SetMeasuredDimension (MeasureLong (widthMeasureSpec), MeasureShort (heightMeasureSpec));
			} else {
				SetMeasuredDimension (MeasureShort (widthMeasureSpec), MeasureLong (heightMeasureSpec));
			}
		}
		
		/**
	     * Determines the width of this view
	     *
	     * @param measureSpec
	     *            A measureSpec packed into an int
	     * @return The width of the view, honoring constraints from measureSpec
	     */
		private int MeasureLong (int measureSpec)
		{
			int result;
			var specMode = MeasureSpec.GetMode (measureSpec);
			var specSize = MeasureSpec.GetSize (measureSpec);
	
			if ((specMode == MeasureSpecMode.Exactly) || (_viewPager == null)) {
				//We were told how big to be
				result = specSize;
			} else {
				//Calculate the width according the views count
				int count = _viewPager.Adapter.Count;
				result = (int)(PaddingLeft + PaddingRight
	                    + (count * 2 * _radius) + (count - 1) * _radius + 1);
				//Respect AT_MOST value if that was what is called for by measureSpec
				if (specMode == MeasureSpecMode.AtMost) {
					result = Java.Lang.Math.Min (result, specSize);
				}
			}
			return result;
		}
		
		/**
	     * Determines the height of this view
	     *
	     * @param measureSpec
	     *            A measureSpec packed into an int
	     * @return The height of the view, honoring constraints from measureSpec
	     */
		private int MeasureShort (int measureSpec)
		{
			int result;
			var specMode = MeasureSpec.GetMode (measureSpec);
			var specSize = MeasureSpec.GetSize (measureSpec);
	
			if (specMode == MeasureSpecMode.Exactly) {
				//We were told how big to be
				result = specSize;
			} else {
				//Measure the height
				result = (int)(2 * _radius + PaddingTop + PaddingBottom + 1);
				//Respect AT_MOST value if that was what is called for by measureSpec
				if (specMode == MeasureSpecMode.AtMost) {
					result = Java.Lang.Math.Min (result, specSize);
				}
			}
			return result;
		}
		
		protected override void OnRestoreInstanceState (IParcelable state)
		{
			
			try {
				SavedState savedState = (SavedState)state;
				base.OnRestoreInstanceState (savedState.SuperState);
				mCurrentPage = savedState.CurrentPage;
				mSnapPage = savedState.CurrentPage;
			} catch {
				base.OnRestoreInstanceState (state);
				// Ignore, this needs to support IParcelable...
			}
			RequestLayout ();
		}
		
		protected override IParcelable OnSaveInstanceState ()
		{
			var superState = base.OnSaveInstanceState ();
		    var savedState = new SavedState(superState)
            {
                CurrentPage = mCurrentPage
            };
		    return savedState;
		}
		
		public class SavedState : BaseSavedState
		{
			public int CurrentPage { get; set; }
	
			public SavedState (IParcelable superState) : base(superState)
			{
			}
	
			private SavedState (Parcel parcel) : base(parcel)
			{
				CurrentPage = parcel.ReadInt ();
			}
			
			public override void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
			{
				base.WriteToParcel (dest, flags);
				dest.WriteInt (CurrentPage);
			}
			
			[ExportField ("CREATOR")]
			static SavedStateCreator InitializeCreator ()
			{
				return new SavedStateCreator ();
			}
			
			class SavedStateCreator : Java.Lang.Object, IParcelableCreator
			{
				public Java.Lang.Object CreateFromParcel (Parcel source)
				{
					return new SavedState (source);
				}
		
				public Java.Lang.Object[] NewArray (int size)
				{
					return new SavedState[size];
				}
			}
		}
    }
}
