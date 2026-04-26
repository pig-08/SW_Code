#pragma once

#include "Single.h"

class GameSystem
{
public:

	void SetDefaultValue();

	int GetRound();
	void SetRound(int value);

	int GetEnemyCount();

	int GetHp();
	void SetHp(int value);

	int GetHpPositionValue();

	int GetEXP();
	void SetEXP(int value);

	int GetTargetEXP();
	void SetTargetEXP(float value, bool isPlus = false);

	static GameSystem* GetInst()
	{
		if (nullptr == m_inst)
		{
			m_inst = new GameSystem;
		}
		return m_inst;
	}
	static void DestroyInst()
	{
		SAFE_DELETE(m_inst)
	}

private:
	static GameSystem* m_inst;
	int round = 1;
	int enemyCount = 5; // 1라인의 나오는 적 수

	int EXP = 0;
	int targetEXP = 100;

	int hpPositionValue = 3;
	int hp = 100;
};

typedef struct _pos
{
	int x;
	int y;
	bool operator == (const _pos& other) const
	{
		return (x == other.x && other.y == y);
	}
}POS, * PPOS;