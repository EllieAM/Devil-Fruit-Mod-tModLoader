using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework;

namespace DevilFruitMod
{
    public class DevilFruitUser : ModPlayer
	{
        public int eatenDevilFruit = 0;
        public int fruitLevel = 0;
        public int maxHands;
        public int damage;
        public int knockback;
        public bool gatlingPressed;
        public int timer;
        public bool falling = false;

        public override void ResetEffects()
		{
		}

		public override void clientClone(ModPlayer clientClone)
		{
			DevilFruitUser clone = clientClone as DevilFruitUser;
		}

		/*public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)ExampleModMessageType.ExampleLifeFruits);
			packet.Write((byte)player.whoAmI);
			packet.Write(eatenDevilFruit);
			packet.Send(toWho, fromWho);
		}*/

		public override TagCompound Save()
		{
			return new TagCompound {
                {"eatenDevilFruit", eatenDevilFruit},
                {"fruitLevel", fruitLevel},
			};
		}

		public override void Load(TagCompound tag)
		{
            eatenDevilFruit = tag.GetInt("eatenDevilFruit");
            fruitLevel = tag.GetInt("fruitLevel");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
		}

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (DevilFruitMod.MobilityHotkey.JustPressed)
            {
                //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                if (player.HeldItem.type == 0 && (!Equals(DevilFruitMod.MobilityHotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                {

                    //Getting the shooting trajectory
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)(Math.Sqrt((double)(clickX * clickX + clickY * clickY)));
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    switch (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit)
                    {
                        case 1:
                            GumGumMobility(directionX, directionY);
                            break;
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

                    switch (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit)
                    {
                        case 1:
                            GumGumPowers(directionX, directionY, 0);
                            break;
                    }
                }
            }
            if (DevilFruitMod.UsePowers2Hotkey.JustPressed)
            {
                //if: Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                if (player.HeldItem.type == 0 && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
                {
                    //Getting the shooting trajectory
                    float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
                    float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
                    float magnitude = (float)Math.Sqrt(clickX * clickX + clickY * clickY);
                    float directionX = 10 * clickX / magnitude;
                    float directionY = 10 * clickY / magnitude;

                    switch (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit)
                    {
                        case 1:
                            GumGumPowers(directionX, directionY, 1);
                            break;
                    }
                }
            }

            if (DevilFruitMod.UsePowers3Hotkey.JustPressed)
            {
                //if: No hands in use, Empty hand, eaten fruit, not in water, and (using mouse -> can use mouse)
                if (DevilFruitMod.hands == 0 && player.HeldItem.type == 0 && !(player.wet && !(player.honeyWet || player.lavaWet)) && (!Equals(DevilFruitMod.UsePowers1Hotkey.GetAssignedKeys(InputMode.Keyboard)[0], "Mouse1") || (Main.hasFocus && !Main.LocalPlayer.mouseInterface && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && !Main.blockInput && !Main.mapFullscreen && !Main.HoveringOverAnNPC && Main.LocalPlayer.talkNPC == -1)))
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

        public override void OnEnterWorld(Player player)
        {
            DevilFruitMod.hands = 0;
            DevilFruitMod.hooks = 0;
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

                        switch (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit)
                        {
                            case 1:
                                GumGumPowers(directionX, directionY, 2);
                                break;
                            case 2:
                                //IceIcePowers(directionX, directionY, 2);
                                break;
                        }
                    }
                }
                timer++;
                if (timer > 10)
                    timer = 0;
            }

            //Applys debuff when touching water
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && player.wet && !(player.honeyWet || player.lavaWet))
            {
                player.AddBuff(ModContent.BuffType<Buffs.waterStun>(), 60, true);
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

        //Debuff effects
        public override void SetControls()
        {
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && player.wet && !(player.honeyWet || player.lavaWet))
            {
                player.controlJump = false;
                player.controlDown = false;
                player.controlLeft = false;
                player.controlRight = false;
                player.controlUp = false;
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

        //Calls when mobility hotkey pressed
        //Spawns Gum Gum Rocket
        public void GumGumMobility(float directionX, float directionY)
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
