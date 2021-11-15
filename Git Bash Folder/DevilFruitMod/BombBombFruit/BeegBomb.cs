using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using Terraria.ID;

namespace DevilFruitMod.BombBombFruit
{
    public class BeegBomb : ModProjectile
    {
		float playerCenterX;
		float playerCenterY;
        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.alpha = 255;
			projectile.timeLeft = 300;

			projectile.penetrate = -1;
		}


		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			// Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
			if (Main.expertMode)
			{
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
				{
					damage /= 5;
				}
			}
		}

		public override void AI()
		{

			if (projectile.owner == Main.myPlayer && projectile.timeLeft == 297)
			{
				projectile.position = projectile.Center;
				projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 0;
				projectile.height = 0;
				projectile.Center = projectile.position;
				projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			}
			else if (projectile.owner == Main.myPlayer && projectile.timeLeft > 297)
			{
				projectile.tileCollide = false;
				// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
				projectile.position = projectile.Center;
				//projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				//projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 300;
				projectile.height = 300;
				playerCenterX = projectile.position.X;
				playerCenterY = projectile.position.Y;
				projectile.Center = projectile.position;
				//projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				//projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

				// Play explosion sound
				Main.PlaySound(SoundID.Item14, projectile.position);
				Main.PlaySound(SoundID.Item14, projectile.position);
				Main.PlaySound(SoundID.Item14, projectile.position);

				// Smoke Dust spawn
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				// Fire Dust spawn
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 3f;
				}
				// Large Smoke Gore spawn
				for (int g = 0; g < 2; g++)
				{
					int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				}

				{
					int explosionRadius = 15;

					int minTileX = (int)(playerCenterX / 16f - (float)explosionRadius);
					int maxTileX = (int)(playerCenterX / 16f + (float)explosionRadius);
					int minTileY = (int)(playerCenterY / 16f - (float)explosionRadius);
					int maxTileY = (int)(playerCenterY / 16f + (float)explosionRadius);
					if (minTileX < 0)
					{
						minTileX = 0;
					}
					if (maxTileX > Main.maxTilesX)
					{
						maxTileX = Main.maxTilesX;
					}
					if (minTileY < 0)
					{
						minTileY = 0;
					}
					if (maxTileY > Main.maxTilesY)
					{
						maxTileY = Main.maxTilesY;
					}
					bool canKillWalls = false;
					for (int x = minTileX; x <= maxTileX; x++)
					{
						for (int y = minTileY; y <= maxTileY; y++)
						{
							float diffX = Math.Abs((float)x - playerCenterX / 16f);
							float diffY = Math.Abs((float)y - playerCenterY / 16f);
							double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
							if (distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].wall == 0)
							{
								canKillWalls = true;
								break;
							}
						}
					}
					AchievementsHelper.CurrentlyMining = true;
					for (int i = minTileX; i <= maxTileX; i++)
					{
						for (int j = minTileY; j <= maxTileY; j++)
						{
							float diffX = Math.Abs((float)i - playerCenterX / 16f);
							float diffY = Math.Abs((float)j - playerCenterY / 16f);
							double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
							if (distanceToTile < (double)explosionRadius)
							{
								bool canKillTile = true;
								if (Main.tile[i, j] != null && Main.tile[i, j].active())
								{
									canKillTile = true;
									if (Main.tileDungeon[(int)Main.tile[i, j].type] || Main.tile[i, j].type == 88 || Main.tile[i, j].type == 21 || Main.tile[i, j].type == 26 || Main.tile[i, j].type == 107 || Main.tile[i, j].type == 108 || Main.tile[i, j].type == 111 || Main.tile[i, j].type == 226 || Main.tile[i, j].type == 237 || Main.tile[i, j].type == 221 || Main.tile[i, j].type == 222 || Main.tile[i, j].type == 223 || Main.tile[i, j].type == 211 || Main.tile[i, j].type == 404)
									{
										canKillTile = false;
									}
									if (!Main.hardMode && Main.tile[i, j].type == 58)
									{
										canKillTile = false;
									}
									if (!TileLoader.CanExplode(i, j))
									{
										canKillTile = false;
									}
									if (canKillTile)
									{
										WorldGen.KillTile(i, j, false, false, false);
										if (!Main.tile[i, j].active() && Main.netMode != NetmodeID.SinglePlayer)
										{
											NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
										}
									}
								}
								if (canKillTile)
								{
									for (int x = i - 1; x <= i + 1; x++)
									{
										for (int y = j - 1; y <= j + 1; y++)
										{
											if (Main.tile[x, y] != null && Main.tile[x, y].wall > 0 && canKillWalls && WallLoader.CanExplode(x, y, Main.tile[x, y].wall))
											{
												WorldGen.KillWall(x, y, false);
												if (Main.tile[x, y].wall == 0 && Main.netMode != NetmodeID.SinglePlayer)
												{
													NetMessage.SendData(MessageID.TileChange, -1, -1, null, 2, (float)x, (float)y, 0f, 0, 0, 0);
												}
											}
										}
									}
								}
							}
						}
					}
					AchievementsHelper.CurrentlyMining = false;
				}

			}

			return;
		}

		public override void Kill(int timeLeft)
        {
            DevilFruitMod.hands-=2;
		}

    }
}

