using Microsoft.Xna.Framework.Input;

namespace LineGame.Core.Gameplay {
	public class InputManager {

		private KeyboardState previousKeyboardState;

		public bool MainTouchHappened() {
			var pressed = Keyboard.GetState().IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up);
			previousKeyboardState = Keyboard.GetState();
			return pressed;
		}
	}
}