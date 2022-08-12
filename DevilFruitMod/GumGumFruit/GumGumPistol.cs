using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.GumGumFruit
{
    public class GumGumPistol : ModProjectile
    {
        public bool initial;
        public bool hit;
        
        SoundStyle GumShootSoundStyle = new SoundStyle("Sounds/GumGumShoot");
        SoundStyle GumRetractSoundStyle = new SoundStyle("Sounds/GumGumRetract");
        SoundStyle GumSnapSoundStyle = new SoundStyle("Sounds/GumGumSnap");

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 1;
            Projectile.scale = 2.0f / 3.0f;
            initial = true;
            hit = true;
        }

        // PreDraw is scawy
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("GumGumFruit/RubberArm").Value;

            Vector2 position = Projectile.Center;
            Vector2 offset = new Vector2(8,0);
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter - offset;
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
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition + offset, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Delete the projectile if the player dies, only important because the projectile is tied to player data
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
            }
            else
            {
                Main.player[Projectile.owner].itemAnimation = 5;
                Main.player[Projectile.owner].itemTime = 5;
                // Only happens the first time this code executes
                if (initial == true)
                {
                    // Look at where the mouse is and change the player's direction to face it
                    // This is unnecisarry for custom weapons because there is already code to do that, but I'm spawning this boi manually
                    if (Main.mouseX - Main.screenWidth / 2 < 0)
                        Main.player[Projectile.owner].ChangeDir(-1);
                    else
                        Main.player[Projectile.owner].ChangeDir(1);
                    initial = false;
                    
                    // Make a 'fwip' above the player
                    Rectangle lowPlayer = new Rectangle(Main.player[Projectile.owner].getRect().X, Main.player[Projectile.owner].getRect().Y + 40, Main.player[Projectile.owner].getRect().Width, Main.player[Projectile.owner].getRect().Height);
                    CombatText.NewText(lowPlayer, Color.White, "Fwip");

                    // Play the fwip sound
                    SoundEngine.PlaySound(GumShootSoundStyle, player.position);
                }

                // determines projectile behavior by it's distance from the player, all that data is grabbed here
                Vector2 location = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float distanceX = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - location.X - 8;
                float distanceY = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - location.Y;
                float magnitude = (float)Math.Sqrt(distanceX * (double)distanceX + distanceY * (double)distanceY);

                // ai[0] == 0 is the first mode, fist extends until it either hits something or gets too far from player, then changes mode
                if (Projectile.ai[0] == 0.0)
                {
                    // fist gets too far, so it changes mode, saves the flag that it didn't hit anything, and plays the retract sound
                    if (magnitude > 500.0)
                    {
                        Projectile.ai[0] = 1f;
                        SoundEngine.PlaySound(GumRetractSoundStyle, Projectile.position);
                        hit = false;
                    }

                    // Uses fist's velocity to determine how it's rotated, calculated in radians
                    Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                    if (Projectile.velocity.X < 0.0)
                        Projectile.spriteDirection = -1;
                    else
                        Projectile.spriteDirection = 1;
                     return;
                }
                else // ai[0] == 1 is the second mode, where it comes back to the player and despawns
                {
                    // goes through all the walls
                    Projectile.tileCollide = false;
                    // Make knuckles face away from player
                    Projectile.rotation = (float)Math.Atan2(distanceY, distanceX) - 1.57f;
                    // get closer to player by ~20px per frame
                    float retractSpeed = 20f;
                    if (magnitude < 50.0) // Kill the projectile if it's too close
                    {
                        Projectile.Kill();
                        // Play the snap sound if the retract sound was played. (Only when it doesn't hit anything do these sound fx sound good)
                        if (hit)
                            SoundEngine.PlaySound(GumSnapSoundStyle, Projectile.position);
                        DevilFruitMod.hands--;
                    }
                    float num5 = retractSpeed / magnitude;
                    float num6 = distanceX * num5;
                    float num7 = distanceY * num5;
                    Projectile.velocity.X = num6;
                    Projectile.velocity.Y = num7;
                    return;
                }
            }
        }

        // This one's pretty self explanatory, it's where the mode actually gets changed
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.ai[0] = 1;
        }

        // Same as the last one, except I also play the vanilla thunky sound when projectiles hit walls
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] = 1;
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        // This was so I can tweak the hitbox so the fist interacted with walls more cleanly
        // No one has complained about the hitbox of the fist yet so I think this is probably good.
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 9;
            return true;
        }

        // Make the fist skin colored.  This is just a tinting feature for light and stuff but I'm using it in a hacky way.
        // I wanted to use the same method for making enemies 'stone colored' when they get hit by Mero Mero but uhh... it didn't work.
        public override Color? GetAlpha(Color lightColor)
        {
            int armR = Main.player[Projectile.owner].skinColor.R * lightColor.R / 255;
            int armG = Main.player[Projectile.owner].skinColor.G * lightColor.G / 255;
            int armB = Main.player[Projectile.owner].skinColor.B * lightColor.B / 255;
            return new Color(armR, armG, armB);
        }
    }
}

