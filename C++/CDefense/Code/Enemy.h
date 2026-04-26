#pragma once
#include "Console.h"
#include "GameSystem.h"
#include "Enums.h"

typedef struct EnemyProperty
{
	ColorPropertyType strengthColor; //강점
	ColorPropertyType weaknessColor; //약점
	COLOR colorType;

}ENEMYPROPERTY, * PENEMYPROPERTY;;

class Enemy
{
	public:
		Enemy(POS Pos, EnemyProperty property);
		void EnemyUpdate();
		void EnemyStatUp();
		POS GetEnemyPos();
		
		bool GetIsDie();
		bool EnemytHit();
		bool HitDelayEnd(bool check);

		void EnemyHitDamage(int damage, ColorPropertyType bulletTyp);
		void SlowEnemy(float slowValue, float SetSlowTime);

	private:

		bool MoveDelayEnd();

		void EnemyDie(bool isEndPoint);

		float moveStartTime;
		float hitStartTime;

		float delayTime;

		bool isMaxSpeed = true;

		float speed = 0.1f;

		float hitDelayTime = 0.7f;

		bool isDie;
		int hp = 5;
		POS enemyPos;

		float slowValue = 0;
		float slowStartTime;
		float slowTime;
		bool isSlow = false;
		
		EnemyProperty enemyProperty;

};


