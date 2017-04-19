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
    class GameObject
    {
        //Flags
        protected int ID = 0;
        protected int Faction = 0;
        protected bool Alive = true;
        protected bool Collidable = true;
        protected bool DebugSelected = false;             //This will be used for adding hardpoints. BUT: XML File and how to handle them. (NOTE FROM FUTURE: Might not be feasible time wise)

        protected Vector2 Position = new Vector2(0, 0);
        protected Vector2 Bounds;

        protected float Health = 0f;
        protected float Acceleration = 0f;
        protected float RotationAcceleration = 0f;
        protected float Rotation = 0f;

        //Animation Vars
        protected int fMax = 0;
        protected int fCur = 0;
        protected int fCnt = 0;
        protected int fDel = 0;
        protected int fWdth = 0;
        protected int fHght = 0;
        protected int fColumns = 0;

        protected Texture2D Texture = null;


        virtual public void Destroy() { }

        //Constructors
        public GameObject() { }
        public GameObject(int ID, Texture2D Texture, Vector2 Position, float Acceleration, float Rotation)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Position = Position;
            this.Acceleration = Acceleration;
      
        }

        public GameObject(int ID, Texture2D Texture, Vector2 Position, int Faction, float Health)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Position = Position;
            this.Faction = Faction;
            this.Health = Health;
        }

        virtual public void Update(GameTime GameTime)
        {
            Rotation += (RotationAcceleration * (float)GameTime.ElapsedGameTime.TotalSeconds);
            Position.X += (Acceleration * (float)Math.Cos(Rotation)) * (float)GameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += (Acceleration * (float)Math.Sin(Rotation)) * (float)GameTime.ElapsedGameTime.TotalSeconds;
            if (Health <= 0)
            {
                Alive = false;
            }
        }

        virtual public void Render(SpriteBatch spriteBatch, float ANGLE_OFFSET, Color Colour)
        {
            ANGLE_OFFSET = MathHelper.ToRadians(ANGLE_OFFSET);
            if (this.Health > 0)
            {
                spriteBatch.Draw(texture: this.Texture, position: this.Position, origin: this.GetCentre(this.Texture), rotation: this.Rotation - ANGLE_OFFSET, color: Colour);
            }
            
            
        }

        /// <summary>
        /// Performs a simple collision check based on a rectangle (Inaccurate)
        /// </summary>
        /// <param name="OtherGameObject"></param>
        /// <returns></returns>
        public bool CheckBasicCollision(GameObject OtherGameObject)
        {
            if (OtherGameObject.getAlive() == true)
            {
                if (Position.X + Texture.Width < OtherGameObject.Position.X)
                    return false;
                if (OtherGameObject.Position.X + OtherGameObject.Texture.Width < Position.X)
                    return false;
                if (Position.Y + Texture.Height < OtherGameObject.Position.Y)
                    return false;
                if (OtherGameObject.Position.Y + OtherGameObject.Texture.Height < Position.Y)
                    return false;
                return true;
            }
            return false;

        }

        virtual public void Handle(GameTime GameTime)
        {

        }
        /// <summary>
        /// Gets the centre of the texture.
        /// </summary>
        /// <param name="Texture"></param>
        /// <returns></returns>
        public Vector2 GetCentre(Texture2D Texture)
        {
            Vector2 Centre = new Vector2((Texture.Bounds.Width / 2), (Texture.Bounds.Height / 2));

            return Centre;
        }

        virtual public void Collided(int Type) { }

        public bool isCollidable()
        {
            return Alive && Collidable;
        }

        public void setPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
        public Vector2 getPosition()
        {
            return Position;
        }
        public Vector2 getBounds()
        {
            return Bounds;
        }
        public int getID()
        {
            return ID;
        }
        public void setID(int ID)
        {
            this.ID = ID;
        }
        public bool getAlive()
        {
            return Alive;
        }
        public void setAlive(bool Alive)
        {
            this.Alive = Alive;
        }
        public bool getCollidable()
        {
            return Collidable;
        }
        public void setCollidable(bool Collidable)
        {
            this.Collidable = Collidable;
        }
        public int getFaction()
        {
            return Faction;
        }
        public void setFaction(int Faction)
        {
            this.Faction = Faction;
        }
        public float getRotation()
        {
            return Rotation;
        }
        public void setRotation(float Rotation)
        {
            this.Rotation = Rotation;
        }
        public float getHealth()
        {
            return Health;
        }
        public void setHealth(float Health)
        {
            this.Health = Health;
        }
    }
}
