#pragma once

#include "Tower.h"
#include "Enums.h"
#include <string>

class Upgrade
{
public:
	Upgrade();
	void NewUpgrade();
	void NewColorUpgrade();

	void DefaultUpgradeType();
	void DefaultColorUpgradeType();

	UpgradeType GetUpgradeType(int index);
	std::string GetUpgradeTypeListText(int index);

private:
	UpgradeType upgradeTypeList[3];
	ColorPropertyType colorUpgradeTypeList[3];
};