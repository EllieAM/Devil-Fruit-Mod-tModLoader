using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using Terraria.ID;

public class GlobalItemClass : GlobalItem
{
    //Makes it so that grabbed items will not enter the first hotbar slot
    //also changes all text to short, and doesn't count fallen star and snowball as ammo (side effects)
    public override bool OnPickup(Item item, Player player)
    {
        bool flag = item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin;
        int num1 = 50;
        int num2 = 0;

        //if ammo (exlcuding sand, star, and snowball ammo) then use normal
        if (((item.ammo > 0 || item.bait > 0) && !item.notAmmo || item.type == ItemID.Wire) && (item.type != ItemID.FallenStar && item.type != ItemID.Snowball && item.type != ItemID.SandBlock && item.type != ItemID.EbonsandBlock && item.type != ItemID.CrimsandBlock && item.type != ItemID.PearlsandBlock))
        {
            return true;
        }

        //use coin numbers
        if (flag)
        {
            return true;
        }
        //if heart or star variant, use normal
        if (item.type == ItemID.Heart || item.type == ItemID.CandyCane || item.type == ItemID.CandyApple || item.type == ItemID.Star || item.type == ItemID.SugarPlum || item.type == ItemID.SoulCake || (item.type > ItemID.NebulaPickup1 && item.type <= ItemID.NebulaPickup3))
        {
            return true;
        }

        //if adding to a stack, use normal
        for (int index = num2; index < 50; ++index)
        {
            int i = index;
            if (i < 0)
                i = 54 + index;
            if (player.inventory[i].type > ItemID.None && player.inventory[i].stack < player.inventory[i].maxStack && item.type == player.inventory[i].type) //item.IsTheSameAs(player.inventory[i])
            {
                if (item.stack + player.inventory[i].stack <= player.inventory[i].maxStack)
                {
                    return true;
                }
            }
        }

        //start at 1, eliminating use of the first slot for usable items
        if (!flag && item.useStyle > 0)
        {
            for (int i = 1; i < 10; ++i)
            {
                if (player.inventory[i].type == ItemID.None)
                {
                    player.inventory[i] = item;
                    PopupText.NewText(PopupTextContext.RegularItemPickup, item, item.stack, false, false);
                    player.DoCoins(i);
                    if (flag)
                        SoundEngine.PlaySound(SoundID.CoinPickup, player.position);
                    else
                        SoundEngine.PlaySound(SoundID.Grab, player.position);
                    if (player.whoAmI == Main.myPlayer)
                        Recipe.FindRecipes();
                    AchievementsHelper.NotifyItemPickup(player, item);
                    return false;
                }
            }
        }

        //end at 1, eliminating use of first slot for unusable items
        if (!item.favorited)
        {
            for (int i = num1 - 1; i >= 1; --i)
            {
                if (player.inventory[i].type == ItemID.None)
                {
                    player.inventory[i] = item;
                    PopupText.NewText(PopupTextContext.RegularItemPickup, item, item.stack, false, false);
                    player.DoCoins(i);
                    if (flag)
                        SoundEngine.PlaySound(SoundID.CoinPickup, player.position);
                    else
                        SoundEngine.PlaySound(SoundID.Grab, player.position);
                    if (player.whoAmI == Main.myPlayer)
                        Recipe.FindRecipes();
                    AchievementsHelper.NotifyItemPickup(player, item);
                    return false;
                }
            }
        }

        return true;
    }
}