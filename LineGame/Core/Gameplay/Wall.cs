using Microsoft.Xna.Framework;
using Troschuetz.Random.Generators;
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable UseDeconstructionOnParameter

namespace LineGame.Core.Gameplay {
	public class Wall : GameComponent, IObserver {
	
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


        public void Notify(IObservable sender) {
	        if (!(sender is Player player) || !player.Alive) return;

	        for (int i = 1; i < wall.VerticesCount; i++) {
				if (Intersects(player.IntersectionLine.PointA, player.IntersectionLine.PointB, wall[i], wall[i - 1]))
					player.Alive = false;        
	        }
        }

        private bool Intersects(Point a, Point b, Point c, Point d) {
	        float denominator = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
	        float numeratorA = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
	        float numeratorB = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));

            if (denominator == 0) return numeratorA == 0 && numeratorB == 0;

	        float r = numeratorA / denominator;
	        float s = numeratorB / denominator;

	        return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }
    }
}