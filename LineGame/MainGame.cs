using LineGame.Core.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LineGame {
	public class MainGame : Game {
		private readonly GraphicsDeviceManager graphics;

		private bool Waiting { get; set; } = true;

		private Player player;
		private WallsBuilder walls;
		private InputManager inputManager;

		public MainGame() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

        protected override void Initialize() {
			graphics.PreferredBackBufferWidth = 720;
			graphics.PreferredBackBufferHeight = 800;
			graphics.PreferMultiSampling = true;
			graphics.ApplyChanges();
			IsMouseVisible = true;

			player = new Player(this);
			walls = new WallsBuilder(this)
				.AddPlayer(player);
			inputManager = new InputManager();

			base.Initialize();
        }

		protected override void Update(GameTime gameTime) {
			if (inputManager.MainTouchHappened()) {
				if (Waiting) {
					player.Alive = true;
					Waiting = false;
				} else if (!player.Alive) {
					Waiting = true;
					walls.ReloadAll();
					player.Reload();
				}
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}
	}
}
