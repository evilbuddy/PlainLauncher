#include "mini.h"

#include <conio.h>
#include <filesystem>
#include <iostream>
#include <string>
#include <vector>
#include <Windows.h>

namespace fs = std::filesystem;

// print function because I'm retarded
void print(const char* text)
{
	std::printf(text);
	std::printf("\n");
}

std::vector<std::string> split(std::string str, char separator)
{
	std::string s;
	std::stringstream ss(str);

	std::vector<std::string> v;

	while (std::getline(ss, s, separator))
		v.push_back(s);

	return v;
}

int main()
{
	if (fs::exists("pl.ini") == false)
	{
		std::ofstream ofs("pl.ini");
		ofs << "[PlainLauncher]\napp = yourapp.exe\nargument = -arg\nfiles = yourfiles\\";
		ofs.close();

		print("config file created !");
		print("press enter to exit.");
		int key = _getch(); // assigned to prevent C6031
		return 1;
	}

	std::vector<fs::directory_entry> files;

	mINI::INIStructure cfg;
	mINI::INIFile("pl.ini").read(cfg);

	for (const fs::directory_entry& entry : fs::directory_iterator(cfg["PlainLauncher"]["files"].c_str()))
		files.push_back(entry);

	for (size_t i = 0; i < files.size(); i++)
	{
		std::printf("[");
		std::printf(std::to_string(i + 1).c_str());
		std::printf("] ");
		print(files[i].path().string().c_str());
	}

	std::string str;
	std::getline(std::cin, str);
	int choice = std::stoi(str) - 1;

	if (choice < 0 || (size_t)choice >= files.size())
	{
		print("press enter to exit.");
		int key = _getch(); // assigned to prevent C6031
		return 2;
	}

	std::string path = "";
	std::string launch = "";

	std::vector<std::string> app = split(cfg["PlainLauncher"]["app"], '\\');

	if (cfg["PlainLauncher"]["argument"] == "NULL" || "")
		launch = app[app.size() - 1] + " \"" + files[choice].path().string() + "\"";
	else
		launch = app[app.size() - 1] + " " + cfg["PlainLauncher"]["argument"] + " \"" + files[choice].path().string() + "\"";

	for (size_t i = 0; i < app.size() - 1; i++)
		path = path + app[i] + "\\";

	system((app[0] + " & cd \"" + path + "\" & " + launch.c_str()).c_str());

	return 0;
}