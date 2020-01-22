using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace DevilFruitMod
{
    class DFWorldClass : ModWorld
    {
        public static int worldDays;

        public override void PreWorldGen()
        {
            worldDays = 0;
        }

        public override void PreUpdate ()
        {
            if (Main.time == 0 && Main.dayTime == true)
            {
                worldDays++;
                Main.NewText("Day " + worldDays);
            }
        }
    }
}
