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

namespace DevilFruitMod.DFTemplateDFTemplateFruit
{
	public class DFTemplateDFTemplateFruit : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mysterious Fruit");
			Tooltip.SetDefault("A fruit that gives you mysterious power but takes your ability to swim.");
		}
		public override void SetDefaults()
		{
			item.width = 44;
			item.height = 42;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 2;
			item.value = 50000;
			item.rare = 2;
			item.UseSound = SoundID.Item2;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (player.GetModPlayer<DevilFruitUser>().eatenDevilFruit > 0)
            {
                player.GetModPlayer<DevilFruitUser>().eatenDevilFruit = 0;
                player.GetModPlayer<DevilFruitUser>().fruitLevel = 0;
                player.GetModPlayer<DevilFruitUser>().devilFruitType = 0;
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "'s consumption of a second devil fruit destroyed their body"),1000,0);
            }
            else
            {
                //UPDATE VALUE!!
                player.GetModPlayer<DevilFruitUser>().eatenDevilFruit = 99;
                player.GetModPlayer<DevilFruitUser>().devilFruitType = 0;
                Main.NewText("You've eaten the DFTemplate-DFTemplate fruit, making you a DFTemplate human. You can...");
                Main.NewText("But be careful of water, you can no longer swim.");
            }
            return true;
        }
    }
}
