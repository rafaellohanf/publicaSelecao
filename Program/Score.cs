using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;


namespace ScoreTracker.Program
{
    [Serializable]
    public class Score
    {
        public enum ScoreType
        {
            HIGH_SCORE,
            LOW_SCORE,
            STANDARD
        }
        private int scoreId;
        private int points;
        public ScoreType scoreType = ScoreType.STANDARD;

        

        public Score(int score_id, int points)
        {
            SetId(score_id);
            SetPoints(points);

        }
        
        public void SetId(int score_id)
        {
            this.scoreId = score_id;
        }

        public void SetPoints(int points)
        {
            this.points = points;
        }

        public void SetScoreType (ScoreType scoreType)
        {
            this.scoreType = scoreType;
        }

        public int GetId()
        {
            return scoreId;
        }

        public int GetPoints()
        {
            return points;
        }

        public ScoreType GetSCoreType()
        {
            return scoreType;
        }
    }
}
