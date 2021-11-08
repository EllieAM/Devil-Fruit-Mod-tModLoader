using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.LoveLoveFruit
{
    public class LoveHuman : DevilFruitUser
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            //if: player has eaten Love Love Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 2 && player.HeldItem.type == ItemID.None && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                float magnitude = (float)(Math.Sqrt(clickX * clickX + clickY * clickY));
                float directionX = 10 * clickX / magnitude;
                float directionY = 10 * clickY / magnitude;

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    LoveLoveMisc();
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    LoveLovePowers(directionX, directionY, 0);    
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    LoveLovePowers(directionX, directionY, 1);
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    LoveLovePowers(clickX, clickY, 2);
                }
            }
        }

        //Calls when hotkeys pressed
        //Spawns attack projectile depending on numAbility
        public void LoveLovePowers(float directionX, float directionY, int numAbility) //Currently working on
        {
            if (numAbility <= player.GetModPlayer<DevilFruitUser>().fruitLevel)
            {
                //scaling damage to progress, change to increase damage,
                //knockback and number of hands for any given level
                switch (player.GetModPlayer<DevilFruitUser>().fruitLevel)
                {
                    //start of game
                    case 0:
                        break;
                    //beaten one boss
                    case 1:
                        damage = 30;
                        break;
                    //Post hardmode
                    case 2:
                        damage = 40;
                        break;
                    //Post mechanical bosses
                    case 3:
                        damage = 76;
                        break;
                }

                //still has hands available
                if (DevilFruitMod.hands < 1 && numAbility == 0)
                {
                    DevilFruitMod.hands = 2;

                    Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX / 10, directionY / 10, mod.ProjectileType("LoveLoveBeam"), 0, 0, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX / 10, directionY / 10, mod.ProjectileType("LoveLoveBeam1"), 0, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                }
                else if (DevilFruitMod.hands < 2 && numAbility == 1)
                {
                    DevilFruitMod.hands++;
                    Main.PlaySound(SoundID.Item67, player.position);
                    Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("PistolKiss"), damage, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                }
                else if (DevilFruitMod.hands < 1 && numAbility == 2)
                {
                    DevilFruitMod.hands = 2;

                    Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, 0, 0, mod.ProjectileType("SlaveArrow"), damage / 2, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                }
            }
        }

        public void LoveLoveMisc()
        {
            if (DevilFruitMod.hands <= 0)
            {
                DevilFruitMod.hands = 3;
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("LoveBackground"), 0, 0, Main.myPlayer);
            }
        }
    }
}
