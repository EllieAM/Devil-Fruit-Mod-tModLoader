using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DevilFruitMod.Buffs
{
    class LogiaDodge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Logia Power");
            Description.SetDefault("Your power makes you an invournable element...");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }

    }
}
