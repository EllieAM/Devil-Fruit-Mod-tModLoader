using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DevilFruitMod.Buffs;
using IL.Terraria.ID;
using static DevilFruitMod.DFWorldClass;

namespace DevilFruitMod.NPCs
{
    public class DFGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool hakiStun;

        private bool noTileCollideDefault;
        private bool noGravityDefault;
        private int aiStyleDefault;

        public override void ResetEffects(NPC npc)
        {
            hakiStun = false;
        }

        public override void SetDefaults(NPC npc)
        {
            noTileCollideDefault = npc.noTileCollide;
            noGravityDefault = npc.noGravity;
            aiStyleDefault = npc.aiStyle;
        }

        public override bool PreAI(NPC npc)
        {
            if (hakiStun)
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                npc.aiStyle = 0;
            }
            else
            {
                npc.noTileCollide = noTileCollideDefault;
                npc.noGravity = noGravityDefault;
                npc.aiStyle = aiStyleDefault;
            }

            return true;
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {

            if (type == 17) //Merchant
            {
                int dayFruit = DFWorldClass.worldDays % 1; //New Fruit Every Day

                if (dayFruit == 0) shop.item[nextSlot].SetDefaults(mod.ItemType("GumGumFruit"));
                //if (dayFruit == 1) shop.item[nextSlot].SetDefaults(mod.ItemType("IceIceFruit"));
                nextSlot++;
            }
        }
    }
}
