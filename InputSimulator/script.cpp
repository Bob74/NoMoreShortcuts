
#include <fstream>
#include <sstream>
#include <iomanip>
#include <chrono>
#include <ctime>

#include "keyboard.h"
#include "script.h"
#include "Tools.h"

using namespace std;

HANDLE hPipe;
char pipeBuffer[1024];
DWORD dwRead;

bool clientConnected = false;

ofstream logFile;
string logFilePath = CurrentPath() + "\\InputSimulator.log";


void main()
{
	resetLog();

	// Creating server pipe
	SetupPipe();

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
	if (ConnectNamedPipe(hPipe, NULL) == 0)
	{
		if (GetLastError() == ERROR_IO_PENDING)
		{
			// Waiting for a client
			log("Waiting for a client");
			WAIT(2500);
			return;
		}
		else if (GetLastError() == ERROR_PIPE_CONNECTED)
		{
			// Client is connected
			if (!clientConnected)
			{
				log("Client connected!");
				clientConnected = true;
			}

			if (ReadFile(hPipe, pipeBuffer, sizeof(pipeBuffer) - 1, &dwRead, NULL) != FALSE)
			{
				// add terminating zero
				pipeBuffer[dwRead] = '\0';

				// do something with data in buffer
				log("pipeBuffer=" + string(pipeBuffer));

				// Sending keys sequence
				string keys = pipeBuffer;
				PressKey(keys);

				CharArrayCleaner(pipeBuffer);
			}
			else
			{
				switch (GetLastError())
				{
				case ERROR_BROKEN_PIPE:
					log("Client has lost connection");
					DisconnectNamedPipe(hPipe);
					clientConnected = false;
					WAIT(2500);
					return;
				case ERROR_MORE_DATA:
					log("Buffer is too small (" + to_string(sizeof(pipeBuffer)) + ") to get all the data!");
					WAIT(2500);
					return;
				default:
					break;
				}
			}
		}
		else if (GetLastError() == ERROR_NO_DATA)
		{
			log("Client has disconnected properly");
			DisconnectNamedPipe(hPipe);
			clientConnected = false;
			WAIT(2500);
			return;
		}
		else
		{
			//log("ConnectNamedPipe has failed");
			WAIT(2500);
			return;
		}
	}
}

void SetupPipe()
{
	hPipe = CreateNamedPipe(TEXT("\\\\.\\pipe\\GTA-Input-Pipe"),
		PIPE_ACCESS_INBOUND,
		PIPE_TYPE_BYTE | PIPE_READMODE_BYTE | PIPE_NOWAIT,
		1,
		1024 * 16,
		1024 * 16,
		NMPWAIT_USE_DEFAULT_WAIT,
		NULL);

	if (hPipe != INVALID_HANDLE_VALUE)
	{
		ConnectNamedPipe(hPipe, NULL);   // wait for someone to connect to the pipe
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