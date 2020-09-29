using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace ScoreTracker.Program
{
    [Serializable]
    public  class ScoreTable
    {
        enum ScoreSort
        {
            LOWSCORE,
            HIGHSCORE,
            SCOREID_DESC,
            SCOREID_ASC,
        };
        enum ScoreFilter
        {
            NO_FILTER,
            ABOVE_AVARAGE,
            BELLOW_AVARAGE,
        };

        private static ScoreTable singInstance = null;

        private List<Score> scoreList;
        private int highScore;
        private int lowScore;
        private int nHighBreak;
        private int nLowBreak;

        private static int idCounter;
        private const String fileName = "ScoreTableSave";

        private ScoreTable()
        {
        }

        public static ScoreTable Instance()
        {
            if (singInstance == null)
            {
                singInstance = new ScoreTable();
                singInstance.Start();
            }
            return singInstance;
        }

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

        public List<Score> GetScoreList()
        {
            return scoreList;
        }

        public bool AddScore(int points)
        {
            if(points <= 0 | points >= 1000)
            {
                return false;
            }
            Score scr = new Score(idCounter++, points);
            UpdateRecords(scr);
            scoreList.Add(scr);
            return true;
        }

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
        
        public String getFileName()
        {
            return fileName;
        }

        public void SortScore()
        {

        }
    }
}
