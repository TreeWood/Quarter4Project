using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.EventManagers
{
    /// <summary>
    /// This class performs events used on a global scale.
    /// </summary>
    class GlobalEvents
    {

        #region Fields

        Game1 game;

        #endregion

        #region Initialization

        public GlobalEvents(Game1 g)
        {
            game = g;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Closes the game.
        /// </summary>
        public void quitGame()
        {
            game.Exit();
        }

        #endregion

    }
}
