using System;
using System.IO;
using Microsoft.Xna.Framework;
using DevilFruitMod.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DevilFruitMod.Buffs
{
    public class hakiStun : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haki Stun");
            Description.SetDefault("You're will wasn't strong enough to be immune to the haki pulse.");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
                /* tModPorter Note: CanBeCleared Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ 
        }       /* Buff IDs: https://terraria.fandom.com/wiki/Buff_IDs */

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DFGlobalNPC>().hakiStun = true;
        }
    }
}
