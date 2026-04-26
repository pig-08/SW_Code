#pragma once

#include <vector>
#include <map>
#include "Enemy.h"
#include "Enums.h"

class Spawner
{
public:
	Spawner(int lineIndex);
	void SpawnerUpdate();

	bool GetStartSpawn();
	void SetStartSpawn(bool StartSpawn);

	bool CheckHitEnemy(POS bulletPos, 
		int range,int damage,
		bool check, bool isSlow,
		float slowValue, float slowTime, ColorPropertyType bulletType);

	bool EnemyAllDie();

private:
	std::vector<Enemy*> enemyList;
	std::map<ColorPropertyType, EnemyProperty> propertyMap;

	POS spawnPos;
	bool isStartSpawn;
	
	ColorPropertyType rendColorType;
	int enemyDieCount;
	int enemyCount;

	int spawnSpeed = 5;
	int startTime;
	
	int line;
};

