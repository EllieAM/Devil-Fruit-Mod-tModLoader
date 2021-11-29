using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;

namespace DevilFruitMod.Util
{
    static class TMath
    {

        public static Random r = new Random();
        public static Vector2 CalculateTrajectory()
        {
            float clickX = (int)(Main.mouseX) - Main.screenWidth / 2;
            float clickY = (int)(Main.mouseY) - Main.screenHeight / 2;
            float magnitude = (float)(Math.Sqrt(clickX * clickX + clickY * clickY));
            float directionX = 10 * clickX / magnitude;
            float directionY = 10 * clickY / magnitude;
            return new Vector2(directionX, directionY);
        }

        public static Vector2 RandomPlayerHitboxPos(Player player)
        {
            Vector2 downLeftPos = player.getRect().BottomLeft();
            int width = player.getRect().Width;
            int height = player.getRect().Height;
            float randomX = downLeftPos.X + r.Next(1, width);
            float randomY = downLeftPos.Y - r.Next(1, height);
            return new Vector2(randomX, randomY);
        }

    }
}
