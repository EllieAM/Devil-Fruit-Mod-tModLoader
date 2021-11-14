using DevilFruitMod.Util;
using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;

namespace DevilFruitMod.DFTemplateDFTemplateFruit
{
    public class DFTemplateHuman : DevilFruitUser
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            /* UPON ADDING A NEW FRUIT:
             * 1. Change DFTemplateDFTemplate to the name of the Devil Fruit being added in this and other files in the folder
             * 2. Add moves
             * 3. If needed, update sounds in the sounds file by following the template named DFTemplateDFTemplateSound.cs
             * 4. Update merchant information (DFGlobalNPC.cs)
             * 5. Update Silvers Rayleigh information (Silvers.cs)
             * 6. Upon finishing and testing the fruit, update build.txt and description.txt before publishing
             * 
             * RESOURCES:
             * Example Mod: https://github.com/tModLoader/tModLoader/tree/master/ExampleMod
             * AI Types: https://tconfig.fandom.com/wiki/List_of_Projectile_AI_Styles
             * Audio: https://terraria.fandom.com/wiki/Category:Sound_effects?filefrom=Dd2+sky+dragons+fury+swing+0.wav#mw-category-media (Refer to the audio files labeled "Item #.wav")
             */

            //if: player has eaten DFTemplate-DFTemplate Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            //UPDATE eatenDevilFruit VALUE!!
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 99 && player.HeldItem.type == ItemID.None && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                Vector2 dir = TMath.CalculateTrajectory();

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    DFTemplateDFTemplateMisc();
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    DFTemplateDFTemplatePowers(dir.X, dir.Y, 0);
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    DFTemplateDFTemplatePowers(dir.X, dir.Y, 1);
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    DFTemplateDFTemplatePowers(dir.X, dir.Y, 2);
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
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && player.wet && !(player.honeyWet || player.lavaWet))
            {
                player.AddBuff(ModContent.BuffType<Buffs.waterStun>(), 60, true);
            }
        }

        //Calls when hotkey is pressed
        //Spawns attack projectile depending on numAbility
        public void DFTemplateDFTemplatePowers(float directionX, float directionY, int numAbility)
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
                        damage = 0;
                        knockback = 0;
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

                if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI)
                {
                    //still has hands available
                    if (DevilFruitMod.hands < 2 && numAbility == 0)
                    {
                        DevilFruitMod.hands++;
                        Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("DFTemplateDFTemplateAttack1"), damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 1)
                    {
                        DevilFruitMod.hands ++;
                        Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("DFTemplateDFTemplateAttack2"), damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 1 && numAbility == 2)
                    {
                        DevilFruitMod.hands += 2;
                        Projectile.NewProjectile(player.Center.X - 8, player.Center.Y - 10, directionX, directionY, mod.ProjectileType("DFTemplateDFTemplateAttack3"), damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                }
            }
        }

        public void DFTemplateDFTemplateMisc()
        {

        }
    }
}
