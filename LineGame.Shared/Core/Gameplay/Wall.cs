﻿using LineGame.Core.UserInterface;
using Microsoft.Xna.Framework;
using Troschuetz.Random.Generators;
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable UseDeconstructionOnParameter

namespace LineGame.Core.Gameplay {
	internal class Wall : GameComponent, IObserver {
	
        public bool Moving { get; set; } = true;

		private readonly DrawableVertexArray wall;
		private readonly Point defaultStartPosition;

		public Wall(Game game, Point startPosition) : base(game) {
			Game.Components.Add(this);

			wall = new DrawableVertexArray(Game);
			defaultStartPosition = startPosition;
			Reload();
		}

		public void Reload() {
			wall.ClearAllVertices();
			wall.AddVertex(defaultStartPosition);
			wall.AddVertex(new Point(Resolution.GameSize.X, defaultStartPosition.Y));
        }

        public override void Update(GameTime gameTime) {
	        if (!Moving) return;
			
	        if (wall[wall.VerticesCount - 1].X < Resolution.GameSize.X) {
		        int randomWidth = new ALFGenerator().Next(50, 200);
		        int randomYOffset = defaultStartPosition.Y + new ALFGenerator().Next(-150, 150);
                wall.AddVertex(new Point(wall[wall.VerticesCount - 1].X + randomWidth, randomYOffset));
	        }

	        if (wall.VerticesCount > 1) {
		        if (wall[0].X < 0 && wall[1].X < 0) {
					wall.RemoveVertexAt(0);
		        }
	        }

			for (int i = 0; i < wall.VerticesCount; ++i) {
				wall[i] += new Point(-7, 0);
			}

			base.Update(gameTime);
		}


        public void Notify(IObservable sender) {
	        if (!(sender is Player player) || !player.Alive) return;

	        for (int i = 0; i < wall.VerticesCount - 1; i++) {
				if (Intersects(player.IntersectionLine.PointA, player.IntersectionLine.PointB,
					wall[i].ToVector2(), wall[i + 1].ToVector2()))
					player.Alive = false;        
	        }
        }

        private bool Intersects(Vector2 a, Vector2 b, Vector2 c, Vector2 d) {
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