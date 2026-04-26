#include <time.h>
#include "GameScene.h"
#include "Console.h"
#include"KeyController.h"
#include "Enums.h"
#include "SoundManager.h"
#include "MapSystem.h"
#include "GameSystem.h"
#include "Core.h"

GameScene::GameScene()
{
	for (int i = 0; i < 4; ++i)
	{
		int tmep = i;
		spawnerList[tmep] = new Spawner(tmep);
		towerList[tmep] = new Tower(tmep, spawnerList[tmep]);
	}

	upgrade = new Upgrade();
	isUpgrade = false; //15


	COORD resolution = GetConsoleResolution();
	POS setUpgradePos;
	setUpgradePos.x = resolution.X / 1.5;
	setUpgradePos.y = (resolution.Y / 10) + 16;

	upgradePosMinY = setUpgradePos.y;
	upgradePosMaxY = setUpgradePos.y + 4;

	upgradePos = setUpgradePos;
}

void GameScene::Update()
{
	GameUpdate();
}

void GameScene::Render()
{
	RenderTorwer();
	RenderUI();
}

void GameScene::Enter()
{
	Gotoxy(0, 0);
	LoadStage();
	GameSystem::GetInst()->SetDefaultValue();
	SoundManager::GetInst()->PlayBGM(BGM::GAMEBGM);
	for (int i = 0; i < MAP_HEIGHT; ++i)
	{
		for (int j = 0; j < MAP_WIDTH; ++j)
		{
			MapSystem::GetInst()->gameColorMap[i][j] = (char)COLOR::WHITE;
		}
	}

	RenderAllMap();

	for (int i = 0; i < 4; ++i)
	{
		int tmep = i;
		towerList[tmep]->SetAttack(true);
		spawnerList[tmep]->SetStartSpawn(true);
	}
	SetAllTowerColor(ColorPropertyType::RED);
}

void GameScene::Exit()
{
	system("cls");
}

bool GameScene::GetIsUpgrade()
{
	return isUpgrade;
}

void GameScene::SetIsUpgrade(bool value)
{
	isUpgrade = value;
}

void GameScene::NextRound()
{
	for (int i = 0; i < 4; ++i)
	{
		spawnerList[i]->SetStartSpawn(true);
	}
	GameSystem::GetInst()->SetRound(1);
}

void GameScene::GameUpdate()
{
	if (GameSystem::GetInst()->GetHp() <= 0)
		Core::GetInst()->ChangeScene(SceneEnum::OVER);

	for (int i = 0; i < 4; ++i)
	{
		towerList[i]->TowerUpdate();
		spawnerList[i]->SpawnerUpdate();
	}

	if (spawnerList[0]->EnemyAllDie() &&
		spawnerList[1]->EnemyAllDie() &&
		spawnerList[2]->EnemyAllDie() &&
		spawnerList[3]->EnemyAllDie())
	{
		NextRound();
	}

	if (GameSystem::GetInst()->GetTargetEXP() <= GameSystem::GetInst()->GetEXP()
		&& isUpgrade == false)
	{
		GameSystem::GetInst()->SetEXP(-GameSystem::GetInst()->GetTargetEXP());
		GameSystem::GetInst()->SetTargetEXP(1.2f);
		upgrade->NewUpgrade();
		isUpgrade = true;
	}

	if (isUpgrade)
	{
		Key upgradeTowerKey = KeyController();
		switch (upgradeTowerKey)
		{
		case Key::ONE:
			TowerUpgrade(0);
			break;
		case Key::TWO:
			TowerUpgrade(1);
			break;
		case Key::THREE:
			TowerUpgrade(2);
			break;
		case Key::FOUR:
			TowerUpgrade(3);
			break;
		}
	}

	Key towerTypeInput = KeyController();

	switch (towerTypeInput)
	{
	case Key::Q:
		SetAllTowerColor(ColorPropertyType::RED);
		break;
	case Key::W:
		SetAllTowerColor(ColorPropertyType::YELLOW);
		break;
	case Key::E:
		SetAllTowerColor(ColorPropertyType::GREEN);
		break;
	case Key::R:
		SetAllTowerColor(ColorPropertyType::BLUE);
		break;
	}
}

void GameScene::TowerUpgrade(int index)
{
	towerList[index]->TowerUpgrade(upgrade->GetUpgradeType(upgradeTypeValue));
	upgrade->DefaultUpgradeType();
	isUpgrade = false;
}

void GameScene::LoadStage() 
{
	strcpy_s(MapSystem::GetInst()->gameMap[0], "0000221220000221220000221220000221220000");
	strcpy_s(MapSystem::GetInst()->gameMap[1], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[2], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[3], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[4], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[5], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[6], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[7], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[8], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[9], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[10], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[11], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[12], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[13], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[14], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[15], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[16], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[17], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[18], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[19], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[20], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[21], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[22], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[23], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[24], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[25], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[26], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[27], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[28], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[29], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[30], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[31], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[32], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[33], "0000222220000222220000222220000222220000");
	strcpy_s(MapSystem::GetInst()->gameMap[34], "0000434340000434340000434340000434340000");
	strcpy_s(MapSystem::GetInst()->gameMap[35], "0000444440000444440000444440000444440000");
	strcpy_s(MapSystem::GetInst()->gameMap[36], "0000444440000444440000444440000444440000");
	strcpy_s(MapSystem::GetInst()->gameMap[37], "0000444440000444440000444440000444440000");
	strcpy_s(MapSystem::GetInst()->gameMap[38], "0000444440000444440000444440000444440000");
	strcpy_s(MapSystem::GetInst()->gameMap[39], "0000444440000444440000444440000444440000");
}

