using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DevilFruitMod.Projectiles
{
    public class HakiPulse : ModProjectile
    {

        private int rippleCount = 1;
        private int rippleSize = 5;
        private int rippleSpeed = 5;
        private float distortStrength = 100f;

        private int silversIndex;
        private bool initial = true;

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 416;
            projectile.height = 416;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.scale = 1f;
            projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (initial)
            {
                if (!DevilFruitMod.npcShockwaveAvailable) projectile.Kill();
                DevilFruitMod.npcShockwaveAvailable = false;
                UpdateSilversIndex();
                initial = false;
            }
            Vector2 offset = new Vector2(0, 11);
            projectile.Center = Main.npc[silversIndex].Center - offset;
            Vector2 spawn = Main.npc[silversIndex].Center - offset;

            if (!Filters.Scene["Shockwave1"].IsActive())
            {
                Filters.Scene.Activate("Shockwave1", spawn).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(spawn);
            }

            float progress = (180f - projectile.timeLeft) / 60f;
            int size = (int)(24960 / 179 * progress) + 2;
            //projectile.width = size;
            //projectile.height = size;
            //projectile.scale = (float) size/416;
            Filters.Scene["Shockwave1"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !(Main.npc[i].aiStyle == 7||Main.npc[i].aiStyle == 24) && projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
                {
                    //make thing happen on collision
                    Main.npc[i].AddBuff(ModContent.BuffType<Buffs.hakiStun>(), 60); 
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            DevilFruitMod.npcShockwaveAvailable = true;
            Filters.Scene["Shockwave1"].Deactivate();
        }

        //Find silvers and updates silversIndex
        private void UpdateSilversIndex()
        {
            int silversType = mod.NPCType("Dark King");
            if (silversIndex >= 0 && Main.npc[silversIndex].active && Main.npc[silversIndex].type == silversType)
            {
                return;
            }

            silversIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == silversType)
                {
                    silversIndex = i;
                    break;
                }
            }
            return;
        }

    }
}

