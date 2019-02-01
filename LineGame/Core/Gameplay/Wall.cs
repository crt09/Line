using Microsoft.Xna.Framework;
using Troschuetz.Random.Generators;

// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace LineGame.Core.Gameplay {

	public class Wall : GameComponent {

		public bool Moving { get; set; } = true;

		private readonly DrawableVertexArray wall;
		private readonly Point defaultStartPosition;

		public Wall(Game game, Point startPosition) : base(game) {
			Game.Components.Add(this);

			wall = new DrawableVertexArray(Game);
			defaultStartPosition = startPosition;
			Reload();
		}

		private void Reload() {
			wall.ClearAllVertices();
			wall.AddVertex(defaultStartPosition);
			wall.AddVertex(new Point(Game.Window.ClientBounds.Width, defaultStartPosition.Y));
        }

        public override void Update(GameTime gameTime) {
	        if (!Moving) return;
			
	        if (wall[wall.VerticesCount - 1].X < Game.Window.ClientBounds.Width) {
		        int randomWidth = new ALFGenerator().Next(100, 400);
		        int randomYOffset = defaultStartPosition.Y + new ALFGenerator().Next(-100, 100);
                wall.AddVertex(new Point(wall[wall.VerticesCount - 1].X + randomWidth, randomYOffset));
	        }

	        if (wall.VerticesCount > 1) {
		        if (wall[0].X < 0 && wall[1].X < 0) {
					wall.RemoveVertexAt(0);
		        }
	        }

			for (int i = 0; i < wall.VerticesCount; ++i) {
				wall[i] += new Point(-5, 0);
			}

			base.Update(gameTime);
		}
	}
}