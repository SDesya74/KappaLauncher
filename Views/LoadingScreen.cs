using System;
using System.Collections.Generic;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using KappaLauncher.Misc;

namespace KappaLauncher.Views {
	class LoadingScreen : LinearLayout {
		public CircleLayerProgressBar ProgressBar { get; private set; }
		public LoadingScreen(Context context) : base(context) {
			SetBackgroundColor(Color.Rgb(0, 100, 0));
			SetGravity(GravityFlags.Center);

			int size = Math.Min(Screen.Width, Screen.Height) * 4 / 5;
			ProgressBar = new CircleLayerProgressBar(context);
			ProgressBar.LayoutParameters = new ViewGroup.LayoutParams(size, size);
			AddView(ProgressBar);
		}


		public class CircleLayerProgressBar : View {
			private new Handler Handler;
			private Java.Lang.Runnable Runnable;
			private List<float> LayerProgressList;

			public float StrokeWidth {
				get { return Paint.StrokeWidth; }
				set { Paint.StrokeWidth = value; }
			}
			public int LayerCount {
				get { return LayerProgressList.Count; }
				set {
					LayerProgressList = new List<float>();
					for (int i = 0; i < value; i++) LayerProgressList.Add(0f);
				}
			}

			public Paint Paint { get; private set; }
			private RectF Bounds;

			public CircleLayerProgressBar(Context context) : base(context) {
				Handler = new Handler();
				Runnable = new Java.Lang.Runnable(() => {
					Invalidate();
					Handler.PostDelayed(Runnable, 15);
				});
				Handler.Post(Runnable);

				LayerProgressList = new List<float>();

				Paint = new Paint(PaintFlags.AntiAlias);
				Paint.SetStyle(Paint.Style.Stroke);
				Paint.Color = Color.White;
				StrokeWidth = 2.Dip();

				Bounds = new RectF();
			}

			public void SetProgress(int layer, float progress) {
				try {
					LayerProgressList[layer] = progress;
				} catch { }
			}
			protected override void OnDraw(Canvas canvas) {
				Bounds.Left = Bounds.Top = StrokeWidth;
				Bounds.Right = Bounds.Bottom = Width - StrokeWidth;


				for (int i = 0; i < LayerProgressList.Count; i++) {
					canvas.DrawArc(Bounds, 270, (float) (LayerProgressList[i] * 360), false, Paint);
					float w = StrokeWidth * 2;
					Bounds.Set(Bounds.Left + w, Bounds.Top + w, Bounds.Right - w, Bounds.Bottom - w);
				}



			}
		}

	}
}