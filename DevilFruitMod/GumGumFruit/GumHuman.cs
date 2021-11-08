using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.GumGumFruit
{
    public class GumHuman : DevilFruitUser
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            //if: player has eaten Gum Gum Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 1 && player.HeldItem.type == ItemID.None && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                float magnitude = (float)(Math.Sqrt(clickX * clickX + clickY * clickY));
                float directionX = 10 * clickX / magnitude;
                float directionY = 10 * clickY / magnitude;

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    GumGumMisc(directionX, directionY);
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    GumGumPowers(directionX, directionY, 0);
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    GumGumPowers(directionX, directionY, 1);       
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    //if: No hands in use, Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                    if (DevilFruitMod.hands == 0)
                    {
                        gatlingPressed = true; //refer to PreUpdate()
                        timer = 0;
                        DevilFruitMod.hands += 2;
                    }
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustReleased)
                {
                    if (gatlingPressed == true)
                    {
                        gatlingPressed = false;
                        DevilFruitMod.hands -= 2;
                    }
                }
            }
        }
        public override void PreUpdate()
        {
            //GumGumGatling loop, see ProcessTriggers
            if (gatlingPressed)
            {
                if (timer == 0)
                {
                    if (player.HeldItem.type == 0 && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                    {
                        //Getting the shooting trajectory
                        float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                        float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                        float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                        float directionX = 10 * clickX / magnitude;
                        float directionY = 10 * clickY / magnitude;

                        GumGumPowers(directionX, directionY, 2);

                    }
                }
                timer++;
                if (timer > 10)
                {
                    timer = 0;
                }
            }

            //No fall damage if eaten Gum Gum Fruit
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 1)
            {
                player.noFallDmg = true;

                //"Boing" if fallen instead of damage
                if (!falling && ((player.gravDir == 1 && player.velocity.Y > 10) || (player.gravDir == -1 && player.velocity.Y < 10)))
                {
                    falling = true;
                }
                if (falling && ((player.gravDir == 1 && player.velocity.Y <= 0) || (player.gravDir == -1 && player.velocity.Y >= 0)))
                {
                    falling = false;
                    if ((player.gravDir == 1 && ((int)((player.position.Y) / 16) - player.fallStart) > (25 + player.extraFall)) || (player.gravDir == -1 && player.fallStart < -(25 + player.extraFall)))
                    {
                        CombatText.NewText(player.getRect(), Color.White, "Boing");
                    }
                }
            }
        }

        //Calls when hotkeys pressed
        //Spawns attack projectile depending on numAbility
        public void GumGumPowers(float directionX, float directionY, int numAbility)
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
                        damage = 15;
                        knockback = 4;
                        break;
                    //beaten one boss
                    case 1:
                        maxHands = 1;
                        damage = 30;
                        knockback = 5;
                        break;
                    //Post hardmode
                    case 2:
                        maxHands = 2;
                        damage = 40;
                        knockback = 7;
                        break;
                    //Post mechanical bosses
                    case 3:
                        maxHands = 2;
                        damage = 76;
                        knockback = 12;
                        break;
                }
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI)
                {
                    //still has hands available
                    if (DevilFruitMod.hands < maxHands && numAbility != 2)
                    {
                        if (numAbility == 0)
                            Projectile.NewProjectile(player.Center.X - 8, player.Center.Y, directionX, directionY, mod.ProjectileType("GumGumPistol"), damage, knockback, Main.myPlayer, 0f, 0f); //Spawning a projectile
                        if (numAbility == 1)
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, -directionX / 3, -directionY / 3, mod.ProjectileType("GumGumRifle"), 2 * damage, 2 * knockback, Main.myPlayer, 0f, 0f);

                        DevilFruitMod.hands++;
                    }
                    if (numAbility == 2)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, directionX, directionY, mod.ProjectileType("GumGumGatling"), damage, knockback, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }

        //Calls when mobility hotkey pressed
        //Spawns Gum Gum Rocket
        public void GumGumMisc(float directionX, float directionY)
        {
            maxHands = 2;
            if (DevilFruitMod.hands < maxHands)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 1.4f * directionX, 1.4f * directionY, mod.ProjectileType("GumGumHook"), 0, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                Main.PlaySound(SoundLoader.customSoundType, (int)player.position.X, (int)player.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/GumGumShoot"));
                DevilFruitMod.hands++;
            }
        }
    }
}
