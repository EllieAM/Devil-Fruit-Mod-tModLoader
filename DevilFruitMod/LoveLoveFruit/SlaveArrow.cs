using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

namespace DevilFruitMod.LoveLoveFruit
{
    public class SlaveArrow : ModProjectile
    {
        Vector2 clickPos;
        Vector2 initPos;
        int damage;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 120;
            clickPos = Main.MouseWorld;
            projectile.scale = 3f;
        }

        public override void AI()
        {
            int time = 120 - projectile.timeLeft;

            if (projectile.ai[0] == 0) //Just shot
            {
                if (projectile.ai[1] == 0) //Just shot
                {
                    initPos = projectile.Center;
                    // Look at where the mouse is and change the player's direction to face it
                    // This is unnecisarry for custom weapons because there is already code to do that, but I'm spawning this boi manually
                    if (Main.mouseX - Main.screenWidth / 2 < 0)
                        Main.player[projectile.owner].ChangeDir(-1);
                    else
                        Main.player[projectile.owner].ChangeDir(1);

                    // Play the initial sound
                    //Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumShoot"));

                    projectile.frame = 0;

                    damage = projectile.damage;
                    projectile.damage = 0;

                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/SlaveArrowKiss"));

                    // Only does this for 1 frame
                    projectile.ai[1] = 1;
                }

                projectile.Center = time*(clickPos - initPos) / 30 + initPos;

                if (time == 30)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/SlaveArrowBubble"));
                    projectile.ai[0] = 1;
                }
            }
            else if (projectile.ai[0] == 1)
            {
                if (time % 3 == 0 && projectile.frame != 4) projectile.frame++;

                Vector2 locShift = new Vector2(clickPos.X - Main.MouseWorld.X, Main.MouseWorld.Y - clickPos.Y);
                if (locShift.X == 0 && locShift.Y == 0) locShift = new Vector2(1,0);
                locShift = Vector2.Normalize(locShift);
                //projectile.position.X = clickPos.X + locShift.Y;
                //projectile.position.Y = clickPos.Y + locShift.X;

                projectile.rotation = (float)(Math.Atan2(locShift.X,locShift.Y) - 1.571);
                if (locShift.X < 0) projectile.rotation -= 3.142f;

                if (time == 60)
                {
                    clickPos = locShift;
                    projectile.ai[0] = 2;
                }
            }
            else if (projectile.ai[0] == 2)
            {
                if (time%4 == 0) Main.PlaySound(SoundID.Item5, projectile.position);
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(50) - 25, projectile.Center.Y + Main.rand.Next(75) - 50, -20*clickPos.X, 20*clickPos.Y, mod.ProjectileType("LoveArrow"), damage, 3, Main.myPlayer, 0f, 0f); //Spawning a projectile
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.AddBuff(ModContent.BuffType<Buffs.LoveStone>(), 300);
        }

        public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
        }
    }
}

