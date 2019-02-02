using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LineGame.Core.Gameplay {
	internal class InputManager {

		private readonly Game game;
		public InputManager(Game game) {
			this.game = game;
		}

		private MouseState previousMouseState;
		private MouseState currentMouseState;

		public bool MainTouchHappened() {
			if (!game.IsActive) return false;

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			// ReSharper disable once InvertIf
			if (currentMouseState.LeftButton == ButtonState.Pressed
			    && previousMouseState.LeftButton == ButtonState.Released) {
				var windowRect = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
				if (windowRect.Contains(new Point(currentMouseState.X, currentMouseState.Y)))
                    return true;
			}

			return false;
		}
	}
}