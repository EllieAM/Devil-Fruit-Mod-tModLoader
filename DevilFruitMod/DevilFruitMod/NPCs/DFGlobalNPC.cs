using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DevilFruitMod.Buffs;
using static DevilFruitMod.DFWorldClass;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace DevilFruitMod.NPCs
{
    public class DFGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true; //necessary for some reason

        //Changed in the buff files
        public bool hakiStun;
        public bool LoveStone;

        private bool resetBatchInPost;

        private bool noTileCollideDefault;
        private bool noGravityDefault;
        private int damageDefault;
        private int aiStyleDefault;
        private int alphaDefault;
        private double frameCounterDefault;

        public override void ResetEffects(NPC npc)
        {
            hakiStun = false;
            LoveStone = false;
        }

        public override void SetDefaults(NPC npc)
        {
            noTileCollideDefault = npc.noTileCollide;
            noGravityDefault = npc.noGravity;
            aiStyleDefault = npc.aiStyle;
            damageDefault = npc.damage;
            frameCounterDefault = npc.frameCounter;
            alphaDefault = npc.alpha;
        }

        public override bool PreAI(NPC npc)
        {
            if (LoveStone)
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                npc.velocity.X = 0;
                npc.damage = 0;
                npc.frameCounter = 0;
                npc.alpha = 0;
                npc.loveStruck = false;
                return false;
            }
            else if (hakiStun)
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
                npc.damage = damageDefault;
                //npc.frameCounter = frameCounterDefault;
                npc.alpha = alphaDefault;
            }

            return true;
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == 17) //Merchant
            {
                //int dayFruit = DFWorldClass.worldDays % 2; //New Fruit Every Day

                shop.item[nextSlot].SetDefaults(mod.ItemType("GumGumFruit"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("LoveLoveFruit"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("HitoHitoFruit"));
                nextSlot++;
                //if (dayFruit == 2) shop.item[nextSlot].SetDefaults(mod.ItemType("CatCatFruitLeopard"));
                //if (dayFruit == 3) shop.item[nextSlot].SetDefaults(mod.ItemType("HollowHollowFruit"));
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (LoveStone && Main.netMode != NetmodeID.Server)
            {
                resetBatchInPost = true; // We're using a dedicated bool for this in the *very* unlikely case the buff somehow gets purged during drawing.

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix); // SpriteSortMode needs to be set to Immediate for shaders to work.

                GameShaders.Misc["LoveStone"].Apply(); // If you need to set any parameters, you can so before Apply (e.g. Misc["EffectName'].UseColor(something).Apply() )

            }

            return true;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (resetBatchInPost)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                resetBatchInPost = false;
            }
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (LoveStone) return Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16);
            else return null;
        }
    }
}