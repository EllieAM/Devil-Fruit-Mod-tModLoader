using DevilFruitMod.Util;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.GumGumFruit
{
    public class GumHuman : DevilFruitUser
    {
        SoundStyle GumShootSoundStyle = new SoundStyle("Sounds/GumGumShoot");
        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            //if: player has eaten Gum Gum Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 1 && Player.HeldItem.type == ItemID.None && !(Player.wet && !(Player.honeyWet || Player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                Vector2 dir = TMath.CalculateTrajectory();

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    GumGumMisc(dir.X, dir.Y);
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    GumGumPowers(dir.X, dir.Y, 0);
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    GumGumPowers(dir.X, dir.Y, 1);       
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
                    if (Player.HeldItem.type == ItemID.None && !(Player.wet && !(Player.honeyWet || Player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                    {
                        //Getting the shooting trajectory
                        Vector2 dir = TMath.CalculateTrajectory();

                        GumGumPowers(dir.X, dir.Y, 2);

                    }
                }
                timer++;
                if (timer > 10)
                {
                    timer = 0;
                }
            }

            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && Player.wet && !(Player.honeyWet || Player.lavaWet))
             {
                Player.AddBuff(ModContent.BuffType<Buffs.waterStun>(), 60, true);
             }
            //No fall damage if eaten Gum Gum Fruit
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 1)
            {
                Player.noFallDmg = true;

                //"Boing" if fallen instead of damage
                if (!falling && ((Player.gravDir == 1 && Player.velocity.Y > 10) || (Player.gravDir == -1 && Player.velocity.Y < 10)))
                {
                    falling = true;
                }
                if (falling && ((Player.gravDir == 1 && Player.velocity.Y <= 0) || (Player.gravDir == -1 && Player.velocity.Y >= 0)))
                {
                    falling = false;
                    if ((Player.gravDir == 1 && ((int)((Player.position.Y) / 16) - Player.fallStart) > (25 + Player.extraFall)) || (Player.gravDir == -1 && Player.fallStart < -(25 + Player.extraFall)))
                    {
                        CombatText.NewText(Player.getRect(), Color.White, "Boing");
                    }
                }
            }
        }

        //Calls when hotkeys pressed
        //Spawns attack projectile depending on numAbility
        public void GumGumPowers(float directionX, float directionY, int numAbility)
        {
            if (numAbility <= Player.GetModPlayer<DevilFruitUser>().fruitLevel)
            {
                //scaling damage to progress, change to increase damage,
                //knockback and number of hands for any given level
                switch (Player.GetModPlayer<DevilFruitUser>().fruitLevel)
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
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI)
                {
                    //still has hands available
                    if (DevilFruitMod.hands < maxHands && numAbility != 2)
                    {
                        if (numAbility == 0)
                            Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y, directionX, directionY, Mod.Find<ModProjectile>("GumGumPistol").Type, damage, knockback, Main.myPlayer, 0f, 0f); //Spawning a projectile
                        if (numAbility == 1)
                            Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, -directionX / 3, -directionY / 3, Mod.Find<ModProjectile>("GumGumRifle").Type, 2 * damage, 2 * knockback, Main.myPlayer, 0f, 0f);

                        DevilFruitMod.hands++;
                    }
                    if (numAbility == 2)
                    {
                        Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, directionX, directionY, Mod.Find<ModProjectile>("GumGumGatling").Type, damage, knockback, Main.myPlayer, 0f, 0f);
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
                Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, 1.4f * directionX, 1.4f * directionY, Mod.Find<ModProjectile>("GumGumHook").Type, 0, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                SoundEngine.PlaySound(GumShootSoundStyle, Player.position);
                DevilFruitMod.hands++;
            }
        }
    }
}
