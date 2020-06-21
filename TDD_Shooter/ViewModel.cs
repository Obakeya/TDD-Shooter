using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_Shooter.Model;

namespace TDD_Shooter
{
    class ViewModel
    {
        public Ship Ship { get; set; }

        internal ViewModel()
        {
            Ship = new Ship();
        }
    }
}
