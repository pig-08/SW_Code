#pragma once

#include "Scene.h"
#include "Tower.h"
#include "Spawner.h"
#include "Upgrade.h"
#include "Enums.h"

const int TOWER_MAP_HEIGHT = 34;

class GameScene : public Scene
{
public:
	virtual void Update() final;

	virtual void Render() final;

	virtual void Enter() final;
	virtual void Exit() final;

	GameScene();

	void NextRound();

	bool GetIsUpgrade();
	void SetIsUpgrade(bool value);

private:
	void GameUpdate();
	void LoadStage();
	void RenderAllMap();
	void RenderTorwer();
	void RenderUI();
	void TowerUpgrade(int index);
	bool InputCollTime();
	void SetAllTowerColor(ColorPropertyType type);
private:

	Upgrade* upgrade;
	Tower* towerList[4];
	Spawner* spawnerList[4];
	
	bool isUpgrade;

	POS upgradePos;
	int upgradePosMaxY;
	int upgradePosMinY;

	int upgradeTypeValue = 0;
	float collTimeStart = 0;
	float coolTime = 0.08f;
};
