using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using DevilFruitMod.Util;
using Microsoft.Xna.Framework;

namespace DevilFruitMod.BombBombFruit
{
    public class BombHuman : DevilFruitUser
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {

            //if: player has eaten Bomb Bomb Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4 && Player.HeldItem.type == ItemID.None && !(Player.wet && !(Player.honeyWet || Player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                Vector2 dir = TMath.CalculateTrajectory();

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    BombBombMisc();
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    BombBombPowers(dir.X, dir.Y, 0);
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    BombBombPowers(dir.X, dir.Y, 1);
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    BombBombPowers(dir.X, dir.Y, 2);
                }

                /*
                if (DevilFruitMod.UsePowers3Hotkey.JustReleased)
                {

                }
                */
            }
        }

        public override void PreUpdate()
        {
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && Player.wet && !(Player.honeyWet || Player.lavaWet))
            {
                Player.AddBuff(ModContent.BuffType<Buffs.waterStun>(), 60, true);
            }
        }

        //Calls when hotkey is pressed
        //Spawns attack projectile depending on numAbility
        public void BombBombPowers(float directionX, float directionY, int numAbility)
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

                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI)
                {
                    //still has hands available
                    if (DevilFruitMod.hands < 3 && numAbility == 0)
                    {
                        DevilFruitMod.hands++;
                        Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("BombBombBooger").Type, damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 1)
                    {
                        DevilFruitMod.hands += 2;
                        Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX * 2, directionY * 2, Mod.Find<ModProjectile>("NoseFancyCannon").Type, damage, knockback / 2, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 2)
                    {
                        DevilFruitMod.hands += 2;
                        Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, 0, 0, Mod.Find<ModProjectile>("BeegBomb").Type, damage * 3, knockback * 2, Main.myPlayer, 0f, 0f); //Spawning a projectile
                    }
                }
            }
        }

        public void BombBombMisc()
        {

        }
    }
}
