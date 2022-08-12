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
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 128;
            Projectile.timeLeft = 90;
			Projectile.penetrate = -1;
		}


		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.timeLeft = 1;
			return false;
		}


		public override void AI()
		{
			//Properly orients the projectile for bullet projectiles
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
		}


		public override void Kill(int timeLeft)
		{
			DevilFruitMod.hands -= 2;
		}
    }
}

