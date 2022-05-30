using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Game
{
    public class Health
    {
        private int _startHp;
        public int CurrentHp { get; private set; }

        public Health(int startHp)
        {
            _startHp = startHp;
            CurrentHp = _startHp;
        }

        //Health functions to be called to modefy the health;
        public void ResetHP()
        {
            CurrentHp = _startHp;
        }
        public void AddHealth(int health)
        {
            CurrentHp += health;
        }
        public void RemoveHealth(int health)
        {
            CurrentHp -= health;
        }
    }
}
