using DevilFruitMod.Util;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
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
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 2 && Player.HeldItem.type == ItemID.None && !(Player.wet && !(Player.honeyWet || Player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                Vector2 dir = TMath.CalculateTrajectory();

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    LoveLoveMisc();
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    LoveLovePowers(dir.X, dir.Y, 0);    
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    LoveLovePowers(dir.X, dir.Y, 1);
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    LoveLovePowers(dir.X, dir.Y, 2);
                }
            }
        }

        public override void PreUpdate()
        {
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && Player.wet && !(Player.honeyWet || Player.lavaWet))
            {
                Player.AddBuff(ModContent.BuffType<Buffs.waterStun>(), 60, true);
            }
        }

        //Calls when hotkeys pressed
        //Spawns attack projectile depending on numAbility
        public void LoveLovePowers(float directionX, float directionY, int numAbility) //Currently working on
        {
            if (numAbility <= Player.GetModPlayer<DevilFruitUser>().fruitLevel)
            {
                //scaling damage to progress, change to increase damage,
                //knockback and number of hands for any given level
                switch (Player.GetModPlayer<DevilFruitUser>().fruitLevel)
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

                    Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX / 10, directionY / 10, Mod.Find<ModProjectile>("LoveLoveBeam").Type, 0, 0, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX / 10, directionY / 10, Mod.Find<ModProjectile>("LoveLoveBeam1").Type, 0, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                }
                else if (DevilFruitMod.hands < 2 && numAbility == 1)
                {
                    DevilFruitMod.hands++;
                    SoundEngine.PlaySound(SoundID.Item67, Player.position);
                    Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("PistolKiss").Type, damage, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                }
                else if (DevilFruitMod.hands < 1 && numAbility == 2)
                {
                    DevilFruitMod.hands = 2;

                    Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, 0, 0, Mod.Find<ModProjectile>("SlaveArrow").Type, damage / 2, 0, Main.myPlayer, 0f, 0f); //Spawning a projectile
                }
            }
        }

        public void LoveLoveMisc()
        {
            if (DevilFruitMod.hands <= 0)
            {
                DevilFruitMod.hands = 3;
                Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, 0, 0, Mod.Find<ModProjectile>("LoveBackground").Type, 0, 0, Main.myPlayer);
            }
        }
    }
}
