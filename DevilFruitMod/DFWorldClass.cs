using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DevilFruitMod
{
    class DFWorldClass : ModSystem
    {
        public static int worldDays;

        public override void PreWorldGen()
        {
            worldDays = 0;
        }

        public override void PreUpdateWorld ()
        {
            if (Main.time == 0 && Main.dayTime == true)
            {
                worldDays++;
                Main.NewText("Day " + worldDays);
            }
        }

        public override void SaveWorldData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            tag.Add("worldDays", worldDays);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            worldDays = tag.GetInt("worldDays");
        }
    }
}
