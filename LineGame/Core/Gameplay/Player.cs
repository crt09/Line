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
			public Vector2 PointA;
			public Vector2 PointB;
		}
		public Line IntersectionLine;

        public bool Alive { get; set; }

		private readonly DrawableVertexArray trail;
		private readonly InputManager inputManager;

		private SpriteBatch spriteBatch;
		private Texture2D playerTexture;
		private Vector2 playerPosition;
		private Vector2 previousPlayerPosition;

        private float accelerator = -0.8f;
		private float yVelocity;

		public Player(Game game) : base(game) {
			Game.Components.Add(this);
			observers = new List<IObserver>();
			inputManager = new InputManager();
			trail = new DrawableVertexArray(Game);

            Reload();            
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

            IntersectionLine.PointA = playerPosition;
            IntersectionLine.PointB = new Vector2(playerPosition.X - 7, previousPlayerPosition.Y);

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
				trail[i] += new Point(-7, 0);
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
				new Vector2(playerTexture.Width , (float)playerTexture.Height / 2), 
				SpriteEffects.None,
				0);
			spriteBatch.End();

			base.Draw(gameTime);
		}

        public void Reload() {
	        playerPosition.X = (float)Game.Window.ClientBounds.Width / 2;
	        playerPosition.Y = (float)Game.Window.ClientBounds.Height / 2;

            trail.ClearAllVertices();
	        trail.AddVertex(new Point(0, playerPosition.ToPoint().Y));
	        trail.AddVertex(playerPosition.ToPoint());

	        yVelocity = 0f;
        }
    }
}