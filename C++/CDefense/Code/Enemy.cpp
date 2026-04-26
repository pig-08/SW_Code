#include "Enemy.h"
#include "Console.h"
#include "MapSystem.h"
#include "Enums.h"
#include "SoundManager.h"

Enemy::Enemy(POS Pos, EnemyProperty property)
{
	enemyPos = Pos;
	isDie = false;
	moveStartTime = 0;
	delayTime = 1.0f;
	enemyProperty = property;
	hitStartTime = clock();
}

void Enemy::EnemyUpdate()
{
	if (EnemytHit())
	{
		EnemyDie(true);
		
		return;
	}

	if (isSlow && (clock() - slowStartTime) / CLOCKS_PER_SEC >= slowTime)
	{
		speed += slowValue;
		isSlow = false;
	}

	Gotoxy(enemyPos.x * 2, enemyPos.y);
	SetColor(enemyProperty.colorType);
	cout << "¢Ý";
	SetColor();

	if (MoveDelayEnd())
	{
		Gotoxy(enemyPos.x * 2, enemyPos.y);
		cout << "  ";
		Gotoxy(enemyPos.x * 2, ++enemyPos.y);
		SetColor(enemyProperty.colorType);
		cout << "¢Ý";
		SetColor();
	}

} 

void Enemy::EnemyStatUp()
{
	for(int i = 0; i < GameSystem::GetInst()->GetRound()-1; ++i)
		hp *= 1.2f;
}

POS Enemy::GetEnemyPos()
{
	return enemyPos;
}

bool Enemy::GetIsDie()
{
	return isDie;
}

bool Enemy::EnemytHit()
{
	return enemyPos.y + 1 >= MAP_HEIGHT - 6;
}

void Enemy::EnemyHitDamage(int damage, ColorPropertyType bulletType)
{
	if ((char)bulletType == (char)enemyProperty.weaknessColor)
	{
		damage = (damage / 2) <= 0 ? 1 : damage / 2;
		hp -= damage;
	}
	else if ((char)bulletType == (char)enemyProperty.strengthColor)
		hp -= (damage * 2);
	else
		hp -= damage;

	SoundManager::GetInst()->PlaySFX(SFX::BRUSH);

	if (hp <= 0)
	{
		EnemyDie(false);
	}
}

bool Enemy::MoveDelayEnd()
{
	if ((clock() - moveStartTime) / CLOCKS_PER_SEC >= delayTime - speed)
	{
		moveStartTime = clock();
		return true;
	}
	return false;
}

bool Enemy::HitDelayEnd(bool check)
{
	if ((clock() - hitStartTime) / CLOCKS_PER_SEC >= hitDelayTime)
	{
		if(check == false)
			hitStartTime = clock();
		return true;
	}
	return false;
}

void Enemy::EnemyDie(bool isEndPoint)
{
	if (isDie) return;

	if(isEndPoint)
	{
		SoundManager::GetInst()->PlaySFX(SFX::HIT);
		GameSystem::GetInst()->SetHp(-hp);
	}
	else
	{
		GameSystem::GetInst()->SetEXP(5);
	}

	Gotoxy(enemyPos.x * 2, enemyPos.y);
	cout << "  ";
	isDie = true;
}


void Enemy::SlowEnemy(float setSlowValue, float SetSlowTime)
{
	isSlow = true;
	slowValue = setSlowValue;
	speed -= setSlowValue;
	slowTime = SetSlowTime;
	slowStartTime = clock();
	Gotoxy(0, 0);
}