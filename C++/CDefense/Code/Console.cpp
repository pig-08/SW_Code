#include "Console.h"

void SetConsoleSettings(int _width, int _height, bool _isFullScreen, const std::wstring& _title)
{
	// 제목 설정
	SetConsoleTitle(_title.c_str());

	HWND hwnd = GetConsoleWindow();
	// 해상도 관련
	if (_isFullScreen)
	{
		// 위에 타이틀바 제거
		SetWindowLong(hwnd, GWL_STYLE, WS_POPUP);
		// 전체 화면
		ShowWindow(hwnd, SW_MAXIMIZE);
	}
	else
	{
		//// 타이틀바 제거
		//LONG style = GetWindowLong(hwnd, GWL_STYLE);
		//style &= ~WS_CAPTION; 		// NAND
		//SetWindowLong(hwnd, GWL_STYLE, style);
		// 해상도 설정
		MoveWindow(hwnd, 0,0, _width, _height, true);
	}

}

void SetLockResize()
{
	HWND hwnd = GetConsoleWindow();
	LONG style = GetWindowLong(hwnd, GWL_STYLE);
	style &= ~WS_MAXIMIZEBOX & ~ WS_SIZEBOX; // NAND
	SetWindowLong(hwnd, GWL_STYLE, style);
}

COORD GetConsoleResolution()
{
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	CONSOLE_SCREEN_BUFFER_INFO buf;
	GetConsoleScreenBufferInfo(handle, &buf);
	short width  = buf.srWindow.Right  - buf.srWindow.Left + 1;
	short height = buf.srWindow.Bottom - buf.srWindow.Top  + 1;
	//COORD crd = { width, height };
	return {width, height};
	//return COORD{width, height};
}

void Gotoxy(int _x, int _y)
{
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	COORD Cur = { _x, _y };
	//Cur.X = _x;
	//Cur.Y = _y;
	SetConsoleCursorPosition(handle, Cur);
}

BOOL IsGotoxy(int _x, int _y)
{
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	COORD Cur = { _x, _y };
	return SetConsoleCursorPosition(handle, Cur);
}


COORD CursorPos()
{
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	CONSOLE_SCREEN_BUFFER_INFO buf;
	// *: 포인터로도 + 역참조연산자
	// &: 참조연산자 + 주소연산자
	GetConsoleScreenBufferInfo(handle, &buf);
	return buf.dwCursorPosition;
}

void SetCursorVisual(bool _isVis, DWORD _size)
{
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	CONSOLE_CURSOR_INFO curInfo;
	curInfo.dwSize = _size; // 1 ~ 100
	curInfo.bVisible = _isVis; // on, off
	SetConsoleCursorInfo(handle, &curInfo);
}

void SetColor(COLOR _textcolor, COLOR _bgcolor)
{
	int textcolor = (int)_textcolor;
	int bgcolor = (int)_bgcolor;
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	SetConsoleTextAttribute(handle,
						(bgcolor << 4) | textcolor);
}

void FrameSync(unsigned int _frame)
{
	clock_t oldtime, curtime;
	oldtime = clock(); // ms
	while (true)
	{
		curtime = clock();
		if (curtime - oldtime > 1000 / _frame)
		{
			oldtime = curtime;
			break;
		}
	}
}
void SetConsoleFont(LPCWSTR fontname, COORD size, UINT weight)
{
	// 콘솔 핸들
	HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	// 구조체 초기화
	CONSOLE_FONT_INFOEX cf = {};
	cf.cbSize = sizeof(CONSOLE_FONT_INFOEX);
	// 현재 폰트 정보 Get
	GetCurrentConsoleFontEx(handle, false, &cf);
	cf.dwFontSize = size;      // 폭, 높이
	cf.FontWeight = weight;    // FW~
	wcscpy_s(cf.FaceName, fontname); // 폰트이름 복사
	// 폰트 정보 Set
	SetCurrentConsoleFontEx(handle, false, &cf);
}