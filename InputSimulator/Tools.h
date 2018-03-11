#pragma once

#include <string>

int StringToNumber(const std::string &Text);
int StringHexToNumber(std::string hexstr);
bool IsHexNotation(std::string const& s);
bool IsInteger(const std::string & s);
void CharArrayCleaner(char *var);