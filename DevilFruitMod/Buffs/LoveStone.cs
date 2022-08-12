using System;
using System.IO;
using Microsoft.Xna.Framework;
using DevilFruitMod.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DevilFruitMod.Buffs
{
    public class LoveStone : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Turned to Stone");
            Description.SetDefault("Oof");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            /* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DFGlobalNPC>().LoveStone = true;
        }
    }
}
