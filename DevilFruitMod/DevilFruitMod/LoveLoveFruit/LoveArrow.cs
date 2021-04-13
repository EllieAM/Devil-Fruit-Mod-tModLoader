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
    public class LoveArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //make thing happen on collision
            target.AddBuff(ModContent.BuffType<Buffs.LoveStone>(), 60);
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            for (int i = 0; i <= 10; i++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, projectile.velocity.X/2, projectile.velocity.Y/2, 0);
            }
        }

        public override bool? CanHitNPC(NPC npc)
        {
            return null;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255,255,255, 128);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }
    }
}

