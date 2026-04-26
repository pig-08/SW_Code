#include "KeyController.h"
#include<Windows.h>
Key KeyController()
{
	if (GetAsyncKeyState(VK_UP) & 0x8000)
	{
		//Sleep(100);
		return Key::UP;
	}
	if (GetAsyncKeyState(VK_DOWN) & 0x8000)
	{
		//Sleep(100);
		return Key::DOWN;
	}
	
	if (GetAsyncKeyState(VK_SPACE) & 0x8000)
	{
		return Key::SPACE;
	}
	if (GetAsyncKeyState(VK_ESCAPE) & 0x8000)
	{
		//Sleep(100);
		return Key::ESC;
	}
	if (GetAsyncKeyState(0x31) & 0x8000)
	{
		return Key::ONE;
	}
	if (GetAsyncKeyState(0x32) & 0x8000)
	{
		return Key::TWO;
	}
	if (GetAsyncKeyState(0x33) & 0x8000)
	{
		return Key::THREE;
	}
	if (GetAsyncKeyState(0x34) & 0x8000)
	{
		return Key::FOUR;
	}
	if (GetAsyncKeyState(0x51) & 0x8000)
	{//Q
		return Key::Q;
	}
	if (GetAsyncKeyState(0x57) & 0x8000)
	{//W
		return Key::W;
	}
	if (GetAsyncKeyState(0x45) & 0x8000)
	{//E
		return Key::E;
	}
	if (GetAsyncKeyState(0x52) & 0x8000)
	{//R
		return Key::R;
	}
	return Key::FAIL;
}
