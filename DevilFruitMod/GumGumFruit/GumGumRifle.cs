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
            this.initial = true;
            this.initial2 = true;
            this.hit = true;
            //projectile.ai[1] = 600;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                DevilFruitMod.hands = 0;
            }
            else
            {
                Main.player[Projectile.owner].itemAnimation = 5;
                Main.player[Projectile.owner].itemTime = 5;
                if (this.initial == true)
                {
                    initial = false;
                    damage = Projectile.damage;

                    playerLoc = new Vector2(Main.player[Projectile.owner].position.X, Main.player[Projectile.owner].position.Y);
                    initSpeed = new Vector2(Projectile.velocity.X,Projectile.velocity.Y);
                    Projectile.velocity.X = 0;
                    Projectile.velocity.Y = 0;

                    clickX = Main.mouseX - Main.screenWidth / 2;
                    clickY = Main.mouseY - Main.screenHeight / 2;
                    

                    if (Main.mouseX - Main.screenWidth / 2 < 0)
                        Main.player[Projectile.owner].ChangeDir(-1);
                    else
                        Main.player[Projectile.owner].ChangeDir(1);

                    Rectangle lowPlayer = new Rectangle(Main.player[Projectile.owner].getRect().X, Main.player[Projectile.owner].getRect().Y + 40, Main.player[Projectile.owner].getRect().Width, Main.player[Projectile.owner].getRect().Height);
                    CombatText.NewText(lowPlayer, Color.White, "Fwip");

                    
                }
                Vector2 location = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float distanceX = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - location.X - 8;
                float distanceY = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - location.Y;
                float magnitude = (float)Math.Sqrt(distanceX * (double)distanceX + distanceY * (double)distanceY);

                Vector2 click = new Vector2(clickX, clickY);
                click -= (Main.player[Projectile.owner].position - playerLoc);
                click.Normalize();

                if (Projectile.ai[0] == 0.0)
                {
                    Projectile.damage = 0;
                    Projectile.tileCollide = false;
                    isTwisted = false;
                    Projectile.rotation = (float)Math.Atan2(distanceY, distanceX) - 1.57f;

                    click *= Projectile.ai[1] * -3;

                    Projectile.position = Main.player[Projectile.owner].position + click;

                    Projectile.ai[1]++;

                    if (Projectile.ai[1] >= 60)
                    {
                        Projectile.ai[0] = 1.0f;
                    }
                    return;
                }
                else if (Projectile.ai[0] == 0.5)
                {
                    Projectile.ai[0] = 1.0f;
                }
                else if (Projectile.ai[0] == 1.0)
                {
                    if (isTwisted == false)
                    {
                        if (Projectile.ai[1] >= 70) {
                            Projectile.damage = damage;
                            Projectile.tileCollide = true;
                            isTwisted = true;
                            SoundEngine.PlaySound(GumShootSoundStyle, player.position);
                        }
                        Projectile.ai[1]++;
                    }

                    if (initial2 == true) {
                        Projectile.velocity.X = 20 * click.X;
                        Projectile.velocity.Y = 20 * click.Y;
                        initial2 = false;
                    }

                    Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;

                    if (magnitude > 500.0)
                    {
                        Projectile.ai[0] = 2f;
                        SoundEngine.PlaySound(GumRetractSoundStyle, Projectile.position);
                        hit = false;
                    }

                    if (Projectile.velocity.X < 0.0)
                        Projectile.spriteDirection = -1;
                    else
                        Projectile.spriteDirection = 1;
                    return;
                }
                else if (Projectile.ai[0] == 2.0)
                {
                    Projectile.tileCollide = false;
                    Projectile.rotation = (float)Math.Atan2(distanceY, distanceX) - 1.57f;
                    float retractSpeed = 20f;
                    if (magnitude < 50.0)
                    {
                        Projectile.Kill();
                        if (hit)
                            SoundEngine.PlaySound(GumSnapSoundStyle, Projectile.position);
                        DevilFruitMod.hands--;
                    }
                    float acceleration = retractSpeed / magnitude;
                    float accelerationX = distanceX * acceleration;
                    float accelerationY = distanceY * acceleration;
                    Projectile.velocity.X = accelerationX;
                    Projectile.velocity.Y = accelerationY;
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
            Projectile.ai[0] = 2;
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            //Main.NewText(width);
            width = 9;
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            int armR = Main.player[Projectile.owner].skinColor.R * lightColor.R / 255;
            int armG = Main.player[Projectile.owner].skinColor.G * lightColor.G / 255;
            int armB = Main.player[Projectile.owner].skinColor.B * lightColor.B / 255;
            return new Color(armR, armG, armB);
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D texture;
            if (!isTwisted)
                texture = ModContent.Request<Texture2D>("GumGumFruit/RifleTwist").Value;
            else
                texture = ModContent.Request<Texture2D>("GumGumFruit/RubberArm").Value;

            Vector2 position = Projectile.Center;
            Vector2 offset = new Vector2(8, 0);
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter - offset;
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
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition + offset, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}
