#include "GameOverScene.h"
#include "Core.h"
#include "Console.h"
#include"KeyController.h"
#include<io.h>
#include<fcntl.h>
#include "Enums.h"
#include "GameScene.h"
#include "SoundManager.h"

void GameOverScene::Update()
{
	GameOverSceneUpdate();
}

void GameOverScene::Render()
{
	RenderUI();
}

void GameOverScene::Enter()
{
	SoundManager::GetInst()->StopAll();
	SoundManager::GetInst()->PlayBGM(BGM::OVERBGM);
	SoundManager::GetInst()->PlaySFX(SFX::DIE);
	COORD resolution = GetConsoleResolution();
	int delaytime = 1;
	Animation(resolution, delaytime);
	system("cls");
}

void GameOverScene::Exit()
{
	SoundManager::GetInst()->StopAll();
}

GameOverScene::GameOverScene()
{
}

void GameOverScene::Animation(COORD _resolution, int _delaytime)
{
}

void GameOverScene::RenderUI()
{

	COORD resolution = GetConsoleResolution();
	int y = resolution.Y / 6;
	Gotoxy(0, y);
	coutmode = _setmode(_fileno(stdout), _O_U16TEXT);

	wcout << L"                                        ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  					 " << '\n';
	wcout << L"                                       ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒					" << '\n';
	wcout << L"                                      ▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒				   " << '\n';
	wcout << L"                                      ░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  					" << '\n';
	wcout << L"                                      ░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒				" << '\n';
	wcout << L"                                       ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░					   " << '\n';
	wcout << L"                                        ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░							  " << '\n';
	wcout << L"                                      ░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 								   " << '\n';
	wcout << L"                                            ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     									  " << '\n';
                                                                      												
	
	wcoutmoe = _setmode(_fileno(stdout), coutmode);

	int x = resolution.X / 2.11;
	y = resolution.Y / 3 * 2;
	Gotoxy(x, y);
	cout << "다시 하기";
	Gotoxy(x, y + 1);
	cout << "게임 종료";
}

Menu GameOverScene::GetCurrentMenu()
{

	Key eKey = KeyController();
	COORD resolution = GetConsoleResolution();
	int x = resolution.X / 2.11;
	static int y = resolution.Y / 3 * 2;
	static int originy = y;
	Gotoxy(x - 2, y);
	cout << ">";
	switch (eKey)
	{
	case Key::UP:
		if (y > originy)
		{
			// 커서를 이동
			Gotoxy(x - 2, y);
			cout << " ";
			Gotoxy(x - 2, --y);
			cout << ">";
			Sleep(80);
			// > (화살표 찍고)
			// 공백도 찍어
		}
		break;
	case Key::DOWN:
		if (y < originy + 1)
		{
			// 커서를 이동
			Gotoxy(x - 2, y);
			cout << " ";
			Gotoxy(x - 2, ++y);
			cout << ">";
			Sleep(80);

			//////
		}
		break;
	case Key::SPACE:
	{
		if (originy == y) return Menu::START;
		else if (originy + 1 == y) return Menu::QUIT;
	}
	break;
	}
	return Menu::FAIL;
}

void GameOverScene::GameOverSceneUpdate()
{

	Menu eMenu = GetCurrentMenu();
	switch (eMenu)
	{
	case Menu::START:
		Core::GetInst()->ChangeScene(SceneEnum::TITLE);
		break;
	case Menu::QUIT:
		Core::GetInst()->ChangeScene(SceneEnum::QUIT);
		break;
	}
}
