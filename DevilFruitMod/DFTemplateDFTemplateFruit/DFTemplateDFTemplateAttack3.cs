using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DevilFruitMod.DFTemplateDFTemplateFruit
{
    public class DFTemplateDFTemplateAttack3 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.alpha = 128;
            projectile.timeLeft = 90;
			projectile.penetrate = -1;
		}


		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.timeLeft = 1;
			return false;
		}


		public override void AI()
		{
			//Properly orients the projectile for bullet projectiles
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
		}


		public override void Kill(int timeLeft)
		{
			DevilFruitMod.hands -= 2;
		}
    }
}

