using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.LoveLoveFruit
{
    class LoveBackground : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 416;
            Projectile.height = 416;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
            Projectile.scale = 1f;
            Projectile.timeLeft = 30;
            DrawHeldProjInFrontOfHeldItemAndArms = true;
        }

        public override void AI()
        {
            Projectile.Center = Main.player[Projectile.owner].Center;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !(Main.npc[i].aiStyle == 7 || Main.npc[i].aiStyle == 24) && Projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
                {
                    //make thing happen on collision
                    Main.npc[i].AddBuff(BuffID.Lovestruck, 300);
                }
            }

            if (Main.rand.NextBool()) //about half the time
            {
                int randX = (int)Projectile.Center.X + Main.rand.Next(-12, 13);
                int randY = (int)Projectile.Center.Y + Main.rand.Next(-20, 18);
                Dust.NewDust(new Vector2(randX, randY), 15, 15, Mod.Find<ModDust>("LoveSparkle").Type);
            }

            // :P
            if (Projectile.timeLeft == 30) SoundEngine.PlaySound(SoundID.Item26, Projectile.position);
            if (Projectile.timeLeft == 23) SoundEngine.PlaySound(SoundID.Item26.WithPitchOffset(.333f), Projectile.position);
            if (Projectile.timeLeft == 16) SoundEngine.PlaySound(SoundID.Item26.WithPitchOffset(.583f), Projectile.position);
            if (Projectile.timeLeft == 9) SoundEngine.PlaySound(SoundID.Item26.WithPitchOffset(1f), Projectile.position);
        }

        public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands = 0;
        }

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return false;
        }
    }
}
