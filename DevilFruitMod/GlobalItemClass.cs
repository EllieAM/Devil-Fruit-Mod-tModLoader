using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

public class GlobalItemClass : GlobalItem
{
    //Makes it so that grabbed items will not enter the first hotbar slot
    //also changes all text to short, and doesn't count fallen star and snowball as ammo (side effects)
    public override bool OnPickup(Item item, Player player)
    {
        bool flag = item.type >= 71 && item.type <= 74;
        int num1 = 50;
        int num2 = 0;

        //if ammo then use normal
        if (((item.ammo > 0 || item.bait > 0) && !item.notAmmo || item.type == 530) && (item.type != 75 && item.type != 949))
        {
            return true;
        }

        //use coin numbers
        if (flag)
        {
            return true;
        }

        //if heart or star vairant, use normal
        if (item.type == 58 || item.type == 1867 || item.type == 1734 || item.type == 184 || item.type == 1868 || item.type == 1735 || (item.type > 3453 && item.type <= 3455))
        {
            return true;
        }

        //if adding to a stack, use normal
        for (int index = num2; index < 50; ++index)
        {
            int i = index;
            if (i < 0)
                i = 54 + index;
            if (player.inventory[i].type > 0 && player.inventory[i].stack < player.inventory[i].maxStack && item.IsTheSameAs(player.inventory[i]))
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
                if (player.inventory[i].type == 0)
                {
                    player.inventory[i] = item;
                    ItemText.NewText(item, item.stack, false, false);
                    player.DoCoins(i);
                    if (flag)
                        Main.PlaySound(38, (int)player.position.X, (int)player.position.Y, 1, 1f, 0.0f);
                    else
                        Main.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0.0f);
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
                if (player.inventory[i].type == 0)
                {
                    player.inventory[i] = item;
                    ItemText.NewText(item, item.stack, false, false);
                    player.DoCoins(i);
                    if (flag)
                        Main.PlaySound(38, (int)player.position.X, (int)player.position.Y, 1, 1f, 0.0f);
                    else
                        Main.PlaySound(7, (int)player.position.X, (int)player.position.Y, 1, 1f, 0.0f);
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