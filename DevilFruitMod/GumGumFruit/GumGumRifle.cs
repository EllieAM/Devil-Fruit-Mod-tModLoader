using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DevilFruitMod.GumGumFruit
{
    public class GumGumRifle : ModProjectile
    {
        public bool initial;
        public bool initial2;
        public bool hit;
        public Vector2 initSpeed;
        public Vector2 playerLoc;
        public int clickX;
        public int clickY;
        public bool isTwisted;
        public int damage;

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
            this.initial = true;
            this.initial2 = true;
            this.hit = true;
            //projectile.ai[1] = 600;
        }

        public override void AI()
        {
            
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                DevilFruitMod.hands = 0;
            }
            else
            {
                Main.player[projectile.owner].itemAnimation = 5;
                Main.player[projectile.owner].itemTime = 5;
                if (this.initial == true)
                {
                    initial = false;
                    damage = projectile.damage;

                    playerLoc = new Vector2(Main.player[projectile.owner].position.X, Main.player[projectile.owner].position.Y);
                    initSpeed = new Vector2(projectile.velocity.X,projectile.velocity.Y);
                    projectile.velocity.X = 0;
                    projectile.velocity.Y = 0;

                    clickX = Main.mouseX - Main.screenWidth / 2;
                    clickY = Main.mouseY - Main.screenHeight / 2;
                    

                    if (Main.mouseX - Main.screenWidth / 2 < 0)
                        Main.player[projectile.owner].ChangeDir(-1);
                    else
                        Main.player[projectile.owner].ChangeDir(1);

                    Rectangle lowPlayer = new Rectangle(Main.player[projectile.owner].getRect().X, Main.player[projectile.owner].getRect().Y + 40, Main.player[projectile.owner].getRect().Width, Main.player[projectile.owner].getRect().Height);
                    CombatText.NewText(lowPlayer, Color.White, "Fwip");

                    
                }
                Vector2 location = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float distanceX = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - location.X - 8;
                float distanceY = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - location.Y;
                float magnitude = (float)Math.Sqrt(distanceX * (double)distanceX + distanceY * (double)distanceY);

                Vector2 click = new Vector2(clickX, clickY);
                click -= (Main.player[projectile.owner].position - playerLoc);
                click.Normalize();

                if (projectile.ai[0] == 0.0)
                {
                    projectile.damage = 0;
                    projectile.tileCollide = false;
                    isTwisted = false;
                    projectile.rotation = (float)Math.Atan2(distanceY, distanceX) - 1.57f;

                    click *= projectile.ai[1] * -3;

                    projectile.position = Main.player[projectile.owner].position + click;

                    projectile.ai[1]++;

                    if (projectile.ai[1] >= 60)
                    {
                        projectile.ai[0] = 1.0f;
                    }
                    return;
                }
                else if (projectile.ai[0] == 0.5)
                {
                    projectile.ai[0] = 1.0f;
                }
                else if (projectile.ai[0] == 1.0)
                {
                    if (isTwisted == false)
                    {
                        if (projectile.ai[1] >= 70) {
                            projectile.damage = damage;
                            projectile.tileCollide = true;
                            isTwisted = true;
                            Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumShoot"));
                        }
                        projectile.ai[1]++;
                    }

                    if (initial2 == true) {
                        projectile.velocity.X = 20 * click.X;
                        projectile.velocity.Y = 20 * click.Y;
                        initial2 = false;
                    }

                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;

                    if (magnitude > 500.0)
                    {
                        projectile.ai[0] = 2f;
                        Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumRetract"));
                        hit = false;
                    }

                    if (projectile.velocity.X < 0.0)
                        projectile.spriteDirection = -1;
                    else
                        projectile.spriteDirection = 1;
                    return;
                }
                else if (projectile.ai[0] == 2.0)
                {
                    projectile.tileCollide = false;
                    projectile.rotation = (float)Math.Atan2(distanceY, distanceX) - 1.57f;
                    float retractSpeed = 20f;
                    if (magnitude < 50.0)
                    {
                        projectile.Kill();
                        if (hit)
                            Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumSnap"));
                        DevilFruitMod.hands--;
                    }
                    float acceleration = retractSpeed / magnitude;
                    float accelerationX = distanceX * acceleration;
                    float accelerationY = distanceY * acceleration;
                    projectile.velocity.X = accelerationX;
                    projectile.velocity.Y = accelerationY;
                    return;
                }
            }
        }

        /*public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = 3;
        }*/

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 2;
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            //Main.NewText(width);
            width = 9;
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            int armR = Main.player[projectile.owner].skinColor.R * lightColor.R / 255;
            int armG = Main.player[projectile.owner].skinColor.G * lightColor.G / 255;
            int armB = Main.player[projectile.owner].skinColor.B * lightColor.B / 255;
            return new Color(armR, armG, armB);
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {

            Texture2D texture;
            if (!isTwisted)
                texture = mod.GetTexture("GumGumFruit/RifleTwist");
            else
                texture = mod.GetTexture("GumGumFruit/RubberArm");

            Vector2 position = projectile.Center;
            Vector2 offset = new Vector2(8, 0);
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter - offset;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition + offset, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}
