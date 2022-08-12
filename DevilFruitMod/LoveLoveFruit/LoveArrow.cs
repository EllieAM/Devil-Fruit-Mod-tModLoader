using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Audio;
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
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //make thing happen on collision
            target.AddBuff(ModContent.BuffType<Buffs.LoveStone>(), 60);
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            for (int i = 0; i <= 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 21, Projectile.velocity.X/2, Projectile.velocity.Y/2, 0);
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
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }
    }
}

