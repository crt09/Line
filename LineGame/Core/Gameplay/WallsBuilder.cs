using Microsoft.Xna.Framework;

namespace LineGame.Core.Gameplay {
	public class WallsBuilder : GameComponent {
		public WallsBuilder(Game game) : base(game) {
			Game.Components.Add(this);
        }

		public override void Initialize() {
			var topWall = new Wall(Game, new Point(0, 200));
			var bottomWall = new Wall(Game, new Point(0, Game.Window.ClientBounds.Height - 200));

            base.Initialize();
		}
	}
}