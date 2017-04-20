using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Timor_Maris
{
    public class Particle
    {
        ParticleGenerator Source;
        
        private Random rngSize = new Random();
        private Random rngDir = new Random();
        private Random rngSpd = new Random();
        private Random rngLyf = new Random();
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

           
            if (Source.RandomSize)
            {
                int randoScale = rngSize.Next((int)Source.getSizeMulti().X, (int)Source.getSizeMulti().Y);
                this.Size.X += this.Size.X * randoScale;
                this.Size.Y += this.Size.Y * randoScale;
            }
            if (Source.RandomDirection)
            { 
                this.Direction = MathHelper.ToRadians(rngDir.Next((int)MathHelper.ToDegrees(Source.dirMin), (int)MathHelper.ToDegrees(Source.dirMax)));
            }
            if (Source.RandomSize == true)
            {
                float multiplier = rngSize.Next(Source.sizeMin, Source.sizeMax);
                this.Size = this.Size * multiplier;
            }
            if (Source.RandomSpeed == true)
            {
                this.Speed = rngSpd.Next(Source.speedMin, Source.speedMax);
            }
            if (Source.RandomLifespan == true)
            {
                this.Lifespan = rngLyf.Next(Source.lyfMin, Source.lyfMax);
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
