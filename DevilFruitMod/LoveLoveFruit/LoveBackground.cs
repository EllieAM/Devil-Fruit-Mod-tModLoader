using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.LoveLoveFruit
{
    class LoveBackground : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 416;
            projectile.height = 416;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.alpha = 0;
            projectile.scale = 1f;
            projectile.timeLeft = 30;
            drawHeldProjInFrontOfHeldItemAndArms = true;
        }

        public override void AI()
        {
            projectile.Center = Main.player[projectile.owner].Center;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !(Main.npc[i].aiStyle == 7 || Main.npc[i].aiStyle == 24) && projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
                {
                    //make thing happen on collision
                    Main.npc[i].AddBuff(BuffID.Lovestruck, 300);
                }
            }

            if (Main.rand.NextBool()) //about half the time
            {
                int randX = (int)projectile.Center.X + Main.rand.Next(-12, 13);
                int randY = (int)projectile.Center.Y + Main.rand.Next(-20, 18);
                Dust.NewDust(new Vector2(randX, randY), 15, 15, mod.DustType("LoveSparkle"));
            }

            // :P
            if (projectile.timeLeft == 30) Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 26, 1, 0f);
            if (projectile.timeLeft == 23) Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 26, 1, .333f);
            if (projectile.timeLeft == 16) Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 26, 1, .583f);
            if (projectile.timeLeft == 9) Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 26, 1, 1f);
        }

        public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands = 0;
        }

        public override bool CanDamage()
        {
            return false;
        }
    }
}
