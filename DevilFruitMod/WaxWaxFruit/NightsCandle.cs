using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace DevilFruitMod.WaxWaxFruit
{
    public class NightsCandle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 48;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
			projectile.timeLeft = 30;
			projectile.penetrate = -1;
		}

        public override void AI()
		{
            if (projectile.velocity.X < 0)
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.spriteDirection = -1;
            }
            else
                Main.player[projectile.owner].ChangeDir(1);


            Vector2 offset;
            offset.X = WaxHuman.gradiantCalc(30, 0, -10, -50, projectile.timeLeft);
            offset.Y = WaxHuman.gradiantCalc(30, 0, 60, 10, projectile.timeLeft);
            projectile.rotation = WaxHuman.gradiantCalc(0, 30, projectile.spriteDirection * .785f, projectile.spriteDirection * -.785f, projectile.timeLeft);
            projectile.Center = Main.player[projectile.owner].MountedCenter - offset;
        }
    }
}

