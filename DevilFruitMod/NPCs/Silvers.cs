using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DevilFruitMod.NPCs
{
	[AutoloadHead]
	public class Silvers : ModNPC
	{
        public override string Texture
        {
            get
            {
                return "DevilFruitMod/NPCs/Silvers";
            }
        }

        /*
		public override bool IsLoadingEnabled(Mod mod)
		{
            mod.ContentAutoloadingEnabled = true;
            mod.GoreAutoloadingEnabled = true;
            mod.MusicAutoloadingEnabled = true;
            mod.BackgroundAutoloadingEnabled = true;
			tModPorter Note: Mod.Properties Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled)
		}
        */

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dark King");
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.DangerDetectRange[NPC.type] = 208;
			NPCID.Sets.AttackType[NPC.type] = 0;
			NPCID.Sets.AttackTime[NPC.type] = 90;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
            NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }
     
		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			for (int k = 0; k < 255; k++)
			{
				Player player = Main.player[k];
				if (player.active)
				{
					if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0)
                    {
                        return true;
                    }
				}
			}
			return false;
		}

		public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
		{
            List<string> list = new List<string>();
            list.Add("Silvers Rayleigh");
            return list;
		}

		public override void FindFrame(int frameHeight)
		{
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}

		public override string GetChat()
		{
            int pirate = NPC.FindFirstNPC(NPCID.Pirate);
            if (pirate >= 0 && Main.rand.NextBool(5)) return Main.npc[pirate].GivenName + " looks awfully familiar. I wonder what kind of stories he has to tell.";
            int cyborg = NPC.FindFirstNPC(NPCID.Cyborg);
            if (cyborg >= 0 && Main.rand.NextBool(5)) return "I gave a cola to " + Main.npc[cyborg].GivenName + " the other day.  He didn't get it...";

            switch (Main.rand.Next(3))
            {
                case 0:
                    switch (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit) {
                        case 1:
                            return "You must have eaten the Gum-Gum Fruit. In the future, I will be able to teach you new moves to use.";
                        case 2:
                            return "You must have eaten the Love-Love Fruit. In the future, I will be able to teach you new moves to use.";
                        case 3:
                            return "You must have eaten the Human-Human Fruit. Unlucky.";
                        case 4:
                            return "You must have eaten the Bomb-Bomb Fruit. In the future, I will be able to teach you new moves to use.";
                    }
                    break;
                case 1:
                    return "Maybe nothing in this life happens by accident. As everything happens for a reason, our destiny slowly takes form.";
                case 2:
                    return "If you're having a hard time figuring out how to use your fruit powers, check your hotkeys.";
            }
            return "";
        }

        public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Upgrade";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
                if (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 0)
                {
                    Main.npcChatText = "You haven't eaten a Devil Fruit, so there isn't much I can do for you";
                }
                else switch (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().fruitLevel)
                {
                    case 0:

                        if ((NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedQueenBee || NPC.downedSlimeKing))
                        {
                            switch (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit)
                            {
                                case 1:
                                    Main.npcChatText = "Your powers are improved and you have unlocked Gum Gum Rifle!";
                                    break;
                                case 2:
                                    Main.npcChatText = "Ah, you remind me of a younger Shakky, also you now can use Pistol Kiss.";
                                    break;
                                case 3:
                                    Main.npcChatText = "F in the chat";
                                    break;
                                case 4:
                                    Main.npcChatText = "Watch where you're aiming that thing, you have unlocked Breeze Breath Bomb!";
                                    break;
                                }
                            Main.LocalPlayer.GetModPlayer<DevilFruitUser>().fruitLevel++;
                            Main.NewText("Level Up!");
                        }
                        else
                            Main.npcChatText = "I can help you develop new power, but first you need experience. Take down a large monster, then come back.";
                            break;

                    case 1:
                        
                        if (Main.hardMode)
                        {
                            switch (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit)
                            {
                                case 1:
                                    Main.npcChatText = "Your powers are improved and you have unlocked Gum Gum Gatling!  You have also gained funtionality in a second hand!  Use it wisely.";
                                    break;
                                case 2:
                                    Main.npcChatText = "Now you're a whole snacc. Enjoy that Slave Arrow, queen.";
                                    break;
                                case 3:
                                    Main.npcChatText = "If only you were a different animal... like a raindeer or something.";
                                    break;
                                case 4:
                                    Main.npcChatText = "I'm staying away from you, you have unlocked Full Body Explosion.";
                                    break;
                                }
                            Main.LocalPlayer.GetModPlayer<DevilFruitUser>().fruitLevel++;
                            Main.NewText("Level Up!");
                        }
                        else
                            Main.npcChatText = "I've heard tales of a terrifying monster in the underworld.  That should do nicely for a second test.";

                        break;

                    case 2:
                        string boss1;
                        string boss2;

                        if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                        {
                            if (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 1) Main.npcChatText = "Your punches will now do devastating damage.  Scavengers beware!";
                            if (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 2) Main.npcChatText = "Dang beech now you got that dumptruck. You could bounce a quarter off them thicc cheeks.";
                            if (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 3) Main.npcChatText = "Wabam, now you're human-i-er or something. Idk man...";
                            if (Main.LocalPlayer.GetModPlayer<DevilFruitUser>().eatenDevilFruit == 4) Main.npcChatText = "Your explosions are even more deadly now!";
                            Main.LocalPlayer.GetModPlayer<DevilFruitUser>().fruitLevel++;
                            Main.NewText("Level Up!");
                        }
                        else if (NPC.downedMechBoss1 ? (NPC.downedMechBoss2 || NPC.downedMechBoss3) : (NPC.downedMechBoss2 && NPC.downedMechBoss3)) //two out of three
                        {
                            if (NPC.downedMechBoss1 && NPC.downedMechBoss2)
                            {
                                    boss1 = "worm";
                                    boss2 = "eyes";
                            }
                            else
                            {
                                boss1 = NPC.downedMechBoss1 ? "worm" : "eyes";
                                boss2 = "skull";
                            }
                            Main.npcChatText = "You have defeated the " + boss1 + " and the " + boss2 + " but the last one still breathes";
                        }
                        else if (NPC.downedMechBossAny) //one of three
                        {
                            if (NPC.downedMechBoss1) boss1 = "worm";
                            else boss1 = NPC.downedMechBoss2 ? "eyes" : "skull";
                            Main.npcChatText = "You have defeated the " + boss1 + " but the other two still breathe.";
                        }
                        else 
                            Main.npcChatText = "There exist three robotic beasts that can be summoned by their respective artifacts. If you destroy all three, another upgrade will come your way.";
                        break;
                    case 3:
                        Main.npcChatText = "Welp, that's all the upgrading you can do.  If you feel like your fruit power still sucks feel free to go complain to the developer. She made a discord you know.";
                        return;
                }
            }
		}

		/*public override void SetupShop(Chest shop, ref int nextSlot)
		{
			if (Main.LocalPlayer.GetModPlayer<DevilFruitUser>(mod).eatenDevilFruit > 0)
            {
                if ((NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedQueenBee || NPC.downedSlimeKing) && Main.LocalPlayer.GetModPlayer<DevilFruitUser>(mod).fruitLevel == 0)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("Scroll1"));
                    nextSlot++;
                }
                if (Main.hardMode && Main.LocalPlayer.GetModPlayer<DevilFruitUser>(mod).fruitLevel == 1)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("Scroll2"));
                    nextSlot++;
                }
            }
		}*/

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 0;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 1;
			randExtraCooldown = 30;
		}

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = Mod.Find<ModProjectile>("HakiPulse").Type;
            attackDelay = 1;
        }

        /*public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }*/
    }
}