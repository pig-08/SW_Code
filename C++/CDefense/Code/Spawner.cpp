#include <time.h>

#include "Console.h"
#include "Spawner.h"
#include "MapSystem.h"
#include "GameSystem.h"
#include "Console.h"

Spawner::Spawner(int lineIndex)
{
	line = lineIndex;
	isStartSpawn = true;

	POS pos;

	pos.y = 1;
	pos.x = 6 + (9 * lineIndex);

	spawnPos = pos;

	propertyMap[ColorPropertyType::RED] = EnemyProperty(ColorPropertyType::BLUE,ColorPropertyType::GREEN, COLOR::LIGHT_RED);
	propertyMap[ColorPropertyType::BLUE] = EnemyProperty(ColorPropertyType::YELLOW,ColorPropertyType::RED, COLOR::LIGHT_BLUE);
	propertyMap[ColorPropertyType::YELLOW] = EnemyProperty(ColorPropertyType::GREEN,ColorPropertyType::BLUE, COLOR::LIGHT_YELLOW);
	propertyMap[ColorPropertyType::GREEN] = EnemyProperty(ColorPropertyType::RED,ColorPropertyType::YELLOW, COLOR::LIGHT_GREEN);
}

void Spawner::SpawnerUpdate()
{
	if (time(NULL) - startTime >= spawnSpeed && isStartSpawn)
	{
		startTime = time(NULL);

		enemyCount++;
		Enemy* newEnemy = new Enemy(spawnPos, propertyMap[rendColorType]);
		enemyList.push_back(newEnemy);

		if (enemyCount >= GameSystem::GetInst()->GetEnemyCount())
			SetStartSpawn(false);
	}

	for (int i = 0; i < enemyList.size(); ++i)
	{
		if (enemyList[i]->GetIsDie())
		{
			delete enemyList[i];
			enemyList.erase(enemyList.begin() + i);
			enemyDieCount--;
		}
		else
			enemyList[i]->EnemyUpdate();
	}
}

bool Spawner::GetStartSpawn()
{
	return isStartSpawn;
}

void Spawner::SetStartSpawn(bool StartSpawn)
{
	isStartSpawn = StartSpawn;

	if (isStartSpawn)
	{
		startTime = time(NULL);
		enemyCount = 0;
		enemyDieCount = GameSystem::GetInst()->GetEnemyCount();
		srand((unsigned int)time(NULL));
		rendColorType = (ColorPropertyType)(rand() % (int)ColorPropertyType::END);
	}
}

bool Spawner::CheckHitEnemy(POS bulletPos, int range,
	int damage, bool check, bool isSlow,
	float slowValue, float slowTime, ColorPropertyType bulletType)
{
	POS enemyPos;

	bool isHit = false;

	for (Enemy* enemy : enemyList)
	{	
		enemyPos = enemy->GetEnemyPos();
		if (enemyPos.y <= bulletPos.y + range &&
			enemyPos.y >= bulletPos.y - range
			&& enemy->HitDelayEnd(check))
		{
			if (check == false)
			{
				enemy->EnemyHitDamage(damage, bulletType);
				if(isSlow)
					enemy->SlowEnemy(slowValue, slowTime);

			}
			isHit = true;;
		}
	}

	return isHit;
}

bool Spawner::EnemyAllDie()
{
	return enemyDieCount <= 0;
}
