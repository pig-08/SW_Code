#include "Bullet.h"

Bullet::Bullet(BULLETSTAT bullectStat, POS firePos, Spawner* spawner)
{
	isDie = false;
	currentBullectStat = bullectStat;
	bulletPos = firePos;
	
	isWallHit = false;
	startTime = 0;

	thisLineSpawner = spawner;

	switch (currentBullectStat.bullectColor)
	{
	case COLOR::LIGHT_RED:
		propertyType = ColorPropertyType::RED;
		break;
	case COLOR::LIGHT_YELLOW:
		propertyType = ColorPropertyType::YELLOW;
		break;
	case COLOR::LIGHT_GREEN:
		propertyType = ColorPropertyType::GREEN;
		break;
	case COLOR::LIGHT_BLUE:
		propertyType = ColorPropertyType::BLUE;
		break;
	}

}

void Bullet::BulletUpdate()
{
	if (BulletHit())
	{
		BulletDie();
		if (currentBullectStat.throughBullet == false ||
			isWallHit)
		{
			isDie = true;
			return;
		}
	}

	Gotoxy(bulletPos.x * 2, bulletPos.y);
	SetColor(currentBullectStat.bullectColor);
	cout << "бу";
	SetColor();

	if (MoveDelayEnd())
	{
		Gotoxy(bulletPos.x * 2, --bulletPos.y);
		SetColor(currentBullectStat.bullectColor);
		cout << "бу";
		SetColor();
		Gotoxy(bulletPos.x * 2, bulletPos.y + 1);
		cout << "  ";
	}
}

void Bullet::BulletDie()
{
	Gotoxy(bulletPos.x * 2, bulletPos.y);
	cout << "  ";
}

bool Bullet::BulletHit()
{
	isWallHit = bulletPos.y + 1 <= 2;
	return isWallHit ||
		thisLineSpawner->CheckHitEnemy(bulletPos, currentBullectStat.range, currentBullectStat.power, false
			, currentBullectStat.isSlow, currentBullectStat.slowValue, currentBullectStat.slowTime, propertyType);
}

POS Bullet::GetBulletPos()
{
	return bulletPos;
}

bool Bullet::GetIsDie()
{
	return isDie;
}

bool Bullet::MoveDelayEnd()
{
	if ((clock() - startTime) / CLOCKS_PER_SEC >= delayTime)
	{
		startTime = clock();
		return true;
	}
	return false;
}


