#pragma once

#include "Single.h"

const int MAP_HEIGHT = 40;
const int MAP_WIDTH = 41;

class MapSystem
{
public:
	static MapSystem* GetInst()
	{
		if (nullptr == m_inst)
		{
			m_inst = new MapSystem;
		}
		return m_inst;
	}
	static void DestroyInst()
	{
		SAFE_DELETE(m_inst)
	}

	char gameMap[MAP_HEIGHT][MAP_WIDTH];
	char gameColorMap[MAP_HEIGHT][MAP_WIDTH];

private:
	static MapSystem* m_inst;
};
