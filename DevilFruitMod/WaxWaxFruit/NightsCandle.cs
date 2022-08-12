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
            Projectile.width = 44;
            Projectile.height = 48;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
		}

        public override void AI()
		{
            if (Projectile.velocity.X < 0)
            {
                Main.player[Projectile.owner].ChangeDir(-1);
                Projectile.spriteDirection = -1;
            }
            else
                Main.player[Projectile.owner].ChangeDir(1);


            Vector2 offset;
            offset.X = WaxHuman.gradiantCalc(30, 0, -10, -50, Projectile.timeLeft);
            offset.Y = WaxHuman.gradiantCalc(30, 0, 60, 10, Projectile.timeLeft);
            Projectile.rotation = WaxHuman.gradiantCalc(0, 30, Projectile.spriteDirection * .785f, Projectile.spriteDirection * -.785f, Projectile.timeLeft);
            Projectile.Center = Main.player[Projectile.owner].MountedCenter - offset;
        }
    }
}

