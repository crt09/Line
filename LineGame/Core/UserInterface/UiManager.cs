using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineGame.Core.UserInterface {
	internal class UiManager : DrawableGameComponent {

		public int Distance { get; set; }
		public bool GameOverVisible { get; set; } = false;

		private SpriteBatch spriteBatch;
		private SpriteFont font;
		private Texture2D gameOverTexture;

		public UiManager(Game game) : base(game) {
			Game.Components.Add(this);
		}

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Game.Content.Load<SpriteFont>("font");
			gameOverTexture = Game.Content.Load<Texture2D>("game_over");

			base.LoadContent();
		}

		public override void Draw(GameTime gameTime) {
			spriteBatch.Begin();
			if (GameOverVisible) {
				var gameOverPosition = new Vector2(Game.Window.ClientBounds.Width / 2 - gameOverTexture.Width / 2, Game.Window.ClientBounds.Height / 2 - gameOverTexture.Height / 2);
                spriteBatch.Draw(gameOverTexture, gameOverPosition, Color.White);
			}
			spriteBatch.DrawString(font, Distance.ToString(), new Vector2((float)Game.Window.ClientBounds.Width / 2 - font.MeasureString(Distance.ToString()).X / 2, 50), Color.White);
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}