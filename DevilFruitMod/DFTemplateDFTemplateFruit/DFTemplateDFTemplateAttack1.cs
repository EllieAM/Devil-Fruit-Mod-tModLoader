using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace DevilFruitMod.DFTemplateDFTemplateFruit
{
    public class DFTemplateDFTemplateAttack1 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 0;
            projectile.height = 0;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.alpha = 128;
			projectile.timeLeft = 50;
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
            if (projectile.timeLeft == 50)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/DFTemplateDFTemplateSound"));
            }
		}

		public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands--;
		}

    }
}

