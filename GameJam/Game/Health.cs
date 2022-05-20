using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Game
{
    public class Health
    {
        private int _startHP;

        public int CurrentHP 
        { 
            get => _startHP;

            set => _startHP = value;
        }
    }
}
