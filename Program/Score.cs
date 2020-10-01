using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ScoreTracker.Program
{
    /// <summary>
    /// The Score unit, keeps track on simple things as point value, ID and Type of the score
    /// </summary>
    [Serializable]
    public class Score
    {
        /// <summary>
        /// Defining diferent types of score: High Score, Low score or standard
        /// </summary>
        public enum ScoreType
        {
            HIGH_SCORE,
            LOW_SCORE,
            STANDARD
        }

        /// <summary>
        ///The ID of a score
        /// </summary>
        private int scoreId;

        /// <summary>
        /// The Current Value of a score
        /// </summary>
        private int points;

        /// <summary>
        /// Stores the type of the score comparing to other members of the list (if it's in one)
        /// </summary>
        public ScoreType scoreType = ScoreType.STANDARD;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="points">Score value (number of points on a match)</param>
        /// <param name="score_id">Id for list creation and index purposes, O if it's empty</param>
        public Score(int points, int score_id = 0)
        {
            SetId(score_id);
            SetPoints(points);
        }

        /// <summary>
        /// Sets score's ID
        /// </summary>
        /// <param name="score_id">Score ID</param>
        public void SetId(int score_id)
        {
            this.scoreId = score_id;
        }

        /// <summary>
        /// Sets score's points
        /// </summary>
        /// <param name="points">Score Points</param>
        public void SetPoints(int points)
        {
            this.points = points;
        }

        /// <summary>
        /// Sets Score's Type
        /// </summary>
        /// <param name="scoreType">The type of the Score: High, Low or Standard</param>
        public void SetScoreType(ScoreType scoreType)
        {
            this.scoreType = scoreType;
        }

        /// <summary>
        /// Returns the Score's ID
        /// </summary>
        /// <returns>Score's ID</returns>
        public int GetId()
        {
            return scoreId;
        }

        /// <summary>
        /// Returns the Score's Point Value
        /// </summary>
        /// <returns>Score's Point Value</returns>
        public int GetPoints()
        {
            return points;
        }

        /// <summary>
        /// Returns the Score Type
        /// </summary>
        /// <returns>Score Type</returns>
        public ScoreType GetSCoreType()
        {
            return scoreType;
        }
    }
}