#include "TitleScene.h"
#include "Core.h"
#include "Console.h"
#include"KeyController.h"
#include<io.h>
#include<fcntl.h>
#include "Enums.h"
#include "GameScene.h"
#include "SoundManager.h"

void TitleScene::TitleSceneUpdate()
{
	Menu eMenu = GetCurrentMenu();
	switch (eMenu)
	{
	case Menu::START:	
		EnterAnimation();
		Core::GetInst()->ChangeScene(SceneEnum::GAME);
		break;
	case Menu::INFO:
		Core::GetInst()->ChangeScene(SceneEnum::INFO);
		break;
	case Menu::QUIT:
		Core::GetInst()->ChangeScene(SceneEnum::QUIT);
		break;
	}
}

void TitleScene::TitleSceneRender()
{
	COORD resolution = GetConsoleResolution();
	int y = resolution.Y / 6;
	Gotoxy(0, y);
	coutmode = _setmode(_fileno(stdout), _O_U16TEXT);

	wcout << L"                     ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄   ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄        ▄  ▄▄▄▄▄▄▄▄▄▄▄  ▄▄▄▄▄▄▄▄▄▄▄" << '\n';
	wcout << L"                    ▐░░░░░░░░░░░▌▐░░░░░░░░░░▌ ▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░▌      ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌" << '\n';
	wcout << L"                    ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░▌░▌     ▐░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀" << '\n';
	wcout << L"                    ▐░▌          ▐░▌       ▐░▌▐░▌          ▐░▌          ▐░▌          ▐░▌▐░▌    ▐░▌▐░▌          ▐░▌" << '\n';
	wcout << L"                    ▐░▌          ▐░▌       ▐░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄ ▐░▌ ▐░▌   ▐░▌▐░▌          ▐░█▄▄▄▄▄▄▄▄▄" << '\n';
	wcout << L"                    ▐░▌          ▐░▌       ▐░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌▐░▌  ▐░▌  ▐░▌▐░▌          ▐░░░░░░░░░░░▌" << '\n';
	wcout << L"                    ▐░▌          ▐░▌       ▐░▌▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░█▀▀▀▀▀▀▀▀▀ ▐░▌   ▐░▌ ▐░▌▐░▌          ▐░█▀▀▀▀▀▀▀▀▀" << '\n';
	wcout << L"                    ▐░▌          ▐░▌       ▐░▌▐░▌          ▐░▌          ▐░▌          ▐░▌    ▐░▌▐░▌▐░▌          ▐░▌" << '\n';
	wcout << L"                    ▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░▌          ▐░█▄▄▄▄▄▄▄▄▄ ▐░▌     ▐░▐░▌▐░█▄▄▄▄▄▄▄▄▄ ▐░█▄▄▄▄▄▄▄▄▄" << '\n';
	wcout << L"                    ▐░░░░░░░░░░░▌▐░░░░░░░░░░▌ ▐░░░░░░░░░░░▌▐░▌          ▐░░░░░░░░░░░▌▐░▌      ▐░░▌▐░░░░░░░░░░░▌▐░░░░░░░░░░░▌" << '\n';
	wcout << L"                     ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀   ▀▀▀▀▀▀▀▀▀▀▀  ▀            ▀▀▀▀▀▀▀▀▀▀▀  ▀        ▀▀  ▀▀▀▀▀▀▀▀▀▀▀  ▀▀▀▀▀▀▀▀▀▀▀" << '\n';
	
	wcoutmoe = _setmode(_fileno(stdout), coutmode);

	int x = resolution.X / 2.11;
	y = resolution.Y / 3 * 2;
	Gotoxy(x, y);
	cout << "게임 시작";
	Gotoxy(x, y + 1);
	cout << "게임 정보";
	Gotoxy(x, y + 2);
	cout << "게임 종료";
}

Menu TitleScene::GetCurrentMenu()
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
		if (y < originy + 2)
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
		else if (originy + 1 == y) return Menu::INFO;
		else if (originy + 2 == y) return Menu::QUIT;
	}
	break;
	}
	return Menu::FAIL;
}

