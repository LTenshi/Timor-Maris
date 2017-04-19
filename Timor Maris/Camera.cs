using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Timor_Maris
{
    class Camera
    {
        //Location of the camera
        public Vector2 location { get; set; }
        //Zoom of the camera
        public float zoom { get; set; }
        //Rotation of the camera
        public float rotation { get; set; }
        //Transform matrix of the camera used for the camera translation
        private Matrix transform;


        public Camera()
        {
            location = Vector2.Zero;
            zoom = 1.0f;
            rotation = 0.0f;
        }

        public Vector2 GetWorldPosition(Vector2 mousePosition, GraphicsDevice GraphicsDevice)
        {
            return Vector2.Transform(mousePosition, Matrix.Invert(get_transformation( GraphicsDevice)));
        }
        public void MoveCamera(Vector2 value)
        {
            location += value;
        }
        public void CenterOnLocation(Vector2 where)
        {
            location = where;
        }
        public Matrix get_transformation(GraphicsDevice GraphicsDevice)
        {
            //Gets the viewport of the current graphics device
            Viewport viewport = GraphicsDevice.Viewport;
            //Inverts the current location vector
            transform = Matrix.CreateTranslation(new Vector3(-location.X, -location.Y, 0))
            //Adds in the rotation
                * Matrix.CreateRotationZ(rotation)
            //Adds in the scale
                * Matrix.CreateScale(new Vector3(zoom, zoom, 1))
            //Adds in the viewport
                * Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            return transform;
        }
    }

}
