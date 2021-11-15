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
            projectile.extraUpdates = 0;
            projectile.width = 21;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.alpha = 128;
            projectile.scale = 1f;
            projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (initial)
            {
                if (Main.mouseX - Main.screenWidth / 2 < 0)
                    Main.player[projectile.owner].ChangeDir(-1);
                else
                    Main.player[projectile.owner].ChangeDir(1);

                projectile.scale = 0;
                initial = false;
            }

            Lighting.AddLight(projectile.Center, 2f, 1.5f, 1.5f);

            if (projectile.scale < 1)
            {
                projectile.scale = (180f - projectile.timeLeft) / 60f;

            }
            //Moves forward

            if (projectile.velocity.X < 0.0)
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 3.14f;
            else
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 75, 98, 128);
        }
    }
}
