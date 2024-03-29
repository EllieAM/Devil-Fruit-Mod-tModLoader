﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DevilFruitMod.GumGumFruit
{
    public class GumGumHook : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 7;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 10;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("GumGumFruit/RubberArm");

            Vector2 position = projectile.Center;
            Vector2 offset = new Vector2(0, 0);
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

        public override Color? GetAlpha(Color lightColor)
        {
            int armR = Main.player[projectile.owner].skinColor.R * lightColor.R / 255;
            int armG = Main.player[projectile.owner].skinColor.G * lightColor.G / 255;
            int armB = Main.player[projectile.owner].skinColor.B * lightColor.B / 255;
            return new Color(armR, armG, armB);
        }

        public override float GrappleRange()
        {
            //Main.NewText("test GrapplRrange");
            return 500f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }

        // default is 11, Lunar is 24
        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            //Main.NewText("test GrappleRetreatSpeed");
            speed = 20f;
        }

        public override void GrapplePullSpeed(Player player, ref float speed)
        {
            //Main.NewText("test GrapplePullSpeed");
            speed = 10;
        }

        public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands--;
        }
    }
}