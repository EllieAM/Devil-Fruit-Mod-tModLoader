using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace DevilFruitMod.DFTemplateDFTemplateFruit
{
    public class DFTemplateDFTemplateAttack2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 128;
            Projectile.timeLeft = 50;
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
			
		}

		public override void Kill(int timeLeft)
		{
			DevilFruitMod.hands--;
		}
    }
}

