using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam.Game
{
    public class Health
    {
        private int _startHp;
        public int CurrentHp { get; private set; }

        internal Health(int startHp)
        {
            _startHp = startHp;
            CurrentHp = _startHp;
        }

        //Health functions to be called to modify the health;
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
            if (CurrentHp <= 0) Dead();
        }

        private void Dead()
        {
            ResetHP();
        }

        internal int GiveHealth()
        {
            return CurrentHp;
        }
    }
}
