using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Timor_Maris
{
    public class Projectile : GameObject
    {
        int Damage = 0;
        SoundEffect Impact;
        
        Projectile() { }

        public Projectile(int ID, Texture2D Texture, Vector2 Position, int Damage, float Acceleration, float Rotation, int Faction, int health, SoundEffect Impact)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Position = Position;
            this.Damage = Damage;
            this.Acceleration = Acceleration;
            this.Rotation = Rotation;
            this.Faction = Faction;
            this.Impact = Impact;
            this.Health = health;
            
        }


        public bool CheckCollision(GameObject otherObject)
        {
            if (CheckBasicCollision(otherObject))
            {
                Impact.Play();
                Health = 0;
                otherObject.setHealth(otherObject.getHealth() - this.Damage);
            }
            return true;
        }
        
        
    }
}
