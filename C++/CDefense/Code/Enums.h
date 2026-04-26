#pragma once

enum class Menu
{
	START,INFO,QUIT, FAIL, END
};
enum class Key
{
	UP, DOWN, SPACE, 
	ESC, ONE, TWO, THREE, FOUR,
	Q, W, E, R, FAIL ,END
};
enum class SceneEnum
{
	TITLE, GAME, INFO, QUIT, OVER, END
};
enum class ColorPropertyType
{
	RED, YELLOW, GREEN, BLUE, END
};

enum class Tile
{
	WALL = '0', START, ROAD , LONG, SHORT, TEST
};

enum class UpgradeType
{
	DMG, SPEED, HP, EXP, END
};

enum class SFX 
{
  FIRE,BRUSH,HIT,DIE,UPGRADE
};

enum class BGM
{
	TITLEBGM,GAMEBGM,OVERBGM
};