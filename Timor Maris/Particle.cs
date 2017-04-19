using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Timor_Maris
{
    class Particle
    {
        ParticleGenerator Source;

        private Random rngDirection = new Random();
        private Random rngSize = new Random();
        private Vector2 Position;
        private Texture2D Texture = null;
        private float Direction = 0;

        private Vector2 Size = new Vector2 (1,1);

        private float Lifespan = 0;
        private float Speed = 0;

        Particle() { }


        public Particle(Vector2 Position, float Direction, Vector2 Size, float Lifespan, float Speed, Texture2D Texture)
        {
            this.Position = Position;
            this.Direction = Direction;
            this.Size = Size;
            this.Lifespan = Lifespan;
            this.Speed = Speed;
            this.Texture = Texture;
        }

        public Particle(ParticleGenerator Source, float Direction, Vector2 Size, float Lifespan, float Speed, Texture2D Texture)
        {
            this.Position = Source.getPosition();
            this.Direction = Direction;
            this.Size = Size;
            this.Lifespan = Lifespan;
            this.Speed = Speed;
            this.Texture = Texture;
            this.Source = Source;

            if (Source.RandomDirection)
            {
                this.Direction = rngDirection.Next(1, 360);
            }
            if (Source.RandomSize)
            {
                int randoScale = rngSize.Next((int)Source.getSizeMulti().X, (int)Source.getSizeMulti().Y);
                this.Size.X += this.Size.X * randoScale;
                this.Size.Y += this.Size.Y * randoScale;
            }

        }

        public void Update(GameTime GameTime)
        {
            
            
            

            Position.X += Speed * (float)Math.Cos(Direction) * (float)GameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += Speed * (float)Math.Sin(Direction) * (float)GameTime.ElapsedGameTime.TotalSeconds;
            Lifespan--;
            

        }
        public void Render(SpriteBatch spriteBatch, Color Colour)
        {
            spriteBatch.Draw(Texture, Position, color: Colour, scale: Size, rotation: Direction);
        }

        public float getLifespan()
        {
            return Lifespan;
        }
        
    }
}
