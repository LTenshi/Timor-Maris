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
        protected bool DebugSelected = false;             //This will be used for adding hardpoints. BUT: XML File and how to handle them

        protected Vector2 Position = new Vector2(0, 0);
        protected Vector2 Bounds = new Vector2(0, 0);

        protected float Health = 0f;
        protected float Acceleration = 0f;
        protected float Direction = 0f;
        protected float Rotation = 0f;

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
        public GameObject(int ID, Texture2D Texture, Vector2 Position, float Acceleration, float Direction)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Position = Position;
            this.Acceleration = Acceleration;
            this.Direction = Direction;
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

            this.Rotation = (Direction * (3.1416f / 180f)) * (float)GameTime.ElapsedGameTime.TotalSeconds;
            Position.X += (Acceleration * (float)Math.Cos(Rotation)) * (float)GameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += (Acceleration * (float)Math.Sin(Rotation)) * (float)GameTime.ElapsedGameTime.TotalSeconds;
        }

        virtual public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: this.Texture, position: this.Position, origin: this.GetCentre(this.Texture), rotation: this.Rotation - (MathHelper.ToRadians(90)));
        }

        /// <summary>
        /// Performs a simple collision check based on a rectangle (Inaccurate)
        /// </summary>
        /// <param name="OtherGameObject"></param>
        /// <returns></returns>
        public bool CheckBasicCollision(GameObject OtherGameObject)
        {
            Vector2 otherPosition = OtherGameObject.getPosition();
            Vector2 otherBounds = OtherGameObject.getBounds();

            if (Position.X + Bounds.X > otherPosition.X - otherBounds.X &&
                 Position.X - Bounds.X < otherPosition.X + otherBounds.X &&
                 Position.Y + Bounds.Y > otherPosition.Y - otherBounds.Y &&
                 Position.Y - Bounds.Y < otherPosition.Y + otherBounds.Y)
                return true;
            else
                return false;
        }

        virtual public void Handle()
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
    }
}
