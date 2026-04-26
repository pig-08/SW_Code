#pragma once
class Scene
{
public:
	virtual void Update() abstract;
	virtual void Render() abstract;

	virtual void Enter() abstract;
	virtual void Exit() abstract;

};

