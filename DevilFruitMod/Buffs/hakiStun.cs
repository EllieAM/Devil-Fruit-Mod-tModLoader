using System;
using System.IO;
using Microsoft.Xna.Framework;
using DevilFruitMod.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace DevilFruitMod.Buffs
{
    public class hakiStun : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Haki Stun");
            Description.SetDefault("You're will wasn't strong enough to be immune to the haki pulse.");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DFGlobalNPC>().hakiStun = true;
        }
    }
}
