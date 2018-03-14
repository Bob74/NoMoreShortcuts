#pragma once

#include <string>
#include "inc\natives.h"
#include "inc\types.h"
#include "inc\enums.h"

#include "inc\main.h"

void ScriptMain();
void update();
void SetupPipe();
void log(std::string);
void resetLog();
std::string CurrentPath();
