using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Timor_Maris
{
    class Turret : GameObject
    {
        GameObject AttachedTo;
        private bool ControlledByPlayer = false;
        private bool ControlledByAI = false;

        private Vector2 FiringLocation;
        Projectile thisProjectile;
        private List<Projectile> ProjectileList = new List<Projectile>();
        Turret() { }
        public Turret(int ID, Texture2D Texture, int Faction, float Health, Ship AttachedTo)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Faction = Faction;
            this.Health = Health;
            this.AttachedTo = AttachedTo;
            this.FiringLocation = new Vector2(this.Position.X  , this.Position.Y);
            this.ControlledByPlayer = AttachedTo.isAI();
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

        public void Update(GameTime GameTime, Texture2D ProjectileSkin, int ProjectileDamage)
        {
            if (ControlledByPlayer == true)
            {
                KeyboardState state = Keyboard.GetState();
                MouseState mState = Mouse.GetState();

                Vector2 mLoc = new Vector2(mState.X, mState.Y);
                Vector2 val = Position - mLoc;
                this.Rotation = (float)(Math.Atan2(val.Y, val.X));

                //TO DO: Rebindable Controlls
                //TO DO: Change velocity to a float and remove the left and right velocity.
                if (state.IsKeyDown(Keys.Space) == true)
                {
                    ProjectileList.Add(new Projectile(1, ProjectileSkin, FiringLocation, 20, 10, this));
                }

            }
            else if (ControlledByAI == true)
            {

            }
            //this.Rotation -= AttachedTo.getRotation() + 90;
            foreach (Projectile Projectile in ProjectileList)
            {
                Projectile.Update(GameTime);
            }
        }
        public void setProjectile(Projectile Projectile)
        {
            this.thisProjectile = Projectile;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            foreach (Projectile Projectile in ProjectileList)
            {
                Projectile.Render(spriteBatch);
            }
        }
    }
}
