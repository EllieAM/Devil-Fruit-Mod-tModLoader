using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

namespace DevilFruitMod.LoveLoveFruit
{
    public class PistolKiss : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.alpha = 128;
            projectile.timeLeft = 90;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //make thing happen on collision
            if (target.loveStruck)
                target.AddBuff(ModContent.BuffType<Buffs.LoveStone>(), 300);
        }

        public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands--;
            Main.PlaySound(SoundID.Dig, projectile.position);
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            for (int i = 0; i <= 10; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, projectile.velocity.X/2, projectile.velocity.Y/2, 0);
            }
        }

        public override bool? CanHitNPC(NPC npc)
        {
            //if (npc.loveStruck == false) return false;
            return null;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255,255,255, 128);
        }
    }
}

