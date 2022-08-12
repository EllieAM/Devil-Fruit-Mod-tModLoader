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
    public class PistolKiss : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 128;
            Projectile.timeLeft = 90;
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
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            for (int i = 0; i <= 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 21, Projectile.velocity.X/2, Projectile.velocity.Y/2, 0);
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

