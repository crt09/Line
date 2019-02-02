using Microsoft.Xna.Framework;

namespace LineGame.Core.UserInterface {
	public static class Resolution {
		private static Vector2 actualSize;
		private static Vector2 virtualSize;

		public static void SetActualSize(Vector2 size) {
			actualSize = size;
		}

		public static void SetVirtualSize(Vector2 size) {
			virtualSize = size;
		}

		public static void Apply() {
			var scaleX = actualSize.X / virtualSize.X;
			var scaleY = actualSize.Y / virtualSize.Y;
			Scale = Matrix.CreateScale(scaleX, scaleY, 1.0f);
		}

		public static Matrix Scale { get; private set; }
        public static Point GameSize => virtualSize.ToPoint();
    }
}