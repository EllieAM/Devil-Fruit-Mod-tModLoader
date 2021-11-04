using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.DataStructures;

namespace DevilFruitMod.BombBombFruit
{
    public class BombHuman : DevilFruitUser
	{
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (DevilFruitMod.MiscHotkey.JustPressed)
            {
                //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                if (player.HeldItem.type == 0 && (!Equals(DevilFruitMod.MiscHotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                {

                    //Getting the shooting trajectory
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)(Math.Sqrt((double)(clickX * clickX + clickY * clickY)));
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4)
                    {
                        BombBombMisc();
                    }
                }
            }

            if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
            {
                //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                if (player.HeldItem.type == 0 && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                {
                    //tester space


                    //Getting the shooting trajectory
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4)
                    {
                        BombBombPowers(directionX, directionY, 0);
                    }
                }
            }

            if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
            {
                //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                if (player.HeldItem.type == 0 && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse2") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                {
                    //Getting the shooting trajectory
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4)
                    {
                        BombBombPowers(directionX, directionY, 1);
                    }
                }
            }

            if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
            {
                if (player.HeldItem.type == 0 && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse3") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                {
                    //Getting the shooting trajectory
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4)
                    {
                        BombBombPowers(directionX, directionY, 2);
                    }
                }
            }
                /*
                if (DevilFruitMod.UsePowers3Hotkey.JustReleased)
                {
                    if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4)
                    {
                    
                    }
                }
                */
        }

        public override void PreUpdate()
        {
            
        }

        //Calls when hotkeys pressed
        //Spawns attack projectile depending on numAbility
        public void BombBombPowers(float directionX, float directionY, int numAbility)
        {
            if (numAbility <= player.GetModPlayer<DevilFruitUser>().fruitLevel)
            {
                //scaling damage to progress, change to increase damage,
                //knockback and number of hands for any given level
                switch (player.GetModPlayer<DevilFruitUser>().fruitLevel)
                {
                    //start of game
                    case 0:
                        maxHands = 1;
                        damage = 40;
                        knockback = 6;
                        break;
                    //beaten one boss
                    case 1:
                        maxHands = 2;
                        damage = 60;
                        knockback = 7;
                        break;
                    //Post hardmode
                    case 2:
                        maxHands = 3;
                        damage = 100;
                        knockback = 8;
                        break;
                    //Post mechanical bosses
                    case 3:
                        maxHands = 3;
                        damage = 150;
                        knockback = 12;
                        break;
                }

                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI)
                {
                    //still has hands available
                    if (DevilFruitMod.hands < 3 && numAbility == 0)
                    {
                        DevilFruitMod.hands++;
                        Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("BombBombBooger"), damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 1)
                    {
                        DevilFruitMod.hands+= 2;
                        Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX*2, directionY*2, mod.ProjectileType("NoseFancyCannon"), damage, knockback / 2, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 2)
                    {
                        DevilFruitMod.hands+= 2;
                        Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, 0, 0, mod.ProjectileType("BeegBomb"), damage * 3, knockback * 2, Main.myPlayer, 0f, 0f); //Spawning a projectile
                    }
                }
            }
        }

        public void BombBombMisc()
        {
            
        }
    }
}
