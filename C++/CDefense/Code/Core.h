#pragma once


#include "Single.h"
#include "Scene.h"
#include "Enums.h"
#include "TitleScene.h"
#include "GameScene.h"
#include "InfoScene.h"
#include "GameOverScene.h"

class Core
{
	public:
		Scene* currScene;
		void ChangeScene(SceneEnum sceneType);

		static Core* GetInst()
		{
			if (nullptr == m_inst)
			{
				m_inst = new Core;
			}
			return m_inst;
		}
		static void DestroyInst()
		{
			SAFE_DELETE(m_inst)
		}

		void Run();

	private:
		GameScene gameScene;
		TitleScene titleScene;
		InfoScene infoScene;
		GameOverScene gameOverScene;
	private:
		bool isGameEnd = false;
		static Core* m_inst;
		void Init();
		void Update();
		void Render();
};


