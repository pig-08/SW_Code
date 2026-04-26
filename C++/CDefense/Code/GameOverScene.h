#pragma once
#include "Scene.h"
#include "Enums.h"
#include"AsciiObjects.h"
#include<Windows.h>
class GameOverScene : public Scene
{
public:
	virtual void Update() final;
	virtual void Render() final;
	virtual void Enter() final;
	virtual void Exit() final;

	GameOverScene();

private:
	void Animation(COORD _resolution, int _delaytime);
	void RenderUI();
	Menu GetCurrentMenu();
	void GameOverSceneUpdate();

private:
	int coutmode;
	int wcoutmoe;
};

