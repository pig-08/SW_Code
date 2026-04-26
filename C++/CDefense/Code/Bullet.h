#pragma once

#include "Spawner.h"
#include "GameSystem.h"
#include "Enums.h"
#include "Console.h"

typedef struct _bulletStat
{
	int power;
	int range;

	COLOR bullectColor;

	bool throughBullet;
	bool isSlow;

	float slowValue;
	float slowTime;

}BULLETSTAT, * PBULLETSTAT;

class Bullet
{
public:
	Bullet(BULLETSTAT bullectStat, POS firePos, Spawner* spawner);
	
	void BulletUpdate() ;
	void BulletDie();

	POS GetBulletPos();

	virtual bool BulletHit();
	virtual bool GetIsDie();

private:

	Spawner* thisLineSpawner;
	
	bool MoveDelayEnd();


	float startTime;
	float delayTime = 0.2f;

	bool isDie;
	bool isWallHit;

	POS bulletPos;

	ColorPropertyType propertyType;
	BULLETSTAT currentBullectStat;
};
