#include <time.h>

#include "Console.h"
#include "Core.h"
#include "Tower.h"
#include "MapSystem.h"
#include "SoundManager.h"

Tower::Tower(int index, Spawner* spawner)
{
	POS pos;

	pos.y = 33;
	pos.x = 6 + (9 * index);

	thisLineSpawner = spawner;

	firePos = pos;

	pos.y = 34;
	pos.x -= 2;


	bullectStatMap[(char)COLOR::LIGHT_RED] = BULLETSTAT(attackPower * 2, 3, COLOR::LIGHT_RED, false);
	bullectStatMap[(char)COLOR::LIGHT_YELLOW] = BULLETSTAT(attackPower, 2, COLOR::LIGHT_YELLOW, true);
	bullectStatMap[(char)COLOR::LIGHT_GREEN] = BULLETSTAT(attackPower, 2, COLOR::LIGHT_GREEN, true, true, 0.5f, 2.0f);
	bullectStatMap[(char)COLOR::LIGHT_BLUE] = BULLETSTAT(attackPower, 2, COLOR::LIGHT_BLUE, false, true, 1.5f, 1.0f);

	towerColorPos = pos;
	SetAttack(false);
}

void Tower::SetPower(int power)
{
	attackPower += power;
}

void Tower::SetAttackSpeed(float speed)
{
	if (attackSpeed - speed < minAttackSpeed)
		attackSpeed = minAttackSpeed;
	else
		attackSpeed -= speed;
}


void Tower::SetAttack(bool isAttack)
{
	isAttackStrat = isAttack;

	if(isAttack)
		startTime = clock();
}

void Tower::TowerUpdate()
{
	if ((clock() - startTime) / CLOCKS_PER_SEC >= attackSpeed && isAttackStrat)
	{
		startTime = clock();
		attackSpeed;
		SoundManager::GetInst()->PlaySFX(SFX::FIRE);
		Bullet* newBullet = new Bullet(bullectStatMap[towerColor], firePos, thisLineSpawner);
		bulletList.push_back(newBullet);
	}

	for (int i = 0; i < bulletList.size(); ++i)
	{
		if (bulletList[i]->GetIsDie())
		{
			delete bulletList[i];
			bulletList.erase(bulletList.begin() + i);
		}
		else
			bulletList[i]->BulletUpdate();

	}
}

void Tower::SetColor(ColorPropertyType type)
{
	switch (type)
	{
	case ColorPropertyType::RED:
		SetTowerColor((char)COLOR::LIGHT_RED);
		break;
	case ColorPropertyType::YELLOW:
		SetTowerColor((char)COLOR::LIGHT_YELLOW);
		break;
	case ColorPropertyType::GREEN:
		SetTowerColor((char)COLOR::LIGHT_GREEN);
		break;
	case ColorPropertyType::BLUE:
		SetTowerColor((char)COLOR::LIGHT_BLUE);
		break;
	}
}

void Tower::SetTowerColor(char color)
{
	for (int y = towerColorPos.y; y < towerColorPos.y + 6; ++y)
	{
		for (int x = towerColorPos.x; x < towerColorPos.x + 5; ++x)
		{
			MapSystem::GetInst()->gameColorMap[y][x] = color;
		}
	}

	towerColor = color;
}

void Tower::TowerUpgrade(UpgradeType type)
{
	switch (type)
	{
	case UpgradeType::DMG:
		SetPower(1);
		break;
	case UpgradeType::SPEED:
		SetAttackSpeed(0.2f);
		break;
	case UpgradeType::HP:
		GameSystem::GetInst()->SetHp(10);
		break;
	case UpgradeType::EXP:
		GameSystem::GetInst()->SetTargetEXP(-10.0f, true);
		break;
	}
	SoundManager::GetInst()->PlaySFX(SFX::UPGRADE);
}

