﻿using System;
using System.Collections.Generic;
using System.Linq;
using LineGame.Core.UserInterface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineGame.Core.Gameplay {
	internal class DrawableVertexArray : DrawableGameComponent {

		private readonly List<Point> points;
		private Texture2D circleTexture;
		private Texture2D lineTexture;

		private SpriteBatch spriteBatch;
		public DrawableVertexArray(Game game) : base(game) {
            Game.Components.Add(this);

			points = new List<Point>();
		}

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);

			circleTexture = Game.Content.Load<Texture2D>("vertex");
			if(circleTexture.Width != circleTexture.Height) throw new Exception("Circle texture must be 1:1");

			lineTexture = new Texture2D(GraphicsDevice, 1, 1);
			lineTexture.SetData(new []{ Color.White });

            base.LoadContent();
		}		

		public override void Draw(GameTime gameTime) {
#if ANDROID
			spriteBatch.Begin(transformMatrix: Resolution.Scale);
#else
            spriteBatch.Begin();
#endif
            for (int i = 0; i < points.Count - 1; i++) {
				 spriteBatch.Draw(
					 circleTexture,
					 new Rectangle(points[i].X, points[i].Y, circleTexture.Width, circleTexture.Height),
					 null,
					 Color.White,
					 0f,
					 new Vector2((float)circleTexture.Width / 2, (float)circleTexture.Height / 2),
					 SpriteEffects.None,
					 0);
				 DrawLine(points[i], points[i + 1]);
            }
			spriteBatch.End();
			base.Draw(gameTime);
		}

        private void DrawLine(Point pointA, Point pointB) {
	        var start = pointA.ToVector2();
	        var end = pointB.ToVector2();
            Vector2 direction = end - start;            
            var rotationAngle = (float)Math.Atan2(direction.Y, direction.X);

            spriteBatch.Draw(
	            lineTexture,
                new Rectangle(pointA.X, pointA.Y, (int)direction.Length(), circleTexture.Width),
                null,
                Color.White,
	            rotationAngle,
                new Vector2(0, 0.5f),
                SpriteEffects.None,
                0);
        }

		public void AddVertex(Point position) {
			if (points.Count == 0 || points.Last() != position) {
				points.Add(position);
			}
		}

		public void RemoveVertexAt(int index) {
			points.RemoveAt(index);
		}

		public void ClearAllVertices() {
			points.Clear();
		}

		public int VerticesCount => points.Count;

		public Point this[int index] {
			get => points[index];
			set => points[index] = value;
		}
	}
}