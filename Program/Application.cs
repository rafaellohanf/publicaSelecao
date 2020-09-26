using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ScoreTracker.Program
{
    class Application
    {
        ScreenManager screen;
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
        /// Application running loop
        /// </summary>
        public void Run()
        {
            screen.Display();           
        }

        /// <summary>
        /// Save the data and ends the application
        /// </summary>
        public void End()
        {

        }
    }
}
