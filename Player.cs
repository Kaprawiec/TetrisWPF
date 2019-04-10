using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    [Serializable]
    public class Player
    {
        public string playerName;
        public long score { get; set; }
        public long lines { get; set; }
        public Player(string pName)
        {
            playerName = pName;
            score = 0;
            lines = 0;
        }
    }
}
