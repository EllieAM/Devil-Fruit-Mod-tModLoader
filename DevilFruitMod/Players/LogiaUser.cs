using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevilFruitMod.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.Players
{
    class LogiaUser : ModPlayer
    {

        public bool logiaDodge = false;
        public int logiaTimer = 0;
        public int logiaDustID = 0;
        public bool LogiaDodges(Player player)
        {
            if (player.GetModPlayer<DevilFruitUser>().devilFruitType == DevilFruitUser.LOGIA && player.HasBuff(ModContent.BuffType<Buffs.LogiaDodge>()))
            {
                if (player.GetModPlayer<DevilFruitUser>().fruitLevel == 0 || player.GetModPlayer<DevilFruitUser>().fruitLevel == 1)
                {
                    return true;
                }
                if (player.GetModPlayer<DevilFruitUser>().fruitLevel == 2)
                {
                    return false;
                }
                return false;
            }
            return false;
        }

        public override void PreUpdate()
        {
            if (player.GetModPlayer<DevilFruitUser>().devilFruitType == DevilFruitUser.LOGIA && player.GetModPlayer<Players.LogiaUser>().logiaDodge == true)
            {
                player.AddBuff(ModContent.BuffType<Buffs.LogiaDodge>(), 30, true);
                player.GetModPlayer<LogiaUser>().logiaTimer++;
                Vector2 vec = TMath.RandomPlayerHitboxPos(player);
                Dust.NewDust(vec, 1, 1, logiaDustID, 0, 0, 255, default, 1.6f);
                Dust.NewDust(vec, 2, 4, logiaDustID, 0, 0, 255, default, 1.6f);
            }
        }

        public override void SetControls()
        {
            if (LogiaDodges(player))
            {
                player.controlJump = false;
                player.controlDown = false;
                player.controlLeft = false;
                player.controlRight = false;
                player.controlUp = false;
                player.controlHook = false;
                player.controlLeft = false;
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (player.GetModPlayer<LogiaUser>().logiaDodge)
            {
                if (player.GetModPlayer<LogiaUser>().logiaTimer >= 20)
                {
                    CombatText.NewText(player.getRect(), Color.White, "Logia Dodge");
                    player.GetModPlayer<LogiaUser>().logiaTimer = 0;
                }
                return false;
            }

            return true;
        }

    }
}
