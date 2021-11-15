using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace DevilFruitMod.LoveLoveFruit
{
    public class LoveSparkle : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 3, 3);
            dust.scale = 2f;
            dust.fadeIn = 1f;
        }

        public override bool Update(Dust dust)
        {
            dust.fadeIn -= .2f;
            if (dust.fadeIn <= 0) dust.active = false;
            return false;
        }
    }
}