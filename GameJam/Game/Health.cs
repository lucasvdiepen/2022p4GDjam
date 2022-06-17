using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJam.Game
{
    public class Health
    {
        public int StartHp { get; private set; }
        public int CurrentHp { get; private set; }

        public Health(int startHp)
        {
            StartHp = startHp;
            CurrentHp = StartHp;
        }

        //Health functions to be called to modify the health;
        public void ResetHP()
        {
            CurrentHp = StartHp;
            Application.Restart();
        }

        public void AddHealth(int health)
        {
            CurrentHp += health;
        }

        public void RemoveHealth(int health)
        {
            CurrentHp -= health;
            if (CurrentHp <= 0) Dead();
        }

        private void Dead()
        {
            ResetHP();
        }
    }
}