void GameScene::RenderAllMap()
{
	for (int i = 0; i < MAP_HEIGHT; ++i)
	{
		for (int j = 0; j < MAP_WIDTH; ++j)
		{
			SetColor((COLOR)MapSystem::GetInst()->gameColorMap[i][j]);
			if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::WALL)
				cout << "■";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::ROAD)
				cout << "  ";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::START)
				cout << "☎";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::SHORT)
				cout << "▣";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::LONG)
				cout << "〓";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::TEST)
				cout << "▲";
			SetColor();
		}

		cout << '\n';
	}
}

void GameScene::RenderTorwer()
{
	Gotoxy(0, TOWER_MAP_HEIGHT);
	for (int i = TOWER_MAP_HEIGHT; i < MAP_HEIGHT; ++i)
	{
		for (int j = 0; j < MAP_WIDTH; ++j)
		{
			SetColor((COLOR)MapSystem::GetInst()->gameColorMap[i][j]);
			if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::WALL)
				cout << "■";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::ROAD)
				cout << "  ";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::START)
				cout << "☎";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::SHORT)
				cout << "▣";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::LONG)
				cout << "〓";
			else if (MapSystem::GetInst()->gameMap[i][j] == (char)Tile::TEST)
				cout << "▲";
			SetColor();
		}

		cout << '\n';
	}
}

void GameScene::RenderUI()
{

	COORD resolution = GetConsoleResolution();
	int x = resolution.X / 1.5;
	int y = resolution.Y / 10;

	Gotoxy(x, y++);
	cout << " ====================";
	Gotoxy(x, y++);
	cout << "  [    게임 정보   ]" << '\n';
	Gotoxy(x, y++);
	cout << " --------------------" << '\n';
	Gotoxy(x, y++);
	cout << "  현재 라운드 : " << GameSystem::GetInst()->GetRound() << '\n';
	Gotoxy(x, y++);
	cout << "  HP : ";

	for (int i = 0; i < 3 - GameSystem::GetInst()->GetHpPositionValue(); i++)
		cout << " ";

	cout << GameSystem::GetInst()->GetHp() <<'\n';
	Gotoxy(x, y++);
	cout << "  경험치 : " << GameSystem::GetInst()->GetEXP() << '/' << GameSystem::GetInst()->GetTargetEXP() << "         "  << '\n';
	Gotoxy(x, y++);
	cout << " ====================" << '\n';

	y += 5;

	Gotoxy(x, y++);
	cout << " ====================" << '\n';
	Gotoxy(x, y++);
	cout << "  [ 업그레이드 정보 ]" << '\n';
	Gotoxy(x, y++);
	cout << " --------------------" << '\n';
	Gotoxy(x, y++);
	cout << "  1 : " << upgrade->GetUpgradeTypeListText(0) << '\n';
	Gotoxy(x, y++);
	cout << " --------------------" << '\n';
	Gotoxy(x, y++);
	cout << "  2 : " << upgrade->GetUpgradeTypeListText(1) << '\n';
	Gotoxy(x, y++);
	cout << " --------------------" << '\n';
	Gotoxy(x, y++);
	cout << "  3 : " << upgrade->GetUpgradeTypeListText(2) << '\n';
	Gotoxy(x, y++);
	cout << " ====================" << '\n';

	if (isUpgrade)
	{
		Gotoxy(upgradePos.x + 7, upgradePos.y);
		cout << ">";

		Key upgradePointKey = Key::FAIL;

		if(InputCollTime())
			upgradePointKey = KeyController();

		switch (upgradePointKey)
		{
		case Key::UP:
			if (upgradePos.y - 2 >= upgradePosMinY)
			{
				Gotoxy(upgradePos.x + 7, upgradePos.y);
				cout << " ";
				upgradePos.y -= 2;
				upgradeTypeValue--;
			}
			break;
		case Key::DOWN:
			if (upgradePos.y + 2 <= upgradePosMaxY)
			{
				Gotoxy(upgradePos.x + 7, upgradePos.y);
				cout << " ";
				upgradePos.y += 2;
				upgradeTypeValue++;
			}
			break;
		default:
			break;
		}

		
	}
}

bool GameScene::InputCollTime()
{
	if ((clock() - collTimeStart) / CLOCKS_PER_SEC >= coolTime)
	{
		collTimeStart = clock();
		return true;
	}
	return false;
}

void GameScene::SetAllTowerColor(ColorPropertyType type)
{
	for (int i = 0; i < 4; ++i)
		towerList[i]->SetColor(type);
}



