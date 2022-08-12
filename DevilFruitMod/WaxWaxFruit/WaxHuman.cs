using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilFruitMod.WaxWaxFruit
{
    public class WaxHuman : DevilFruitUser
    {
        public bool arsenalPressed;
        public int roulette;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            //if: player has eaten Wax-Wax Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 5 && Player.HeldItem.type == ItemID.None && !(Player.wet && !(Player.honeyWet || Player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                float directionX = 10 * clickX / magnitude;
                float directionY = 10 * clickY / magnitude;

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    WaxWaxMisc();
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    roulette = 0;
                    timer = 0;
                    arsenalPressed = true;
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustReleased)
                {
                    arsenalPressed = false;
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    WaxWaxPowers(directionX, directionY, 1);
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    WaxWaxPowers(directionX, directionY, 2);
                }
            }
        }

        public override void PreUpdate()
        {
            if (arsenalPressed)
            {
                if (timer == 0)
                {
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    WaxWaxPowers(directionX, directionY, 0);
                }
                timer++;
            }
        }

        //Calls when hotkey is pressed
        //Spawns attack projectile depending on numAbility
        public void WaxWaxPowers(float directionX, float directionY, int numAbility)
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
                        damage = 10;
                        knockback = 10;
                        break;
                    //beaten one boss
                    case 1:
                        maxHands = 1;
                        damage = 0;
                        knockback = 0;
                        break;
                    //Post hardmode
                    case 2:
                        maxHands = 2;
                        damage = 0;
                        knockback = 0;
                        break;
                    //Post mechanical bosses
                    case 3:
                        maxHands = 2;
                        damage = 0;
                        knockback = 0;
                        break;
                }

                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI)
                {
                    //still has hands available
                    if (numAbility == 0)
                    {
                        switch (roulette)
                        {
                            case 0:
                                Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("NightsCandle").Type, damage, knockback, Main.myPlayer, 0f, 0f);
                                break;
                            case 1:
                                Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("CandleNaginata").Type, damage, knockback, Main.myPlayer, 0f, 3f);
                                break;
                            case 3:
                                Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("WaxAxe").Type, damage, knockback, Main.myPlayer, 0f, 3f);
                                break;
                            case 4:
                                Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("CandleTsunami").Type, damage, knockback, Main.myPlayer, 0f, 3f);
                                break;
                        }
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 1)
                    {
                        DevilFruitMod.hands ++;
                        //Projectile.NewProjectile(null, player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("WaxWaxAttack2"), damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 1 && numAbility == 2)
                    {
                        DevilFruitMod.hands += 2;
                        //Projectile.NewProjectile(null, player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("WaxWaxAttack3"), damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                }
            }
        }

        public void WaxWaxMisc()
        {

        }

        public static float gradiantCalc(int start, int end, float startVal, float endVal, int current)
        {
            return (startVal - endVal) * (current - end) / (start - end) + endVal;
        }
    }
}
