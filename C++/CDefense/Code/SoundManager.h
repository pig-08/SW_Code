#pragma once

#include <string>
#include <Windows.h>
#include <mmsystem.h>
#include "Enums.h"

#pragma comment(lib, "winmm.lib")


class SoundManager
{
private:
    static SoundManager* instance;
    SoundManager() = default;

    std::string GetSFXPath(SFX sfx);
    std::string GetBGMPath(BGM bgm);

    std::string GetExeFolder();
    std::string GetFullPath(const std::string& relativePath);

public:
    static SoundManager* GetInst();

    void PlaySFX(SFX sfx);
    void PlayBGM(BGM bgm);
    void StopBGM();
    void StopAll();
};
