#pragma once
#include<vector>
#include<string>
using std::vector;
using std::string;
struct AsciiObjects
{
	
	vector<string> vecCastle;
	vector<string> vecCastle2;
};

void ObjectInit(AsciiObjects& _objs);
void ObjectUpdate(AsciiObjects& _objs);
void ObjectRender(const AsciiObjects& _objs);

	