using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DevilFruitMod.GumGumFruit
{
    public class GumGumPistol : ModProjectile
    {
        public bool initial;
        public bool hit;

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 0;
            projectile.melee = true;
            projectile.extraUpdates = 1;
            projectile.scale = 2.0f / 3.0f;
            initial = true;
            hit = true;
        }

        // PreDraw is scawy
        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("GumGumFruit/RubberArm");

            Vector2 position = projectile.Center;
            Vector2 offset = new Vector2(8,0);
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter - offset;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if (vector2_4.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition + offset, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }

        public override void AI()
        {
            // Delete the projectile if the player dies, only important because the projectile is tied to player data
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
            }
            else
            {
                Main.player[projectile.owner].itemAnimation = 5;
                Main.player[projectile.owner].itemTime = 5;
                // Only happens the first time this code executes
                if (initial == true)
                {
                    // Look at where the mouse is and change the player's direction to face it
                    // This is unnecisarry for custom weapons because there is already code to do that, but I'm spawning this boi manually
                    if (Main.mouseX - Main.screenWidth / 2 < 0)
                        Main.player[projectile.owner].ChangeDir(-1);
                    else
                        Main.player[projectile.owner].ChangeDir(1);
                    initial = false;
                    
                    // Make a 'fwip' above the player
                    Rectangle lowPlayer = new Rectangle(Main.player[projectile.owner].getRect().X, Main.player[projectile.owner].getRect().Y + 40, Main.player[projectile.owner].getRect().Width, Main.player[projectile.owner].getRect().Height);
                    CombatText.NewText(lowPlayer, Color.White, "Fwip");

                    // Play the fwip sound
                    Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumShoot"));
                }

                // determines projectile behavior by it's distance from the player, all that data is grabbed here
                Vector2 location = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float distanceX = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - location.X - 8;
                float distanceY = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - location.Y;
                float magnitude = (float)Math.Sqrt(distanceX * (double)distanceX + distanceY * (double)distanceY);

                // ai[0] == 0 is the first mode, fist extends until it either hits something or gets too far from player, then changes mode
                if (projectile.ai[0] == 0.0)
                {
                    // fist gets too far, so it changes mode, saves the flag that it didn't hit anything, and plays the retract sound
                    if (magnitude > 500.0)
                    {
                        projectile.ai[0] = 1f;
                        Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumRetract"));
                        hit = false;
                    }

                    // Uses fist's velocity to determine how it's rotated, calculated in radians
                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                    if (projectile.velocity.X < 0.0)
                        projectile.spriteDirection = -1;
                    else
                        projectile.spriteDirection = 1;
                     return;
                }
                else // ai[0] == 1 is the second mode, where it comes back to the player and despawns
                {
                    // goes through all the walls
                    projectile.tileCollide = false;
                    // Make knuckles face away from player
                    projectile.rotation = (float)Math.Atan2(distanceY, distanceX) - 1.57f;
                    // get closer to player by ~20px per frame
                    float retractSpeed = 20f;
                    if (magnitude < 50.0) // Kill the projectile if it's too close
                    {
                        projectile.Kill();
                        // Play the snap sound if the retract sound was played. (Only when it doesn't hit anything do these sound fx sound good)
                        if (hit)
                            Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumSnap"));
                        DevilFruitMod.hands--;
                    }
                    float num5 = retractSpeed / magnitude;
                    float num6 = distanceX * num5;
                    float num7 = distanceY * num5;
                    projectile.velocity.X = num6;
                    projectile.velocity.Y = num7;
                    return;
                }
            }
        }

        // This one's pretty self explanatory, it's where the mode actually gets changed
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = 1;
        }

        // Same as the last one, except I also play the vanilla thunky sound when projectiles hit walls
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1;
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            return false;
        }

        // This was so I can tweak the hitbox so the fist interacted with walls more cleanly
        // No one has complained about the hitbox of the fist yet so I think this is probably good.
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 9;
            return true;
        }

        // Make the fist skin colored.  This is just a tinting feature for light and stuff but I'm using it in a hacky way.
        // I wanted to use the same method for making enemies 'stone colored' when they get hit by Mero Mero but uhh... it didn't work.
        public override Color? GetAlpha(Color lightColor)
        {
            int armR = Main.player[projectile.owner].skinColor.R * lightColor.R / 255;
            int armG = Main.player[projectile.owner].skinColor.G * lightColor.G / 255;
            int armB = Main.player[projectile.owner].skinColor.B * lightColor.B / 255;
            return new Color(armR, armG, armB);
        }
    }
}

