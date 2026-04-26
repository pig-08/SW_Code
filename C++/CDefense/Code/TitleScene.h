#pragma once
#include "Scene.h"
#include "Enums.h"
#include"AsciiObjects.h"
#include<Windows.h>

class TitleScene : public Scene
{
public:
	virtual void Update() final;
	virtual void Render() final;
	virtual void Enter() final;
	virtual void Exit() final;


	TitleScene();

private:
	AsciiObjects _objs;
	void TitleSceneUpdate();
	void TitleSceneRender();
	Menu GetCurrentMenu();
	void EnterAnimation();
	void CrossAnimation(COORD _resolution, int _delaytime);

private:
	int coutmode;
	int wcoutmoe;
};

