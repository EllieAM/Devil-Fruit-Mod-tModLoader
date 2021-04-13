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
    public class LoveLoveBeam : ModProjectile
    {
        Vector2 spawnLoc;

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 21;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.alpha = 128;
            projectile.scale = 1f;
            projectile.timeLeft = 180;
            projectile.hide = true;
        }

        public override void AI()
        {
            if (projectile.ai[0] == 0) //only happens once
            {
                // save position for recursive spawning
                spawnLoc = projectile.Center;

                if (Main.mouseX - Main.screenWidth / 2 < 0)
                    Main.player[projectile.owner].ChangeDir(-1);
                else
                    Main.player[projectile.owner].ChangeDir(1);

                Main.PlaySound(SoundID.Item44, projectile.position);

                projectile.scale = 0;

                projectile.ai[0] = 1;
            }

            if (projectile.ai[1] > 1 && projectile.timeLeft == 170)
            {
                Projectile.NewProjectile(spawnLoc, projectile.velocity, mod.ProjectileType("LoveLoveBeam"), 0, 0, Main.myPlayer, 0f, projectile.ai[1]-1); //Spawning a projectile
                Projectile.NewProjectile(spawnLoc, projectile.velocity, mod.ProjectileType("LoveLoveBeam1"), 0, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
            }

            Lighting.AddLight(projectile.Center, 2f, 1.5f, 1.5f);

            if (projectile.scale < 1) projectile.scale = (180f - projectile.timeLeft) / 60f;

            //Moves forward

            if (projectile.velocity.X < 0.0)
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 3.14f;
            else
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !(Main.npc[i].aiStyle == 7 || Main.npc[i].aiStyle == 24) && projectile.Hitbox.Intersects(Main.npc[i].Hitbox) && Main.npc[i].loveStruck)
                {
                    //make thing happen on collision
                    Main.PlaySound(SoundID.Dig, Main.npc[i].position);
                    Main.npc[i].AddBuff(ModContent.BuffType<Buffs.LoveStone>(), 300);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[1] == 1) DevilFruitMod.hands = 0;
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            // Add this projectile to the list of projectiles that will be drawn BEFORE tiles and NPC are drawn. This makes the projectile appear to be BEHIND the tiles and NPC.
            drawCacheProjsBehindNPCs.Add(index);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 75, 98, 128);
        }
    }
}

