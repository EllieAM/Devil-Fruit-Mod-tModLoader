using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DevilFruitMod.ChopChopFruit
{
	public class ChopChopFruit : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mysterious Fruit");
			Tooltip.SetDefault("A fruit that gives you mysterious power but takes your ability to swim.");
		}
		public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 42;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 2;
			Item.value = 50000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item2;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0)
            {
                player.GetModPlayer<DevilFruitUser>().eatenDevilFruit = 0;
                player.GetModPlayer<DevilFruitUser>().fruitLevel = 0;
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "'s consumption of a second devil fruit destroyed their body"),1000,0);
            }
            else
            {
                //UPDATE VALUE!!
                player.GetModPlayer<DevilFruitUser>().eatenDevilFruit = 6;
                Main.NewText("You've eaten the Chop-Chop fruit, making you a Chop human. You can separate your limbs and move them freely!");
                Main.NewText("But be careful of water, you can no longer swim.");
            }
            return true;
        }
    }
}
