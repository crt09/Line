using LineGame.Core.UserInterface;
using Microsoft.Xna.Framework;

namespace LineGame.Core.Gameplay {
	internal class WallsBuilder : GameComponent {

		private readonly Wall topWall;
		private readonly Wall bottomWall;
		private Player player;

        public WallsBuilder(Game game) : base(game) {
			Game.Components.Add(this);

			topWall = new Wall(Game, new Point(0, 150));
			bottomWall = new Wall(Game, new Point(0, (int)Resolution.GameSize.Y - 150));
        }

		public WallsBuilder AddPlayer(Player playerInstance) {
			player = playerInstance;
			player.AddObserver(topWall);
			player.AddObserver(bottomWall);
			return this;
		}

		public override void Update(GameTime gameTime) {
			topWall.Moving = player?.Alive ?? true;
			bottomWall.Moving = player?.Alive ?? true;

			base.Update(gameTime);
		}

		public void ReloadAll() {
			topWall.Reload();
			bottomWall.Reload();
		}
	}
}