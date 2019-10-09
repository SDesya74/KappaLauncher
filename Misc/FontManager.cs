using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;

namespace KappaLauncher.Misc {
	class FontManager {
		private static Font CurrentFont;
		public static Typeface Current {
			get { return CurrentFont != null ? CurrentFont.Typeface : Typeface.Default; }
		}


		private static AssetManager Assets;
		private static string InternalStorage;
		public static void Init(Context context) {
			Assets = context.Assets;
			InternalStorage = context.FilesDir.ToString();
		}

		public static void Load() {

		}


		public class Font {
			public Typeface Typeface { get; private set; }
			public string Name { get; private set; }

			public Font(Typeface typeface, string name) {
				Typeface = typeface;
				Name = name;
			}
		}
	}
}