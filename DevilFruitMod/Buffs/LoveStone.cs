using System;
using System.IO;
using Microsoft.Xna.Framework;
using DevilFruitMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace DevilFruitMod.Buffs
{
    public class LoveStone : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Turned to Stone");
            Description.SetDefault("Oof");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DFGlobalNPC>().LoveStone = true;
        }
    }
}
