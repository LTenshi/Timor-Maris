using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Timor_Maris
{
    class Projectile : GameObject
    {
        int Damage = 0;
        Projectile() { }

        public Projectile(int ID, Texture2D Texture, Vector2 Position, int Damage, float Acceleration, Turret Origin)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Position = Position;
            this.Damage = Damage;
            this.Acceleration = Acceleration;
            this.Direction = Origin.getRotation();
            this.Rotation = Origin.getRotation();
        }

        public bool CheckCollision(GameObject otheObject)
        {
            return true;
        }
    }
}
