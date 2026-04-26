#include "Core.h"
#include "Console.h"

Core* Core::m_inst = new Core();
int count;
void Core::Run()
{
	Init();

	while (currScene != nullptr && isGameEnd == false)
	{
		Update();
		Render();
	}
}

void Core::Update()
{
	currScene->Update();
}

void Core::Render()
{
	currScene->Render();
}


void Core::Init()
{
	gameScene = GameScene();
	titleScene = TitleScene();
	infoScene = InfoScene();
	gameOverScene = GameOverScene();

	SetConsoleSettings(1100, 800, false, TEXT("CD"));
	SetCursorVisual(false, 10);
	ChangeScene(SceneEnum::TITLE);
}

void Core::ChangeScene(SceneEnum sceneType)
{
	if(currScene != nullptr)
		currScene->Exit();

	switch (sceneType)
	{
	case SceneEnum::TITLE:
		currScene = &titleScene;
		break;
	case SceneEnum::GAME:
		currScene = &gameScene;
		break;
	case SceneEnum::INFO:
		currScene = &infoScene;
		break;
	case SceneEnum::OVER:
		currScene = &gameOverScene;
		break;
	case SceneEnum::QUIT:
	{
		isGameEnd = true;
		break;
	}
	}

	if (currScene != nullptr)
		currScene->Enter();
}
