using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ScoreTracker.Program
{
    class ScoreManager
    {
        List<Score> scrList;
        enum sortParameter
        {
            LowToHigh,
            HighToLow,
            IdAscendant,
            IdDescendant,
        };
        enum scoreFilter
        {
            NoFilter,
            AvarageOrAbove,
            AvaregeOrBellow,
        };

        public void AddScore(int id, int Score)
        {
            
        }

        public void RemoveScore(Score score)
        {

        }

        public void SortScore()
        {

        }
    }
}
