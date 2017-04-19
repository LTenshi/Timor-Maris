using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Timor_Maris
{
    class ParticleGenerator
    {
        private Random rng = new Random();

        private Vector2 Position;
        private float Rotation;

        private List<Particle> ParticleList = new List<Particle>();
        private List<Texture2D> ParticleTextures = new List<Texture2D>();

        private float pLifespan;
        private Vector2 pSize = new Vector2(1, 1);
        private float pSpeed;
        private float pDirection;


        private int pCount;


        public bool RandomDirection {  get; set; }

        public bool RandomSpeed { get; set; }
        private int speedMin = 0;
        private int speedMax = 0;

        public bool RandomSize { get; set; }
        private int sizeMin = 0;
        private int sizeMax = 0;

        private bool Alive = true;

        //Generic Constructor
        ParticleGenerator() { }

        ~ParticleGenerator()
        {
            this.Alive = false;
        }

        //Takes single Texture into consideration
        public ParticleGenerator(Vector2 Position, float Rotation, Texture2D pTexture, float pLifespan, Vector2 pSize, float pSpeed, float pDirection, int pCount)
        {
            this.ParticleTextures.Add(pTexture);
            this.Position = Position;
            this.Rotation = Rotation;

            this.pLifespan = pLifespan;
            this.pSize = pSize;
            this.pSpeed = pSpeed;
            this.pDirection = pDirection;
            this.pCount = pCount;
            
        }

        //Can take a whole list
        public ParticleGenerator(Vector2 Position, float Rotation, List<Texture2D> ParticleTextures, float pLifespan, Vector2 pSize, float pSpeed, float pDirection, int pCount)
        {
            this.ParticleTextures = ParticleTextures;

            this.Position = Position;
            this.Rotation = Rotation;

            this.pLifespan = pLifespan;
            this.pSize = pSize;
            this.pSpeed = pSpeed;
            this.pDirection = pDirection;
            this.pCount = pCount;
            
        }


        public void Render(SpriteBatch SpriteBatch)
        {
            if (Alive)
            {
                foreach (Particle Particle in ParticleList)
                {
                    Particle.Render(SpriteBatch, Color.White);
                }
            }
            
        }
        
        public void Update(GameTime GameTime)
        {
            // Particle(ParticleGenerator Source, float Direction, Vector2 Size, float Lifespan, float Speed, Texture2D Texture
            for (int i = 0; i != pCount; i++)
            {
                int rando = rng.Next(0, ParticleTextures.Count);
                ParticleList.Add(new Particle(this, pDirection, pSize, pLifespan, pSpeed, ParticleTextures[rando]));
            }

            
            for (int i = 0; i != ParticleList.Count; i++)
            {

                ParticleList[i].Update(GameTime);
                if (ParticleList[i].getLifespan() <= 0)
                {
                    ParticleList.RemoveAt(i);
                };
                
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
        public Vector2 getPosition()
        {
            return Position;
        }
        public void setRandomDirection(bool value)
        {
            this.RandomDirection = value;
        }

        public void setRandomSpeed(bool value, int speedMin, int speedMax)
        {
            this.RandomDirection = value;
            this.speedMin = speedMin;
            this.speedMax = speedMax;
        }


        public void setRandomSize(bool value, int sizeMin, int sizeMax)
        {
            this.RandomDirection = value;
            this.sizeMin = sizeMin;
            this.sizeMax = sizeMax;
        }
      

        public Vector2 getSizeMulti()
        {
            return new Vector2(sizeMin, sizeMax);
        }
    }
}
