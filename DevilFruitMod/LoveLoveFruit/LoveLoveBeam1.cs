using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DevilFruitMod.LoveLoveFruit
{
    public class LoveLoveBeam1 : ModProjectile
    {
        private bool initial = true;
        private int[] projectileNum = { 2, 170 };

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 21;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 128;
            Projectile.scale = 1f;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (initial)
            {
                if (Main.mouseX - Main.screenWidth / 2 < 0)
                    Main.player[Projectile.owner].ChangeDir(-1);
                else
                    Main.player[Projectile.owner].ChangeDir(1);

                Projectile.scale = 0;
                initial = false;
            }

            Lighting.AddLight(Projectile.Center, 2f, 1.5f, 1.5f);

            if (Projectile.scale < 1)
            {
                Projectile.scale = (180f - Projectile.timeLeft) / 60f;

            }
            //Moves forward

            if (Projectile.velocity.X < 0.0)
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
            else
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 75, 98, 128);
        }
    }
}
