

#include <iostream>
#include <iomanip>
#include <sstream>
#include <chrono>
#include <ctime>
#include <process.h>
#include <fstream>

#include "keyboard.h"
#include "script.h"
#include "Tools.h"

using namespace std;

ofstream logFile;
string logFilePath = CurrentPath() + "\\InputSimulator.log";

fstream exchangeFile;
string exchangeFilePath = CurrentPath() + "\\NoMoreShortcuts.tmp";


void main()
{
	resetLog();

	while (true)
	{
		update();
		WAIT(100);
	}
}

void ScriptMain()
{
	main();
}

void update()
{
	// Check if the file can be opened
	if (std::ifstream(exchangeFilePath))
	{
		exchangeFile.open(exchangeFilePath);
		
		// Reading file content
		char buffer[128];
		exchangeFile.read(buffer, 128);
		exchangeFile.close();

		// Removing file
		remove(exchangeFilePath.c_str());

		// Sending keys sequence
		string keys = buffer;
		PressKey(keys);

		// DEBUG
		//log("[DEBUG] Key pressed: " + keys);
		
		// Reset buffer's memory location
		CharArrayCleaner(buffer);
	}
}

// Append text to the log file
void log(string text)
{
	// https://stackoverflow.com/questions/16357999/current-date-and-time-as-string/16358111
	time_t t = std::time(nullptr);
	tm tm;
	localtime_s(&tm, &t);

	std::ostringstream oss;
	oss << std::put_time(&tm, "%d-%m-%Y %H-%M-%S");
	auto timeStr = oss.str();
	
	logFile.open(logFilePath, std::ios_base::app);
	logFile << "[" << timeStr << "] " << text << endl;
	logFile.close();
}
// Empty log file
void resetLog()
{
	logFile.open(logFilePath);
	logFile.close();
}

// .asi path
string CurrentPath()
{
	char buffer[MAX_PATH];
	GetModuleFileName(NULL, buffer, MAX_PATH);
	string::size_type pos = string(buffer).find_last_of("\\/");
	return string(buffer).substr(0, pos);
}