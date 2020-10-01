using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ScoreTracker.Program
{
    /// <summary>
    /// Stores information about the list of scores
    /// </summary>
    [Serializable]
    public  class ScoreTable
    {
        /// <summary>
        /// Singleton instance, preventing other scorelists to be instantiated at the same time
        /// </summary>
        private static ScoreTable singInstance = null;

        /// <summary>
        /// The list of scores will be added and read
        /// </summary>
        private List<Score> scoreList;

        /// <summary>
        /// Stores the highest score on the list
        /// </summary>
        private int highScore;

        /// <summary>
        /// Stores the lowest score on the list
        /// </summary>
        private int lowScore;

        /// <summary>
        /// Stores the number of times a high score has been overcome
        /// </summary>
        private int nHighBreak;

        /// <summary>
        /// Stores the number of times a low score has been overcome
        /// </summary>
        private int nLowBreak;

        /// <summary>
        /// Stores the next ID of the Table
        /// </summary>
        private int idCounter;

        /// <summary>
        /// Stores the file name if this table needs to be saved
        /// </summary>
        private const String fileName = "ScoreTableSave";

        /// <summary>
        /// Standard private constructor, It's private preventing the class to be instantiated out of the singleton's instantiate method
        /// </summary>
        private ScoreTable()
        {
        }

        /// <summary>
        /// Instantiates the single instance of the class
        /// </summary>
        /// <returns>The adress of this single Instance</returns>
        public static ScoreTable Instance()
        {
            if (singInstance == null)
            {
                singInstance = new ScoreTable();
                singInstance.Start();
            }
            return singInstance;
        }

        /// <summary>
        /// Automatically load the saved file if there is one, else initialize the class and saves the object
        /// </summary>
        private void Start()
        { 
            try
            {
                singInstance = FileManager.Load<ScoreTable>(fileName);
            }
            catch
            {
                idCounter = 0;
                nHighBreak = 0;
                nLowBreak = 0;

                scoreList = new List<Score>();
                FileManager.Save<ScoreTable>(fileName, this);
            }
        }

        /// <summary>
        /// Reset the class to it's new state
        /// </summary>
        public void Restart()
        {
            idCounter = 0;
            nHighBreak = 0;
            nLowBreak = 0;
            highScore = 0;
            lowScore = 0;

            idCounter = 0;
            scoreList.Clear();
        }

        /// <summary>
        /// return the list of the score
        /// </summary>
        /// <returns>The Score List</returns>
        public List<Score> GetScoreList()
        {
            return scoreList;
        }

        /// <summary>
        /// Add Another Score to the list if its on the valid Score range
        /// </summary>
        /// <param name="points">Score Points</param>
        /// <returns>If the score is out of the range, returns false</returns>
        public bool AddScore(int points)
        {
            if(points <= 0 | points >= 1000)
            {
                return false;
            }
            Score scr = new Score(points, idCounter++);
            UpdateRecords(scr);
            scoreList.Add(scr);

            return true;
        }

        /// <summary>
        /// Updates de High Scores and Low Scores information
        /// </summary>
        /// <param name="score">The score to be compared to the list</param>
        private void UpdateRecords(Score score)
        {
            if (scoreList.Count == 0)
            {
                highScore = score.GetPoints();
                lowScore = score.GetPoints();
                score.SetScoreType(Score.ScoreType.STANDARD);
            }
            else
            {
                if (score.GetPoints() > highScore)
                {
                    highScore = score.GetPoints();
                    score.SetScoreType(Score.ScoreType.HIGH_SCORE);
                    nHighBreak++;
                }
                else if(score.GetPoints() < lowScore)
                {
                    lowScore = score.GetPoints();
                    score.SetScoreType(Score.ScoreType.LOW_SCORE);
                    nLowBreak++;
                }   
            }
        }
        
        /// <summary>
        /// Returns the Save/Load filename
        /// </summary>
        /// <returns>File name</returns>
        public String getFileName()
        {
            return fileName;
        }

        /// <summary>
        /// Returns the number of times a high score has been overcome
        /// </summary>
        /// <returns></returns>
        public int getNHighScore()
        {
            return nHighBreak;
        }

        /// <summary>
        /// Returns the number of times a low score has been overcome
        /// </summary>
        /// <returns></returns>
        public int getNLowScore()
        {
            return nLowBreak;
        }
    }
}