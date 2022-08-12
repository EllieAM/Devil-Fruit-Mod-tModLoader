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
    public class LoveLoveBeam : ModProjectile
    {
        Vector2 spawnLoc;

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 21;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.alpha = 128;
            Projectile.scale = 1f;
            Projectile.timeLeft = 180;
            Projectile.hide = true;
        }

        /*
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ID.GoreID.Sets.DrawBehind[Type] = index;
        }
        */


        public override void AI()
        {
            if (Projectile.ai[0] == 0) //only happens once
            {
                // save position for recursive spawning
                spawnLoc = Projectile.Center;

                if (Main.mouseX - Main.screenWidth / 2 < 0)
                    Main.player[Projectile.owner].ChangeDir(-1);
                else
                    Main.player[Projectile.owner].ChangeDir(1);

                SoundEngine.PlaySound(SoundID.Item44, Projectile.position);

                Projectile.scale = 0;

                Projectile.ai[0] = 1;
            }

            if (Projectile.ai[1] > 1 && Projectile.timeLeft == 170)
            {
                Projectile.NewProjectile(null, spawnLoc, Projectile.velocity, Mod.Find<ModProjectile>("LoveLoveBeam").Type, 0, 0, Main.myPlayer, 0f, Projectile.ai[1]-1); //Spawning a projectile
                Projectile.NewProjectile(null, spawnLoc, Projectile.velocity, Mod.Find<ModProjectile>("LoveLoveBeam1").Type, 0, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
            }

            Lighting.AddLight(Projectile.Center, 2f, 1.5f, 1.5f);

            if (Projectile.scale < 1) Projectile.scale = (180f - Projectile.timeLeft) / 60f;

            //Moves forward

            if (Projectile.velocity.X < 0.0)
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
            else
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !(Main.npc[i].aiStyle == 7 || Main.npc[i].aiStyle == 24) && Projectile.Hitbox.Intersects(Main.npc[i].Hitbox) && Main.npc[i].loveStruck)
                {
                    //make thing happen on collision
                    SoundEngine.PlaySound(SoundID.Dig, Main.npc[i].position);
                    Main.npc[i].AddBuff(ModContent.BuffType<Buffs.LoveStone>(), 300);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.ai[1] == 1) DevilFruitMod.hands = 0;
        }

        /*
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            // Add this projectile to the list of projectiles that will be drawn BEFORE tiles and NPC are drawn. This makes the projectile appear to be BEHIND the tiles and NPC.
            drawCacheProjsBehindNPCs.Add(index);
        }
        */

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 75, 98, 128);
        }
    }
}

