using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Timor_Maris
{

    public class GameTimor : Game
    {
        enum GameState{menu = 0, options = 1, gameOptions = 2, game = 3};

        SpriteFont FontGothic20;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera Camera = new Camera();
        //TEST PLAYER
        Ship testShip;
        Turret testTurret;
        Turret testTurret2;

        ParticleGenerator testParticleGen;

        //TEST ENEMY
        Ship enemyShip;
        Turret enemyTurret;

        Texture2D testShipTexture;
        Texture2D testTurretTexture;
        Texture2D testProjectileTexture;

        SoundEffect testFire;
        SoundEffect testImpact;
        public GameTimor()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            base.IsMouseVisible = true;

            Window.IsBorderless = true;
            Window.AllowUserResizing = true;

            graphics.PreferredBackBufferWidth = this.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = this.GraphicsDevice.DisplayMode.Height;
            
            graphics.ApplyChanges();
        }

        
        protected override void LoadContent()
        {
            //Load in all the data
            FontGothic20 = Content.Load<SpriteFont>("Fonts/FranklinGothic");
            testShipTexture = Content.Load<Texture2D>("testItems/testShip");
            testTurretTexture = Content.Load<Texture2D>("testItems/testTurret");
            testProjectileTexture = Content.Load<Texture2D>("testItems/testProjectile");
            testFire = Content.Load<SoundEffect>("testItems/testsound");
            testImpact = Content.Load<SoundEffect>("testItems/testexplosion");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Create Objects!
            enemyShip = new Ship(2, testShipTexture, new Vector2(600, 200), 1, 2, 20);
            enemyTurret = new Turret(2, testTurretTexture, 5, enemyShip, 1f, testFire, testImpact);

            testShip = new Ship(1, testShipTexture, new Vector2(200, 200),1, 1, 100);
            testTurret = new Turret(1, testTurretTexture, 50, testShip, 0.1f, testFire, testImpact);
            testTurret2 = new Turret(1, testTurretTexture, 50, testShip, 0.6f, testFire, testImpact);

            testTurret.SetControlByPlayer(true);
            testTurret2.SetControlByPlayer(true);
            testShip.setAsPlayer(true);

            enemyShip.setRotation(90);

            testParticleGen = new ParticleGenerator(testShip.getPosition(), 1, testProjectileTexture, 400, new Vector2(1, 1), 20, 1, 1);
            testParticleGen.setRandomDirection(true);

            // TODO: use this.Content to load your game content here
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            testTurret.setPosition(new Vector2(testShip.getPosition().X - 20 * (float)Math.Cos(testShip.getRotation()), testShip.getPosition().Y - 20 * (float)Math.Sin(testShip.getRotation())));
            testTurret2.setPosition(new Vector2(testShip.getPosition().X + 20 * (float)Math.Cos(testShip.getRotation()), testShip.getPosition().Y + 20 * (float)Math.Sin(testShip.getRotation())));
            //Does the input handling
            testShip.Handle(gameTime);
            //Upadates based on the input
            testShip.Update(gameTime);
            testTurret.Update(gameTime, testProjectileTexture, 20, enemyShip);
            testTurret2.Update(gameTime, testProjectileTexture, 20, enemyShip);
            // TODO: Add your update logic here

            enemyTurret.setPosition(new Vector2(enemyShip.getPosition().X - 20 * (float)Math.Cos(enemyShip.getRotation()), enemyShip.getPosition().Y - 20 * (float)Math.Sin(enemyShip.getRotation())));
            enemyShip.Handle(gameTime);
            enemyShip.Update(gameTime);
            enemyTurret.Update(gameTime, testProjectileTexture, 20, testShip);

            testParticleGen.setPosition(testShip.getPosition());
            testParticleGen.Update(gameTime);


            testTurret.setAimLoc(Camera.GetWorldPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), GraphicsDevice));
            testTurret2.setAimLoc(Camera.GetWorldPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), GraphicsDevice));

            Camera.CenterOnLocation(testShip.getPosition());

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

           

            spriteBatch.Begin(transformMatrix: Camera.get_transformation(graphics.GraphicsDevice));

            //
            
            testShip.Render(spriteBatch, 90, Color.Aqua);
            testTurret.Render(spriteBatch, 90, Color.Aqua);
            testTurret2.Render(spriteBatch, 90, Color.Aqua);

            //testParticleGen.Render(spriteBatch);
            

            
            enemyShip.Render(spriteBatch, 90, Color.Red);
            enemyTurret.Render(spriteBatch, 90, Color.Red);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(FontGothic20, "Shots fired: " + (testTurret.getShotsFired() + testTurret2.getShotsFired()), new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(FontGothic20, "Speed: " + testShip.getSpeed(), new Vector2(0, this.GraphicsDevice.DisplayMode.Height - 70), Color.Black);
            spriteBatch.DrawString(FontGothic20, "Rotation Speed: " + testShip.getRotaSpeed(), new Vector2(0, this.GraphicsDevice.DisplayMode.Height - 140), Color.Black);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
