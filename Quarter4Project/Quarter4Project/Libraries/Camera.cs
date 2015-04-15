using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarter4Project
{
    /// <summary>
    /// Creates a matrix to be used in spriteBatch.Begin();
    /// </summary>
    public class Camera
    {

        #region Fields

        public Matrix matrix; // Matrix of the camera position.
        public Vector2 pos = Vector2.Zero; // Camera Position.

        #endregion

        #region Methods

        /// <summary>
        /// Sets matrix using the position of the camera.
        /// </summary>
        /// <returns>Returns matrix version of the camera position.</returns>
        public Matrix getMatrix()
        {
            matrix = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 1));
            return matrix;
        }

        #endregion

    }
}