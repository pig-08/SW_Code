#pragma once

#include <vector>
#include <map>

#include "Spawner.h"
#include "GameSystem.h"
#include "Bullet.h"
#include "Enemy.h"
#include "Enums.h"

class Tower
{
public :
	Tower(int index, Spawner* spawner);
	void SetPower(int power);
	void SetAttackSpeed(float speed);

	void SetAttack(bool isAttack);
	void TowerUpdate();

	void SetColor(ColorPropertyType type);

	void TowerUpgrade(UpgradeType type);

private:
	int attackPower = 1;
	
	float attackSpeed = 2.0f;
	float minAttackSpeed = 1.0f;

private:

	void SetTowerColor(char color);
	
	std::vector<Bullet*> bulletList;
	std::map<char, BULLETSTAT> bullectStatMap;

	POS firePos;
	POS towerColorPos;

	Spawner* thisLineSpawner;
	
	char towerColor;
	bool isAttackStrat;
	float startTime;
};

