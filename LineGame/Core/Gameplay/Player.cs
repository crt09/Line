using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LineGame.Core.Gameplay {
	public class Player : DrawableGameComponent, IObservable {

		private readonly List<IObserver> observers;

		public void AddObserver(IObserver observer) {
			observers.Add(observer);
		}

		public void RemoveObserver(IObserver observer) {
			observers.Remove(observer);
		}

		public void NotifyAllObservers() {
			foreach (var observer in observers) {
				observer.Notify(this);
			}
		}

		public struct Line {
			public Point PointA;
			public Point PointB;
			public Line(Point pointA, Point pointB) {
				PointA = pointA;
				PointB = pointB;
			}
		} public Line IntersectionLine;

        public bool Alive { get; set; } = true;

		private readonly DrawableVertexArray trail;
		private readonly InputManager inputManager;

		private SpriteBatch spriteBatch;
		private Texture2D playerTexture;
		private Vector2 playerPosition;
		private Vector2 previousPlayerPosition;

        private float accelerator = 0.3f;
		private float yVelocity;

		public Player(Game game) : base(game) {
			Game.Components.Add(this);
			observers = new List<IObserver>();
			inputManager = new InputManager();

            playerPosition.X = 600;
            playerPosition.Y = 360;

            trail = new DrawableVertexArray(Game);
			trail.AddVertex(new Point(0, (int)playerPosition.Y));
			trail.AddVertex(playerPosition.ToPoint());
        }

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);
			playerTexture = Game.Content.Load<Texture2D>("player_dot");

            base.LoadContent();
		}
	
		public override void Update(GameTime gameTime) {
			if (!Alive) return;

            // movement

            if (inputManager.MainTouchHappened())
	            accelerator = -accelerator;

            previousPlayerPosition = playerPosition;

            yVelocity += accelerator;            
            playerPosition.Y += yVelocity;

            // intersections

            IntersectionLine.PointA = new Point((int)playerPosition.X, (int)playerPosition.Y - playerTexture.Height / 2);
            IntersectionLine.PointB = new Point((int)playerPosition.X, (int)playerPosition.Y + playerTexture.Height / 2);

            if (previousPlayerPosition.Y < IntersectionLine.PointA.Y)
	            IntersectionLine.PointA = previousPlayerPosition.ToPoint();

            if (previousPlayerPosition.Y > IntersectionLine.PointB.Y)
	            IntersectionLine.PointB = previousPlayerPosition.ToPoint();

            NotifyAllObservers();

            // trail

            trail.AddVertex(playerPosition.ToPoint());

			// remove non drawable points
			if (trail.VerticesCount > 1) {
		       if (trail[0].X < 0 && trail[1].X < 0) {
			       trail.RemoveVertexAt(0);
		       }
			}

			// move trail
			for (int i = 0; i < trail.VerticesCount; i++) {
				trail[i] += new Point(-5, 0);
			}
      			
            base.Update(gameTime);
		}

        public override void Draw(GameTime gameTime) {
			spriteBatch.Begin();
			spriteBatch.Draw(
				playerTexture,
				new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerTexture.Width, playerTexture.Height),
				null,
				Color.White,
				0f,
				new Vector2(playerTexture.Width, (float)playerTexture.Height / 2), 
				SpriteEffects.None,
				0);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}