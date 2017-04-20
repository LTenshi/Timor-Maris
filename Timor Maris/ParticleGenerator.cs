using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Timor_Maris
{
    public class ParticleGenerator
    {
        private Random rngTex = new Random();

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
        public float dirMin { get; set; }
        public float dirMax { get; set; }

        public bool RandomSpeed { get; set; }
        public int speedMin { get; set; }
        public int speedMax { get; set; }

        public bool RandomSize { get; set; }
        public int sizeMin { get; set; }
        public int sizeMax { get; set; }

        public bool RandomLifespan { get; set; }
        public int lyfMin { get; set; }
        public int lyfMax { get; set; }

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
                
                int rando = rngTex.Next(0, ParticleTextures.Count);
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
        public void setRandomDirection(bool value, float dirMin, float dirMax)
        {
            this.RandomDirection = value;
            this.dirMin = dirMin;
            this.dirMax = dirMax;
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

        public void setRandomLifespan(bool value, int lyfMin, int lyfMax)
        {
            this.RandomLifespan = value;
            this.lyfMin = lyfMin;
            this.lyfMax = lyfMax;
        }


        public Vector2 getSizeMulti()
        {
            return new Vector2(sizeMin, sizeMax);
        }
      
    }
}
