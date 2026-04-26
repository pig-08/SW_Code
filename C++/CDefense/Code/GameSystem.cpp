#include "Console.h"
#include "GameSystem.h"

GameSystem* GameSystem::m_inst = new GameSystem();

void GameSystem::SetDefaultValue()
{
    round = 1;
    hp = 100;
    EXP = 0;
    targetEXP = 100;
    hpPositionValue = 3;
    enemyCount = 5;
}

int GameSystem::GetRound()
{
    return round;
}

void GameSystem::SetRound(int value)
{
    round += value;
}

int GameSystem::GetEnemyCount()
{
    return enemyCount;
}

int GameSystem::GetHp()
{
    return hp;
}

void GameSystem::SetHp(int value)
{
    hp += value;

    string temp = std::to_string(hp);
    hpPositionValue = temp.size();
}

int GameSystem::GetHpPositionValue()
{
    return hpPositionValue;
}

int GameSystem::GetEXP()
{
    return EXP;
}

void GameSystem::SetEXP(int value)
{
    EXP += value;
}

int GameSystem::GetTargetEXP()
{
    return targetEXP;
}

void GameSystem::SetTargetEXP(float value, bool isPlus)
{
    if (isPlus == false)
        targetEXP *= value;
    else
        targetEXP += value;
}
