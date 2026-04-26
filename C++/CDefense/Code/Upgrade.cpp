
#include "Upgrade.h"
#include "Console.h"

Upgrade::Upgrade()
{
	DefaultUpgradeType();
}

void Upgrade::NewUpgrade()
{
	srand((unsigned int)time(NULL));
	for (int i = 0; i < 3; i++)
	{
		int rnadType = rand() % (int)UpgradeType::END;
		upgradeTypeList[i] = (UpgradeType)rnadType;
	}
}

void Upgrade::NewColorUpgrade()
{
	srand((unsigned int)time(NULL));
	for (int i = 0; i < 3; i++)
	{
		int rnadType = rand() % (int)ColorPropertyType::END;
		colorUpgradeTypeList[i] = (ColorPropertyType)rnadType;
	}
}

void Upgrade::DefaultUpgradeType()
{
	for (int i = 0; i < 3; i++)
		upgradeTypeList[i] = UpgradeType::END;
}

void Upgrade::DefaultColorUpgradeType()
{
	for (int i = 0; i < 3; i++)
		colorUpgradeTypeList[i] = ColorPropertyType::END;
}

UpgradeType Upgrade::GetUpgradeType(int index)
{
	return upgradeTypeList[index];
}

std::string Upgrade::GetUpgradeTypeListText(int index)
{
	switch (upgradeTypeList[index])
	{
	case UpgradeType::DMG:
		return "데미지 증가";
		break;
	case UpgradeType::SPEED:
		return "발사 속도 감소";
		break;
	case UpgradeType::EXP:
		return "레벨업까지 필요 경험치량 감소";
		break;
	case UpgradeType::HP:
		return "체력증가";
		break;
	default:
		return "                              ";
		break;
	}

}

