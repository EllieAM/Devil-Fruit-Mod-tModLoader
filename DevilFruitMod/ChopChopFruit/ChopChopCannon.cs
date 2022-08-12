using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace DevilFruitMod.ChopChopFruit
{
	public class ChopChopCannon : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			//projectile.aiStyle = 9; // Vanilla magic missile uses this aiStyle, but using it wouldn't let us fine tune the projectile speed or dust
			Projectile.friendly = true;
			Projectile.light = 0.8f;
			Projectile.DamageType = DamageClass.Melee;
			DrawOriginOffsetY = -6;
			Projectile.penetrate = -1;
		}

		public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 0);

		public override void AI()
		{

			// This part makes the projectile do a shime sound every 10 ticks as long as it is moving.
			if (Projectile.soundDelay == 0 && Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 2f)
			{
				Projectile.soundDelay = 100;
				SoundEngine.PlaySound(SoundID.Item1, Projectile.position);
			}

			// In Multi Player (MP) This code only runs on the client of the projectile's owner, this is because it relies on mouse position, which isn't the same across all clients.
			if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
			{
				Projectile.timeLeft = 40;
				Player player = Main.player[Projectile.owner];

				// If the player stops channeling, destroy projectile
				if (DevilFruitMod.UsePowers1Hotkey.JustReleased)
				{
					// This code block is very similar to the previous one, but only runs once after the player stops channeling their weapon.
					Projectile.netUpdate = true;

					float maxDistance = 4f; // This also sets the maximum speed the projectile can reach after it stops following the cursor.
					Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
					float distanceToCursor = vectorToCursor.Length();

					//If the projectile was at the cursor's position, set it to move in the oposite direction from the player.
					if (distanceToCursor == 0f)
					{
						vectorToCursor = Projectile.Center - player.Center;
						distanceToCursor = vectorToCursor.Length();
					}

					distanceToCursor = maxDistance / distanceToCursor;
					vectorToCursor *= distanceToCursor;

					Projectile.velocity = vectorToCursor;

					if (Projectile.velocity == Vector2.Zero)
					{
						Projectile.Kill();
					}

					Projectile.ai[0] = 1f;
					//projectile.Kill();
				}

				// If the player channels the weapon, do something
				else if (Projectile.ai[0] == 0f)
				{

					float maxDistance = 20f; // This also sets the maximum speed the projectile can reach while following the cursor.
					Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
					float distanceToCursor = vectorToCursor.Length();

					// Here we can see that the speed of the projectile depends on the distance to the cursor.
					if (distanceToCursor > maxDistance)
					{
						distanceToCursor = maxDistance / distanceToCursor;
						vectorToCursor *= distanceToCursor;
					}

					int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
					int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 1000f);
					int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
					int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 1000f);

					// This code checks if the precious velocity of the projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
					// We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
					if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
					{
						Projectile.netUpdate = true;
					}

					Projectile.velocity = vectorToCursor;

				}
			}

			// Set the rotation so the projectile points towards where it's going.
			if (Projectile.velocity != Vector2.Zero)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
			}
		}

		public override void Kill(int timeLeft)
		{

			DevilFruitMod.hands--;

			// If the projectile dies without hitting an enemy, crate a small explosion that hits all enemies in the area.
			if (Projectile.penetrate == 1)
			{
				// Makes the projectile hit all enemies as it circunvents the penetrate limit.
				Projectile.maxPenetrate = -1;
				Projectile.penetrate = -1;

				int explosionArea = 60;
				Vector2 oldSize = Projectile.Size;
				// Resize the projectile hitbox to be bigger.
				Projectile.position = Projectile.Center;
				Projectile.Size += new Vector2(explosionArea);
				Projectile.Center = Projectile.position;

				Projectile.tileCollide = false;
				Projectile.velocity *= 0.01f;
				// Damage enemies inside the hitbox area
				Projectile.Damage();
				Projectile.scale = 0.01f;

				//Resize the hitbox to its original size
				Projectile.position = Projectile.Center;
				Projectile.Size = new Vector2(10);
				Projectile.Center = Projectile.position;
			}
			
		}
	}
}

