using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication
{
    public struct ApplicationSettings
    {
        public char amountSplitCharacter;
        public char commentSplitCharacter;

        public ApplicationSettings(char a, char c)
        {
            amountSplitCharacter = a;
            commentSplitCharacter = c;
        }
    }
}
