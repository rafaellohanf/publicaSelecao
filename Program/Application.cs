using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ScoreTracker.Program
{
    /// <summary>
    /// Base class of the application, starts and runs everythig
    /// </summary>
    class Application
    {
        /// <summary>
        /// Application Screen Manager, it will control and show everything on screen
        /// </summary>
        ScreenManager screen;

        /// <summary>
        /// Where all the score's information are stored
        /// </summary>
        ScoreTable scoreTable;

        /// <summary>
        /// Load the data of the Application and setup the objects
        /// </summary>
        public void Start()
        {
            scoreTable = ScoreTable.Instance();
            screen = ScreenManager.Instance();
            screen.Start(scoreTable);
        }

        /// <summary>
        /// Run the Screen Manager Loop
        /// </summary>
        public void Run()
        {
            screen.Display();           
        }
    }
}
