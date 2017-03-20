using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Timor_Maris
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ship testShip;

        Turret testTurret;

        Texture2D testShipTexture;
        Texture2D testTurretTexture;
        Texture2D testProjectileTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Window.IsBorderless = true;
            Window.AllowUserResizing = true;

        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testShipTexture = Content.Load<Texture2D>("testItems/testShip");
            testTurretTexture = Content.Load<Texture2D>("testItems/testTurret");
            testProjectileTexture = Content.Load<Texture2D>("testItems/testProjectile");

            testShip = new Ship(1, testShipTexture, new Vector2(200, 200),1, 1, 100);
            //testShip.setRotation(testShip.getRotation() );
            testTurret = new Turret(1, testTurretTexture, 1, 50, testShip);

            testTurret.SetControlByPlayer(true);
            testShip.setAsPlayer(true);

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

            testTurret.setPosition(testShip.getPosition());

            //Does the input handling
            testShip.Handle();
            //Upadates based on the input
            testShip.Update(gameTime);
            testTurret.Update(gameTime, testProjectileTexture, 20);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            testShip.Render(spriteBatch);
            testTurret.Render(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
