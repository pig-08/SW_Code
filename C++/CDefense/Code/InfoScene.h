#pragma once
#include "Scene.h"

class InfoScene : public Scene
{
public:
	virtual void Update() final;

	virtual void Render() final;
	virtual void Enter() final;
	virtual void Exit() final;
private:
	bool isOnText;
	void InfoUpdate();
	void InfoRender();
};

