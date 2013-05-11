using System;

namespace Minesweeper
{
    /// <summary>
    /// Class keeping the information on the score records
    /// </summary>
    public class ScoreRecord
    {
        private string personName;
        private int scorePoints;

        public ScoreRecord()
        {
        }

        public ScoreRecord(string personName, int points)
        {
            this.personName = personName;
            this.scorePoints = points;
        }
        /// <value>PersonName accesses the value of the personName data member</value>
       
        public string PersonName
        {
            get
            {
                return personName;
            }
            set
            {
                personName = value;
            }
        }
        /// <value>ScorePoints accesses the value of the scorePoints data member</value>
       
        public int ScorePoints
        {
            get
            {
                return scorePoints;
            }
            set
            {
                scorePoints = value;
            }
        }
    }
}