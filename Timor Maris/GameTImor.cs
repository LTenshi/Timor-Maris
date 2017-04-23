using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Timor_Maris
{

    public class GameTimor : Game
    {
#region DECLARATIONS
        //GAMESTATE OF THE GAME, USED TO DETERMINE WHICH "SCREEN" TO DRAW
        enum GameState{menu, options, gameOptions, game, gameOver};
        GameState cSTATE = GameState.menu;

        //FONTS, DEVICES, BATCHES AND CAMERA DECLARATION
        SpriteFont FontGothic20;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera Camera = new Camera();

        //SONGS AND SFX
        Song BackgroundBattle;
        SoundEffect Fire;
        SoundEffect Impact;

        //TEXTURES
        Texture2D ShipTexture;
        Texture2D TurretTexture;
        Texture2D ProjectileTexture;
        Texture2D ParticleTexture;
        Texture2D VolcanoTexture;

        // PLAYER
        Ship PlayerShip;
        List<Turret> PlayerTurrets = new List<Turret>();
        
        // ENEMY
        Ship enemyShip;
        Turret enemyTurret;
        
        //MISC OBJECTS
        GameObject Volcano;
        ParticleGenerator VolcanoPart;

#endregion
        public GameTimor()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
           
            base.Initialize();
            base.IsMouseVisible = true;

            //WINDOW PROPERTIES CHANGES
            Window.IsBorderless = true;
            Window.AllowUserResizing = true;

            //SETS THE WINDOW TO FULLSCREEN
            graphics.PreferredBackBufferWidth = this.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = this.GraphicsDevice.DisplayMode.Height;
            
            graphics.ApplyChanges();
            Camera.zoom = 1f;
        }

        
        protected override void LoadContent()
        {
            //Load in all the data
#region DATA LOADING
            //FONTS
            FontGothic20 = Content.Load<SpriteFont>("Fonts/FranklinGothic");

            //AUDIO
            BackgroundBattle = Content.Load<Song>("Music/battle");
            Fire = Content.Load<SoundEffect>("Sounds/FireCannon");
            Impact = Content.Load<SoundEffect>("Sounds/Explosion");

            //TEXTURES
            ShipTexture = Content.Load<Texture2D>("Items/Ship");
            TurretTexture = Content.Load<Texture2D>("Items/Turret");
            ProjectileTexture = Content.Load<Texture2D>("Items/Projectile");
            ParticleTexture = Content.Load<Texture2D>("Items/bigSmoke");
            VolcanoTexture = Content.Load<Texture2D>("Items/Island_V");
#endregion
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Create Objects!
            
            //ADD A PLAYER SHIP
            PlayerShip = new Ship(1, ShipTexture, new Vector2(-200, 200), 1, 1, 100);
            PlayerShip.setRotation(MathHelper.ToRadians(270));
            //ADD TURRETS TO PLAYER SHIP
            PlayerTurrets.Add(new Turret(1, TurretTexture, 50, PlayerShip, 0.1f, Fire, Impact));
            PlayerTurrets.Add(new Turret(1, TurretTexture, 50, PlayerShip, 0.1f, Fire, Impact));
            //SET FLAGS
            PlayerShip.setAsPlayer(true);
            foreach (Turret turret in PlayerTurrets)
            {
                turret.SetControlByPlayer(true);
            }

            //ADD ANY ENEMIES
            enemyShip = new Ship(2, ShipTexture, new Vector2(2000, 200), 1, 2, 20);
            enemyTurret = new Turret(2, TurretTexture, 5, enemyShip, 1f, Fire, Impact);
            enemyShip.setRotation(MathHelper.ToRadians(90));

            //ADD MISC OBJECTS
            Volcano = new GameObject(1, VolcanoTexture, new Vector2(500, 500), 0, 99999);
            VolcanoPart = new ParticleGenerator(new Vector2 (Volcano.getPosition().X, Volcano.getPosition().Y - 170), 1, ParticleTexture, 400, new Vector2(1, 1), 250, 1, 1);
            VolcanoPart.setRandomDirection(true, 0.5f, 0.9f);
            VolcanoPart.setRandomLifespan(true, 20, 300);
        }

        
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (cSTATE)
            {
                case GameState.menu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.options:
                    UpdateOptionsMenu(gameTime);
                    break;
                case GameState.gameOptions:
                    UpdateGameOptions(gameTime);
                    break;
                case GameState.game:
                    UpdateGame(gameTime);
                    break;
                case GameState.gameOver:
                    UpdateGameOver(gameTime);
                    break;
            }
        }
        protected void UpdateMainMenu(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                cSTATE = GameState.game;
            if (Keyboard.GetState().IsKeyDown(Keys.O))
                cSTATE = GameState.options;
        }
        protected void UpdateOptionsMenu(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                MediaPlayer.Volume -= 0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                MediaPlayer.Volume += 0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                SoundEffect.MasterVolume -= 0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                SoundEffect.MasterVolume += 0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                SoundEffect.MasterVolume = 0;
                MediaPlayer.Volume = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B))
                cSTATE = GameState.menu;

            
        }
        protected void UpdateGameOptions(GameTime gameTime)
        {
            //PLACEHOLDER
        }
        protected void UpdateGame(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (PlayerShip.getAlive() == false)
            {
                cSTATE = GameState.gameOver;
            }
            //CHECK IF MEDIA PLAYER IS PLAYING IF NOT THEN EXECUTE:
            if (MediaPlayer.State == MediaState.Stopped)
            {
                //REPEAT THE SONGS
                MediaPlayer.IsRepeating = true;
                //PLAY THE BACKGROUND MUSIC
                MediaPlayer.Play(BackgroundBattle);
            }

            PlayerTurrets[0].setPosition(new Vector2(PlayerShip.getPosition().X -20  * (float)Math.Cos(PlayerShip.getRotation()), PlayerShip.getPosition().Y - 20 * (float)Math.Sin(PlayerShip.getRotation())));
            PlayerTurrets[1].setPosition(new Vector2(PlayerShip.getPosition().X +20  * (float)Math.Cos(PlayerShip.getRotation()), PlayerShip.getPosition().Y + 20 * (float)Math.Sin(PlayerShip.getRotation())));
            //HANDLES INPUT
            PlayerShip.Handle(gameTime);
            //UPDATES THE SHIP
            PlayerShip.Update(gameTime);

            foreach (Turret turret in PlayerTurrets)
            {
                turret.Update(gameTime, ProjectileTexture, 20, enemyShip); // CAN BE SWITCHED TO CHECK A LIST OF OBJECTS
            }
            
            //UPDATE POSITION OF ENEMY TURRETS
            enemyTurret.setPosition(new Vector2(enemyShip.getPosition().X - 20 * (float)Math.Cos(enemyShip.getRotation()), enemyShip.getPosition().Y - 20 * (float)Math.Sin(enemyShip.getRotation())));
            enemyShip.Handle(gameTime);
            enemyShip.Update(gameTime);
            enemyTurret.Update(gameTime, ProjectileTexture, 20, PlayerShip);
            enemyTurret.setAimLoc(PlayerShip.getPosition());

            //UPDATE VOLCANO PARTICLES
            VolcanoPart.Update(gameTime);
            if (Volcano.CheckBasicCollision(enemyShip))
            {
                enemyShip.setHealth(0);
                Impact.Play();
            }
            if (Volcano.CheckBasicCollision(PlayerShip))
            {
                PlayerShip.setHealth(0);
                Impact.Play();
            }
            

            //SET PLAYER TURRET AIM
            foreach (Turret turret in PlayerTurrets)
            {
                turret.setAimLoc(Camera.GetWorldPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), GraphicsDevice));
            }

            //CENTER CAMERA ON PLAYER
            Camera.CenterOnLocation(PlayerShip.getPosition());

        }

        protected void UpdateGameOver(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            base.Draw(gameTime);

            switch (cSTATE)
            {
                case GameState.menu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.options:
                    DrawOptionsMenu(gameTime);
                    break;
                case GameState.gameOptions:
                    DrawGameOptions(gameTime);
                    break;
                case GameState.game:
                    DrawGame(gameTime);
                    break;
                case GameState.gameOver:
                    DrawGameOver(gameTime);
                    break;
            }
            


        }

        //DRAWS MAIN MENU
        protected void DrawMainMenu(GameTime gameTime)
        {
            //CLEARS THE SCREEN WITH BLACK
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //USING VIEWPORT ALLOWS FOR ADAPTABILITY TO DIFFERENT SCREEN RESOLUTIONS!
            spriteBatch.DrawString(FontGothic20, "TIMOR MARIS", new Vector2(GraphicsDevice.Viewport.Width/ 10, GraphicsDevice.Viewport.Height / 10), Color.White);
            spriteBatch.DrawString(FontGothic20, "Press ENTER to PLAY", new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 2.5f), Color.White);
            spriteBatch.DrawString(FontGothic20, "Press O for OPTIONS", new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.DrawString(FontGothic20, "Press ESC to QUIT", new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 1.5f), Color.White);
            spriteBatch.End();
        }
        protected void DrawOptionsMenu(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(FontGothic20, "OPTIONS MENU", new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
            spriteBatch.DrawString(FontGothic20, "Music Volume  (Q = - | E = +): " + MediaPlayer.Volume, new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 4), Color.White);
            spriteBatch.DrawString(FontGothic20, "SFX Volume    (A = - | D = +): " + SoundEffect.MasterVolume, new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 3), Color.White);
            spriteBatch.DrawString(FontGothic20, "Press M to mute all sound", new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.DrawString(FontGothic20, "Press B to go back to MAIN MENU", new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 1.5f), Color.White);
            spriteBatch.End();
        }
        protected void DrawGameOptions(GameTime gameTime)
        {
            //PLACEHOLDER
        }
        protected void DrawGame(GameTime gameTime)
        {
            //MORE COMPLICATED VERSION OF A SPRITE BATCH, THIS TAKES IN THE THE TRANSFORMATION MATRIX FROM THE CAMERA THEREFORE ALLOWING FOR THE CAMERA TO IMPACT THE INCLUDED OBJECTS IN THE BATCH
            spriteBatch.Begin(transformMatrix: Camera.get_transformation(graphics.GraphicsDevice));

            PlayerShip.Render(spriteBatch, 90, Color.Aqua);
            foreach (Turret turret in PlayerTurrets)
            {
                turret.Render(spriteBatch, 90, Color.Aqua);
            }
            enemyShip.Render(spriteBatch, 90, Color.Red);
            enemyTurret.Render(spriteBatch, 90, Color.Red);

            Volcano.Render(spriteBatch, 0, Color.White);
            VolcanoPart.Render(spriteBatch);
            spriteBatch.End();

            //SEPARATE SPRITE BATCH FOR UI ELEMENTS, IF NOT SEPARATE THEY WOULD MOVE OFF SCREEN
            spriteBatch.Begin();
            spriteBatch.DrawString(FontGothic20, "Shots fired: " + (PlayerTurrets[0].getShotsFired() + PlayerTurrets[1].getShotsFired()), new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(FontGothic20, "Speed: " + PlayerShip.getSpeed(), new Vector2(0, this.GraphicsDevice.Viewport.Height - 70), Color.Black);
            spriteBatch.DrawString(FontGothic20, "Rotation Speed: " + PlayerShip.getRotaSpeed(), new Vector2(0, this.GraphicsDevice.Viewport.Height - 140), Color.Black);
            spriteBatch.End();

        }
        protected void DrawGameOver(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(FontGothic20, "GAME OVER" , new Vector2(0, this.GraphicsDevice.Viewport.Height/2), Color.White);
            spriteBatch.End();
        }
    }
}
