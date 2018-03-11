#include <sstream>
#include "Tools.h"

using namespace std;

// http://www.cplusplus.com/forum/articles/9645/
int StringToNumber(const string &Text)	//Text not by const reference so that the function can be used with a character array as argument
{
	stringstream ss(Text);
	int result;
	return ss >> result ? result : 0;
}

// https://helloacm.com/cc-function-to-convert-hex-string-to-decimal-number/
int StringHexToNumber(string hexstr)
{
	return (int)strtol(hexstr.c_str(), 0, 16);
}

// https://stackoverflow.com/questions/8899069/how-to-find-if-a-given-string-conforms-to-hex-notation-eg-0x34ff-without-regex
bool IsHexNotation(string const& s)
{
	return (s.compare(0, 2, "0x") == 0 || s.compare(0, 2, "0X") == 0)
		&& s.size() > 2
		&& s.find_first_not_of("0123456789abcdefABCDEF", 2) == string::npos;
}

// https://stackoverflow.com/questions/4654636/how-to-determine-if-a-string-is-a-number-with-c
bool IsNumber(const string& s)
{
	string::const_iterator it = s.begin();
	while (it != s.end() && isdigit(*it)) ++it;
	return !s.empty() && it == s.end();
}

// https://stackoverflow.com/questions/2844817/how-do-i-check-if-a-c-string-is-an-int
bool IsInteger(const string & s)
{
	if (s.empty() || ((!isdigit(s[0])) && (s[0] != '-') && (s[0] != '+'))) return false;

	char * p;
	strtol(s.c_str(), &p, 10);

	return (*p == 0);
}

// https://stackoverflow.com/questions/632846/clearing-a-char-array-c
void CharArrayCleaner(char *var)
{
	int i = 0;
	while (var[i] != '\0') {
		var[i] = '\0';
		i++;
	}
}