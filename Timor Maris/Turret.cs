using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Timor_Maris
{
    public class Turret : GameObject
    {
        GameObject AttachedTo;

        private float MousePositionRotation;

        private Vector2 FiringLocation = Vector2.Zero;
        private float firerate;                         //THIS NEEDS TO BE IN SECONDS FOR EASE OF USE
        private float timer;

        private SoundEffect FireSound;
        private SoundEffect ImpactSound;

        private Vector2 AimLoc;
        private List<Projectile> ProjectileList = new List<Projectile>();

        private int ShotsFiredCounter;

        //Flags:
        private bool ControlledByPlayer = false;
        private bool ControlledByAI = false;
        private bool OnCooldown = false;

        Turret() { }
        public Turret(int ID, Texture2D Texture,  float Health, Ship AttachedTo, float firerate, SoundEffect FireSound, SoundEffect Impact)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Faction = AttachedTo.getFaction();
            this.Health = Health;
            this.AttachedTo = AttachedTo;
            this.FiringLocation = Vector2.Zero;
            this.ControlledByPlayer = AttachedTo.isAI();
            this.firerate = firerate;
            this.timer = firerate;
            this.FireSound = FireSound;
            this.ImpactSound = Impact;
        }

        public void SetControlByPlayer(bool value)
        {
            if (value == true)
            {
                ControlledByPlayer = true;
                ControlledByAI = false;
            }

            else
            {
                ControlledByPlayer = false;
                ControlledByAI = true;
            }

        }

        public void Update(GameTime GameTime, Texture2D ProjectileSkin, int ProjectileDamage, GameObject otherObject)
        {
            


            if (AttachedTo.getAlive() == false)
            {
                this.Health = 0;
            }

            if (ControlledByPlayer == true)
            {
                KeyboardState state = Keyboard.GetState();
                
                
                //Vector2 mLoc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 val = Position - AimLoc;

                MousePositionRotation = (float)(Math.Atan2(val.Y, val.X));


                if (MousePositionRotation < Rotation) { MousePositionRotation += MathHelper.TwoPi; }
                
                if ((MousePositionRotation - Rotation) < MathHelper.Pi)
                {
                    Rotation += MathHelper.ToRadians(40) * (float)GameTime.ElapsedGameTime.TotalSeconds;
                }
                
                else if ((MousePositionRotation - Rotation) > MathHelper.Pi)
                {
                    Rotation -= MathHelper.ToRadians(40) * (float)GameTime.ElapsedGameTime.TotalSeconds;
                }
                if (Rotation <= -Math.PI)
                {
                    Rotation += (float)MathHelper.TwoPi;
                }
                if (Rotation >= Math.PI)
                {
                    Rotation -= (float)MathHelper.TwoPi;
                }
                timer += (float)GameTime.ElapsedGameTime.TotalSeconds;
                if (timer > firerate)
                {
                    OnCooldown = false;
                }
                if (state.IsKeyDown(Keys.Space) == true && timer > firerate)
                {
                    OnCooldown = true;
                    this.FiringLocation.X = Position.X - (Texture.Height / 2) * (float)Math.Cos(Rotation);
                    this.FiringLocation.Y = Position.Y - (Texture.Height / 2) * (float)Math.Sin(Rotation);

                    ProjectileList.Add(new Projectile(1, ProjectileSkin, this.FiringLocation, ProjectileDamage, 1000, this.Rotation - MathHelper.ToRadians(180), this.Faction, 100,ImpactSound));
                        
                    ShotsFiredCounter++;
                    FireSound.Play();

                    timer = 0;
                }

                //STRETCH TO DO: Rebindable Controlls

            }
            else if (ControlledByAI == true)
            {
                this.FiringLocation = new Vector2(Position.X, Position.Y - Texture.Height);
            }
            //this.Rotation -= AttachedTo.getRotation() + 90;

            foreach (Projectile Projectile in ProjectileList)
            {

                Projectile.CheckCollision(otherObject);

                Projectile.setHealth(Projectile.getHealth() - 1);
                Projectile.Update(GameTime);
                
            }

        }

        public void setAimLoc(Vector2 value)
        {
            AimLoc = value;
        }

        public override void Render(SpriteBatch spriteBatch, float ANGLE_OFFSET, Color Colour)
        {
            base.Render(spriteBatch, ANGLE_OFFSET, Colour);
           foreach (Projectile Projectile in ProjectileList)
           {
              Projectile.Render(spriteBatch, -90, Colour);
           }

        }
        public int getShotsFired()
        {
            return ShotsFiredCounter;
        }
        public float getMousePositionRotation()
        {
            return MousePositionRotation;
        }
    }
}
