#include "InfoScene.h"
#include "Console.h"
#include "Core.h"
#include "KeyController.h"

void InfoScene::Update()
{
	InfoUpdate();
}

void InfoScene::Render()
{
	InfoRender();
}

void InfoScene::Enter()
{
	isOnText = false;
	system("cls");
}

void InfoScene::Exit()
{

}

void InfoScene::InfoUpdate()
{
	// ESC키를 누르면 다시 TitleScene으로 돌아오게 해보세요
	Key eKey = KeyController();
	if (eKey == Key::ESC)
	{
		system("cls");
		Core::GetInst()->ChangeScene(SceneEnum::TITLE);
	}

}

void InfoScene::InfoRender()
{
	if (isOnText)
	{
		return;
	}
	isOnText = true;

	cout << "ESC를 눌러 타이틀로 돌아가기" << '\n' << '\n';

	cout << "타워 스탯 - 기초 " << '\n';
	cout << "	공격력 - 1" << '\n';
	cout << "	공격 속도 - 2초(최소 1초)								 " << '\n';
	cout << "														 " << '\n';
	cout << "	플레이어 기초 스탯										 " << '\n';
	cout << "	체력 100										   " << '\n';
	cout << "	필요 경험치 - 100										  " << '\n';
	cout << "	처치시 획득 경험치 - 5									 " << '\n';
	cout << "														 " << '\n';
	cout << "	적													" << '\n';
	cout << "	체력 - 5												" << '\n';
	cout << "	이동속도 - 1										   " << '\n';
	cout << "	특이사항 라운드가 지날수록 스탯이 증가 할 예정				   " << '\n';
	cout << "														 " << '\n';
	cout << "	증강													" << '\n';
	cout << "	타워증강 - 타워 스탯 증가					 " << '\n';
	cout << "	전체증강 - 기초 스탯증가, 체력증가		" << '\n';
	cout << "														 " << '\n';
	cout << "	속성													" << '\n';
	cout << "	빨 - 적중시 3칸의 범위까지 데미지  - 좀 범위기 느낌		   " << '\n';
	cout << "	노 - 번개속성으로 관통하며 지나갑니다 - 좀 빠른 관통기		  " << '\n';
	cout << "	초 - 둔화시키고 관통되고 범위넓고 - 관통 cc기				" << '\n';
	cout << "	파 - 얼음으로 둔화 시킴 - cc기							" << '\n';

	cout << '\n'<< "	빨강 - 강점 : 초, 약점 : 파" << '\n';
	cout << "	파랑 - 강점 : 빨, 약점 : 노" << '\n';
	cout << "	노랑 - 강점 : 파, 약점 : 초" << '\n';
	cout << "	초록 - 강점 : 노, 약점 : 빨" << '\n';
}
