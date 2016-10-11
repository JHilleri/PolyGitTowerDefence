using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    interface Pausable
    {
        void OnPauseGame();
        void OnResumeGame();
    }
}
