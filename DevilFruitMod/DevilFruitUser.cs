using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.DataStructures;

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

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            base.SendClientChanges(clientPlayer);
        }

        /*public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = mod.GetPacket();
			packet.Write((byte)ExampleModMessageType.ExampleLifeFruits);
			packet.Write((byte)player.whoAmI);
			packet.Write(eatenDevilFruit);
			packet.Send(toWho, fromWho);
		}*/

        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
		{
            tag.Add("eatenDevilFruit", eatenDevilFruit);
            tag.Add("fruitLevel", fruitLevel);
		}

		public override void LoadData(TagCompound tag)
		{
            eatenDevilFruit = tag.GetInt("eatenDevilFruit");
            fruitLevel = tag.GetInt("fruitLevel");
		}

        /* OUT OF DATE
		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
		}
        */

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            DevilFruitMod.hands = 0;
        }

        public override void OnEnterWorld(Player player)
        {
            DevilFruitMod.hands = 0;
            DevilFruitMod.hooks = 0;
        }

        //Debuff effects
        public override void SetControls()
        {
            if (Player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0 && Player.wet && !(Player.honeyWet || Player.lavaWet))
            {
                Player.controlJump = false;
                Player.controlDown = false;
                Player.controlLeft = false;
                Player.controlRight = false;
                Player.controlUp = false;
            }
        }
    }
}
