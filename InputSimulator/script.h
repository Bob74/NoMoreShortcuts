/*
	THIS FILE IS A PART OF GTA V SCRIPT HOOK SDK
				http://dev-c.com
			(C) Alexander Blade 2015
*/

#pragma once

#include <string>
#include "inc\natives.h"
#include "inc\types.h"
#include "inc\enums.h"

#include "inc\main.h"

void ScriptMain();
void update();
void log(std::string);
void resetLog();
std::string CurrentPath();
