﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Timor_Maris
{
    class Ship : GameObject
    {


        private bool ControlledByPlayer = false;
        private bool ControlledByAI = false;
        private bool isFlooding = false;

        protected float maxVelocity = 0f;
        protected float rotationSpeed = 0f;
        protected float maxRotationSpeed = 0f;

        protected int floodingRate = 0;
        protected float waterHP = 0f;

        private List<Vector2> Hardpoint = new List<Vector2>();
        
        public Ship() { }

        public Ship(int ID, Texture2D Texture, Vector2 Position,int Acceleration, int Faction, float Health)
        {
            this.ID = ID;
            this.Texture = Texture;
            this.Position = Position;
            this.Acceleration = Acceleration;
            this.Faction = Faction;
            this.Health = Health;
        }
        public override void Handle(GameTime GameTime)
        {
            if (ControlledByPlayer == true)
            {
                Console.WriteLine("Handling!");
                //TO DO: Gamepad Support?
                KeyboardState state = Keyboard.GetState();
                //TO DO: Rebindable Controlls
                //TO DO: Change velocity to a float and remove the left and right velocity.
                if (state.IsKeyDown(Keys.A) == true || state.IsKeyDown(Keys.Left) == true)
                {
                    RotationAcceleration -= 0.001f;
                }
                if (state.IsKeyDown(Keys.D) == true || state.IsKeyDown(Keys.Left) == true)
                {
                    RotationAcceleration += 0.001f;
                }
                if (state.IsKeyDown(Keys.W) == true || state.IsKeyDown(Keys.Up) == true)
                {
                    Acceleration -= 1;
                }
                if (state.IsKeyDown(Keys.S) == true || state.IsKeyDown(Keys.Down) == true)
                {
                    Acceleration += 1;
                }
                if (state.IsKeyDown(Keys.U) == true)
                {
                    this.Health = 0;
                }
            }
            else if (ControlledByAI == true)
            {

            }
        }

        public void addHardpoint(Vector2 position)
        {
            Hardpoint.Add(position);
        }

        public bool isPlayer()
        {
            return ControlledByPlayer;
        }
        public void setAsPlayer(bool ControlledByPlayer)
        {
            this.ControlledByPlayer = ControlledByPlayer;
        }
        public bool isAI()
        {
            return ControlledByAI;
        }
        public void setAsAI(bool ControlledByAI)
        {
            this.ControlledByAI = ControlledByAI;
        }
        public float getSpeed()
        {
            return Math.Abs(Acceleration);
        }
        public float getRotaSpeed()
        {
            return RotationAcceleration;
        }
    }

}
