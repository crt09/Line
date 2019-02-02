using LineGame.Core.Gameplay;
using LineGame.Core.UserInterface;
using Microsoft.Xna.Framework;

namespace LineGame {
	public class MainGame : Game {
		private readonly GraphicsDeviceManager graphics;

		private bool Waiting { get; set; } = true;

		private Player player;
		private WallsBuilder walls;
		private InputManager inputManager;
		private UiManager uiManager;

		public MainGame() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

        protected override void Initialize() {
			graphics.PreferredBackBufferWidth = 500;
			graphics.PreferredBackBufferHeight = 800;
			graphics.PreferMultiSampling = true;
			graphics.ApplyChanges();
			IsMouseVisible = true;

			player = new Player(this);
			walls = new WallsBuilder(this)
				.AddPlayer(player);
            inputManager = new InputManager(this);
			uiManager = new UiManager(this);

			base.Initialize();
        }

		protected override void Update(GameTime gameTime) {
			if (player != null && walls != null) {
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
				uiManager.GameOverVisible = !player.Alive && !Waiting;
				uiManager.Distance = player.Distance;
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}
	}
}
