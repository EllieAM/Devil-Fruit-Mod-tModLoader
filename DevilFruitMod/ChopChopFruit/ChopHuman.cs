using System;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;


namespace DevilFruitMod.ChopChopFruit
{
    public class ChopHuman : DevilFruitUser
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            /* UPON ADDING A NEW FRUIT:
             * 1. Change ChopChop to the name of the Devil Fruit being added in this and other files in the folder
             * 2. Add moves
             * 3. If needed, update sounds in the sounds file by following the template named ChopChopSound.cs
             * 4. Update merchant information (DFGlobalNPC.cs)
             * 5. Update Silvers Rayleigh information (Silvers.cs)
             * 6. Upon finishing and testing the fruit, update build.txt and description.txt before publishing
             * 
             * RESOURCES:
             * Example Mod: https://github.com/tModLoader/tModLoader/tree/master/ExampleMod
             * AI Types: https://tconfig.fandom.com/wiki/List_of_Projectile_AI_Styles
             * Audio: https://terraria.fandom.com/wiki/Category:Sound_effects?filefrom=Dd2+sky+dragons+fury+swing+0.wav#mw-category-media (Refer to the audio files labeled "Item #.wav")
             */

            //if: player has eaten Chop-Chop Fruit and...
            //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
            //UPDATE eatenDevilFruit VALUE!!
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 6 && Player.HeldItem.type == ItemID.None && !(Player.wet && !(Player.honeyWet || Player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
            {
                //Getting the shooting trajectory
                float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                float directionX = 10 * clickX / magnitude;
                float directionY = 10 * clickY / magnitude;

                if (DevilFruitMod.MiscHotkey.JustPressed)
                {
                    ChopChopMisc();
                }

                if (DevilFruitMod.UsePowers1Hotkey.JustPressed)
                {
                    ChopChopPowers(directionX, directionY, 0);
                }

                if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
                {
                    ChopChopPowers(directionX, directionY, 1);
                }

                if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
                {
                    ChopChopPowers(directionX, directionY, 2);
                }

                /*
                if (DevilFruitMod.UsePowers3Hotkey.JustReleased)
                {

                }
                */
            }
        }

        //Calls when hotkey is pressed
        //Spawns attack projectile depending on numAbility
        public void ChopChopPowers(float directionX, float directionY, int numAbility)
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
                        damage = 30;
                        knockback = 10;
                        break;
                    //beaten one boss
                    case 1:
                        maxHands = 1;
                        damage = 50;
                        knockback = 10;
                        break;
                    //Post hardmode
                    case 2:
                        maxHands = 2;
                        damage = 80;
                        knockback = 10;
                        break;
                    //Post mechanical bosses
                    case 3:
                        maxHands = 2;
                        damage = 120;
                        knockback = 10;
                        break;
                }

                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI)
                {
                    //still has hands available
                    if (DevilFruitMod.hands < 2 && numAbility == 0)
                    {
                        DevilFruitMod.hands++;
                        Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("ChopChopCannon").Type, damage, knockback, Main.myPlayer, 0f, 0f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 2 && numAbility == 1)
                    {
                        //DevilFruitMod.hands++;
                        Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("ChopChopAttack2").Type, damage, knockback, Main.myPlayer, 0f, 0f); //Spawning a projectile
                    }
                    else if (DevilFruitMod.hands < 1 && numAbility == 2)
                    {
                        DevilFruitMod.hands += 2;
                        Projectile.NewProjectile(null, Player.Center.X - 8, Player.Center.Y - 10, directionX, directionY, Mod.Find<ModProjectile>("ChopChopAttack3").Type, damage, knockback, Main.myPlayer, 0f, 3f); //Spawning a projectile
                    }
                }
            }
        }

        public void ChopChopMisc()
        {

        }
    }
}