void TitleScene::EnterAnimation()
{
	COORD resolution = GetConsoleResolution();
	int delaytime = 1;
	CrossAnimation(resolution, delaytime);
	system("cls");
}
void TitleScene::CrossAnimation(COORD resolution, int delaytime)
{
	SoundManager::GetInst()->StopBGM();
	COLOR paintColors[] = {
	   COLOR::LIGHT_VIOLET, COLOR::LIGHT_BLUE, COLOR::MINT,
	   COLOR::LIGHT_GREEN, COLOR::LIGHT_YELLOW, COLOR::LIGHT_RED,
	   COLOR::VOILET, COLOR::BLUE, COLOR::SKYBLUE,
	   COLOR::GREEN, COLOR::YELLOW, COLOR::RED
	};
	int colorCount = 12;

	system("cls");
	//	너무 랜덤이면 이상해보이기 때문에,화면을 최대한 채울수 있도록, 위3칸 아래 3칸 나눠서 골고루 할 수 있게 화면을 나눔
	int bucketX[6], bucketY[6];
	// 윗줄 3개
	bucketX[0] = resolution.X / 8;     bucketY[0] = resolution.Y / 8;
	bucketX[1] = resolution.X / 2;     bucketY[1] = resolution.Y / 8;
	bucketX[2] = resolution.X * 3 / 4; bucketY[2] = resolution.Y / 8;
	// 아랫즐 3개
	bucketX[3] = resolution.X / 8;     bucketY[3] = resolution.Y * 2 / 3;
	bucketX[4] = resolution.X / 2;     bucketY[4] = resolution.Y * 2 / 3;
	bucketX[5] = resolution.X * 3 / 4; bucketY[5] = resolution.Y * 2 / 3;

	// 자연스러워 보일려고 오프셋 추가
	for (int i = 0; i < 6; i++) {
		bucketX[i] += rand() % (resolution.X / 6);
		bucketY[i] += rand() % (resolution.Y / 6);
	}

	for (int cycle = 0; cycle < 2; cycle++) // 테스트를 해봤는데 2번이 제일 적당 
	{
		
		for (int bucket = 0; bucket < 6; bucket++) 
		{
			// 최대한 겹치지 않게 , 몇번 할건지에 맞춰서
			COLOR currentColor = paintColors[(cycle * 6 + bucket) % colorCount];
			SetColor(COLOR::BLACK, currentColor);

			int centerX = bucketX[bucket]; //각 그릴것의 중심 좌표
			int centerY = bucketY[bucket]; //각 그릴것의 중심 좌표
			int maxRadius = 25 + (rand() % 16); //너무 작거나 크지 않으면서 랜덤적으로 나오게 하기 위해

			// 
			for (int r = 0; r <= maxRadius; r += 5) // 중심부터 5칸씩 퍼지게 하고 싶어서
			{
				for (int angle = 0; angle < 360; angle += 12) //점들을 각도로 나누면서 원을 제작
				{
					double radian = angle * 3.14159 / 180.0;
					int x = centerX + (int)(r * cos(radian));
					int y = centerY + (int)(r * sin(radian) * 0.6); // 너무 둥글면 괴기해 보여서 수정 + 페인트처럼 보이는게 목적이라 수정

					//최대한 화면을 꽉 채우고 싶어서, 
					if (x >= 0 && x < resolution.X && y >= 0 && y < resolution.Y) {
						Gotoxy(x, y);
						cout << " ";

						//
						for (int dx = -3; dx <= 3; dx++) {
							for (int dy = -2; dy <= 2; dy++) {
								int fillX = x + dx, fillY = y + dy;
								if (fillX >= 0 && fillX < resolution.X &&fillY >= 0 && fillY < resolution.Y &&rand() % 100 < 80) { //그려지는 물감의 느낌을 살리기 위해서 군데군데 덜 그려지게 
									Gotoxy(fillX, fillY);
									cout << " ";
								}
							}
						}
					}
				}
				
				Sleep(max(1, delaytime / 450)); // 그리기 속도 조절
			}
						SoundManager::GetInst()->PlaySFX(SFX::BRUSH);
		}

		Sleep(max(2, delaytime / 250)); // 사이클 간 대기

		// 다음 사이클에 그려질때 같은 위치면 안되기 때문에, 어짜피 2번이라서 임의로 다른 위치로 설정
		for (int i = 0; i < 6; i++) {
			int zone = (i + cycle) % 6;
			switch (zone) {
			case 0: bucketX[i] = resolution.X / 12;      bucketY[i] = resolution.Y / 12; break;
			case 1: bucketX[i] = resolution.X * 3 / 8;   bucketY[i] = resolution.Y / 12; break;
			case 2: bucketX[i] = resolution.X * 5 / 8;   bucketY[i] = resolution.Y / 12; break;
			case 3: bucketX[i] = resolution.X / 12;      bucketY[i] = resolution.Y * 5 / 8; break;
			case 4: bucketX[i] = resolution.X * 3 / 8;   bucketY[i] = resolution.Y * 5 / 8; break;
			case 5: bucketX[i] = resolution.X * 5 / 8;   bucketY[i] = resolution.Y * 5 / 8; break;
			}
			// 똑같이 랜덤 오프셋
			bucketX[i] += rand() % (resolution.X / 4);
			bucketY[i] += rand() % (resolution.Y / 4);

			
		}
	}

	SetColor(); // 색상 초기화
	system("cls"); // 화면 지우기
}


void TitleScene::Update()
{
	TitleSceneUpdate();
	ObjectUpdate(_objs);

}

void TitleScene::Render()
{
	//TitleSceneRender();
	ObjectRender(_objs);
}

void TitleScene::Enter()
{
	TitleSceneRender();
	SoundManager::GetInst()->PlayBGM(BGM::TITLEBGM);
}

void TitleScene::Exit()
{
	_setmode(_fileno(stdout), coutmode);
	SoundManager::GetInst()->StopBGM();
}

TitleScene::TitleScene()
{
	ObjectInit(_objs);
}
