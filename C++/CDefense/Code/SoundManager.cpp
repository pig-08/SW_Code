#include "SoundManager.h"
#include <string>
#include "Enums.h"

SoundManager* SoundManager::instance = nullptr;

SoundManager* SoundManager::GetInst()
{
    if (instance == nullptr)
        instance = new SoundManager();
    return instance;
}

std::string SoundManager::GetExeFolder()
{
    char path[MAX_PATH];
    GetModuleFileNameA(NULL, path, MAX_PATH);
    std::string fullPath(path);
    size_t pos = fullPath.find_last_of("\\/");
    return (pos == std::string::npos) ? "" : fullPath.substr(0, pos);
}

std::string SoundManager::GetFullPath(const std::string& relativePath)
{
    std::string basePath = GetExeFolder();
    if (basePath.empty())
        return relativePath;
    return basePath + "\\" + relativePath;
}

std::string SoundManager::GetSFXPath(SFX sfx)
{
    switch (sfx)
    {
    case SFX::FIRE:    return "sounds/Fire.wav";
    case SFX::BRUSH:   return "sounds/Paint.wav";
    case SFX::HIT:     return "sounds/Hit.wav";
    case SFX::DIE:     return "sounds/Die.wav";
    case SFX::UPGRADE: return "sounds/Upgrade.wav";
    default:           return "";
    }
}

std::string SoundManager::GetBGMPath(BGM bgm)
{
    switch (bgm)
    {
    case BGM::TITLEBGM: return "sounds/Title.wav";
    case BGM::GAMEBGM:  return "sounds/Game.wav";
    case BGM::OVERBGM:  return "sounds/GameOver.wav";
    default:           return "";
    }
}

void SoundManager::PlaySFX(SFX sfx)
{
    std::string relativePath = GetSFXPath(sfx);
    std::string path = GetFullPath(relativePath);

    if (!path.empty())
    {
        static int sfxCount = 0;
        std::string alias = "sfx" + std::to_string(sfxCount++);

        std::string openCmd = "open \"" + path + "\" type waveaudio alias " + alias;
        MCIERROR err = mciSendStringA(openCmd.c_str(), NULL, 0, NULL);
        if (err != 0)
        {
            char errorMsg[256];
            mciGetErrorStringA(err, errorMsg, 256);
            MessageBoxA(NULL, errorMsg, "MCI OPEN ERROR", MB_OK);
            return;
        }

        std::string playCmd = "play " + alias + " from 0";
        err = mciSendStringA(playCmd.c_str(), NULL, 0, NULL);
        if (err != 0)
        {
            char errorMsg[256];
            mciGetErrorStringA(err, errorMsg, 256);
            MessageBoxA(NULL, errorMsg, "MCI PLAY ERROR", MB_OK);
            return;
        }

    }
}

void SoundManager::PlayBGM(BGM bgm)
{
    std::string relativePath = GetBGMPath(bgm);
    std::string path = GetFullPath(relativePath);

    if (!path.empty())
    {
        mciSendStringA("close bgm", NULL, 0, NULL);

        std::string openCmd = "open \"" + path + "\" type mpegvideo alias bgm";
        MCIERROR err = mciSendStringA(openCmd.c_str(), NULL, 0, NULL);
        if (err != 0)
        {
            char errorMsg[256];
            mciGetErrorStringA(err, errorMsg, 256);
            MessageBoxA(NULL, errorMsg, "MCI OPEN ERROR", MB_OK);
            return;
        }

        std::string playCmd = "play bgm repeat";
        mciSendStringA(playCmd.c_str(), NULL, 0, NULL);
    }
}

void SoundManager::StopBGM()
{
    mciSendStringA("stop bgm", NULL, 0, NULL);
    mciSendStringA("close bgm", NULL, 0, NULL);
}

void SoundManager::StopAll()
{
    StopBGM();
}
