using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.Audio;
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
        SoundStyle SlaveBubbleSoundStyle = new SoundStyle("Sounds/SlaveArrowBubble");
        SoundStyle SlaveKissSoundStyle = new SoundStyle("Sounds/SlaveArrowKiss");

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 120;
            clickPos = Main.MouseWorld;
            Projectile.scale = 3f;
        }

        public override void AI()
        {
            int time = 120 - Projectile.timeLeft;

            if (Projectile.ai[0] == 0) //Just shot
            {
                if (Projectile.ai[1] == 0) //Just shot
                {
                    initPos = Projectile.Center;
                    // Look at where the mouse is and change the player's direction to face it
                    // This is unnecisarry for custom weapons because there is already code to do that, but I'm spawning this boi manually
                    if (Main.mouseX - Main.screenWidth / 2 < 0)
                        Main.player[Projectile.owner].ChangeDir(-1);
                    else
                        Main.player[Projectile.owner].ChangeDir(1);

                    // Play the initial sound

                    Projectile.frame = 0;

                    damage = Projectile.damage;
                    Projectile.damage = 0;

                    SoundEngine.PlaySound(SlaveKissSoundStyle, Projectile.position);

                    // Only does this for 1 frame
                    Projectile.ai[1] = 1;
                }

                Projectile.Center = time*(clickPos - initPos) / 30 + initPos;

                if (time == 30)
                {
                    SoundEngine.PlaySound(SlaveBubbleSoundStyle, Projectile.position);
                    Projectile.ai[0] = 1;
                }
            }
            else if (Projectile.ai[0] == 1)
            {
                if (time % 3 == 0 && Projectile.frame != 4) Projectile.frame++;

                Vector2 locShift = new Vector2(clickPos.X - Main.MouseWorld.X, Main.MouseWorld.Y - clickPos.Y);
                if (locShift.X == 0 && locShift.Y == 0) locShift = new Vector2(1,0);
                locShift = Vector2.Normalize(locShift);
                //projectile.position.X = clickPos.X + locShift.Y;
                //projectile.position.Y = clickPos.Y + locShift.X;

                Projectile.rotation = (float)(Math.Atan2(locShift.X,locShift.Y) - 1.571);
                if (locShift.X < 0) Projectile.rotation -= 3.142f;

                if (time == 60)
                {
                    clickPos = locShift;
                    Projectile.ai[0] = 2;
                }
            }
            else if (Projectile.ai[0] == 2)
            {
                if (time%4 == 0) SoundEngine.PlaySound(SoundID.Item5, Projectile.position);
                Projectile.NewProjectile(null, Projectile.Center.X + Main.rand.Next(50) - 25, Projectile.Center.Y + Main.rand.Next(75) - 50, -20*clickPos.X, 20*clickPos.Y, Mod.Find<ModProjectile>("LoveArrow").Type, damage, 3, Main.myPlayer, 0f, 0f); //Spawning a projectile
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
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return true;
        }
    }
}

