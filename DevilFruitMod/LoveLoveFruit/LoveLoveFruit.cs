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

namespace DevilFruitMod.LoveLoveFruit
{
    public class LoveLoveFruit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Fruit");
            Tooltip.SetDefault("A fruit that gives you mysterious power but takes your ability to swim.");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
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
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "'s consumption of a second devil fruit destroyed their body"), 1000, 0);
            }
            else
            {
                player.GetModPlayer<DevilFruitUser>().eatenDevilFruit = 2;
                player.GetModPlayer<DevilFruitUser>().devilFruitType = DevilFruitUser.PARAMECIA;
                Main.NewText("You've eaten the Love-Love Fruit, so you can turn foes to stone with your beauty. An attack on an empty hotbar slot will allow you act cute and make foes fall in love, and another will fire a beam to petrify them");
                Main.NewText("But be careful of water, you can no longer swim");
            }
            return true;
        }
    }
}
